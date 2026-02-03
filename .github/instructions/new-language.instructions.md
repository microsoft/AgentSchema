---
applyTo: "agentschema-emitter/**/*"
description: "Instructions for adding a new language runtime to AgentSchema"
---

# Adding a New Language Runtime to AgentSchema

This guide documents the process for adding a new language runtime to the AgentSchema code generation system.

## Critical Requirement

**All language runtimes MUST have identical test coverage.** The tests must be the same across TypeScript, Python, C#, Go, and any new language. Only the syntax differs - the test categories do not. See "Test Standardization Requirements" section below.

## Overview

AgentSchema generates runtime libraries for multiple languages from TypeSpec definitions. Each language requires:

1. **Emitter code** (`agentschema-emitter/src/{language}.ts`) - TypeScript code that builds template contexts
2. **Templates** (`agentschema-emitter/src/templates/{language}/`) - Nunjucks templates for code generation
3. **Runtime output** (`runtime/{language}/`) - Generated code destination
4. **Documentation** - Updates to docs for the new language

## Step-by-Step Process

### 1. Create the Emitter File

Create `agentschema-emitter/src/{language}.ts` with:

```typescript
import { EmitContext } from "@typespec/compiler";
import { enumerateTypes, TypeNode, PropertyNode } from "./ast.js";
import { createTemplateEngine } from "./template-engine.js";

// Type mapper for scalar types
export const {language}TypeMapper: Record<string, string> = {
  "string": "{native_string_type}",
  "number": "{native_number_type}",
  "boolean": "{native_bool_type}",
  "int32": "{native_int_type}",
  "int64": "{native_int64_type}",
  // ... add all scalar mappings
};

// Main emit function
export async function emit{Language}(context: EmitContext, emitTarget: EmitTarget): Promise<void> {
  const engine = createTemplateEngine("{language}");
  const nodes = enumerateTypes(context.program);

  // Generate files for each type
  for (const node of nodes) {
    const fileContext = buildFileContext(node);
    const content = engine.render('file.{ext}.njk', fileContext);
    await emitFile(context, `{filename}.{ext}`, content, emitTarget["output-dir"]);

    // Generate test file
    if (emitTarget["test-dir"]) {
      const testContext = buildTestContext(node);
      const testContent = engine.render('test.{ext}.njk', testContext);
      await emitFile(context, `{test_filename}.{ext}`, testContent, emitTarget["test-dir"]);
    }
  }
}
```

### 2. Define Context Interfaces

Add interfaces to `agentschema-emitter/src/ast.ts` or the language file:

```typescript
// File generation context
interface {Language}FileContext {
  node: TypeNode;
  // ... language-specific properties
}

// Test generation context
interface {Language}TestContext {
  node: TypeNode;
  examples: Array<{
    json: string[];
    yaml: string[];
    validation: Array<{ key: string; value: any; delimeter: string }>;
  }>;
  alternates: Array<{
    title: string;
    scalar: string;
    value: string;
    rawValue: string;  // Unquoted value for YAML tests
    validation: Array<{ key: string; value: any; delimeter: string }>;
  }>;
  isAbstract: boolean;  // Required for test generation
  isPolymorphic: boolean;  // For polymorphic type handling
}
```

### 3. Create Templates Directory

Create `agentschema-emitter/src/templates/{language}/` with:

- `file.{ext}.njk` - Main class/struct template
- `test.{ext}.njk` - Test file template
- `_macros.njk` - Shared macros (optional)

### 4. Register in Emitter

Update `agentschema-emitter/src/emitter.ts` to include the new language:

```typescript
import { emit{Language} } from "./{language}.js";

// In the emit function, add:
if (emitTarget.language === "{language}") {
  await emit{Language}(context, emitTarget);
}
```

### 5. Configure Output in tspconfig.yaml

Add the new language target in `agentschema/tspconfig.yaml`:

```yaml
emit:
  - "@agentschema/emitter"
options:
  "@agentschema/emitter":
    targets:
      - language: "{language}"
        output-dir: "../runtime/{language}/{package}/src"
        test-dir: "../runtime/{language}/{package}/tests"
```

---

## Test Standardization Requirements

**All new language runtimes MUST include the following test categories:**

### Required Test Categories

| Category            | Description                              | Template Pattern                     |
| ------------------- | ---------------------------------------- | ------------------------------------ |
| **Load JSON**       | Load instance from JSON string           | `Type.fromJson(jsonString)`          |
| **Load YAML**       | Load instance from YAML string           | `Type.fromYaml(yamlString)`          |
| **Round-trip JSON** | Load → serialize → reload → compare      | Load, toJson, fromJson, assert equal |
| **Round-trip YAML** | Load → serialize → reload → compare      | Load, toYaml, fromYaml, assert equal |
| **Shorthand JSON**  | Load from scalar via JSON                | Parse scalar as JSON, load           |
| **Shorthand YAML**  | Load from scalar via YAML                | Parse scalar as YAML, load           |
| **Dictionary load** | Load from native dict/map                | `Type.load(emptyDict)`               |
| **Dictionary save** | Save to native dict/map                  | `instance.save()` returns dict       |
| **ToJSON output**   | Verify serialization produces valid JSON | toJson returns parseable string      |
| **ToYAML output**   | Verify serialization produces valid YAML | toYaml returns parseable string      |

### Test Template Structure

```nunjucks
{# {language} Test Template #}

{% for sample in examples %}
// Load JSON test
test_load_json_{{ loop.index }}() {
  json = `{{ sample.json | join("\\n") }}`;
  instance = Type.fromJson(json);
  assert(instance != null);
  {% for val in sample.validation %}
  assert(instance.{{ val.key }} == {{ val.value }});
  {% endfor %}
}

// Load YAML test
test_load_yaml_{{ loop.index }}() {
  yaml = `{{ sample.yaml | join("\\n") }}`;
  instance = Type.fromYaml(yaml);
  assert(instance != null);
}

// Round-trip JSON test
test_roundtrip_json_{{ loop.index }}() {
  instance = Type.fromJson(jsonData);
  output = instance.toJson();
  reloaded = Type.fromJson(output);
  assert(reloaded.field == instance.field);
}

// Round-trip YAML test
test_roundtrip_yaml_{{ loop.index }}() {
  instance = Type.fromYaml(yamlData);
  output = instance.toYaml();
  reloaded = Type.fromYaml(output);
  assert(reloaded.field == instance.field);
}

// ToJSON output test
test_to_json_output_{{ loop.index }}() {
  instance = Type.fromJson(jsonData);
  output = instance.toJson();
  assert(output != null);
  parsed = parseJson(output);  // Should not throw
  assert(typeof parsed == "object");
}

// ToYAML output test
test_to_yaml_output_{{ loop.index }}() {
  instance = Type.fromYaml(yamlData);
  output = instance.toYaml();
  assert(output != null);
  reloaded = Type.fromYaml(output);  // Should not throw
  assert(reloaded != null);
}
{% endfor %}

{% if alternates.length > 0 %}
{% for alt in alternates %}
// Shorthand JSON test
test_shorthand_json_{{ alt.title }}() {
  json = JSON.stringify({{ alt.value }});
  instance = Type.fromJson(json);
  assert(instance != null);
  {% for val in alt.validation %}
  assert(instance.{{ val.key }} == {{ val.value }});
  {% endfor %}
}

// Shorthand YAML test
test_shorthand_yaml_{{ alt.title }}() {
  {% if alt.scalar == "string" %}
  yaml = '"{{ alt.rawValue }}"';
  {% else %}
  yaml = '{{ alt.rawValue }}';
  {% endif %}
  data = parseYaml(yaml);
  instance = Type.load(data);
  assert(instance != null);
}
{% endfor %}
{% endif %}

{% if not isAbstract %}
// Dictionary load test
test_load_from_dict() {
  data = {};  // empty dict/map
  instance = Type.load(data);
  assert(instance != null);
}

// Dictionary save test
test_save_to_dict() {
  instance = new Type();
  data = instance.save();
  assert(data != null);
  assert(typeof data == "dict/map");
}
{% endif %}
```

### Abstract Type Handling

For abstract/polymorphic base types:

- **Skip** construction tests (can't instantiate abstract class)
- **Skip** dictionary load/save tests
- **Include** serialization tests (uses factory methods that return concrete types)

Use `isAbstract` flag in test context:

```typescript
const isAbstract =
  node.isAbstract ||
  (node.discriminator !== undefined && node.discriminator.length > 0);
```

### Collection Type Handling

For types with collections that may not have a `name` property:

- Add `hasNameProperty` flag to collection metadata
- Use array format for save when `hasNameProperty` is false

```typescript
const hasNameProperty =
  p.type?.properties.some((t) => t.name === "name") || false;
```

---

## Test File Naming Convention

Follow these naming patterns for consistency:

| Language   | Test File Pattern     | Example           |
| ---------- | --------------------- | ----------------- |
| TypeScript | `{type-name}.test.ts` | `binding.test.ts` |
| Python     | `test_{typename}.py`  | `test_binding.py` |
| C#         | `{TypeName}Tests.cs`  | `BindingTests.cs` |
| Go         | `{type_name}_test.go` | `binding_test.go` |

---

## Checklist for New Language

- [ ] Create emitter file `agentschema-emitter/src/{language}.ts`
- [ ] Add type mapper for scalar types
- [ ] Create `buildFileContext()` function
- [ ] Create `buildTestContext()` function with `isAbstract` and `rawValue` for alternates
- [ ] Add context interfaces (to ast.ts or language file)
- [ ] Create templates directory `agentschema-emitter/src/templates/{language}/`
- [ ] Create `file.{ext}.njk` template
- [ ] Create `test.{ext}.njk` template with ALL required test categories
- [ ] Register language in `emitter.ts`
- [ ] Add target in `tspconfig.yaml`
- [ ] Create runtime directory structure `runtime/{language}/`
- [ ] Add package configuration (package.json, go.mod, pyproject.toml, etc.)
- [ ] Update documentation in `docs/`
- [ ] Update Contributing guide with new language
- [ ] Run full test suite and verify all tests pass
- [ ] Verify test counts are reasonable (expect ~300+ tests per runtime)

---

## Reference Implementations

Use these existing implementations as references:

| Aspect                       | Best Reference                           |
| ---------------------------- | ---------------------------------------- |
| Emitter structure            | `typescript.ts`                          |
| Test context with isAbstract | `typescript.ts` → `buildTestContext()`   |
| Test template                | `templates/typescript/test.ts.njk`       |
| Collection hasNameProperty   | `typescript.ts` → `getCollectionTypes()` |
| Polymorphic handling         | `templates/go/test.go.njk`               |
| Abstract type conditionals   | `templates/typescript/test.ts.njk`       |


---
applyTo: "agentschema-emitter/src/templates/**/test*.njk"
description: "Test standardization requirements for all language runtimes"
---

# Test Standardization Guidelines

**All AgentSchema runtime test templates MUST implement identical test coverage.** These standards are universal and non-negotiable across TypeScript, Python, C#, Go, and any future languages.

## Core Principle

Every language runtime must have the **same tests**. Implementation syntax varies (pytest vs xUnit vs Jest), but coverage does not. If TypeScript has a test category, Python, C#, and Go must have it too.

## Required Test Categories

Every test template MUST include these test types:

### 1. Deserialization Tests

```
✅ Load from JSON string
✅ Load from YAML string
```

### 2. Round-trip Tests

```
✅ JSON: load → toJson → fromJson → compare
✅ YAML: load → toYaml → fromYaml → compare
```

### 3. Serialization Output Tests

```
✅ toJson() produces valid, parseable JSON
✅ toYaml() produces valid, parseable YAML
```

### 4. Shorthand/Alternate Tests

```
✅ Load from scalar via JSON (e.g., `"value"` → Type)
✅ Load from scalar via YAML (e.g., `"value"` → Type)
```

### 5. Dictionary/Map Tests (also validates construction/defaults)

```
✅ Load from empty dictionary/map (tests default values work)
✅ Save to dictionary/map
```

## Required Context Properties

Test contexts must include:

```typescript
{
  node: TypeNode,
  examples: Array<{
    json: string[],      // JSON lines
    yaml: string[],      // YAML lines
    validation: Array<{
      key: string,       // Property name
      value: any,        // Expected value
      delimeter: string  // Quote character for strings
    }>
  }>,
  alternates: Array<{
    title: string,       // Test name
    scalar: string,      // Scalar type (string, number, etc.)
    value: string,       // Quoted value for code (e.g., '"example"')
    rawValue: string,    // Unquoted value for YAML strings (Python, Go only)
    validation: Array<...>
  }>,
  isAbstract: boolean,   // Skip construction/dict tests if true
  isPolymorphic: boolean, // Optional: for polymorphic handling (Go uses this)

  // Language-specific (not all templates need these):
  renderName: function,  // Property name formatter (TypeScript, C#)
  namespace: string,     // Package/namespace name (TypeScript, C#, Go)
}
```

## Abstract Type Handling

Use `{% if not isAbstract %}` to wrap:

- Construction tests (`new Type()`) - TypeScript only
- Dictionary load tests (`Type.load({})`)
- Dictionary save tests (`instance.save()`)

Abstract types CAN still have:

- Serialization tests (fromJson/fromYaml use factory methods)
- Round-trip tests (same reason)

**Note:** TypeScript has construction tests (`new Type()`) that other languages don't have.
This is intentional - TypeScript classes support default construction while Python/C#/Go
use factory methods (load/Load) as the primary instantiation pattern.

## Polymorphic Type Handling (Go-specific)

Go uses `isPolymorphic` to skip validation on polymorphic base types:

```nunjucks
{%- if isPolymorphic %}
_ = instance // Load succeeded, exact type depends on discriminator
// Note: Validation skipped for polymorphic base types
{%- else %}
// Normal validation
{%- endif %}
```

Other languages handle polymorphism transparently through factory methods.

## Collection hasNameProperty

For collection save methods, check if item type has `name` property:

```typescript
const hasNameProperty =
  p.type?.properties.some((t) => t.name === "name") || false;
```

If `hasNameProperty` is false, always use array format for saving.

## Test Naming by Language

| Language   | Pattern                       | Example                         |
| ---------- | ----------------------------- | ------------------------------- |
| TypeScript | `it("should {action}")`       | `it("should load from JSON")`   |
| Python     | `def test_{action}_{type}():` | `def test_load_json_binding():` |
| C#         | `public void {Action}()`      | `public void LoadJsonInput()`   |
| Go         | `func Test{Type}{Action}(t)`  | `func TestBindingLoadJSON(t)`   |

## Validation Assertions

Handle boolean values specially (language-specific true/false):

```nunjucks
{%- if validation.value == "True" or validation.value == "False" %}
// Use boolean assertion
{%- else %}
// Use equality assertion
{%- endif %}
```

## Expected Test Counts

A properly implemented test template should generate approximately:

- **37 types** with tests
- **~8-12 tests per type** (varies by examples/alternates)
- **300-400 total tests** per runtime

All runtimes should have similar test counts (within ~15% of each other). If test counts are significantly lower, review for missing test categories.

## Universality Principle

**All languages MUST implement the same test categories.** The standard is not optional - every runtime must have:

1. JSON load tests
2. YAML load tests
3. JSON round-trip tests
4. YAML round-trip tests
5. JSON serialization output tests
6. YAML serialization output tests
7. JSON shorthand tests (where applicable)
8. YAML shorthand tests (where applicable)
9. Dictionary/map load tests
10. Dictionary/map save tests

**Implementation may vary, but coverage must not.** Each language achieves these tests idiomatically:

| Aspect     | Standard Requirement        | Implementation Varies By Language |
| ---------- | --------------------------- | --------------------------------- |
| Load tests | Must test all load methods  | Factory method names differ       |
| Round-trip | Must verify load→save→load  | Assertion syntax differs          |
| Shorthand  | Must test scalar alternates | Quote handling differs            |
| Dictionary | Must test dict input/output | Dict/map types differ             |

## Implementation Notes

These are implementation details, not exceptions to the standard:

- **Quote handling in shorthand**: Some emitters provide `rawValue` (unquoted), others handle quoting inline
- **Polymorphic types**: Some languages need explicit handling (Go's `isPolymorphic`), others handle transparently
- **Optional fields**: Some languages need pointer checks (Go's `isPointer`), others use nullable types


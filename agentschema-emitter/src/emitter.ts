import { fileURLToPath } from 'url';
import path, { dirname } from 'path';

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

import { EmitContext, emitFile, resolvePath } from "@typespec/compiler";
import { resolveModel } from "./ast.js";
import { AgentSchemaEmitterOptions } from "./lib.js";
import { generateMarkdown } from "./markdown.js";
import { generatePython } from "./python.js";
import { generateCsharp } from "./csharp.js";
import { generateTypeScript } from "./typescript.js";
import { generateGo } from "./go.js";
import { generateBotDefinition } from './bot.js';


export async function $onEmit(context: EmitContext<AgentSchemaEmitterOptions>) {

  const options = {
    emitterOutputDir: context.emitterOutputDir,
    templateDir: path.resolve(__dirname, 'templates'),
    ...context.options,
  }


  // resolving top level Prompty model
  // this is the "Model" entry point for the emitter
  const rootObject = options["root-object"];
  const m = context.program.resolveTypeReference(rootObject);
  if (!m[0] || m[0].kind !== "Model") {
    throw new Error(
      `${rootObject} model not found or is not a model type.`
    );
  }

  const model = resolveModel(context.program, m[0], new Set(), options["root-namespace"] || "AgentSchema", options["root-alias"] || "AgentSchema");
  if (options["root-alias"]) {
    model.typeName = {
      namespace: model.typeName.namespace,
      name: options["root-alias"]
    }
  }

  const targets = options["emit-targets"] || [];
  const targetNames = targets.map(t => t.type.toLowerCase().trim());


  if (targetNames.includes("markdown")) {
    const idx = targetNames.indexOf("markdown");
    const target = targets[idx];
    // emit markdown
    await generateMarkdown(context, options.templateDir, model, target);
  }

  if (targetNames.includes("python")) {
    const idx = targetNames.indexOf("python");
    const target = targets[idx];
    // emit python
    await generatePython(context, options.templateDir, model, target);
  }

  if (targetNames.includes("csharp")) {
    const idx = targetNames.indexOf("csharp");
    const target = targets[idx];
    // emit csharp
    await generateCsharp(context, options.templateDir, model, target);
  }

  if (targetNames.includes("typescript")) {
    const idx = targetNames.indexOf("typescript");
    const target = targets[idx];
    // emit typescript
    await generateTypeScript(context, options.templateDir, model, target);
  }

  if (targetNames.includes("go")) {
    const idx = targetNames.indexOf("go");
    const target = targets[idx];
    // emit go
    await generateGo(context, options.templateDir, model, target);
  }

  if (targetNames.includes("botdefinition")) {
    const idx = targetNames.indexOf("botdefinition");
    const target = targets[idx];
    // emit bot definition
    await generateBotDefinition(context, options.templateDir, model, target);
  }

  await emitFile(context.program, {
    path: resolvePath(context.emitterOutputDir, "json-ast", "model.json"),
    content: JSON.stringify(model.getSanitizedObject(), null, 2),
  });
}

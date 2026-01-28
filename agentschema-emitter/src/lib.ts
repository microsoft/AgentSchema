import { createTypeSpecLibrary, JSONSchemaType } from "@typespec/compiler";

export interface EmitTarget {
  "type": string;
  "output-dir"?: string;
  "test-dir"?: string;
  "alias"?: { [key: string]: any };
  "format"?: boolean;
}
export interface AgentSchemaEmitterOptions {
  "root-object": string;
  "emit-targets"?: EmitTarget[];
  "root-namespace"?: string;
  "root-alias"?: string;
}

const AgentSchemaEmitterOptionsSchema: JSONSchemaType<AgentSchemaEmitterOptions> = {
  type: "object",
  additionalProperties: false,
  properties: {
    "emit-targets": {
      type: "array",
      items: {
        type: "object",
        properties: {
          "type": {
            type: "string"
          },
          "output-dir": {
            type: "string",
            nullable: true
          },
          "test-dir": {
            type: "string",
            nullable: true
          },
          "alias": {
            type: "object",
            additionalProperties: true,
            nullable: true
          },
          "format": {
            type: "boolean",
            nullable: true,
            default: true,
            description: "Run formatters (ruff, black) on emitted Python files"
          }
        },
        required: ["type"]
      },
      nullable: true,
      description: "List of target languages to emit code for"
    },
    "root-namespace": {
      type: "string",
      nullable: true,
      description: "Root namespace for the emitted code"
    },
    "root-object": {
      type: "string",
      nullable: false,
      description: "Root object for the emitted artifacts"
    },
    "root-alias": {
      type: "string",
      nullable: true,
      description: "Alias for the root object"
    }
  },
  required: ["root-object"],
};

export const $lib = createTypeSpecLibrary({
  name: "@agentschema/emitter",
  diagnostics: {},
  emitter: { options: AgentSchemaEmitterOptionsSchema },
  state: {
    samples: { description: "Sample values for properties" },
    shorthands: { description: "Shorthand models creation" },
    abstracts: { description: "Abstract models" }
  }
});

export const { reportDiagnostic, createDiagnostic } = $lib;
export const StateKeys = $lib.stateKeys;

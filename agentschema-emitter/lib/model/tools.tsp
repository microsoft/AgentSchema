import "@agentschema/emitter";
import "./core.tsp";
import "./tools";
import "./properties.tsp";

// TODO: MATCH THESE https://github.com/dotnet/extensions/blob/main/src/Libraries/Microsoft.Extensions.AI.Abstractions/Tools/AITool.cs

namespace AgentSchema;

/**
 * Represents a binding between an input property and a tool parameter.
 */
@shorthand(string, #{ input: "{value}" })
model Binding {
  @doc("The input property that will be bound to the tool parameter argument")
  @sample(#{ input: "input-variable" })
  input: string;
}

alias Bindings = Record<Binding> | Named<
  Binding,
  "Name of the binding",
  #{ name: "my-tool" }
>[];

union ToolTypes {
  functionType: "function",
  string,
}

/**
 * Represents a tool that can be used in prompts.
 */
@abstract
@discriminator("kind")
model Tool {
  // look here: https://github.com/dotnet/extensions/blob/main/src/Libraries/Microsoft.Extensions.AI.Abstractions/Tools/AITool.cs
  @doc("The kind identifier for the tool")
  @sample(#{ kind: "function" })
  kind: ToolTypes;

  @doc("A short description of the tool for metadata purposes")
  @sample(#{ description: "A description of the tool" })
  description?: string = "";

  @doc("Tool argument bindings to input properties")
  @sample(#{ bindings: #{ input: "value" } })
  bindings?: Bindings = #[];
}

/**
 * Represents a local function tool.
 */
model FunctionTool extends Tool {
  @doc("The kind identifier for function tools")
  @sample(#{ kind: "function" })
  kind: "function";

  @doc("Parameters accepted by the function tool")
  @sample(#{
    parameters: #{
      properties: #[
        #{ name: "firstName", kind: "string", value: "Jane" },
        #{ name: "lastName", kind: "string", value: "Doe" },
        #{
          name: "question",
          kind: "string",
          value: "What is the meaning of life?",
        }
      ],
    },
  })
  @sample(#{
    parameters: #{
      properties: #{
        firstName: #{ kind: "string", value: "Jane" },
        lastName: #{ kind: "string", value: "Doe" },
        question: #{ kind: "string", value: "What is the meaning of life?" },
      },
    },
  })
  parameters: PropertySchema;

  @doc("Indicates whether the function tool enforces strict validation on its parameters")
  @sample(#{ strict: true })
  strict?: boolean = true;
}

/**
 * Represents a generic server tool that runs on a server
 * This tool kind is designed for operations that require server-side execution
 * It may include features such as authentication, data storage, and long-running processes
 * This tool kind is ideal for tasks that involve complex computations or access to secure resources
 * Server tools can be used to offload heavy processing from client applications
 */
model CustomTool extends Tool {
  @doc("The kind identifier for server tools. This is a wildcard and can represent any server tool type not explicitly defined.")
  kind: "*";

  @doc("Connection configuration for the server tool")
  @sample(#{ connection: #{ kind: "reference" } })
  connection: Connection;

  @doc("Configuration options for the server tool")
  @sample(#{ options: #{ timeout: 30, retries: 3 } })
  options: Record<unknown> = #{};
}

alias Tools = Record<Tool> | Named<
  Tool,
  "Name of the tool. If a function tool, this is the function name, otherwise it is the type",
  #{ name: "my-tool" }
>[];

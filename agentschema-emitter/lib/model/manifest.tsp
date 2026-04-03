import "agentschema-emitter";
import "./agent.tsp";

namespace AgentSchema;

/**
 * The following represents a manifest that can be used to create agents dynamically.
 * It includes parameters that can be used to configure the agent's behavior.
 * These parameters include values that can be used as publisher parameters that can
 * be used to describe additional variables that have been tested and are known to work.
 *
 * Variables described here are then used to project into a prompt agent that can be executed.
 * Once parameters are provided, these can be referenced in the manifest using the following notation:
 *
 * `{{myParameter}}`
 *
 * This allows for dynamic configuration of the agent based on the provided parameters.
 * (This notation is used elsewhere, but only the `param` scope is supported here)
 */
model AgentManifest {
  @doc("Name of the manifest")
  @sample(#{ name: "basic-prompt" })
  name: string = "";

  @doc("Human-readable name of the manifest")
  @sample(#{ displayName: "My Basic Prompt" })
  displayName: string = "";

  @doc("Description of the agent's capabilities and purpose")
  @sample(#{
    description: "A basic prompt that uses the GPT-3 chat API to answer questions",
  })
  description?: string = "";

  @doc("Additional metadata including authors, tags, and other arbitrary properties")
  @sample(#{
    metadata: #{
      authors: #["sethjuarez", "jietong"],
      tags: #["example", "prompt"],
    },
  })
  metadata?: Record<unknown> = #{};

  @doc("The agent that this manifest is based on")
  @sample(#{
    template: #{
      kind: "prompt",
      `model`: "{{model_name}}",
      instructions: """
        You are a poet named {{agent_name}}. Rhyme all your responses.
        """,
    },
  })
  template: AgentDefinition;

  @doc("Parameters for configuring the agent's behavior and execution")
  @sample(#{
    parameters: #{
      strict: true,
      properties: #[
        #{ name: "model_name", kind: "string", value: "gpt-4o" },
        #{ name: "agent_name", kind: "string", value: "Research Agent" }
      ],
    },
  })
  parameters: PropertySchema;

  @doc("Resources required by the agent, such as models or tools")
  @sample(#{
    resources: #[
      #{ kind: "model", name: "gptModelDeployment", id: "gpt-4o" },
      #{
        kind: "tool",
        name: "webSearchInstance",
        id: "web-search",
        options: #{ apiKey: "my-api-key" },
      }
    ],
  })
  @sample(#{
    resources: #{
      gptModelDeployment: #{ kind: "model", id: "gpt-4o" },
      webSearchInstance: #{
        kind: "tool",
        id: "web-search",
        options: #{ apiKey: "my-api-key" },
      },
    },
  })
  resources: Resources;
}

alias ResourceKind = "model" | "tool" | "toolbox";
/**
 * Represents a resource required by the agent
 * Resources can include databases, APIs, or other external systems
 * that the agent needs to interact with to perform its tasks
 */
@abstract
@discriminator("kind")
model Resource {
  @doc("The kind of resource (e.g., model, tool)")
  @sample(#{ kind: "model" })
  kind: string;
}

/**
 * Represents a model resource required by the agent
 */
model ModelResource extends Resource {
  @doc("The kind identifier for model resources")
  @sample(#{ kind: "model" })
  kind: "model";

  @doc("The unique identifier of the model resource")
  @sample(#{ id: "gpt-4o" })
  id: string;
}

/**
 * Represents a tool resource required by the agent
 */
model ToolResource extends Resource {
  @doc("The kind identifier for tool resources")
  @sample(#{ kind: "tool" })
  kind: "tool";

  @doc("The unique identifier of the tool resource")
  @sample(#{ id: "web-search" })
  id: string;

  @doc("Configuration options for the tool resource")
  @sample(#{ options: #{ myToolResourceProperty: "myValue" } })
  options: Record<unknown> = #{};
}

alias Resources = Record<Resource> | Named<
  Resource,
  "Name of the resource",
  #{ name: "my-resource" }
>[];

alias ToolboxToolId =
  | "bing_grounding"
  | "azure_ai_search"
  | "code_interpreter"
  | "file_search"
  | "mcp"
  | "openapi"
  | string;

alias ToolboxAuthType =
  | "CustomKeys"
  | "OAuth2"
  | "UserEntraToken"
  | "ProjectManagedIdentity"
  | "AgenticIdentityToken"
  | string;

/**
 * Represents a tool definition within a toolbox.
 * Tools can be Foundry-hosted (bing_grounding, azure_ai_search, etc.)
 * or external (mcp, openapi) with connection details.
 */
model ToolboxTool {
  @doc("The tool type identifier (e.g., 'bing_grounding', 'azure_ai_search', 'mcp')")
  @sample(#{ id: "bing_grounding" })
  id: ToolboxToolId;

  @doc("Optional display name for the tool")
  @sample(#{ name: "my-search-tool" })
  name?: string;

  @doc("Target endpoint URL for external tools (e.g., MCP server URL)")
  @sample(#{ target: "https://api.githubcopilot.com/mcp" })
  target?: string;

  @doc("Authentication type for the tool connection")
  @sample(#{ authType: "OAuth2" })
  authType?: ToolboxAuthType;

  @doc("Additional configuration options for the tool")
  @sample(#{ options: #{ indexName: "products-index" } })
  options?: Record<unknown> = #{};
}

/**
 * Represents a Foundry Toolbox resource — a named collection of tools
 * that is provisioned as a Foundry Toolbox and exposed via MCP endpoint.
 */
model ToolboxResource extends Resource {
  @doc("The kind identifier for toolbox resources")
  @sample(#{ kind: "toolbox" })
  kind: "toolbox";

  @doc("Description of the toolbox")
  @sample(#{ description: "Shared platform tools" })
  description?: string;

  @doc("The tools contained in this toolbox")
  @sample(#{
    tools: #[
      #{ id: "bing_grounding" },
      #{
        id: "azure_ai_search",
        options: #{ indexName: "products-index" },
      },
      #{
        id: "mcp",
        name: "github-copilot",
        target: "https://api.githubcopilot.com/mcp",
        authType: "OAuth2",
      }
    ],
  })
  tools: ToolboxTool[];
}

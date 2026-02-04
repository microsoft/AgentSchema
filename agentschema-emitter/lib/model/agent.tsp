import "agentschema-emitter";
import "./core.tsp";
import "./model.tsp";
import "./tools.tsp";
import "./template.tsp";
import "./container.tsp";

namespace AgentSchema;

alias AgentKind = "prompt" | "hosted" | "workflow";

/**
 * The following is a specification for defining AI agents with structured metadata, inputs, outputs, tools, and templates.
 * It provides a way to create reusable and composable AI agents that can be executed with specific configurations.
 * The specification includes metadata about the agent, model configuration, input parameters, expected outputs,
 * available tools, and template configurations for prompt rendering.
 */
@abstract
@discriminator("kind")
model AgentDefinition {
  // AgentDefinition metadata
  @doc("Kind represented by the document")
  @sample(#{ kind: "prompt" })
  kind: AgentKind;

  @doc("Human-readable name of the agent")
  @sample(#{ name: "basic-prompt" })
  name: string = "";

  @doc("Display name of the agent for UI purposes")
  @sample(#{ displayName: "Basic Prompt Agent" })
  displayName?: string = "";

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

  // Inputs and Outputs
  @doc("Input parameters that participate in template rendering")
  @sample(#{
    inputSchema: #{
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
    inputSchema: #{
      properties: #{
        firstName: #{ kind: "string", value: "Jane" },
        lastName: #{ kind: "string", value: "Doe" },
        question: #{ kind: "string", value: "What is the meaning of life?" },
      },
    },
  })
  inputSchema?: PropertySchema;

  @doc("Expected output format and structure from the agent")
  @sample(#{
    outputSchema: #{
      properties: #[
        #{
          name: "answer",
          kind: "string",
          description: "The answer to the user's question.",
        }
      ],
    },
  })
  @sample(#{
    outputSchema: #{
      properties: #{
        answer: #{
          kind: "string",
          description: "The answer to the user's question.",
        },
      },
    },
  })
  outputSchema?: PropertySchema;
}

/**
 * Prompt based agent definition. Used to create agents that can be executed directly.
 * These agents can leverage tools, input parameters, and templates to generate responses.
 * They are designed to be straightforward and easy to use for various applications.
 */
model PromptAgent extends AgentDefinition {
  @doc("Type of agent, e.g., 'prompt'")
  @sample(#{ kind: "prompt" })
  kind: "prompt";

  // model execution
  @doc("Primary AI model configuration for the agent")
  @sample(#{
    `model`: #{
      id: "gpt-35-turbo",
      connection: #{
        kind: "key",
        endpoint: "https://{your-custom-endpoint}.openai.azure.com/",
        key: "{your-api-key}",
      },
    },
  })
  `model`: Model | string;

  // tools
  @doc("Tools available to the agent for extended functionality")
  @sample(#{
    tools: #{
      getCurrentWeather: #{
        kind: "function",
        description: "Get the current weather in a given location",
        parameters: #{
          location: #{
            kind: "string",
            description: "The city and state, e.g. San Francisco, CA",
          },
          unit: #{
            kind: "string",
            description: "The unit of temperature, e.g. Celsius or Fahrenheit",
          },
        },
      },
    },
  })
  @sample(#{
    tools: #[
      #{
        name: "getCurrentWeather",
        kind: "function",
        description: "Get the current weather in a given location",
        parameters: #{
          location: #{
            kind: "string",
            description: "The city and state, e.g. San Francisco, CA",
          },
          unit: #{
            kind: "string",
            description: "The unit of temperature, e.g. Celsius or Fahrenheit",
          },
        },
      }
    ],
  })
  tools?: Tools = #[];

  // template values
  @doc("Template configuration for prompt rendering")
  @sample(#{ template: #{ format: "mustache", parser: "prompty" } })
  template?: Template;

  @doc("Give your agent clear directions on what to do and how to do it. Include specific tasks, their order, and any special instructions like tone or engagement style. (can use this for a pure yaml declaration or as content in the markdown format)")
  @sample(#{
    instructions: """
      system:
      You are an AI assistant who helps people find information.
      As the assistant, you answer questions briefly, succinctly,
      and in a personable manner using markdown and even add some 
      personal flair with appropriate emojis.
      
      # Customer
      You are helping {{firstName}} {{lastName}} to find answers to 
      their questions. Use their name to address them in your responses.
      user:
      {{question}}
      """,
  })
  instructions?: string = "";

  @doc("Additional instructions or context for the agent, can be used to provide extra guidance (can use this for a pure yaml declaration)")
  additionalInstructions?: string = "";
}

/**
 * A workflow agent that can orchestrate multiple steps and actions.
 * This agent type is designed to handle complex workflows that may involve
 * multiple tools, models, and decision points.
 *
 * The workflow agent can be configured with a series of steps that define
 * the flow of execution, including conditional logic and parallel processing.
 * This allows for the creation of sophisticated AI-driven processes that can
 * adapt to various scenarios and requirements.
 *
 * Note: The detailed structure of the workflow steps and actions is not defined here
 * and would need to be implemented based on specific use cases and requirements.
 */
model Workflow extends AgentDefinition {
  @doc("Type of agent, e.g., 'workflow'")
  @sample(#{ kind: "workflow" })
  kind: "workflow";

  @doc("The steps that make up the workflow")
  trigger?: Record<unknown>;
}

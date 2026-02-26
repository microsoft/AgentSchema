import "agentschema-emitter";
import "./core.tsp";
import "./connection.tsp";
import "./tools.tsp";
import "./agent.tsp";

namespace AgentSchema;

/**
 * Definition for an environment variable used in containerized agents.
 */
model EnvironmentVariable {
  @doc("Environment variable resolution")
  @sample(#{ value: "my-value" })
  value: string;
}

alias EnvironmentVariables = Record<EnvironmentVariable> | Named<
  EnvironmentVariable,
  "Name of the environment variable",
  #{ name: "MY_ENV_VAR" }
>[];

alias AgentProtocol = "responses" | "activity" | string;

/*
 * A record mapping for a single protocol and its version.
 */
model ProtocolVersionRecord {
  @doc("The protocol type.")
  @sample(#{ protocol: "responses" })
  protocol: AgentProtocol;

  @doc("The version string for the protocol, e.g. 'v0.1.1'.")
  @sample(#{ version: "v0.1.1" })
  version: string;
}

/**
 * Resource allocation for a containerized agent.
 * Valid CPU and memory pairings depend on the target hosting provider.
 */
model ContainerResources {
  @doc("CPU allocation for the container (e.g., '0.5', '1', '2')")
  @sample(#{ cpu: "1" })
  cpu: string;

  @doc("Memory allocation for the container (e.g., '0.5Gi', '2Gi')")
  @sample(#{ memory: "2Gi" })
  memory: string;
}

/**
 * This represents a container based agent hosted by the provider/publisher.
 * The intent is to represent a container application that the user wants to run
 * in a hosted environment that the provider manages.
 */
model ContainerAgent extends AgentDefinition {
  @doc("Type of agent, e.g., 'hosted'")
  @sample(#{ kind: "hosted" })
  kind: "hosted";

  @doc("Protocol used by the containerized agent")
  @sample(#{ protocols: #[#{ protocol: "responses", version: "v0.1.1" }] })
  protocols: ProtocolVersionRecord[];

  @doc("Container image path (e.g., '<acr-endpoint>/<container-image-name>')")
  @sample(#{ image: "myregistry.azurecr.io/my-agent" })
  image: string;

  @doc("Resource allocation for the container")
  @sample(#{ resources: #{ cpu: "1", memory: "2Gi" } })
  resources: ContainerResources;

  @doc("Environment variables to set in the container")
  @sample(#{
    environmentVariables: #[#{ name: "MY_ENV_VAR", value: "my-value" }],
  })
  environmentVariables?: EnvironmentVariables;
}

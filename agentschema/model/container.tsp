import "@agentschema/emitter";
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

  @doc("Environment variables to set in the container")
  @sample(#{
    environmentVariables: #[#{ name: "MY_ENV_VAR", value: "my-value" }],
  })
  environmentVariables?: EnvironmentVariables;
}

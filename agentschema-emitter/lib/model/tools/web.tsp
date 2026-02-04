import "../core.tsp";
import "../tools.tsp";
import "../connection.tsp";

namespace AgentSchema;

/**
 * The Bing search tool.
 */
model WebSearchTool extends Tool {
  @doc("The kind identifier for Bing search tools")
  @sample(#{ kind: "bing_search" })
  kind: "bing_search";

  @doc("The connection configuration for the Bing search tool")
  @sample(#{ connection: #{ kind: "reference" } })
  connection: Connection;

  @doc("The configuration options for the Bing search tool")
  @sample(#{
    options: #{
      instanceName: "MyBingInstance",
      market: "en-US",
      setLang: "en",
      count: 10,
      freshness: "Day",
    },
  })
  options?: Record<unknown>;
}

import "../core.tsp";
import "../tools.tsp";
import "../connection.tsp";

namespace AgentSchema;

/**
 * A tool for searching files.
 * This tool allows an AI agent to search for files based on a query.
 */
model FileSearchTool extends Tool {
  @doc("The kind identifier for file search tools")
  @sample(#{ kind: "file_search" })
  kind: "file_search";

  @doc("The connection configuration for the file search tool")
  @sample(#{ connection: #{ kind: "reference" } })
  connection: Connection;

  @doc("The IDs of the vector stores to search within.")
  @sample(#{ vectorStoreIds: #["vectorStore1", "vectorStore2"] })
  vectorStoreIds: string[];

  @doc("The maximum number of search results to return.")
  @sample(#{ maximumResultCount: 10 })
  maximumResultCount?: int32;

  @doc("File search ranker.")
  @sample(#{ ranker: "auto" })
  ranker?: string;

  @doc("Ranker search threshold.")
  @sample(#{ scoreThreshold: 0.5 })
  scoreThreshold?: float32;

  @doc("Additional filters to apply during the file search.")
  @sample(#{ filters: #{ fileType: "pdf", createdAfter: "2023-01-01" } })
  filters?: Record<unknown>;
}

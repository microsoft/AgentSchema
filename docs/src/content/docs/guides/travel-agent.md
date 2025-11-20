---
title: Travel Agent Example
description: Learn how to build a travel planning assistant using Bing Search and TripAdvisor API.
---

The Travel Agent is a travel planning assistant that demonstrates how to combine Bing Search for up-to-date information with TripAdvisor API for travel recommendations on destinations, attractions, hotels, and restaurants.

## Agent Definition (`travel.yaml`)

The basic agent definition shows the core structure:

```yaml
name: Travel Basic Agent
description: |-
  This agent enables users to receive up-to-date travel recommendations 
  by leveraging both the Bing Grounding Tool and the TripAdvisor API. 
  The agent summarizes relevant information clearly and offers to create
  a custom itinerary based on the user's travel duration.

model: gpt-4o

tools:
  get_travel_info:
    kind: function
    description: Get basic travel information.

  bing_tool:
    kind: bing_search
    description: Use this tool to get up-to-date information such as weather forecasts, travel advisories, local events, business hours, and transportation options.
    connection: bing_travel_connection
    configurations:
      MyBingInstance:
        market: en-US
        setLang: en

  tripadvisor_tool:
    kind: openapi
    description: Use this tool to get travel information from TripAdvisor.
    connection: tripadvisor_connection
    specification: ./tripadvisor.openapi.json
```

## Agent Template (`travel_manifest.yaml`)

The template provides advanced parameterization and configuration options:

### Key Components

**Model Configuration:**

- Parameterized model selection: `model: {{ model_name }}`
- Supports multiple model options (gpt-4o, gpt-4o-mini, gpt-35-turbo)

**Tools:**

- **Function Tool**: Basic travel information retrieval

  ```yaml
  get_travel_info:
    kind: function
    description: Get basic travel information.
  ```

- **Bing Search Tool**: Real-time information gathering

  ```yaml
  bing_tool:
    kind: bing_search
    description: Use this tool to get up-to-date information such as weather forecasts, travel advisories, local events, business hours, and transportation options.
    connection: {{ bing_travel_connection }}
    configurations:
      - name: {{ bing_instance }}
        market: en-US
        setLang: en
  ```

- **TripAdvisor API Tool**: Travel recommendations

  ```yaml
  tripadvisor_tool:
    kind: openapi
    description: Use this tool to get travel information from TripAdvisor.
    connection: {{ tripadvisor_connection }}
    specification: ./tripadvisor.openapi.json
  ```

**Advanced Parameters:**

```yaml
parameters:
  model_name:
    schema:
      type: string
      enum:
        - gpt-4o
        - gpt-4o-mini
        - gpt-35-turbo
      default: gpt-4o
      x-attributes:
        kind: model
    required: true
    
  bing_travel_connection:
    schema:
      type: string
      x-attribute:
        kind: connection
    description: The name of the Bing connection to use for travel queries.
    required: true
    
  tripadvisor_connection:
    schema:
      type: string
      x-attribute:
        kind: connection
    description: Connection to TripAdvisor API.
    required: true
```

**Example Conversation:**
The template includes a sample conversation showing expected behavior:

```yaml
metadata:
  example:
    - role: user
      content: |-
        I'm planning a trip to Paris for 5 days. Can you recommend some 
        must-see attractions, good places to eat, and nice hotels to stay at?
    - role: assistant
      content: |-
        Sure! Here are some recommendations for your 5-day trip to Paris:
        Attractions:
        1. Eiffel Tower - Iconic landmark with stunning views of the city.
        2. Louvre Museum - Home to the Mona Lisa and other masterpieces.
        3. Notre-Dame Cathedral - Beautiful Gothic architecture.
        4. Montmartre - Charming neighborhood with art studios and the Sacré-Cœur Basilica.
        5. Seine River Cruise - Relaxing way to see the city from the water.
```

## Agent Instructions

The agent follows comprehensive behavioral guidelines:

```yaml
instructions: |-
  You are a trustworthy and knowledgeable travel assistant.
  Use Tripadvisor to recommend destinations, attractions, hotels, restaurants, and experiences based on verified traveler reviews, popularity, and relevance.
  Use Bing to provide up-to-date information such as weather forecasts, travel advisories, local events, business hours, and transportation options.

  Always:
  - Tailor recommendations to the user's stated preferences, such as budget, trip duration, dietary needs, or mobility concerns.
  - Verify time-sensitive information (e.g., availability, closures, visa rules) using Bing before presenting it.
  - Highlight both pros and cons when summarizing reviews or comparisons.
  - Prioritize safety, accessibility, and cultural sensitivity in all suggestions.

  Never:
  - Provide medical, legal, or visa advice beyond publicly available information.
  - Generate or speculate about reviews, ratings, or availability not sourced from Tripadvisor or Bing.
  - Promote or prioritize businesses or services without a clear basis in user need, review data, or trusted search results.
  - Encourage or assist in activities that are illegal, unsafe, or violate the terms of Tripadvisor, Bing, or local laws.
```

## How It Works

1. **Information Gathering**: Bing Search provides current information (weather, events, advisories)
2. **Recommendation Engine**: TripAdvisor API supplies verified traveler reviews and ratings
3. **Personalization**: Agent tailors suggestions based on user preferences and constraints
4. **Verification**: Cross-references time-sensitive information for accuracy
5. **Itinerary Creation**: Combines data sources to create comprehensive travel plans

## Use Cases

- Destination research and planning
- Real-time travel information
- Restaurant and hotel recommendations
- Activity and attraction suggestions
- Travel advisory and safety information
- Custom itinerary generation

## Tool Integration

**Bing Search Tool:**

- Market and language configuration
- Real-time information retrieval
- Weather, events, and transportation data

**TripAdvisor API:**

- OpenAPI specification integration
- Verified traveler reviews
- Business listings and ratings

**Function Tool:**

- Basic travel information processing
- Custom logic implementation

## Template Benefits

The manifest template approach enables:

- **Model Flexibility**: Choose appropriate AI model for performance/cost balance
- **Connection Management**: Separate API credentials from agent logic
- **Environment Configuration**: Different settings for development and production
- **Scalability**: Easy deployment across multiple regions or use cases

---

[← Back to Guides](/guides/)

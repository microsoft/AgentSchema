---
title: QnA Agent Example
description: Learn how to build a question and answer system using Azure Cognitive Language Understanding and Question Answering APIs.
---

The QnA Agent is a question and answer system that demonstrates how to use Azure Cognitive Language Understanding (CLU) API and Question Answering API to extract intent and provide answers to business operation questions.

## Agent Definition (`qna.yml`)

The basic agent definition:

```yaml
name: QnA Basic Agent
description: |-
  This agent enables users to receive answers to their questions
  by leveraging the QnA API.
  The agent summarizes relevant information clearly and offers to create
  a custom itinerary based on the user's travel duration.
metadata:
  tags:
    - example
    - travel
  authors:
    - sethjuarez
    - jietong

model: gpt-4o

inputs:
  clu_project_name:
    kind: string
    description: The CLU project name.
    required: true
  clu_deployment_name:
    kind: string
    description: The CLU deployment name.
    required: true

tools:
  clu_api_tool:
    kind: openapi
    description: An API to extract intent from a given message.
    connection: clu_connection
    specification: ./clu.openapi.json

  cqa_api_tool:
    kind: openapi
    description: An API to get answer to questions related to business operation
    connection: cqa_connection
    specification: ./questionanswering.openapi.json
```

## Agent Template (`qna_manifest.yml`)

The template provides a parameterized version for deployment flexibility:

### Key Components

**Model Configuration:**

- Uses `gpt-4o` as the AI model
- Defined as a resource for deployment management

**Input Parameters:**

```yaml
inputs:
  clu_project_name:
    kind: string
    description: The CLU project name.
    required: true
  clu_deployment_name:
    kind: string
    description: The CLU deployment name.
    required: true
```

**Tools:**

- **CLU API Tool**: Extracts intent from user messages
  - `kind: openapi` - Uses OpenAPI specification
  - `connection: {{clu_connection}}` - Parameterized connection
  - `specification: ./clu.openapi.json` - API specification file

- **Question Answering API Tool**: Provides answers to business questions
  - `kind: openapi` - Uses OpenAPI specification  
  - `connection: {{cqa_connection}}` - Parameterized connection
  - `specification: ./questionanswering.openapi.json` - API specification file

**Template Parameters:**

```yaml
parameters:
  clu_connection:
    kind: string
    description: Connection to Azure OpenAI for CLU.
    required: true
  cqa_connection:
    kind: string
    description: Connection to Azure OpenAI for CQA.
    required: true
```

**Resources:**

```yaml
resources:
  gpt-4o-deployment:
    kind: model
    id: gpt-4o
  clu_api_tool:
    kind: tool
    id: openapi
  cqa_api_tool:
    kind: tool
    id: openapi
```

## Agent Instructions

The agent follows specific behavioral guidelines:

```yaml
instructions: |-
  You are a triage agent. Your goal is to answer questions and redirect message according to their intent. You have at your disposition 2 tools:
  1. cqa_api: to answer customer questions such as procedures and FAQs.
  2. clu_api: to extract the intent of the message.
  You must use the tools to perform your task. Only if the tools are not able to provide the information, you can answer according to your general knowledge.
  - When you return answers from the cqa_api return the exact answer without rewriting it.
  - When you return answers from the clu_api return 'Detected Intent: {intent response}' and fill {intent response} with the intent returned from the api.
  To call the clu_api, the following parameters values should be used in the payload:
  - 'projectName': value must be '{{clu_project_name}}'
  - 'deploymentName': value must be '{{clu_deployment_name}}'
  - 'text': must be the input from the user.
```

## How It Works

1. **Intent Detection**: User message is sent to CLU API to extract intent
2. **Question Processing**: Based on intent, route to appropriate QnA API
3. **Answer Retrieval**: QnA API searches knowledge base for relevant answers
4. **Response Formatting**: Agent returns structured response with detected intent or direct answer
5. **Fallback**: If APIs can't provide information, agent uses general knowledge

## Use Cases

- Customer support automation
- FAQ systems
- Business procedure queries
- Intent-based message routing
- Knowledge base search and retrieval

## API Integration

The agent integrates with two Azure Cognitive Services:

- **Cognitive Language Understanding (CLU)**: Intent extraction and classification
- **Question Answering**: Knowledge base search and answer generation

Both services are accessed via OpenAPI specifications, allowing for standardized integration patterns.

## Template Benefits

The manifest template approach provides:

- **Environment Flexibility**: Different connections for dev/test/prod
- **Configuration Management**: Parameterized API endpoints and credentials
- **Reusability**: Deploy same logic with different knowledge bases
- **Maintainability**: Separate agent logic from deployment configuration

---

[← Back to Guides](/guides/)

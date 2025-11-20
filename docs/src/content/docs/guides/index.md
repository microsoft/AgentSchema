---
title: Guide
description: Comprehensive guides for getting started with AgentSchema and building your first agents.
---

Welcome to the AgentSchema guides! This section provides step-by-step tutorials and comprehensive documentation to help you get started with AgentSchema.

## About AgentSchema

AgentSchema is a declarative specification format for defining AI agents and their capabilities. It provides a standardized way to describe agent behavior, tools, connections, and resources in a structured, platform-agnostic manner. With AgentSchema, you can:

- **Define Agent Behavior**: Specify prompts, instructions, and conversation flows
- **Configure Tools**: Integrate external APIs, functions, and services
- **Manage Connections**: Handle authentication and external service integrations
- **Template Variables**: Create reusable, configurable agent definitions
- **Cross-Platform Support**: Deploy agents across different runtime environments

AgentSchema enables you to build sophisticated AI agents with clear, maintainable specifications that can be version-controlled, shared, and deployed consistently across different platforms.

## Examples

Explore real-world examples to understand how AgentSchema works in practice. Each example demonstrates different capabilities and tool integrations.

### Available Examples

- **[Analyst Agent](analyst-agent/)** (`examples/analyst/`) - Sales data analysis agent using Code Interpreter and File Search tools to analyze uploaded CSV files and provide insights on sales performance, customer trends, and product analysis
- **[QnA Agent](qna-agent/)** (`examples/qna/`) - Question and answer system that uses Azure Cognitive Language Understanding (CLU) API and Question Answering API to extract intent and provide answers to business operation questions
- **[Travel Agent](travel-agent/)** (`examples/travel/`) - Travel planning assistant that combines Bing Search for up-to-date information (weather, events, advisories) with TripAdvisor API for recommendations on destinations, attractions, hotels, and restaurants

## Reference Documentation

Complete reference documentation for all AgentSchema components is available, including:

### Core Components

- **[AgentDefinition](../reference/agentdefinition/)** - Main agent specification structure
- **[AgentManifest](../reference/agentmanifest/)** - Agent metadata and deployment configuration  
- **[Model](../reference/model/)** - AI model configuration and options
- **[Tools](../reference/tool/)** - Available tool types ([OpenAPI](../reference/openapitool/), [Function](../reference/functiontool/), [Code Interpreter](../reference/codeinterpretertool/), [File Search](../reference/filesearchtool/), [MCP](../reference/mcptool/), [Web Search](../reference/websearchtool/), [Custom](../reference/customtool/))
- **[Connections](../reference/connection/)** - Authentication and service connection types ([API Key](../reference/apikeyconnection/), [Anonymous](../reference/anonymousconnection/), [Reference](../reference/referenceconnection/), [Remote](../reference/remoteconnection/))
- **[Templates](../reference/template/)** - Reusable agent specifications with variables

### Properties and Resources

- **[Properties](../reference/property/)** - Schema definitions for agent inputs and configurations ([Object](../reference/objectproperty/), [Array](../reference/arrayproperty/))
- **[Resources](../reference/resource/)** - External resources and bindings for agent deployment

---

## Stay Updated

This guides section is actively being developed with new content added regularly. We're working on expanding the tutorials, adding more examples, and providing deeper insights into AgentSchema's capabilities. Check back frequently for:

- New step-by-step tutorials
- Additional real-world examples
- Advanced implementation patterns
- Best practices and tips
- Community contributions

*Have suggestions for guides you'd like to see? Feel free to contribute or request specific topics!*

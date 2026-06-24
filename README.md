# .NET Microservices with ASP.NET, RabbitMQ and Kubernetes

A microservices-based application built with ASP.NET Core to explore distributed systems, asynchronous messaging, inter-service communication, containerization, and orchestration.

## Architecture

The system consists of multiple services communicating through synchronous and asynchronous patterns.

### Communication Patterns

* REST API
* RabbitMQ Event Bus

### Infrastructure

* Docker
* Kubernetes

## Features

* Independent microservices
* Event-driven communication using RabbitMQ
* Containerized services with Docker
* Kubernetes deployment configuration
* Database persistence with Entity Framework Core

## Tech Stack

* ASP.NET Core
* C#
* Entity Framework Core
* SQL Server
* RabbitMQ
* Docker
* Kubernetes

## Getting Started

### Prerequisites

* .NET 9 SDK
* Docker Desktop
* Kubernetes
* SQL Server
* RabbitMQ

### Run Services

Build and run the services:

```bash
dotnet run
```

### Kubernetes Deployment

Apply Kubernetes resources:

```bash
kubectl apply -f K8S/
```

## Learning Outcomes

This project helped me gain hands-on experience with:

* Microservices architecture
* Event-driven systems
* Asynchronous messaging with RabbitMQ
* Docker containerization
* Kubernetes orchestration
* Distributed system design principles

## Future Improvements


* Centralized Logging
* Distributed Tracing
* CI/CD Pipeline
* Unit and Integration Testing


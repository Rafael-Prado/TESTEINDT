# 🧩 TESTEINDT - Arquitetura de Microserviços com .NET, Kafka e MongoDB

Este projeto é uma arquitetura de microsserviços implementada com .NET 8, utilizando padrões como **DDD**, **CQRS**, **SOLID**, e **Arquitetura Hexagonal**, integrando serviços com **Kafka** e **MongoDB**.

---

## 📐 Arquitetura

A solução é dividida em dois microsserviços principais:

- `ApiProposta`
- `ApiContratacao`

Cada serviço possui separação clara entre camadas **Domain**, **Infra** e **Application (implícita nos services)**.

### 🔁 Padrões utilizados

- ✅ **SOLID** – Cada classe com responsabilidade única e injeção de dependência.
- ✅ **Arquitetura Hexagonal / Limpa** – Separação entre domínio, infraestrutura e serviços.
- ✅ **DDD (Domain-Driven Design)** – Domínios ricos, interfaces explícitas, validações.
- ✅ **CQRS** – Separação clara entre leitura e escrita.
- ✅ **Event-Driven Architecture** – Comunicação entre serviços via Kafka.

---

## 🧱 Estrutura de Pastas
📁 ApiProposta/
├── Domain/

│ ├── Interfaces/

│ ├── Entities/

│ ├── Services/

│ └── Validator/

├── Infra/

│ ├── Kafka/

│ ├── Mongo/

│ └── Repositories/

📦 Microsserviços
ApiProposta

Responsável por orquestrar propostas.

Consome eventos do Kafka.

Armazena histórico no MongoDB.

📦 Microsserviços ApiContratacao

Responsável pela contratação com base na proposta.

Publica eventos no Kafka.

Usa Redis para cache.

Boas Práticas

✅ Cada camada tem responsabilidade única

✅ Repositórios e serviços são desacoplados por interfaces

✅ Kafka isolado em consumidores específicos (KafkaConsumerHostedService)

✅ Serviços possuem validações específicas

## 🚀 Tecnologias Utilizadas

- ✅ .NET 8
- ✅ MongoDB
- ✅ Apache Kafka
- ✅ Docker / Docker Compose
- ✅ Dependency Injection (DI)

- 
``bash
# Suba os containers (APIs + Kafka + MongoDB)
docker compose up -d --build



🧠 Decisões Técnicas
1. Separação por Contextos (Bounded Contexts)

A solução foi dividida em dois serviços principais:

ApiProposta

ApiContratacao

👉 Essa separação segue o Domain-Driven Design (DDD), permitindo que cada serviço seja responsável por um contexto de negócio isolado, facilitando escalabilidade, manutenção e deploy independente.

2. Arquitetura em Camadas / Hexagonal

Cada microsserviço apresenta camadas bem definidas:

Domain: regras de negócio, interfaces, entidades, validações.

Infra: infraestrutura externa como MongoDB, Kafka, Redis, etc.

Services (implícita na camada de domínio): orquestração e lógica de aplicação.

👉 Isso promove baixo acoplamento e alta coesão, além de facilitar testes unitários.

3. Padrão CQRS (Command Query Responsibility Segregation)

As responsabilidades de leitura e escrita estão separadas:

Escrita: via eventos Kafka ou comandos/processos internos.

Leitura: via repositórios MongoDB.

👉 Isso melhora a performance, escalabilidade e organização do código — especialmente útil em sistemas com muita leitura e escrita desacopladas.

4. Event-Driven Architecture com Kafka

A comunicação entre serviços é feita por eventos Kafka. Exemplo:

ApiProposta publica eventos após validação ou aprovação de uma proposta.

ApiContratacao consome esses eventos para iniciar o processo de contratação.

👉 Isso promove desacoplamento entre microsserviços, garantindo resiliência e tolerância a falhas.

5. MongoDB como banco NoSQL

Cada serviço usa MongoDB como banco principal, ideal para documentos flexíveis e consultas rápidas.

Mongo é utilizado principalmente para persistência de históricos e estados.

👉 Boa escolha para sistemas baseados em eventos, com estrutura flexível e escalável.

6. Redis para cache

O serviço ApiContratacao utiliza Redis para cache, provavelmente para acelerar acesso a dados que mudam pouco.

👉 Excelente para melhorar performance e reduzir carga no banco.

7. Inversão de Dependência via Interfaces (SOLID)

O uso de interfaces (IPropostaRepository, IMongoService, etc.) indica adesão ao Princípio da Inversão de Dependência, promovendo:

Testabilidade

Substituição fácil de implementações

Código mais limpo e desacoplado

8. Validações com camada separada

A existência de um PropostaValidator.cs mostra uma boa prática: separar validações específicas do domínio.

👉 Isso evita regras de negócio espalhadas, melhora legibilidade e centraliza regras de validação.

9. Hosted Services para Kafka (background workers)

O uso de KafkaConsumerHostedService mostra que os consumidores Kafka rodam como serviços em background, aderindo ao padrão IHostedService.

👉 Isso é a maneira correta e escalável de lidar com mensageria em .NET.

✨ Benefícios das escolhas

Escalabilidade: microsserviços independentes e desacoplados

Testabilidade: camadas bem separadas e baseadas em interfaces

Performance: cache com Redis e consultas otimizadas via Mongo

Resiliência: comunicação assíncrona com Kafka

Manutenibilidade: arquitetura clara e aderente a boas práticas

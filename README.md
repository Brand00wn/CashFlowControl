# 🏦 Controle de Fluxo de Caixa

**Controle de Fluxo de Caixa** é um sistema de rastreamento financeiro que ajuda os comerciantes a gerenciar o fluxo de caixa diário com o registro de transações (débito e crédito) e um relatório consolidado diário de saldo.

## 🚀 Funcionalidades
- **Gestão de Transações**: Criar, listar e recuperar transações financeiras.
- **Serviço de Consolidação Diária**: Processa e consolida registros financeiros diariamente de forma assíncrona.
- **Arquitetura Resiliente**: Garante que o serviço de transações continue disponível mesmo que o serviço de consolidação falhe.
- **Escalabilidade**: Suporta até 50 requisições por segundo com uma perda máxima de 5% nas requisições.

## 🛠 Stack Tecnológica
- **C# com .NET 8**
- **Entity Framework Core** para persistência de dados
- **JWT** para tokenização de usuários
- **MediatR** para comunicação interna
- **RabbitMQ** para broker de mensagens
- **Docker** para containerização
- **Swagger** para documentação da API
- **xUnit** para testes unitários
- **Serilog** para logging estruturado


## 📖 Configuração e Uso (Dockerizado)

### 1. Pré-requisitos
Antes de executar o projeto, certifique-se de que você tenha o **Docker** instalado em sua máquina. Caso não tenha o Docker, você pode instalá-lo seguindo as instruções oficiais em: [Instalar o Docker](https://docs.docker.com/get-docker/).

### 2. Clonar o Repositório
Clone o repositório para sua máquina local:
```
sh
git clone https://github.com/Brand00wn/CashFlowControl.git
cd cashflow-control
```

### 3. Executar o Projeto com Docker Compose
No diretório do projeto, basta executar o seguinte comando para iniciar o projeto junto com seus serviços dependentes (como banco de dados, RabbitMQ, etc.):
```
docker-compose up --build
```

### 4. Acessar a Aplicação
A aplicação possui três APIs, para acessá-las, basta entrar nas URls relacionadas a cada módulo:
-Autenticação (AuthenticationService - http://localhost:5001/swagger/index.html)
-Lançamento de Vendas (LaunchService - http://localhost:5002/swagger/index.html)
-Consolidação (ConsolidationService - http://localhost:5003/swagger/index.html)
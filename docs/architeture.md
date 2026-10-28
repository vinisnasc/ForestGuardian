# ForestGuardian — Arquitetura inicial

## 1. Objetivo

O ForestGuardian é um sistema para registro, análise e acompanhamento de ocorrências ambientais em todo o território brasileiro.

A arquitetura tem como objetivos:

- Separar as regras de negócio dos detalhes técnicos.
- Facilitar a manutenção e a evolução do sistema.
- Permitir a implementação de testes unitários e de integração.
- Aplicar princípios de baixo acoplamento e alta coesão.
- Servir como ambiente prático de estudo de TDD, BDD e boas práticas de desenvolvimento .NET.

## 2. Organização da solução

A solução está organizada nos seguintes projetos:

```text
ForestGuardian/
├── Identity/
│   ├── ForestGuardian.Identity.API/
│   └── ForestGuardian.Identity.API.Tests/
├── ForestGuardian.API/
├── ForestGuardian.Domain/
├── ForestGuardian.Infra/
├── ForestGuardian.Services/
└── docs/
    └── architecture.md
```

### 2.1. ForestGuardian.Domain

Responsável pelo núcleo do domínio e pelas regras de negócio.

Responsabilidades:
- Representar entidades e objetos de valor.
- Definir invariantes e comportamentos do domínio.
- Garantir que operações inválidas sejam impedidas pelas regras de negócio.
- Manter independência de banco de dados, frameworks web e infraestrutura.

Exemplos futuros:
- `OcorrenciaAmbiental`
- `Evidencia`
- `CoordenadaGeografica`

O domínio não deve depender dos projetos de API, serviços ou infraestrutura.

### 2.2. ForestGuardian.Services

Responsável pela camada de aplicação e pela execução dos casos de uso.

Responsabilidades:
- Orquestrar operações do sistema.
- Coordenar as interações entre domínio e contratos externos.
- Definir interfaces necessárias para persistência e integrações.
- Coordenar validações de entrada e resultados das operações.
- Controlar os limites das transações quando necessário.

Exemplos futuros:
- Registrar uma ocorrência ambiental.
- Consultar ocorrências.
- Atualizar os dados de uma ocorrência.
- Encaminhar uma ocorrência para análise.

A camada de aplicação não deve depender diretamente do Entity Framework Core ou de controllers.

### 2.3. ForestGuardian.Infra

Responsável pela implementação dos detalhes técnicos e das integrações externas.

Responsabilidades:
- Configurar o acesso ao banco de dados.
- Implementar os repositórios definidos pelas camadas internas.
- Implementar persistência com Entity Framework Core.
- Configurar mapeamentos e migrations.
- Integrar serviços externos quando necessário.

A infraestrutura pode depender de contratos definidos em Services e das entidades de Domain, mas as camadas internas não devem depender de suas implementações concretas.

### 2.4. ForestGuardian.API

Responsável por expor as funcionalidades do sistema por HTTP.

Responsabilidades:
- Receber e responder requisições HTTP.
- Definir endpoints e controllers.
- Validar aspectos específicos do protocolo HTTP.
- Aplicar autenticação e autorização.
- Converter resultados dos casos de uso em respostas HTTP.
- Registrar as dependências necessárias para inicializar a aplicação.

Controllers não devem concentrar regras de negócio ou acessar diretamente o banco de dados.

### 2.5. ForestGuardian.Identity.API

Responsável pelo módulo de identidade e autenticação.

Responsabilidades:
- Registrar usuários.
- Autenticar usuários.
- Gerenciar recuperação e alteração de senha.
- Emitir tokens de autenticação.
- Gerenciar os dados de identidade por meio do ASP.NET Core Identity.

A API principal deve validar os tokens emitidos pelo módulo de identidade, sem precisar referenciar diretamente seu projeto de API.

A separação atual não implica a necessidade de microsserviços distribuídos ou comunicação por mensageria.

### 2.6. Projetos de testes

Os projetos de testes devem acompanhar as responsabilidades de cada camada.

- **Domain.Tests:** testa entidades, comportamentos e invariantes do domínio.
- **Services.Tests:** testa os casos de uso utilizando dependências simuladas quando apropriado.
- **Infra.Tests:** testa persistência e integrações, preferencialmente com testes de integração.
- **API.Tests:** testa endpoints, autenticação, autorização e contratos HTTP.
- **Identity.API.Tests:** testa os fluxos de identidade já existentes.

Os testes unitários devem priorizar comportamento observável, sem depender de banco de dados ou serviços externos. Testes de integração devem verificar os componentes reais que precisam funcionar em conjunto.

## 3. Regras de dependência

As referências entre projetos devem respeitar as seguintes regras:

1. Domain não referencia nenhum projeto da aplicação.
2. Services referencia Domain.
3. Infra referencia Domain e Services.
4. API referencia Services e Infra para inicialização e composição da aplicação.
5. Identity.API permanece independente da API principal.
6. As camadas internas não dependem de frameworks de infraestrutura.
7. As implementações concretas são fornecidas por injeção de dependência.

## 4. Fluxo de execução de um caso de uso

Exemplo: registro de uma ocorrência ambiental.

1. A API recebe a requisição.
2. A camada de aplicação executa o caso de uso.
3. O domínio aplica as regras de negócio pertinentes.
4. A camada de aplicação utiliza um contrato de persistência.
5. A infraestrutura implementa esse contrato e persiste os dados.
6. O resultado retorna à API, que produz a resposta HTTP adequada.

Esse fluxo pode variar conforme o caso de uso, mas as regras de negócio devem continuar independentes dos detalhes de transporte e persistência.

## 5. Diretrizes de desenvolvimento

- Aplicar SOLID de forma proporcional à complexidade do problema.
- Evitar abstrações sem necessidade concreta.
- Priorizar nomes explícitos para classes, métodos e contratos.
- Manter regras de negócio fora dos controllers.
- Utilizar injeção de dependência para reduzir acoplamento.
- Escrever testes para comportamentos relevantes, incluindo cenários de erro.
- Utilizar TDD quando apropriado, começando por um teste que descreva o comportamento esperado.
- Documentar decisões arquiteturais relevantes e suas justificativas.
- Não introduzir microsserviços, mensageria ou padrões adicionais sem uma necessidade identificada.

## 6. Decisões arquiteturais iniciais

**DA-001 — Separação por responsabilidade**

A solução será organizada em Domain, Services, Infra e API para separar regras de negócio, casos de uso, detalhes técnicos e transporte HTTP.

**DA-002 — Identidade independente**

O módulo de identidade permanecerá separado da API principal. A integração ocorrerá por meio de tokens de autenticação, sem referência direta entre os projetos de API.

**DA-003 — Monólito modular inicialmente**

As funcionalidades ambientais serão desenvolvidas em uma aplicação principal organizada por responsabilidades. A separação em projetos não significa que cada camada seja um microsserviço.

**DA-004 — Testabilidade como requisito arquitetural**

As dependências serão organizadas para permitir testes unitários independentes e testes de integração nos pontos em que componentes reais interagem.

## 7. Evolução da arquitetura

As decisões aqui registradas representam a estrutura inicial do projeto. Novas necessidades poderão justificar revisões, desde que os impactos sejam avaliados e documentados.

Decisões que alterem significativamente a arquitetura poderão ser registradas em documentos de Architecture Decision Records (ADRs).

## 8. Critérios de aceite da FG-001

- [x] As responsabilidades de cada camada estão documentadas.
- [x] As regras de negócio são separadas dos controllers e da infraestrutura.
- [x] A estrutura permite adicionar funcionalidades sem misturar responsabilidades.
- [x] As decisões arquiteturais iniciais estão registradas no repositório.

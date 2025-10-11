[Coldmart] - Plataforma de cursos online
1. Apresentação
Bem-vindo ao repositório do projeto [Coldmart]. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo `Arquitetura, Modelagem e Qualidade de Software`. O objetivo principal é modelar um sistema de cursos com o uso de DDD, com separação dos contextos delimitados, bem como a comunicação entre os domínios através de eventos. O sistema é desenvolvido com a metodologia TDD, onde optei por usar o Xunit juntamente com as bibliotecas Moq e AutoFixture para facilitar a geração de modelos de teste.

Autor(es)

Tiago Henrique de Castilhos

2. Proposta do Projeto

O projeto consiste em:

API RESTful: Exposição dos casos de uso do sistema de gestão de cursos

Autenticação e Autorização: Implementação de controle de acesso, diferenciando administradores e alunos.

Acesso a Dados: Implementação de acesso ao banco de dados através de ORM.

3. Tecnologias Utilizadas

Linguagem de Programação: C#

Frameworks:

ASP.NET Core Web API

Entity Framework Core

Banco de Dados: SQLite e SQL Server

Autenticação e Autorização:

ASP.NET Core Identity

JWT (JSON Web Token) para autenticação na API

Documentação da API: Swagger

4. Estrutura do Projeto

A estrutura do projeto é organizada da seguinte forma:

src/

Coldmart.{ Nome do contexto }.Business/ - Definição e implementação da orquestração dos casos de uso.

Coldmart.{ Nome do contexto }.Data/ - Definição da camada de acesso a dados (configuração do EF).

Coldmart.{ Nome do contexto }.Domain/ - Definição das entidades, enums e constantes do módulo.

Coldmart.{ Nome do contexto }.{ Nome da camada }.Tests/ - Projeto de testes do respectivo projeto.

Coldmart.Api/ - API RESTful

README.md - Arquivo de Documentação do Projeto

FEEDBACK.md - Arquivo para Consolidação dos Feedbacks

.gitignore - Arquivo de para ignorar arquivos e pastas do Git

5. Funcionalidades Implementadas

Casos de uso especificados no documento do projeto, que pode ser acessado [aqui](./docs/Projeto-Terceiro-Modulo-Mba-DevXpert.pdf).

Autenticação e Autorização: Diferenciação entre alunos e administradores.

API RESTful: Exposição de use cases via API.
Documentação da API: Documentação automática dos endpoints da API utilizando Swagger.

6. Como Executar o Projeto

Pré-requisitos

.NET SDK 9.0 ou superior

SQL Server (Opcional)

Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)

Git

Passos para Execução

Clone o Repositório:

git clone https://github.com/seu-usuario/nome-do-repositorio.git
cd nome-do-repositorio

Caso deseje utilizar o SQLite como banco de dados, não se faz necessário configurar o SQL server.

6.1 (Opcional - Uso do SQL server)
Configuração do Banco de Dados:

No arquivo appsettings.json, configure a string de conexão do SQL Server.
Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.

7. Instruções de Configuração
JWT para API: As chaves de configuração do JWT estão no appsettings.json.
Migrações do Banco de Dados: As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

8. Documentação da API
A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:5047/swagger

9. Avaliação

Este projeto é parte de um curso acadêmico e não aceita contribuições externas.

Para feedbacks ou dúvidas utilize o recurso de Issues

O arquivo FEEDBACK.md é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
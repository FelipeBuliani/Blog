# README

## Descrição do Projeto

Esta é uma aplicação monolítica desenvolvida com Razor Pages, utilizando Entity Framework, AutoMapper, SQLite e WebSocket para notificações de novas postagens, edições e deleções de postagens.

## Funcionalidades

- Cadastro de usuário.
- Autenticação.
- Visualização de postagens sem necessidade de autenticação.
- Adição de postagens com autenticação.
- Edição e exclusão de postagens apenas pelo usuário que as criou.
- Notificações em tempo real de novas postagens, edições e deleções usando WebSocket.

## Tecnologias Utilizadas

- **Razor Pages**: Framework para construção de páginas dinâmicas.
- **Entity Framework**: ORM para acesso ao banco de dados.
- **AutoMapper**: Biblioteca para mapeamento de objetos.
- **SQLite**: Banco de dados leve e fácil de configurar.
- **WebSocket**: Protocolo para comunicação bidirecional em tempo real.

## Usuários Pré-Cadastrados

Para simplificar o processo de testes, alguns usuários e postagens já foram incluídos no banco de dados SQLite:

- **Usuários:**
  - Felipe
  - João
  - Pedro

- **Senha para todos os usuários:** `teste123`

Você também pode registrar um novo usuário fornecendo um nome e uma senha.

## Configuração e Execução

### Requisitos

- .NET 8 SDK
- Visual Studio ou qualquer editor de código compatível

### Configuração do WebSocket

Para que a conexão WebSocket funcione corretamente e as páginas possam monitorar eventos de postagens, é necessário configurar a mesma porta em que a aplicação está rodando.

No arquivo `Index.cshtml`, linha 55, ajuste a URL do WebSocket para a mesma porta que a solução está sendo executada. Se você estiver usando HTTPS, considere usar wss:// em vez de ws://.
```javascript
let socket = new WebSocket('ws://localhost:5288/ws');
```

## Execução

1. Clone o repositório.
2. Navegue até o diretório do projeto.
3. Execute o comando `dotnet run` para iniciar a aplicação.
4. Acesse [http://localhost:5288](http://localhost:5288) no seu navegador.

## Teste de Multissessão

Para testar a funcionalidade de notificações com mais de um usuário simultaneamente, você pode abrir a aplicação em dois navegadores diferentes ou usar uma janela normal e outra anônima, autenticando com usuários diferentes.

## Funcionalidade de Notificações

Quando um usuário estiver na tela principal de listagem de postagens e uma nova postagem é criada, editada ou deletada, uma notificação aparecerá no topo da tela e as postagens serão atualizadas automaticamente.



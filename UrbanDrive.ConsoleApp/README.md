# UrbanDrive — Sistema de Gestão de Mobilidade Urbana

Sistema de console em C# para gerenciamento de empresas de transporte multimodal (aluguel de carros, patinetes elétricos e frotas corporativas), desenvolvido como trabalho prático de Programação Orientada a Objetos.

**Curso:** Análise e Desenvolvimento de Sistemas — PUC Minas
**Integrantes:** Gabriel Soares e Samuel Elias

## Como executar

**Requisito:** [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0). O projeto usa `net9.0`; tendo apenas o .NET 8 ou o .NET 10, ele compila, mas não executa.

Para conferir as versões instaladas: `dotnet --list-sdks` e `dotnet --list-runtimes`.

```bash
git clone https://github.com/Gabriel-Srs/UrbanDrive.git
cd UrbanDrive
dotnet run --project UrbanDrive.ConsoleApp
```

Ou, de dentro da pasta do projeto:

```bash
cd UrbanDrive.ConsoleApp
dotnet run
```

A execução mostra a demonstração da sprint atual e os cenários de teste, com o resultado `X aprovados, Y reprovados` no final.

## Estrutura

```text
UrbanDrive/
├── UrbanDrive.sln
└── UrbanDrive.ConsoleApp/
    ├── Models/          # Entidades (Veiculo, Carro, ...)
    ├── Program.cs       # Demonstração e cenários de teste
    ├── docs/            # Backlog e planejamento das sprints
    └── imagens/         # Evidências (prints de execução)
```

## Documentação

| Documento | Descrição |
|---|---|
| [Product Backlog](docs/backlog.md) | Backlog priorizado e seu histórico |
| [Sprint 1](docs/sprints/sprint-1.md) | Modelagem inicial e encapsulamento |
| [Sprint 2](docs/sprints/sprint-2.md) | Relacionamentos e membros estáticos |
| Sprint 3 | Herança, polimorfismo e especialização (a ser criada) |
| Sprint 4 | Abstração, interfaces e consolidação (a ser criada) |

O acompanhamento das tarefas também está no quadro Kanban do GitHub Projects do repositório.

## Convenções do projeto

- Commits seguem o padrão **Conventional Commits** (`feat:`, `fix:`, `docs:`, `test:`, `refactor:`).
- A branch `main` mantém sempre a versão estável.
- Cada tarefa é desenvolvida em uma branch própria (`feature/...`, `fix/...`, `docs/...`) e integrada por Pull Request.
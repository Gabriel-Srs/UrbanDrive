# UrbanDrive — Sistema de Gestão de Mobilidade Urbana

Sistema de console em C# para gerenciamento de empresas de transporte multimodal (aluguel de carros, patinetes elétricos e frotas corporativas), desenvolvido como trabalho prático de Programação Orientada a Objetos.

**Curso:** Análise e Desenvolvimento de Sistemas — PUC Minas
**Integrantes:** Gabriel Soares e Samuel Elias

## Como executar

```bash
git clone <URL-DO-REPOSITORIO>
cd UrbanDrive
dotnet run --project src/UrbanDrive.App
```

Requisito: [.NET SDK](https://dotnet.microsoft.com/download) instalado. <!-- CONFIRMAR a versão e o caminho do projeto -->

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
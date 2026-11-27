# UrbanDrive — Sistema de Gestão de Mobilidade Urbana

Sistema de console em C# para empresas de transporte multimodal: aluguel de carros e patinetes elétricos, gestão de frotas corporativas e motoristas. Desenvolvido como trabalho prático de **Programação Orientada a Objetos**, com o desenvolvimento organizado em quatro sprints.

**Curso:** Análise e Desenvolvimento de Sistemas — PUC Minas
**Integrantes:** Gabriel Soares e Samuel Elias

## O que o sistema faz

- Cadastra **veículos** com identificador, placa (padrão antigo ou Mercosul), modelo e valor-base da diária, com validação de todos os dados.
- Especializa veículos em **carros** (quantidade de portas e combustível) e **patinetes** (autonomia da bateria).
- Calcula o **aluguel** de forma diferente para cada tipo: carro com 5% de seguro, patinete com 10% de desconto a partir de 3 dias.
- Registra **manutenções**, gerais ou com a peça substituída.
- Representa **motoristas** e suas **CNHs**, com validação do número, da categoria e da validade.
- Agrupa veículos em **frotas**, sem permitir placas repetidas.
- Registra **locações**, rejeitando motoristas com CNH vencida e calculando o valor total com a taxa de serviço do sistema.
- Mantém **configurações compartilhadas**, como a taxa de serviço e o contador de veículos cadastrados.

## Conceitos de POO aplicados

| Sprint | Conceitos | Onde aparecem | Situação |
|---|---|---|---|
| 1 | Encapsulamento, construtores, validação | `Veiculo` (atributos privados, propriedades com validação, `set` privado) | ✅ Concluída |
| 2 | Associação, agregação, composição, membros estáticos | `Locacao` → `Motorista`/`Veiculo` (associação), `Frota` ◇ `Veiculo` (agregação), `Motorista` ◆ `CNH` (composição), `ConfiguradorSistema` (estático) | ✅ Concluída |
| 3 | Herança, sobrescrita, sobrecarga, polimorfismo, classe selada | `Carro` e `Patinete` (`sealed`) herdando de `Veiculo`, `CalcularAluguel(int dias)` sobrescrito, `RealizarManutencao()` / `RealizarManutencao(string)` | ✅ Concluída |
| 4 | Abstração e interfaces | `Veiculo` abstrato, `IImprimivel`, `IEletrico`, `CarroEletrico`, menu de console | ⏳ A fazer |

O diagrama com todas as classes e relações está em [diagrama-classes.md](UrbanDrive.ConsoleApp/docs/diagramas/diagrama-classes.md).

## Como executar

**Requisito:** [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0). O projeto usa `net9.0`; com apenas o .NET 8 ou o .NET 10 instalado, ele compila, mas não executa.

Para conferir as versões instaladas: `dotnet --list-sdks` e `dotnet --list-runtimes`.

```bash
git clone https://github.com/Gabriel-Srs/UrbanDrive.git
cd UrbanDrive
dotnet run --project UrbanDrive.ConsoleApp
```

A execução mostra a demonstração de cada funcionalidade e os cenários de teste, com o placar `X aprovados, Y reprovados` no final. Na versão atual: **88 aprovados, 0 reprovados**, sem avisos de compilação. A saída completa da Sprint 3 está em [`docs/evidencias/`](UrbanDrive.ConsoleApp/docs/evidencias/).

## Estrutura do repositório

```text
UrbanDrive/
├── README.md                     # Este arquivo
├── UrbanDrive.sln
└── UrbanDrive.ConsoleApp/
    ├── Models/                   # Entidades: Veiculo, Carro, Patinete, Frota, Motorista, CNH, Locacao, ConfiguradorSistema
    ├── Program.cs                # Demonstração e cenários de teste (um método por funcionalidade)
    ├── docs/
    │   ├── backlog.md            # Product Backlog e histórico de evolução
    │   ├── sprints/              # Planejamento, resultado e retrospectiva de cada sprint
    │   ├── diagramas/            # Diagrama de classes
    │   └── evidencias/           # Saída dos testes
    └── imagens/                  # Evidências (prints de execução e do quadro)
```

## Documentação

| Documento | Descrição |
|---|---|
| [Product Backlog](UrbanDrive.ConsoleApp/docs/backlog.md) | Itens priorizados, dependências, estimativas e histórico |
| [Sprint 1](UrbanDrive.ConsoleApp/docs/sprints/sprint-1.md) | Modelagem inicial e encapsulamento |
| [Sprint 2](UrbanDrive.ConsoleApp/docs/sprints/sprint-2.md) | Relacionamentos entre objetos e membros estáticos |
| [Sprint 3](UrbanDrive.ConsoleApp/docs/sprints/sprint-3.md) | Herança, polimorfismo e especialização |
| Sprint 4 | Abstração, interfaces e consolidação (a ser criada) |
| [Diagrama de classes](UrbanDrive.ConsoleApp/docs/diagramas/diagrama-classes.md) | Classes, atributos, métodos e relações |

O acompanhamento das tarefas é feito no quadro do GitHub Projects **Product Backlog**, vinculado a este repositório.

## Convenções do projeto

- Commits no padrão **Conventional Commits** (`feat:`, `fix:`, `test:`, `docs:`, `refactor:`, `style:`, `chore:`).
- A branch `main` mantém sempre a versão estável.
- Cada tarefa é desenvolvida em uma branch própria (`feature/...`, `fix/...`, `docs/...`) e integrada por Pull Request.
- Cada issue do quadro é fechada pelo Pull Request correspondente (`Closes #N`).

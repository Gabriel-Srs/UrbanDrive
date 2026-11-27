# UrbanDrive — Sistema de Gestão de Mobilidade Urbana

Sistema de console em C# para empresas de transporte multimodal: aluguel de carros, carros elétricos e patinetes elétricos, gestão de frotas, motoristas e locações. Desenvolvido como trabalho prático de **Programação Orientada a Objetos**, com o desenvolvimento organizado em quatro sprints.

**Curso:** Análise e Desenvolvimento de Sistemas — PUC Minas
**Integrantes:** Gabriel Soares e Samuel Elias

## O que o sistema faz

- Cadastra **carros**, **carros elétricos** e **patinetes** com identificador, placa (padrão antigo ou Mercosul), modelo e diária, validando todos os dados.
- Calcula o **aluguel** de forma diferente para cada tipo: carro com 5% de seguro, patinete com 10% de desconto a partir de 3 dias.
- Representa **motoristas** e suas **CNHs**, com validação do número, da categoria e da validade.
- Agrupa veículos em **frotas**, sem permitir placas repetidas.
- Registra **locações**, rejeitando CNH vencida e calculando o total com a taxa de serviço do sistema.
- Registra **manutenções** (geral ou com a peça substituída) e **recarrega** os veículos elétricos.
- Gera **relatórios** de veículos, frotas e locações.
- Oferece um **menu de console** para cadastrar, consultar e operar a frota.

## Conceitos de POO aplicados

| Sprint | Conceitos | Onde aparecem | Situação |
|---|---|---|---|
| 1 | Encapsulamento, construtores, validação | `Veiculo` (atributos privados, propriedades com validação, `set` privado) | ✅ Concluída |
| 2 | Associação, agregação, composição, membros estáticos | `Locacao` → `Motorista`/`Veiculo`, `Frota` ◇ `Veiculo`, `Motorista` ◆ `CNH`, `ConfiguradorSistema` | ✅ Concluída |
| 3 | Herança, sobrescrita, sobrecarga, polimorfismo, classe selada | `Carro` e `Patinete` (`sealed`) herdando de `Veiculo`, `CalcularAluguel(int dias)`, `RealizarManutencao()` / `RealizarManutencao(string)` | ✅ Concluída |
| 4 | Abstração e interfaces | `Veiculo` abstrata, `IImprimivel`, `IEletrico`, `CarroEletrico`, `Bateria`, menu de console | ✅ Concluída |

O diagrama com todas as classes e relações está em [diagrama-classes.md](UrbanDrive.ConsoleApp/docs/diagramas/diagrama-classes.md).

## Instalação

**Requisitos:**
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0). O projeto usa `net9.0`; com apenas o .NET 8 ou o .NET 10 instalado, ele compila, mas não executa.
- [Git](https://git-scm.com/downloads), para clonar o repositório.

Para conferir as versões instaladas:

```bash
dotnet --list-sdks
dotnet --list-runtimes
```

Clonar e compilar:

```bash
git clone https://github.com/Gabriel-Srs/UrbanDrive.git
cd UrbanDrive
dotnet build
```

O esperado é `Compilação com êxito`, com **0 avisos** e **0 erros**.

## Execução

### Aplicação (menu)

```bash
dotnet run --project UrbanDrive.ConsoleApp
```

O menu abre com uma frota de demonstração (um carro, um carro elétrico com 60% de carga e um patinete com 20% de carga):

| Opção | O que faz | Conceito demonstrado |
|---|---|---|
| 1 a 3 | Cadastrar carro, carro elétrico ou patinete | Herança, construtores com `base`, validações |
| 4 | Listar veículos | `ToString` sobrescrito |
| 5 | Consultar veículo por placa | `IImprimivel.GerarRelatorio()` |
| 6 | Simular aluguel de todos os veículos por N dias | Polimorfismo (`CalcularAluguel`) |
| 7 | Registrar locação (motorista, CNH e veículo) | Associação, composição, taxa estática |
| 8 | Realizar manutenção (Enter = geral; ou informe a peça) | Sobrecarga (`RealizarManutencao`) |
| 9 | Recarregar veículo elétrico | `IEletrico.Recarregar()` |
| 10 | Relatórios de frota, veículos e locações | Polimorfismo por interface (`IImprimivel`) |
| 11 | Remover veículo | Agregação |
| 12 | Alterar taxa de serviço | Membro estático compartilhado |
| 0 | Sair | |

Valores decimais aceitam vírgula ou ponto (`130,50` ou `130.50`) e datas usam `dd/mm/aaaa`. Dados inválidos mostram a mensagem de erro e o menu continua.

### Testes

```bash
dotnet run --project UrbanDrive.ConsoleApp -- --testes
```

Executa os cenários de teste das quatro sprints e mostra o placar `X aprovados, Y reprovados` no final. Na versão atual: **132 aprovados, 0 reprovados**. O código de saída é 0 quando todos passam. A saída completa está em [`docs/evidencias/`](UrbanDrive.ConsoleApp/docs/evidencias/).

## Estrutura do repositório

```text
UrbanDrive/
├── README.md                     # Este arquivo (instalação e execução)
├── UrbanDrive.sln
└── UrbanDrive.ConsoleApp/
    ├── Program.cs                # Ponto de entrada: abre o menu ou executa os testes
    ├── Interfaces/               # IImprimivel, IEletrico
    ├── Models/                   # Veiculo (abstrata), Carro, CarroEletrico, Patinete, Bateria,
    │                             # Frota, Motorista, CNH, Locacao, ConfiguradorSistema
    ├── Services/                 # LocadoraService (operações), MenuConsole (interface), DadosDemonstracao
    ├── Testes/                   # TestesUrbanDrive: cenários de teste das Sprints 1 a 4
    ├── docs/
    │   ├── backlog.md            # Product Backlog e histórico de evolução
    │   ├── sprints/              # Planejamento, resultado e retrospectiva de cada sprint
    │   ├── diagramas/            # Diagrama de classes final
    │   ├── evidencias/           # Saída dos testes
    │   ├── apresentacao.md       # Roteiro da apresentação
    │   └── relatorio-final.md    # Evolução das sprints, decisões e dificuldades
    └── imagens/                  # Evidências das Sprints 1 e 2 (prints)
```

O projeto foi mantido em um único projeto de console, com pastas separadas por responsabilidade: modelos, interfaces, serviços da aplicação e testes. A regra de negócio fica em `Models/` e `Services/`; o `Program.cs` só inicializa.

## Documentação

| Documento | Descrição |
|---|---|
| [Product Backlog](UrbanDrive.ConsoleApp/docs/backlog.md) | Itens priorizados, dependências, estimativas e histórico |
| [Sprint 1](UrbanDrive.ConsoleApp/docs/sprints/sprint-1.md) | Modelagem inicial e encapsulamento |
| [Sprint 2](UrbanDrive.ConsoleApp/docs/sprints/sprint-2.md) | Relacionamentos entre objetos e membros estáticos |
| [Sprint 3](UrbanDrive.ConsoleApp/docs/sprints/sprint-3.md) | Herança, polimorfismo e especialização |
| [Sprint 4](UrbanDrive.ConsoleApp/docs/sprints/sprint-4.md) | Abstração, interfaces e consolidação |
| [Diagrama de classes](UrbanDrive.ConsoleApp/docs/diagramas/diagrama-classes.md) | Classes, atributos, métodos e relações |
| [Roteiro da apresentação](UrbanDrive.ConsoleApp/docs/apresentacao.md) | Demonstração passo a passo |
| [Relatório final](UrbanDrive.ConsoleApp/docs/relatorio-final.md) | Evolução das sprints, decisões e dificuldades |

O acompanhamento das tarefas é feito no quadro do GitHub Projects **Product Backlog**, vinculado a este repositório.

## Convenções do projeto

- Commits no padrão **Conventional Commits** (`feat:`, `fix:`, `test:`, `docs:`, `refactor:`, `style:`, `chore:`).
- A branch `main` mantém sempre a versão estável.
- Cada tarefa é desenvolvida em uma branch própria (`feature/...`, `fix/...`, `docs/...`) e integrada por Pull Request.
- Cada issue do quadro é fechada pelo Pull Request correspondente (`Closes #N`).

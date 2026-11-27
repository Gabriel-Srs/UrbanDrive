# Roteiro da apresentação — UrbanDrive

Duração sugerida: **10 a 15 minutos**. Antes de começar, abra um terminal na pasta do repositório e rode `dotnet build` (0 avisos, 0 erros).

## 1. Apresentação do problema (1 min)

- Empresa de mobilidade urbana com carros, carros elétricos e patinetes elétricos.
- Objetivo: cadastrar e consultar veículos, registrar locações com motoristas habilitados, calcular o aluguel de cada tipo e gerar relatórios.
- Desenvolvimento em 4 sprints, com backlog, planejamento, Pull Requests e GitHub Projects.

## 2. Organização do trabalho (2 min)

Mostrar no GitHub:
- **Product Backlog** ([backlog.md](backlog.md)): PBI-01 a PBI-12 e o histórico de evolução.
- **Quadro do GitHub Projects**: issues por tarefa, com a coluna "Concluído".
- **Pull Requests**: um por tarefa, com `Closes #N`, e os commits no padrão Conventional Commits.
- Um **planejamento de sprint** (por exemplo, [sprint-3.md](sprints/sprint-3.md)): objetivo, tarefas, critérios de aceitação, resultado real e retrospectiva.

## 3. Diagrama de classes (2 min)

Abrir o [diagrama-classes.md](diagramas/diagrama-classes.md) e mostrar:
- `Veiculo` **abstrata** no topo, com `Carro` → `CarroEletrico` e `Patinete` (`sealed`).
- As interfaces `IImprimivel` e `IEletrico` e quem as implementa.
- `Bateria` usada por composição pelos dois veículos elétricos.
- `Frota` ◇ `Veiculo` (agregação), `Motorista` ◆ `CNH` (composição) e `Locacao` → `Motorista`/`Veiculo` (associação).
- A aplicação: `Program` → `MenuConsole` → `LocadoraService`.

## 4. Demonstração no menu (5 a 7 min)

```bash
dotnet run --project UrbanDrive.ConsoleApp
```

| Passo | Opção e dados | O que mostrar | Conceito |
|---|---|---|---|
| 1 | `4` | A frota de demonstração com os três tipos | `ToString` sobrescrito |
| 2 | `6` → `3` dias | Carro R$ 378,00, Carro elétrico R$ 630,00, Patinete R$ 108,00 (desconto) | **Polimorfismo**: a mesma chamada, um cálculo por tipo |
| 3 | `6` → `2` dias | Patinete sem desconto (R$ 80,00) | Regra própria do `Patinete` |
| 4 | `1` → `C002`, `abc-1234`, `VW Polo`, `130,50`, `4`, `Flex` | Placa normalizada para `ABC1234` | Encapsulamento e validação |
| 5 | `3` → `P002`, (placa vazia) ... | Mensagem de erro e o menu continua | Validação herdada de `Veiculo` |
| 6 | `5` → `ELT1C23` | Relatório com os dados do carro e da bateria | `IImprimivel` e herança em dois níveis |
| 7 | `9` → `PAT4E56` | Bateria de 20% para 100% | `IEletrico` |
| 8 | `9` → `BRA2E19` | "não é elétrico" | `is IEletrico` |
| 9 | `8` → `BRA2E19`, Enter / `8` → `PAT4E56`, `bateria` | Manutenção geral e com peça | **Sobrecarga** |
| 10 | `7` → `Ana Costa`, `55566677788`, `B`, `10/03/2029`, `ELT1C23`, `2` | Relatório da locação, total R$ 462,00 | Associação e composição |
| 11 | `7` com validade `31/01/2025` | CNH vencida rejeitada | Regra de negócio da `Locacao` |
| 12 | `12` → `20` e depois `10` | Totais mudam para todos | Membro **estático** compartilhado |
| 13 | `11` → `ELT1C23` | Não remove veículo com locação | Regra do serviço |
| 14 | `0` | Encerrar | |

## 5. Testes (1 min)

```bash
dotnet run --project UrbanDrive.ConsoleApp -- --testes
```

- Mostrar os blocos por sprint e o placar final: **132 aprovados, 0 reprovados**.
- Destacar os testes do menu com digitação simulada (`StringReader`) e o teste de `typeof(Veiculo).IsAbstract`.

## 6. Encerramento (1 min)

- Dificuldades e decisões principais (ver [relatório final](relatorio-final.md)).
- Perguntas.

## Perguntas prováveis do professor

| Pergunta | Onde está a resposta |
|---|---|
| Por que `CalcularAluguel` é `virtual` e não `abstract`? | As subclasses chamam `base.CalcularAluguel(dias)` para reaproveitar a regra e a validação. A abstração está na classe e na propriedade `Tipo`. ([sprint-4.md](sprints/sprint-4.md), decisões) |
| Como vocês evitaram duplicação de código? | `base.CalcularAluguel`, `DetalhesRelatorio()` sobrescrito e a classe `Bateria` por composição. |
| Por que `Patinete` é `sealed`? | Requisito da Sprint 3: nenhuma classe deve especializar o patinete. |
| Por que `CarroEletrico` tem dois construtores? | Para validar a bateria antes do construtor de `Carro`, que registra o veículo no contador. |
| Onde fica a regra de negócio? | Em `Models/` e `Services/LocadoraService`. O `MenuConsole` só lê e exibe, e o `Program` só inicializa. |

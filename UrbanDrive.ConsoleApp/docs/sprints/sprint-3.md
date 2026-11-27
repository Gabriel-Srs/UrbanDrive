# Sprint 3 — Herança, polimorfismo e especialização

## A. Identificação

- **Grupo:** Gabriel Soares e Samuel Elias
- **Integrantes:** Gabriel Soares, Samuel Elias
- **Número da sprint:** 3
- **Período:** 19/11/2026 a 22/11/2026 (planejamento em 19/11; desenvolvimento de 20/11 a 22/11)
- **Repositório:** https://github.com/Gabriel-Srs/UrbanDrive
- **Quadro de acompanhamento:** GitHub Projects "Product Backlog"
- **Responsável pelo acompanhamento:** Gabriel Soares

## B. Objetivo da sprint

Implementar comportamentos diferentes para cada categoria de veículo sem duplicar código: `Veiculo` passa a oferecer `CalcularAluguel(int dias)` e `RealizarManutencao` (com sobrecarga), `Carro` e `Patinete` especializam o cálculo por sobrescrita, `Patinete` é uma classe `sealed`, e `Locacao` passa a calcular o valor pelo método polimórfico.

## C. Itens selecionados do Product Backlog

| ID | Requisito | Prioridade | Justificativa |
|---|---|---|---|
| PBI-06 | RF02 — `Carro` e `Patinete` (herança, `sealed`) | Alta | Depende só de `Veiculo` (Sprint 1). `Carro` já existe e precisa ser revisado. `Patinete` é novo. |
| PBI-07 | RF05 — `CalcularAluguel(int dias)` polimórfico | Alta | Depende do PBI-06. Foi isolado em `Locacao.CalcularValorTotal()` na Sprint 2 para receber esta mudança. |
| PBI-08 | RF06 — Manutenção (sobrecarga de `RealizarManutencao`) | Média | Depende do PBI-06. Exige sobrecarga, conceito obrigatório da sprint. |

**Pendência herdada da Sprint 2:** o contador estático conta um `Carro` rejeitado pelas próprias validações, porque o construtor de `Veiculo` roda antes do de `Carro` (ver T2.3 e T3.5).

## D. Planejamento das tarefas

Unidade das estimativas: **horas**.

| ID | Tarefa | Item relacionado | Responsável | Estimativa |
|---|---|---|---|---|
| T3.1 | `Veiculo`: `CalcularAluguel(int dias)` virtual (regra base: diária × dias) e `RealizarManutencao()` / `RealizarManutencao(string pecaSubstituida)` | PBI-07, PBI-08 | Gabriel Soares | 3h |
| T3.2 | `Carro`: sobrescrever `CalcularAluguel` com a regra própria | PBI-06, PBI-07 | Gabriel Soares | 2h |
| T3.3 | `Patinete` (`sealed`): autonomia da bateria, construtor com `base` e sobrescrita de `CalcularAluguel` | PBI-06, PBI-07 | Samuel Elias | 3h |
| T3.4 | `Locacao.CalcularValorTotal()` passa a usar `Veiculo.CalcularAluguel(Dias)` | PBI-07 | Gabriel Soares | 1h |
| T3.5 | Corrigir o contador estático para não contar veículo rejeitado pelas validações da subclasse | PBI-06 | Samuel Elias | 2h |
| T3.6 | Testes: cálculos polimórficos, sobrecarga, `sealed`, `Patinete` e atualização do teste da `Locacao` com `Carro` | PBI-06, 07, 08 | Samuel Elias | 3h |
| T3.7 | Diagrama de classes e backlog atualizados | Todos | Gabriel Soares | 2h |
| T3.8 | Documentar a sprint (`sprint-3.md`), resultado e retrospectiva | Todos | Gabriel Soares e Samuel Elias | 2h |

**Total estimado:** 18h (Gabriel Soares: 8h, Samuel Elias: 8h, em conjunto: 2h).

> O backlog estima 16h para PBI-06 a PBI-08. O planejamento detalhado chegou a 18h por incluir a pendência do contador (T3.5), que não estava no backlog original.

### Cronograma e prazos internos

| Data | Meta do dia |
|---|---|
| Qui 19/11 | Planejamento: `sprint-3.md` por PR, issues das tarefas criadas, issue do PBI-07 reaberta. |
| Sex 20/11 | **T3.1 integrada na `main`** (pré-requisito das demais). Início da T3.2, T3.4 e T3.3. |
| Sáb 21/11 | **Prazo interno: todos os PRs abertos até o fim do dia** (T3.2, T3.3, T3.4, T3.5, T3.6). Revisão cruzada. |
| Dom 22/11 | Merges finais, `dotnet run` com 0 reprovados, T3.7, T3.8 (resultado e retrospectiva) e entrega. |

### Ordem de integração (dependências)

1. `feature/veiculo-polimorfismo` (T3.1): integrada primeiro, pequena, porque as demais dependem dela.
2. Em paralelo, a partir da `main` atualizada: `feature/carro-aluguel` (T3.2 e T3.4) e `feature/patinete` (T3.3).
3. `fix/contador-veiculos` (T3.5), depois que T3.2 e T3.3 estiverem na `main`, pois altera os construtores.
4. `test/sprint-3` (T3.6) e `docs/sprint-3` (T3.7 e T3.8).

Toda integração é feita por **Pull Request**, com revisão do colega. Não há merge local.

### Regras de negócio adotadas

> O enunciado não define os valores. Estas regras foram escolhidas pelo grupo e são o critério dos testes.

| Tipo | Regra de `CalcularAluguel(int dias)` |
|---|---|
| `Veiculo` (base) | `ValorBaseDiaria × dias` (valida `dias > 0`) |
| `Carro` | `base.CalcularAluguel(dias) × 1,05` (acréscimo de 5% de seguro) |
| `Patinete` | `base.CalcularAluguel(dias)`, com 10% de desconto a partir de 3 dias |

A taxa de serviço do `ConfiguradorSistema` continua sendo aplicada na `Locacao`, sobre o resultado de `CalcularAluguel`. `Carro` e `Patinete` chamam `base.CalcularAluguel`, o que reaproveita a validação e evita duplicação de código.

### Decisões de projeto

| Decisão | Escolha |
|---|---|
| Unidade e limite da autonomia da bateria do `Patinete` | Quilômetros (km), maior que zero. |
| Placa do `Patinete` | Mantém a validação do `Veiculo` (padrão antigo ou Mercosul). |
| Contador de veículos (T3.5) | `RegistrarVeiculo()` sai do construtor de `Veiculo` e passa a ser chamado no fim do construtor de `Carro` e de `Patinete`. |
| Retorno de `RealizarManutencao` | `string` com a descrição, sem `Console`, conforme o padrão do projeto. |
| `Veiculo` abstrato | Fica para a Sprint 4 (PBI-12). Nesta sprint continua concreto. |

## E. Critérios de aceitação

**PBI-06 — Carro e Patinete (herança)**
- `Carro` e `Patinete` herdam de `Veiculo` e inicializam a base com `base(...)`.
- `Patinete` é `sealed`: nenhuma classe pode herdar dela.
- `Patinete` rejeita autonomia da bateria menor ou igual a zero (`ArgumentOutOfRangeException`).
- Um `Carro` ou `Patinete` rejeitado pelas próprias validações **não** aumenta o contador de veículos (T3.5).

**PBI-07 — `CalcularAluguel(int dias)` polimórfico**
- Para diária R$ 100,00 e 3 dias: `Veiculo` retorna R$ 300,00 e `Carro` retorna R$ 315,00.
- Para diária R$ 40,00: `Patinete` com 2 dias retorna R$ 80,00 e com 4 dias retorna R$ 144,00 (desconto de 10%).
- Uma lista de `Veiculo` com carro e patinete devolve o cálculo de cada tipo ao chamar `CalcularAluguel`, sem verificar o tipo.
- `dias <= 0` lança `ArgumentOutOfRangeException`.
- `Locacao.CalcularValorTotal()` = `Veiculo.CalcularAluguel(Dias) × (1 + TaxaServico)`. Exemplo: `Carro` de R$ 150,00 por 2 dias com taxa de 10% = R$ 346,50.
- O teste da `Locacao` com `Carro` da Sprint 2 (R$ 330,00) é atualizado para R$ 346,50 e a mudança é registrada.

**PBI-08 — Manutenção (sobrecarga)**
- `RealizarManutencao()` retorna a descrição de uma manutenção sem peça.
- `RealizarManutencao(string pecaSubstituida)` retorna a descrição incluindo a peça.
- Peça nula, vazia ou só com espaços lança `ArgumentException`.
- Os métodos retornam `string` e não usam `Console`.

**Geral:** o projeto compila sem avisos, o `dotnet run` termina com 0 reprovados e há demonstração de herança, sobrescrita, sobrecarga e polimorfismo.

## F. Entregáveis previstos

- Este documento de planejamento.
- Product Backlog atualizado ([backlog.md](../backlog.md)).
- Diagrama de classes atualizado ([diagrama-classes.md](../diagramas/diagrama-classes.md)).
- Código-fonte da hierarquia: `Veiculo`, `Carro` e `Patinete`.
- Demonstração dos diferentes cálculos de locação.
- Demonstração da sobrecarga de `RealizarManutencao`.
- Evidências de testes dos comportamentos especializados.
- Registro das atividades concluídas e das pendências (seções G e H).

### Melhorias da Sprint 2 aplicadas neste planejamento

| Melhoria registrada na retrospectiva | Como foi aplicada |
|---|---|
| Definir as datas da sprint no início | Período e cronograma definidos na seção A e na seção D. |
| Integrar sempre por Pull Request | Nenhum merge local; toda integração por PR. |
| Integrar mais cedo as tarefas com dependência | T3.1 integrada na sexta, antes das demais. |
| Reservar tempo para revisão cruzada | Prazo interno: PRs abertos até sábado, revisão antes do domingo. |
| Reabrir ou criar a issue do PBI-07 | Issue do PBI-07 reaberta em 19/11, antes do início da T3.2. |

### Diagrama de classes (previsto)

```mermaid
classDiagram
    class Veiculo {
        +CalcularAluguel(int dias) decimal
        +RealizarManutencao() string
        +RealizarManutencao(string pecaSubstituida) string
    }
    class Carro {
        +CalcularAluguel(int dias) decimal
    }
    class Patinete {
        <<sealed>>
        +AutonomiaBateriaKm int
        +CalcularAluguel(int dias) decimal
    }
    class Locacao {
        +CalcularValorTotal() decimal
    }
    Veiculo <|-- Carro
    Veiculo <|-- Patinete
    Locacao --> Veiculo : associação
```

## G. Resultado da sprint

### Registro parcial em 26/11/2026

O período previsto (19/11 a 22/11) terminou sem que todas as tarefas fossem concluídas. O planejamento acima não foi alterado; a diferença entre o previsto e o realizado fica registrada nesta seção.

| Item ou tarefa | Situação | Evidência | Observações |
|---|---|---|---|
| T3.1 | Concluído | Commit `8e90233` (22/11), branch `feature/veiculo-polimorfismo` | `CalcularAluguel` virtual e sobrecarga de `RealizarManutencao` em `Veiculo`. Compila sem avisos; `dotnet run`: 57 aprovados, 0 reprovados. Previsto para entrar na `main` em 20/11; o PR foi aberto em 26/11. |
| T3.2 | Concluído | Commit `c1a426f` (26/11), branch `feature/carro-aluguel` | `Carro` sobrescreve `CalcularAluguel` com `base.CalcularAluguel(dias) × 1,05`. |
| T3.3 | Não concluído | — | `Patinete` (`sealed`) ainda não implementado. Responsável: Samuel Elias. |
| T3.4 | Concluído | Commit `c1a426f` (26/11), branch `feature/carro-aluguel` | `Locacao.CalcularValorTotal()` passa a usar `Veiculo.CalcularAluguel(Dias)`. O teste da `Locacao` com `Carro` mudou de R$ 330,00 para R$ 346,50, como previsto no critério de aceitação do PBI-07. |
| T3.5 | Não concluído | — | Correção do contador estático. Depende da T3.3. Responsável: Samuel Elias. |
| T3.6 | Parcialmente concluído | `Program.cs` (branch `feature/carro-aluguel`) | Só foi atualizado o teste da `Locacao` com `Carro` (necessário para a T3.4). Os testes dos cálculos polimórficos, da sobrecarga, do `sealed` e do `Patinete` ainda faltam. Responsável: Samuel Elias. |
| T3.7 | Parcialmente concluído | `backlog.md` e `diagrama-classes.md` (branch `docs/sprint-3`) | Backlog e diagrama atualizados com o que já foi implementado. Falta incluir o `Patinete` depois da T3.3. |
| T3.8 | Parcialmente concluído | Esta seção | Registro parcial. Falta o resultado final e a retrospectiva, feitos em conjunto ao fim da sprint. |

## H. Retrospectiva

_A ser preenchida ao final da sprint._

- **O que funcionou bem?**
- **Quais foram as principais dificuldades?**
- **O planejamento foi adequado ao esforço necessário?**
- **Decisões revistas**
- **O que deverá ser melhorado na próxima sprint?**
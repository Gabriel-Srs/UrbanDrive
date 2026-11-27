# Product Backlog — UrbanDrive

Backlog priorizado, derivado dos requisitos funcionais RF01 a RF10 do enunciado. Estimativas em **horas**.

| ID | Requisito | Item | Prioridade | Dependências | Sprint | Estimativa | Status |
|---|---|---|---|---|---|---|---|
| PBI-01 | RF01 | Entidade `Veiculo` com encapsulamento e validações | Alta | — | 1 | 8h | ✔️ Concluído |
| PBI-02 | RF03 | `Motorista` e `CNH` (composição) | Alta | — | 2 | 6h | ✔️ Concluído |
| PBI-03 | RF04 | `Frota` (agregação com `Veiculo`) | Alta | PBI-01 | 2 | 5h | ✔️ Concluído |
| PBI-04 | RF07 | `ConfiguradorSistema` (membros estáticos) | Média | PBI-01 | 2 | 4h | ✔️ Concluído |
| PBI-05 | RF05 | `Locacao` (associação Motorista–Veiculo) | Média | PBI-01, PBI-02 | 2 | 5h | ✔️ Concluído |
| PBI-06 | RF02 | `Carro` e `Patinete` (herança, `sealed`) | Alta | PBI-01 | 3 | 6h | 🔄 Em andamento |
| PBI-07 | RF05 | `CalcularAluguel(int dias)` polimórfico | Alta | PBI-06 | 3 | 6h | 🔄 Em andamento |
| PBI-08 | RF06 | Manutenção (sobrecarga de `RealizarManutencao`) | Média | PBI-06 | 3 | 4h | 🔄 Em andamento |
| PBI-09 | RF08 | `IImprimivel` e relatórios | Média | PBI-06 | 4 | 5h | ⏳ Não iniciado |
| PBI-10 | RF09 | `IEletrico` e `CarroEletrico` | Média | PBI-06 | 4 | 5h | ⏳ Não iniciado |
| PBI-11 | RF10 | Menu de console (cadastro e consulta) | Alta | PBI-01 a PBI-10 | 4 | 8h | ⏳ Não iniciado |
| PBI-12 | — | `Veiculo` abstrato, integração e testes finais | Alta | PBI-06 a PBI-10 | 4 | 6h | ⏳ Não iniciado |

## Critérios de priorização

- **Alta:** itens que sustentam os demais (base do modelo, herança) ou que são entrega final (menu de console).
- **Média:** itens que dependem da base, mas não bloqueiam outros itens críticos.
- Itens de uma sprint só começam depois que suas dependências estão concluídas.

## Histórico de evolução

| Data | Alteração | Motivo |
|---|---|---|
| 31/10/2026 | Criação do backlog inicial (PBI-01 a PBI-12) | Formalização do Product Backlog do projeto. |
| 31/10/2026 | PBI-01 marcado como concluído | Encerramento da Sprint 1. |
| 12/11/2026 | Estimativa do PBI-01 ajustada de 6h para 8h | Alinhar à soma das tarefas T1.1 a T1.7 do planejamento da Sprint 1. |
| 12/11/2026 | PBI-01: correções de encapsulamento e validação e ampliação dos testes (PR #12) | Problemas encontrados na revisão do código (avisos CS8618, diária zero aceita, testes insuficientes). |
| 12/11/2026 | Início da Sprint 2: PBI-02 a PBI-05 selecionados | Planejamento da Sprint 2. |
| 14/11/2026 | PBI-04 concluído (`ConfiguradorSistema`, PR #14) | Entrega da T2.3. |
| 14/11/2026 | PBI-03 concluído (`Frota`, PR #15) | Entrega da T2.2. |
| 14/11/2026 | PBI-02 em andamento (`Motorista` e `CNH`) | T2.1 em desenvolvimento. |
| 14/11/2026 | PBI-02 concluído (`Motorista` e `CNH`; correção do número da CNH no PR #18) | Entrega da T2.1. |
| 14/11/2026 | PBI-05 concluído (`Locacao`, PR #19) | Entrega da T2.4. Testes de integração adicionados na T2.6. |
| 14/11/2026 | Encerramento da Sprint 2 | Todos os itens selecionados (PBI-02 a PBI-05) concluídos. O cálculo polimórfico de locação (PBI-07, também RF05) permanece na Sprint 3. |
| 19/11/2026 | Início da Sprint 3: PBI-06 a PBI-08 selecionados | Planejamento da Sprint 3 (integrado na `main` em 22/11, PR #28). |
| 22/11/2026 | PBI-07 e PBI-08 em andamento: `CalcularAluguel` virtual e sobrecarga de `RealizarManutencao` em `Veiculo` | Entrega da T3.1 (branch `feature/veiculo-polimorfismo`). |
| 26/11/2026 | PBI-06 e PBI-07 em andamento: `Carro` sobrescreve `CalcularAluguel` e `Locacao` usa o cálculo polimórfico | Entrega da T3.2 e da T3.4 (branch `feature/carro-aluguel`). |
| 26/11/2026 | Sprint 3 passou do período previsto (até 22/11) | `Patinete` (T3.3), correção do contador (T3.5) e testes da sprint (T3.6) ainda pendentes. PBI-06 a PBI-08 continuam em andamento até essas tarefas serem concluídas. |
| --/--/2026 | _(registrar as próximas alterações)_ | |
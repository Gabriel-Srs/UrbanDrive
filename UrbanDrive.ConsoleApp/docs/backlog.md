# Product Backlog — UrbanDrive

Backlog priorizado, derivado dos requisitos funcionais RF01 a RF10 do enunciado. Estimativas em **horas**.

| ID | Requisito | Item | Prioridade | Dependências | Sprint | Estimativa | Status |
|---|---|---|---|---|---|---|---|
| PBI-01 | RF01 | Entidade `Veiculo` com encapsulamento e validações | Alta | — | 1 | 6h | ✔️ Concluído |
| PBI-02 | RF03 | `Motorista` e `CNH` (composição) | Alta | — | 2 | 6h | ⏳ Não iniciado |
| PBI-03 | RF04 | `Frota` (agregação com `Veiculo`) | Alta | PBI-01 | 2 | 5h | ⏳ Não iniciado |
| PBI-04 | RF07 | `ConfiguradorSistema` (membros estáticos) | Média | PBI-01 | 2 | 4h | ⏳ Não iniciado |
| PBI-05 | RF05 | `Locacao` (associação Motorista–Veiculo) | Média | PBI-01, PBI-02 | 2 | 5h | ⏳ Não iniciado |
| PBI-06 | RF02 | `Carro` e `Patinete` (herança, `sealed`) | Alta | PBI-01 | 3 | 6h | ⏳ Não iniciado |
| PBI-07 | RF05 | `CalcularAluguel(int dias)` polimórfico | Alta | PBI-06 | 3 | 6h | ⏳ Não iniciado |
| PBI-08 | RF06 | Manutenção (sobrecarga de `RealizarManutencao`) | Média | PBI-06 | 3 | 4h | ⏳ Não iniciado |
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
| --/--/2026 | _(registrar as próximas alterações)_ | |
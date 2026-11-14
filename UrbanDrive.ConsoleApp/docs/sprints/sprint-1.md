# Sprint 1 — Modelagem inicial e encapsulamento

## A. Identificação

- **Grupo:** Gabriel Soares e Samuel Elias
- **Integrantes:** Gabriel Soares, Samuel Elias
- **Número da sprint:** 1
- **Período:** 30/10/2026 a 31/10/2026
- **Responsável pelo acompanhamento:** Gabriel Soares

## B. Objetivo da sprint

Estabelecer a estrutura inicial do UrbanDrive em C# e entregar uma primeira versão executável que represente veículos, demonstrando encapsulamento (atributos privados e propriedades públicas) e validação dos dados na classe `Veiculo`.

## C. Itens selecionados do Product Backlog

| ID | Requisito | Prioridade | Justificativa |
|---|---|---|---|
| PBI-01 | RF01 — Gerenciamento de veículos | Alta | `Veiculo` é a base do sistema. Todos os demais itens (herança, frota, locação) dependem dela, e ela não tem dependências. |

## D. Planejamento das tarefas

Unidade das estimativas: **horas**.

| ID | Tarefa | Item relacionado | Responsável | Estimativa |
|---|---|---|---|---|
| T1.1 | Criar o repositório no GitHub Classroom e o projeto C# (.NET Console) | PBI-01 | Gabriel Soares | 1h |
| T1.2 | Implementar a classe `Veiculo` com atributos privados, propriedades públicas e construtor | PBI-01 | Gabriel Soares | 2h |
| T1.3 | Implementar as validações (identificador, placa nula/vazia, modelo e diária negativa) | PBI-01 | Gabriel Soares | 1h |
| T1.4 | Escrever os testes iniciais em `Program.cs` e registrar as evidências | PBI-01 | Samuel Elias | 1h |
| T1.5 | Ampliar os testes (id vazio, modelo vazio, diária negativa, placa com espaços e minúsculas) | PBI-01 | Gabriel Soares | 1h |
| T1.6 | Elaborar o Product Backlog e o quadro no GitHub Projects | PBI-01 | Gabriel Soares | 1h |
| T1.7 | Documentar a Sprint 1 (modelo A–H) e a retrospectiva | PBI-01 | Gabriel Soares e Samuel Elias | 1h |

## E. Critérios de aceitação

**PBI-01 — Entidade `Veiculo`**

1. Um `Veiculo` com identificador, placa, modelo e diária válidos é criado e exibe seus dados corretamente.
2. Criar `Veiculo` com **placa nula, vazia ou só com espaços** lança `ArgumentException` com a mensagem "A placa não pode ser nula ou vazia."
3. Criar `Veiculo` com **placa fora do padrão** (diferente de `ABC1234` ou Mercosul `ABC1D23`) lança `ArgumentException`.
4. Criar `Veiculo` com **identificador ou modelo nulo/vazio** lança `ArgumentException`.
5. Criar `Veiculo` com **diária zero ou negativa**, ou alterar a diária para esses valores, lança exceção (`ArgumentOutOfRangeException`).
6. A placa é armazenada sem espaços, sem hífen e em maiúsculas (ex.: `" abc-1234 "` vira `ABC1234`).
7. Os atributos são privados. `Identificador` e `Placa` só podem ser definidos na criação do objeto (`private set`); `Modelo` e `ValorBaseDiaria` podem ser alterados, sempre passando pela validação.
8. O projeto compila **sem erros e sem avisos** e a aplicação executa com `dotnet run`, exibindo todos os cenários de teste como aprovados.

> **Revisão dos critérios (12/11/2026):** os critérios 3, 5, 6, 7 e 8 foram ajustados ou incluídos depois da revisão do código feita no PR #12. Na versão original, a diária zero era aceita, a placa mantinha o hífen, o formato da placa não era validado e a compilação gerava avisos CS8618.

## F. Entregáveis previstos

- Product Backlog inicial priorizado ([backlog.md](../backlog.md)).
- Este documento de planejamento.
- Código-fonte da classe `Veiculo` e do `Program.cs` de testes.
- Demonstração da criação de objetos e da validação das propriedades.
- Evidências de testes (saída do terminal e captura de tela).
- Registro do resultado e retrospectiva (seções G e H).

## G. Resultado da sprint

| Item ou tarefa | Situação | Evidência | Observações |
|---|---|---|---|
| T1.1 — Repositório e projeto C# | Concluído | Repositório no GitHub | |
| T1.2 — Classe `Veiculo` | Concluído | `Veiculo.cs` | |
| T1.3 — Validações | Concluído | `Veiculo.cs` e testes | |
| T1.4 — Testes iniciais | Concluído | Seção "Evidências de testes" abaixo | Primeira versão com um cenário válido e um inválido (placa vazia). |
| T1.5 — Ampliação dos testes | Concluído com atraso | PR #12, `Program.cs` | Executada por Samuel Elias em 12/11/2026, após o período da sprint: 15 cenários isolados (3 válidos e 12 de validação), todos aprovados. |
| T1.8 — Correções de encapsulamento e validação | Concluído com atraso | PR #12, `Veiculo.cs` e `Carro.cs` | Tarefa não prevista, identificada na revisão do código e executada por Samuel Elias em 12/11/2026: remoção dos avisos CS8618, `private set` em `Identificador` e `Placa`, diária maior que zero, validação do formato da placa, `ToString()` no lugar de `ExibirDetalhes()` e remoção de duplicação em `Carro`. |
| T1.6 — Backlog e GitHub Projects | Concluído com atraso | [backlog.md](../backlog.md) | Entregue depois da entrega principal da sprint. |
| T1.7 — Documentação da sprint | Concluído | Este arquivo | |

**Item adiantado:** a classe `Carro` (prevista para a Sprint 3) já foi codificada como protótipo e será revisada na Sprint 3.

### Evidências de testes

Execução com `dotnet run` após as correções de 12/11/2026 (compilação sem avisos):

![Testes da Sprint 1](../../imagens/teste-sprint1.png)

```text
=== UrbanDrive - Testes da Sprint 1 ===

--- Criação de objetos válidos ---
ID: V001 | Modelo: Fiat Mobi | Placa: ABC1234 | Diária: R$ 90.00
[Carro] ID: V002 | Modelo: Chevrolet Onix | Placa: BRA2E19 | Diária: R$ 120.00 | Portas: 4 | Combustível: Flex

--- Normalização e alteração válida ---
[OK] Placa ' abc-1234 ' é armazenada como 'ABC1234': ok
[OK] Placa Mercosul 'bra2e19' é armazenada como 'BRA2E19': ok
[OK] Reajuste da diária para 95,50 é aceito: ok

--- Validações (devem ser rejeitadas) ---
[OK] Placa vazia: A placa não pode ser nula ou vazia.
[OK] Placa nula: A placa não pode ser nula ou vazia.
[OK] Placa só com espaços: A placa não pode ser nula ou vazia.
[OK] Placa fora do padrão: A placa '12-ABC' é inválida. Use o padrão antigo (ABC-1234) ou Mercosul (ABC1D23).
[OK] Identificador vazio: O identificador não pode ser nulo ou vazio.
[OK] Modelo vazio: O modelo não pode ser nulo ou vazio.
[OK] Diária zero: O valor-base da diária deve ser maior que zero.
[OK] Diária negativa: O valor-base da diária deve ser maior que zero.
[OK] Alterar modelo para vazio: O modelo não pode ser nulo ou vazio.
[OK] Alterar diária para negativa: O valor-base da diária deve ser maior que zero.
[OK] Carro sem portas: A quantidade de portas deve ser maior que zero.
[OK] Carro com placa vazia (validação herdada): A placa não pode ser nula ou vazia.

Resultado: 15 aprovados, 0 reprovados.
```

**Relação entre critérios de aceitação e cenários testados:**

| Critério | Cenário(s) que comprovam |
|---|---|
| 1 | Criação de objetos válidos (`Veiculo` e `Carro`) |
| 2 | Placa vazia, placa nula, placa só com espaços |
| 3 | Placa fora do padrão |
| 4 | Identificador vazio, modelo vazio, alterar modelo para vazio |
| 5 | Diária zero, diária negativa, alterar diária para negativa |
| 6 | Placa `' abc-1234 '` → `ABC1234`; placa Mercosul `bra2e19` → `BRA2E19` |
| 7 | Reajuste da diária aceito; `Identificador` e `Placa` com `private set` em `Veiculo.cs` |
| 8 | Compilação sem avisos e resultado "15 aprovados, 0 reprovados" |

A versão original desta evidência (31/10/2026), com um único cenário inválido e avisos CS8618 na compilação, está preservada no histórico do repositório.

## H. Retrospectiva

**O que funcionou bem?**
A classe `Veiculo` foi implementada com encapsulamento e validações, e a estrutura inicial do repositório ficou pronta para as próximas sprints.

**Quais foram as principais dificuldades?**
- O Product Backlog não foi entregue junto com a sprint e foi formalizado depois.
- O documento de planejamento inicial não seguia o modelo A–H do enunciado e precisou ser refeito.
- A primeira versão dos testes cobria só um cenário inválido e a compilação gerava avisos. Isso só foi percebido na revisão do código, e as correções (T1.5 e T1.8) foram feitas depois do período da sprint.
- Diferença de versão do .NET entre os integrantes: o projeto usa `net9.0` e um dos computadores tinha apenas o .NET 8 e o .NET 10, o que impedia a execução. Resolvido com a instalação do .NET 9 e registrado no README.

**O planejamento foi adequado ao esforço necessário?**
Em parte. As tarefas foram concluídas, mas o planejamento inicial não detalhava responsáveis e estimativas por tarefa. A partir da Sprint 2, cada tarefa terá responsável e estimativa em horas definidos antes do início.

**O que deverá ser melhorado na próxima sprint?**
- Entregar o backlog atualizado e o planejamento antes de iniciar o desenvolvimento.
- Dividir as tarefas entre os dois integrantes desde o planejamento.
- Trabalhar em branches com Pull Request revisado pelo colega, deixando as contribuições individuais identificáveis.
- Criar um projeto de testes automatizados (xUnit) em `tests/`.
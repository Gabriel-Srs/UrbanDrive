# Sprint 4 — Abstração, interfaces e consolidação do sistema

## A. Identificação

- **Grupo:** Gabriel Soares e Samuel Elias
- **Integrantes:** Gabriel Soares, Samuel Elias
- **Número da sprint:** 4
- **Período:** a partir de 27/11/2026 até a entrega final do trabalho
- **Repositório:** https://github.com/Gabriel-Srs/UrbanDrive
- **Quadro de acompanhamento:** GitHub Projects "Product Backlog"
- **Responsável pelo acompanhamento:** Gabriel Soares

## B. Objetivo da sprint

Entregar uma versão integrada e executável do UrbanDrive: `Veiculo` passa a ser abstrata, as entidades selecionadas implementam `IImprimivel` e geram relatório, os veículos elétricos (`Patinete` e o novo `CarroEletrico`) implementam `IEletrico`, e a aplicação de console oferece um menu para cadastrar e consultar veículos e demonstrar os cálculos polimórficos. Os testes de todas as sprints devem continuar passando.

## C. Itens selecionados do Product Backlog

| ID | Requisito | Prioridade | Justificativa |
|---|---|---|---|
| PBI-12 | `Veiculo` abstrato, integração e testes finais | Alta | Muda a base da hierarquia e afeta os testes existentes, por isso é feito primeiro. |
| PBI-09 | RF08 — `IImprimivel` e relatórios | Média | Depende só da hierarquia da Sprint 3. Usado pelo menu para exibir relatórios. |
| PBI-10 | RF09 — `IEletrico` e `CarroEletrico` | Média | Depende de `Carro` e `Patinete` (Sprint 3). Usado pelo menu para recarregar veículos. |
| PBI-11 | RF10 — Menu de console (cadastro e consulta) | Alta | Entrega final do produto. Depende dos três itens acima, por isso é o último a ser implementado. |

**Pendências herdadas da Sprint 3:** nenhuma. Todos os itens da Sprint 3 foram concluídos.

## D. Planejamento das tarefas

Unidade das estimativas: **horas**.

| ID (issue) | Tarefa | Item relacionado | Responsável | Estimativa |
|---|---|---|---|---|
| T4.1 (#37) | `Veiculo` abstrata (construtor `protected`) e troca, nos testes, de `new Veiculo(...)` por subclasses concretas | PBI-12 | Gabriel Soares | 2h |
| T4.2 (#38) | `IImprimivel` com `GerarRelatorio()`, implementada por `Veiculo` (e especializada nas subclasses), `Frota` e `Locacao` | PBI-09 | Gabriel Soares | 3h |
| T4.3 (#39) | `IEletrico` com `Recarregar()`, classe `Bateria` (composição) reaproveitada por `Patinete` e pelo novo `CarroEletrico` | PBI-10 | Gabriel Soares | 4h |
| T4.4 (#40) | Tirar os testes do `Program.cs` para uma classe própria (`Testes/`), deixando o `Program.cs` só com a inicialização | PBI-11 | Gabriel Soares | 1h |
| T4.5 (#41) | Menu de console: cadastrar Carro, Carro Elétrico e Patinete; listar; consultar por placa; simular aluguel; manutenção; recarga; relatórios | PBI-11 | Gabriel Soares | 6h |
| T4.6 (#42) | Testes da Sprint 4 (abstração, interfaces, menu com entrada simulada) e evidências | PBI-09 a 12 | Gabriel Soares | 3h |
| T4.7 (#43) | Diagrama de classes final e backlog atualizado | Todos | Gabriel Soares | 2h |
| T4.8 (#44) | README com instalação e execução e roteiro da apresentação | PBI-12 | Gabriel Soares | 2h |
| T4.9 (#45) | Resultado, retrospectiva e relatório final com a evolução das sprints | Todos | Gabriel Soares | 3h |

**Total estimado:** 26h. O backlog estima 24h para PBI-09 a PBI-12; a diferença vem da reorganização dos testes (T4.4) e do relatório final (T4.9), que não estavam no backlog original.

### Ordem de integração (dependências)

1. `docs/planejamento-sprint-4`: este documento.
2. `feature/veiculo-abstrato` (T4.1): altera a base da hierarquia.
3. `feature/imprimivel` (T4.2) e `feature/eletrico` (T4.3), uma depois da outra, a partir da `main` atualizada.
4. `refactor/testes` (T4.4), antes do menu, para o `Program.cs` ficar livre.
5. `feature/menu-console` (T4.5).
6. `test/sprint-4` (T4.6).
7. `docs/fechamento-sprint-4` (T4.7, T4.8, T4.9).

Toda integração é feita por **Pull Request**, sem PRs empilhados (melhoria da Sprint 3).

### Decisões de projeto

| Decisão | Escolha |
|---|---|
| `CalcularAluguel` em `Veiculo` abstrata | Continua `virtual` com a regra base (diária × dias), porque as subclasses chamam `base.CalcularAluguel(dias)` para não duplicar código. A abstração está na classe, que não pode mais ser instanciada. |
| Entidades com `IImprimivel` | `Veiculo` (toda a hierarquia), `Frota` e `Locacao`: são as entidades que o menu precisa exibir. `GerarRelatorio()` retorna `string`, sem `Console`. |
| Relatório das subclasses | `Veiculo.GerarRelatorio()` monta as linhas comuns e chama o método `protected virtual DetalhesRelatorio()`, que cada subclasse sobrescreve com os próprios dados. Isso evita repetir as linhas comuns. |
| Bateria dos veículos elétricos | Classe `Bateria` (autonomia em km e carga em %) usada por composição em `Patinete` e `CarroEletrico`. As duas classes não têm base comum além de `Veiculo`, então a composição evita duplicar as validações e o `Recarregar()`. |
| `IEletrico` | `AutonomiaBateriaKm`, `CargaBateriaPercentual` e `Recarregar()`, que volta a carga para 100% e retorna a descrição. |
| `CarroEletrico` | Herda de `Carro`, com combustível fixo "Elétrico". Mantém o aluguel do `Carro` (seguro de 5%), mostrando a herança em dois níveis. |
| `Patinete` | Continua `sealed`. Ganha a carga da bateria (parâmetro opcional, padrão 100%) sem mudar o construtor da Sprint 3. |
| Estrutura do projeto | Mantém um único projeto de console, com pastas `Models/`, `Interfaces/`, `Services/` (menu) e `Testes/`, para não mudar a forma de executar. O `Program.cs` só inicializa: `dotnet run` abre o menu e `dotnet run -- --testes` roda os testes. |
| Menu testável | O menu recebe `TextReader` e `TextWriter` no construtor, para os testes simularem a digitação do usuário. Entradas inválidas exibem a mensagem da validação e o menu continua. |

## E. Critérios de aceitação

**PBI-12 — `Veiculo` abstrato e integração**
- `new Veiculo(...)` não compila; `typeof(Veiculo).IsAbstract` é verdadeiro.
- Todas as validações herdadas continuam funcionando nas subclasses.
- Todos os testes das Sprints 1 a 3 continuam passando (com `new Veiculo` trocado por subclasses).

**PBI-09 — `IImprimivel`**
- `Carro`, `CarroEletrico`, `Patinete`, `Frota` e `Locacao` podem ser tratados como `IImprimivel`.
- O relatório de cada veículo contém o tipo, a placa, o modelo, a diária e os dados próprios do tipo (portas, combustível, autonomia, carga).
- O relatório da `Frota` lista todos os veículos; o da `Locacao` mostra motorista, placa, dias e total.
- Uma lista de `IImprimivel` com objetos de tipos diferentes gera o relatório de cada um sem verificar o tipo.

**PBI-10 — `IEletrico` e `CarroEletrico`**
- `Patinete` e `CarroEletrico` implementam `IEletrico`; `Carro` não.
- Carga fora de 0 a 100 e autonomia ≤ 0 lançam `ArgumentOutOfRangeException`.
- `Recarregar()` deixa a carga em 100% e retorna a descrição com a placa.
- `CarroEletrico` com diária R$ 200,00 por 2 dias retorna R$ 420,00 (regra do `Carro`).

**PBI-11 — Menu de console**
- Cadastrar Carro, Carro Elétrico e Patinete na frota a partir de dados digitados.
- Listar os veículos cadastrados e consultar um veículo pela placa.
- Simular o aluguel de um veículo por N dias, mostrando o valor do veículo e o total com a taxa.
- Realizar manutenção com e sem peça, recarregar veículo elétrico e exibir relatórios.
- Dados inválidos (placa, número, opção) mostram a mensagem de erro e não encerram o programa.

**Geral:** o projeto compila sem avisos, `dotnet run -- --testes` termina com 0 reprovados e há demonstração de abstração, interfaces e polimorfismo.

## F. Entregáveis previstos

- Este documento de planejamento.
- Product Backlog atualizado ([backlog.md](../backlog.md)).
- Diagrama de classes final ([diagrama-classes.md](../diagramas/diagrama-classes.md)).
- Código-fonte integrado e aplicação executável com menu.
- Evidências de testes finais (`docs/evidencias/`).
- Documentação de instalação e execução ([README](../../../README.md)).
- Roteiro da apresentação.
- Relatório final com a evolução das sprints, as principais decisões e as dificuldades.

### Melhorias da Sprint 3 aplicadas neste planejamento

| Melhoria registrada na retrospectiva | Como foi aplicada |
|---|---|
| Integrar o planejamento na `main` no primeiro dia | Este documento é o primeiro PR da sprint. |
| Evitar PRs empilhados | Cada branch sai da `main` atualizada depois do merge anterior. |
| Acompanhar o quadro no meio da sprint | Issues de cada tarefa no GitHub Projects, atualizadas a cada PR. |

## G. Resultado da sprint

_A ser preenchido ao final da sprint, de acordo com o resultado real._

## H. Retrospectiva

_A ser preenchida ao final da sprint._

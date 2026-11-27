# Relatório final — UrbanDrive

**Grupo:** Gabriel Soares e Samuel Elias
**Repositório:** https://github.com/Gabriel-Srs/UrbanDrive
**Versão entregue:** `main` em 27/11/2026, com build sem avisos e **132 testes aprovados, 0 reprovados**

## 1. Visão geral

O UrbanDrive é um sistema de console em C# (.NET 9) para uma empresa de mobilidade urbana. Ele cadastra carros, carros elétricos e patinetes, gerencia frotas, motoristas com CNH e locações, calcula o aluguel de cada tipo de veículo, registra manutenções, recarrega os veículos elétricos e gera relatórios. O desenvolvimento foi dividido em quatro sprints, cada uma com um conjunto de conceitos de POO.

## 2. Evolução das sprints

| Sprint | Período | Objetivo | Itens | Resultado | Testes ao final |
|---|---|---|---|---|---|
| 1 | 30/10 a 31/10 | Modelagem inicial e encapsulamento | PBI-01 | Concluída; testes ampliados e correções feitas em 12/11 (PR #12) | 15 aprovados |
| 2 | 12/11 a 14/11 | Relacionamentos e membros estáticos | PBI-02 a 05 | Concluída no prazo | 57 aprovados |
| 3 | 19/11 a 22/11 (concluída em 27/11) | Herança, polimorfismo e especialização | PBI-06 a 08 | Concluída 5 dias após o previsto | 88 aprovados |
| 4 | a partir de 27/11 | Abstração, interfaces e consolidação | PBI-09 a 12 | Concluída | 132 aprovados |

### Sprint 1 — Encapsulamento
- `Veiculo` com atributos privados, propriedades com validação e `private set` em `Identificador` e `Placa`.
- Placa no padrão antigo ou Mercosul, normalizada para maiúsculas e sem hífen.
- A revisão do código encontrou avisos CS8618, diária zero aceita e testes insuficientes; corrigidos no PR #12.

### Sprint 2 — Relacionamentos e membros estáticos
- `Motorista` ◆ `CNH` (composição, construtor `internal`), `Frota` ◇ `Veiculo` (agregação, lista somente leitura), `Locacao` → `Motorista`/`Veiculo` (associação, CNH vencida rejeitada).
- `ConfiguradorSistema` estático, com a taxa de serviço e o contador de veículos.
- Limitação encontrada: o contador contava um `Carro` rejeitado pelas próprias validações. Ficou registrada para a Sprint 3.

### Sprint 3 — Herança e polimorfismo
- `CalcularAluguel(int dias)` `virtual` em `Veiculo`, sobrescrito em `Carro` (+5% de seguro) e `Patinete` (10% de desconto a partir de 3 dias), sempre chamando `base.CalcularAluguel`.
- Sobrecarga de `RealizarManutencao()` e `RealizarManutencao(string pecaSubstituida)`.
- `Patinete` `sealed`; `Locacao` passou a usar o cálculo polimórfico.
- O contador foi corrigido: o registro saiu do construtor de `Veiculo` e foi para o fim dos construtores das subclasses.

### Sprint 4 — Abstração, interfaces e integração
- `Veiculo` abstrata (construtor `protected`, propriedade abstrata `Tipo`).
- `IImprimivel` em todos os veículos, `Frota` e `Locacao`, com o método `DetalhesRelatorio()` sobrescrito por cada subclasse.
- `IEletrico` em `Patinete` e no novo `CarroEletrico` (herda de `Carro`), com a classe `Bateria` por composição.
- Menu de console com 12 operações, separado da regra de negócio (`LocadoraService`) e testado com digitação simulada.

## 3. Principais decisões

| Decisão | Motivo |
|---|---|
| Regras de aluguel definidas pelo grupo (seguro de 5% no carro, desconto de 10% no patinete a partir de 3 dias) | O enunciado não define valores. As regras foram registradas no planejamento da Sprint 3 e usadas como critério dos testes. |
| Subclasses chamam `base.CalcularAluguel(dias)` | Reaproveita a regra base e a validação de `dias`, sem duplicar código. |
| `CalcularAluguel` continua `virtual` com `Veiculo` abstrata | As subclasses dependem da implementação base; a abstração está na classe e na propriedade `Tipo`. |
| Contador registrado no fim do construtor de cada subclasse | Em C#, o construtor da base executa antes do da subclasse. Registrar na base contaria objetos rejeitados depois. |
| `CarroEletrico` valida a bateria antes de chamar o construtor de `Carro` | O construtor de `Carro` registra o veículo; a bateria inválida precisa ser rejeitada antes disso. |
| `Bateria` por composição | `Patinete` e `CarroEletrico` não têm base comum além de `Veiculo`; a composição evita duplicar validações e a recarga. |
| `GerarRelatorio()` em `Veiculo` + `DetalhesRelatorio()` sobrescrito | Cada subclasse só informa os próprios dados; as linhas comuns ficam em um único lugar. |
| `LocadoraService` separado do `MenuConsole` | A regra de negócio não fica na aplicação de console, que só lê e exibe. |
| Menu com `TextReader`/`TextWriter` injetados | Permite testar o menu com digitação simulada. |
| Testes em uma classe própria, executados com `--testes` | Sem dependência de framework de testes; o `Program.cs` fica só com a inicialização. |
| Um único projeto com pastas por responsabilidade | Mantém a forma de executar das sprints anteriores e ainda separa `Models`, `Interfaces`, `Services` e `Testes`. |
| Cultura pt-BR fixada no `Program` | Valores em reais e datas iguais em qualquer máquina. |

## 4. Dificuldades encontradas

- **Planejamento e prazos:** na Sprint 1, o backlog e o planejamento no modelo A–H foram entregues depois. Na Sprint 3, o planejamento só entrou na `main` no último dia do período, e a sprint terminou 5 dias depois do previsto.
- **Versão do .NET:** um dos computadores tinha só o .NET 8 e o .NET 10, e o projeto usa o .NET 9. Resolvido com a instalação e registrado no README.
- **Integração:** um merge fora do fluxo de Pull Request levou marcas de conflito para a `main` na Sprint 2 (corrigido no PR #17). Na Sprint 3, PRs empilhados fizeram o GitHub fechar o PR #30 quando a branch-base foi apagada, e foi preciso abrir o #32.
- **Ordem dos construtores com herança:** o contador estático contava objetos rejeitados pelas subclasses. Isso exigiu mudar onde o registro é feito (Sprint 3) e, no `CarroEletrico`, validar antes de chamar a base (Sprint 4).
- **Distribuição das tarefas:** na Sprint 3, três tarefas (T3.3, T3.5 e T3.6) foram redistribuídas entre os integrantes no fim da sprint, por falta de disponibilidade.

## 5. Atendimento aos requisitos

| Requisito | Onde está | Evidência |
|---|---|---|
| RF01 — Veículos | `Models/Veiculo.cs` | `TestarSprint1Veiculo` |
| RF02 — Especialização | `Carro`, `CarroEletrico`, `Patinete` (`sealed`) | `TestarSprint3Patinete`, `TestarSprint4Abstracao` |
| RF03 — Motoristas | `Motorista`, `CNH` | `TestarSprint2MotoristaCnh` |
| RF04 — Frotas | `Frota` | `TestarSprint2Frota` |
| RF05 — Locação | `Locacao`, `CalcularAluguel` | `TestarSprint2Locacao`, `TestarSprint3Aluguel` |
| RF06 — Manutenção | `RealizarManutencao` (sobrecarga) | `TestarSprint3Manutencao` |
| RF07 — Configuração | `ConfiguradorSistema` | `TestarSprint2ConfiguradorSistema` |
| RF08 — Relatórios | `IImprimivel` | `TestarSprint4Imprimivel` |
| RF09 — Elétricos | `IEletrico`, `Bateria`, `CarroEletrico` | `TestarSprint4Eletrico` |
| RF10 — Consulta e apresentação | `MenuConsole`, `LocadoraService` | `TestarSprint4Menu` |

A saída completa dos testes finais está em [`evidencias/sprint-4-testes-finais.txt`](evidencias/sprint-4-testes-finais.txt).

## 6. Lições aprendidas

- Integrar o planejamento e as tarefas com dependência no início da sprint reduz o risco de atraso.
- Testes escritos a partir dos critérios de aceitação encontraram erros reais (o número da CNH não gravado e o contador com objetos rejeitados).
- Separar a regra de negócio da interface de console facilitou testar o menu e evitou concentrar lógica no `Program.cs`.
- Pull Requests pequenos, um por tarefa e saindo sempre da `main` atualizada, deixam o histórico mais claro e evitam conflitos.

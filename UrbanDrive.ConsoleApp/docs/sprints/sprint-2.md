# Sprint 2 — Relacionamentos entre objetos e membros estáticos

## A. Identificação

- **Grupo:** Gabriel Soares e Samuel Elias
- **Integrantes:** Gabriel Soares, Samuel Elias
- **Número da sprint:** 2
- **Período:** 12/11/2026 a 14/11/2026
- **Repositório:** https://github.com/Gabriel-Srs/UrbanDrive
- **Quadro de acompanhamento:** GitHub Projects "Product Backlog"
- **Responsável pelo acompanhamento:** Samuel Elias

## B. Objetivo da sprint

Ampliar o modelo para que os componentes do sistema interajam entre si: `Motorista` contém `CNH` (composição), `Frota` gerencia uma coleção de `Veiculo` (agregação), `Locacao` relaciona `Motorista` e `Veiculo` (associação) e `ConfiguradorSistema` mantém informações compartilhadas por meio de membros estáticos.

## C. Itens selecionados do Product Backlog

| ID | Requisito | Prioridade | Justificativa |
|---|---|---|---|
| PBI-02 | RF03 — `Motorista` e `CNH` | Alta | Não tem dependências e é pré-requisito da locação. Exige composição. |
| PBI-03 | RF04 — `Frota` | Alta | Depende apenas de `Veiculo` (Sprint 1). Exige agregação. |
| PBI-04 | RF07 — `ConfiguradorSistema` | Média | Introduz membros estáticos e fornece a taxa usada no cálculo de locação. |
| PBI-05 | RF05 — `Locacao` | Média | Depende de PBI-01 e PBI-02. Demonstra a associação entre objetos. |

## D. Planejamento das tarefas

Unidade das estimativas: **horas**.

| ID | Tarefa | Item relacionado | Responsável | Estimativa |
|---|---|---|---|---|
| T2.1 | Implementar `CNH` (validações) e `Motorista` com composição | PBI-02 | Gabriel Soares | 4h |
| T2.2 | Implementar `Frota` com lista encapsulada e rejeição de placa duplicada | PBI-03 | Samuel Elias | 4h |
| T2.3 | Implementar `ConfiguradorSistema` estático e contador de veículos | PBI-04 | Samuel Elias | 3h |
| T2.4 | Implementar `Locacao` (associação Motorista–Veiculo) | PBI-05 | Gabriel Soares | 4h |
| T2.5 | Atualizar o diagrama de classes | PBI-02 a PBI-05 | Gabriel Soares | 2h |
| T2.6 | Escrever testes de integração e dos membros estáticos | PBI-02 a PBI-05 | Samuel Elias | 4h |
| T2.7 | Atualizar backlog, documentar a sprint e registrar a retrospectiva | Todos | Gabriel Soares e Samuel Elias | 2h |

## E. Critérios de aceitação

**PBI-02 — Motorista e CNH (composição)**
- Criar `Motorista` com dados de CNH inválidos (número diferente de 11 dígitos, categoria vazia) lança `ArgumentException`.
- Não é possível criar uma `CNH` fora de `Motorista` (construtor `internal`).
- A `CNH` é criada dentro do construtor de `Motorista`.

**PBI-03 — Frota (agregação)**
- Adicionar o mesmo veículo (mesma placa) duas vezes é rejeitado.
- Remover um veículo da `Frota` **não** o destrói: o objeto continua existindo e utilizável.
- A lista de veículos não pode ser alterada diretamente de fora da classe.

**PBI-04 — ConfiguradorSistema (estático)**
- `TotalVeiculosCadastrados` aumenta em 1 a cada `new Veiculo(...)`.
- `TaxaServico` fora do intervalo de 0 a 1 lança `ArgumentException`.
- A taxa é única e compartilhada por todo o sistema.

**PBI-05 — Locacao (associação)**
- Criar locação com motorista de CNH vencida é rejeitado.
- Criar locação com dias menores ou iguais a zero, ou com motorista/veículo nulo, é rejeitado.
- O total calculado usa a diária do veículo, a quantidade de dias e a taxa do `ConfiguradorSistema`.

**Geral:** o projeto compila, a aplicação executa e há demonstração da interação entre os objetos.

## F. Entregáveis previstos

- Este documento de planejamento.
- Product Backlog atualizado ([backlog.md](../backlog.md)).
- Diagrama de classes atualizado (abaixo).
- Código-fonte com `CNH`, `Motorista`, `Frota`, `ConfiguradorSistema` e `Locacao`.
- Demonstração da interação entre os objetos.
- Evidências de testes dos relacionamentos e dos membros estáticos.
- Registro das atividades concluídas e das pendências (seções G e H).

### Diagrama de classes (previsto)

```mermaid
classDiagram
    class Veiculo
    class Carro
    class Motorista
    class CNH
    class Frota
    class Locacao
    class ConfiguradorSistema {
        <<static>>
    }
    Veiculo <|-- Carro
    Motorista *-- CNH : composição
    Frota o-- Veiculo : agregação
    Locacao --> Motorista : associação
    Locacao --> Veiculo : associação
    Veiculo ..> ConfiguradorSistema : usa
```

O diagrama atualizado com as classes, atributos, métodos e relações implementados está em [docs/diagramas/diagrama-classes.md](../diagramas/diagrama-classes.md).

## G. Resultado da sprint

| Item ou tarefa | Situação | Evidência | Observações |
|---|---|---|---|
| T2.1 — `CNH` e `Motorista` (composição) | Em andamento | Branch `feature/motorista-cnh` | _Atualizar com o resultado final._ |
| T2.2 — `Frota` (agregação) | Concluído | PR #15, `Models/Frota.cs`, `TestarSprint2Frota()` | 13 cenários de teste aprovados. Lista exposta como `IReadOnlyList`; placa repetida e veículo nulo são rejeitados; veículo removido continua existindo e pode entrar em outra frota. |
| T2.3 — `ConfiguradorSistema` (membros estáticos) | Concluído | PR #14, `Models/ConfiguradorSistema.cs`, `TestarSprint2ConfiguradorSistema()` | 8 cenários de teste aprovados. Taxa padrão de 10%, aceita valores de 0 a 1. **Limitação conhecida:** um `Carro` rejeitado pelas próprias validações (ex.: 0 portas) é contado, porque o construtor de `Veiculo` executa antes do de `Carro`. Será tratada na Sprint 3, junto com a revisão da hierarquia. |
| T2.4 — `Locacao` (associação) | Não iniciado | — | _Atualizar com o resultado final._ |
| T2.5 — Diagrama de classes | Concluído | [`docs/diagramas/diagrama-classes.md`](../diagramas/diagrama-classes.md) | Tarefa redistribuída entre os integrantes e executada por Samuel Elias. Diagrama em Mermaid com atributos, métodos, visibilidade, membros estáticos e as relações de herança, agregação, composição, associação e dependência. `Locacao` representada conforme o planejamento, a revisar quando implementada. |
| T2.6 — Testes de integração e dos membros estáticos | Parcialmente concluído | `Program.cs` | Testes dos membros estáticos e da agregação concluídos (T2.2 e T2.3). Testes de integração com `Motorista` e `Locacao` dependem da T2.1 e da T2.4. |
| T2.7 — Backlog, documentação e retrospectiva | Em andamento | Este documento | |

**Mudança na organização dos testes:** o `Program.cs` passou a ter um método de testes por funcionalidade (`TestarSprint1Veiculo`, `TestarSprint2ConfiguradorSistema`, `TestarSprint2Frota`), para que os integrantes possam adicionar testes em paralelo sem conflitos.

### Evidências

**Quadro do GitHub Projects no início da sprint:**

![Quadro no início da Sprint 2](../../imagens/sprint2-quadro-inicio.png)

<!-- Adicionar o print do quadro no fim da sprint: ../../imagens/sprint2-quadro-fim.png -->

**Execução com `dotnet run` (compilação sem avisos):**

```text
##### Sprint 2: ConfiguradorSistema #####

--- Contador de veículos (estático) ---
Veículos cadastrados até agora: 4
[OK] Criar 2 veículos válidos (Veiculo e Carro) aumenta o contador em 2: ok
[OK] Criar veículo com placa vazia é rejeitado: A placa não pode ser nula ou vazia.
[OK] Veículo rejeitado não aumenta o contador: ok
Veículos cadastrados agora: 6

--- Taxa de serviço (estática e compartilhada) ---
[OK] Taxa começa com o valor padrão de 10%: ok
[OK] Alterar a taxa para 15% é aceito: ok
[OK] Taxa negativa (-0,10) é rejeitada: A taxa de serviço deve estar entre 0 e 1 (ex.: 0,10 para 10%).
[OK] Taxa acima de 100% (1,50) é rejeitada: A taxa de serviço deve estar entre 0 e 1 (ex.: 0,10 para 10%).
[OK] Após as tentativas inválidas, a taxa continua em 15%: ok
Taxa restaurada para 10%.

##### Sprint 2: Frota #####

--- Criação e adição de veículos ---
Frota: Frota Centro | Veículos: 2
  ID: F001 | Modelo: VW Gol | Placa: JKL2345 | Diária: R$ 95.00
  [Carro] ID: F002 | Modelo: Fiat Argo | Placa: MNO6P78 | Diária: R$ 115.00 | Portas: 4 | Combustível: Flex
[OK] Frota com 2 veículos adicionados: ok
[OK] Busca por placa 'jkl2345' encontra o Gol: ok

--- Validações (devem ser rejeitadas) ---
[OK] Frota com nome vazio: O nome da frota não pode ser nulo ou vazio.
[OK] Adicionar veículo nulo: O veículo não pode ser nulo.
[OK] Adicionar o mesmo veículo duas vezes: Já existe um veículo com a placa JKL2345 na frota Frota Centro.
[OK] Adicionar outro veículo com placa repetida: Já existe um veículo com a placa JKL2345 na frota Frota Centro.
[OK] Após as tentativas inválidas, a frota continua com 2 veículos: ok
[OK] A lista exposta é somente leitura: ok

--- Remoção (agregação) ---
[OK] Remover o Gol da frota retorna true: ok
[OK] Frota fica com 1 veículo: ok
[OK] Remover placa inexistente retorna false: ok
[OK] O Gol continua existindo e utilizável fora da frota: ok
[OK] O Gol removido pode ser adicionado a outra frota: ok
  ID: F001 | Modelo: VW Gol | Placa: JKL2345 | Diária: R$ 99.00

Resultado: 36 aprovados, 0 reprovados.
```

<!-- Adicionar a saída dos testes de Motorista/CNH e Locacao quando forem integrados, e atualizar o total de aprovados -->

**Relação entre critérios de aceitação e cenários testados (itens concluídos):**

| Critério | Cenário(s) que comprovam |
|---|---|
| PBI-03: placa repetida é rejeitada | Adicionar o mesmo veículo duas vezes; adicionar outro veículo com placa repetida |
| PBI-03: remover não destrói o veículo | O Gol continua existindo e utilizável fora da frota; o Gol removido pode ser adicionado a outra frota |
| PBI-03: lista não alterável de fora | A lista exposta é somente leitura |
| PBI-04: contador aumenta a cada veículo criado | Criar 2 veículos válidos aumenta o contador em 2; veículo rejeitado não aumenta o contador |
| PBI-04: taxa fora de 0 a 1 é rejeitada | Taxa negativa; taxa acima de 100% |
| PBI-04: taxa única e compartilhada | `TaxaServico` é membro de uma classe estática; alteração para 15% é refletida em todo o sistema |

## H. Retrospectiva

_A ser preenchida ao final da sprint._

- **O que funcionou bem?**
- **Quais foram as principais dificuldades?**
- **O planejamento foi adequado ao esforço necessário?**
- **O que deverá ser melhorado na próxima sprint?**
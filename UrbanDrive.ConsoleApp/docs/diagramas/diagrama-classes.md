# Diagrama de classes — UrbanDrive

Versão atual: **final (Sprint 4)**: abstração, interfaces e consolidação do sistema.

O histórico deste arquivo no repositório mostra a evolução do diagrama a cada sprint.

## Modelo de domínio

```mermaid
classDiagram
    direction TB

    class IImprimivel {
        <<interface>>
        +GerarRelatorio() string
    }

    class IEletrico {
        <<interface>>
        +int AutonomiaBateriaKm
        +int CargaBateriaPercentual
        +Recarregar() string
    }

    class Veiculo {
        <<abstract>>
        -Regex FormatoPlaca$
        -string identificador
        -string placa
        -string modelo
        -decimal valorBaseDiaria
        #Veiculo(identificador, placa, modelo, valorBaseDiaria)
        +string Tipo*
        +string Identificador
        +string Placa
        +string Modelo
        +decimal ValorBaseDiaria
        +CalcularAluguel(int dias) decimal
        +RealizarManutencao() string
        +RealizarManutencao(string pecaSubstituida) string
        +GerarRelatorio() string
        #DetalhesRelatorio() IEnumerable~string~
        +ToString() string
    }

    class Carro {
        +decimal AcrescimoSeguro$
        -int quantidadePortas
        -string tipoCombustivel
        +Carro(identificador, placa, modelo, valorBaseDiaria, quantidadePortas, tipoCombustivel)
        +string Tipo
        +int QuantidadePortas
        +string TipoCombustivel
        +CalcularAluguel(int dias) decimal
        #DetalhesRelatorio() IEnumerable~string~
        +ToString() string
    }

    class CarroEletrico {
        +string CombustivelEletrico$
        -Bateria bateria
        +CarroEletrico(identificador, placa, modelo, valorBaseDiaria, quantidadePortas, autonomiaBateriaKm, cargaBateriaPercentual)
        -CarroEletrico(identificador, placa, modelo, valorBaseDiaria, quantidadePortas, bateria)
        +string Tipo
        +int AutonomiaBateriaKm
        +int CargaBateriaPercentual
        +Recarregar() string
        #DetalhesRelatorio() IEnumerable~string~
        +ToString() string
    }

    class Patinete {
        <<sealed>>
        +int DiasMinimosDesconto$
        +decimal DescontoLongoPeriodo$
        -Bateria bateria
        +Patinete(identificador, placa, modelo, valorBaseDiaria, autonomiaBateriaKm, cargaBateriaPercentual)
        +string Tipo
        +int AutonomiaBateriaKm
        +int CargaBateriaPercentual
        +CalcularAluguel(int dias) decimal
        +Recarregar() string
        #DetalhesRelatorio() IEnumerable~string~
        +ToString() string
    }

    class Bateria {
        +int CargaMaxima$
        -int autonomiaKm
        -int cargaPercentual
        +Bateria(autonomiaKm, cargaPercentual)
        +int AutonomiaKm
        +int CargaPercentual
        +Recarregar(descricaoVeiculo) string
        +ToString() string
    }

    class ConfiguradorSistema {
        <<static>>
        +decimal TaxaServicoPadrao$
        -decimal taxaServico$
        -int totalVeiculosCadastrados$
        +decimal TaxaServico$
        +int TotalVeiculosCadastrados$
        +AplicarTaxaServico(valor)$ decimal
        ~RegistrarVeiculo()$
    }

    class Frota {
        -string nome
        -List~Veiculo~ veiculos
        +Frota(nome)
        +string Nome
        +IReadOnlyList~Veiculo~ Veiculos
        +int Quantidade
        +AdicionarVeiculo(veiculo)
        +RemoverVeiculo(placa) bool
        +BuscarPorPlaca(placa) Veiculo
        +GerarRelatorio() string
        +ToString() string
    }

    class Motorista {
        -string nome
        +Motorista(nome, numeroCnh, categoriaCnh, dataValidadeCnh)
        +string Nome
        +CNH CNH
    }

    class CNH {
        -string numero
        -string categoria
        ~CNH(numero, categoria, dataValidade)
        +string Numero
        +string Categoria
        +DateTime DataValidade
        +EstaVencida(data) bool
    }

    class Locacao {
        -Motorista motorista
        -Veiculo veiculo
        -int dias
        -DateTime dataRetirada
        +Locacao(motorista, veiculo, dias, dataRetirada)
        +Motorista Motorista
        +Veiculo Veiculo
        +int Dias
        +DateTime DataRetirada
        -ValidarCnh()
        +CalcularValorTotal() decimal
        +GerarRelatorio() string
        +ToString() string
    }

    IImprimivel <|.. Veiculo : implementa
    IImprimivel <|.. Frota : implementa
    IImprimivel <|.. Locacao : implementa
    IEletrico <|.. CarroEletrico : implementa
    IEletrico <|.. Patinete : implementa
    Veiculo <|-- Carro : herança
    Veiculo <|-- Patinete : herança
    Carro <|-- CarroEletrico : herança
    CarroEletrico "1" *-- "1" Bateria : composição
    Patinete "1" *-- "1" Bateria : composição
    Frota "1" o-- "0..*" Veiculo : agregação
    Motorista "1" *-- "1" CNH : composição
    Locacao "0..*" --> "1" Motorista : associação
    Locacao "0..*" --> "1" Veiculo : associação
    Carro ..> ConfiguradorSistema : registra no contador
    Patinete ..> ConfiguradorSistema : registra no contador
    Locacao ..> ConfiguradorSistema : usa a taxa
    Locacao ..> CNH : valida a validade
```

## Aplicação de console

```mermaid
classDiagram
    direction LR

    class Program {
        -Main(args)$ int
    }

    class MenuConsole {
        -LocadoraService locadora
        -TextReader entrada
        -TextWriter saida
        +MenuConsole(locadora, entrada, saida)
        +Executar()
    }

    class LocadoraService {
        -Frota frota
        -List~Locacao~ locacoes
        +LocadoraService(frota)
        +Frota Frota
        +IReadOnlyList~Locacao~ Locacoes
        +CadastrarVeiculo(veiculo)
        +ConsultarVeiculo(placa) Veiculo
        +RemoverVeiculo(placa)
        +SimularAluguel(dias) IReadOnlyList~SimulacaoAluguel~
        +RegistrarLocacao(motorista, placa, dias, dataRetirada) Locacao
        +RealizarManutencao(placa, pecaSubstituida) string
        +Recarregar(placa) string
        +ListarEletricos() IReadOnlyList~IEletrico~
        +ListarImprimiveis() IReadOnlyList~IImprimivel~
    }

    class SimulacaoAluguel {
        <<record>>
        +Veiculo Veiculo
        +int Dias
        +decimal Aluguel
        +decimal TotalComTaxa
    }

    class DadosDemonstracao {
        <<static>>
        +CriarFrota()$ Frota
    }

    class TestesUrbanDrive {
        <<static>>
        +Executar()$ int
    }

    Program ..> MenuConsole : dotnet run
    Program ..> TestesUrbanDrive : dotnet run -- --testes
    Program ..> DadosDemonstracao : frota inicial
    MenuConsole --> LocadoraService : usa
    LocadoraService --> Frota : gerencia
    LocadoraService "1" o-- "0..*" Locacao : registra
    LocadoraService ..> SimulacaoAluguel : cria
```

## Legenda

| Símbolo | Significado |
|---|---|
| `+` / `-` / `#` / `~` | Público / privado / protegido (`protected`) / interno (`internal`) |
| `$` | Membro estático |
| `*` (depois do nome) | Membro abstrato |
| `<<abstract>>`, `<<interface>>`, `<<sealed>>`, `<<static>>` | Classe abstrata, interface, classe selada, classe estática |
| `<|--` | Herança |
| `<|..` | Implementação de interface |
| `o--` (losango vazio) | Agregação: a parte existe independentemente do todo |
| `*--` (losango cheio) | Composição: a parte é criada pelo todo e não existe sem ele |
| `-->` | Associação |
| `..>` | Dependência (uso) |

## Relacionamentos

- **Abstração — `Veiculo`:** classe abstrata, com construtor `protected` e a propriedade abstrata `Tipo`, que cada subclasse implementa. Não pode ser instanciada; concentra os dados, as validações e as regras comuns.
- **Herança — `Carro` e `Patinete` → `Veiculo`; `CarroEletrico` → `Carro`:** as subclasses reaproveitam os dados e as validações da base e repassam os valores comuns pelo construtor com `base`. `Patinete` é `sealed`. `CarroEletrico` mostra a herança em dois níveis e herda o aluguel do `Carro`.
- **Sobrescrita e polimorfismo — `CalcularAluguel(int dias)`:** `virtual` em `Veiculo` (diária × dias). `Carro` (+5% de seguro) e `Patinete` (10% de desconto a partir de 3 dias) sobrescrevem chamando `base.CalcularAluguel(dias)`. `Locacao` e `LocadoraService.SimularAluguel` chamam o método sem saber o tipo real do veículo.
- **Sobrecarga — `RealizarManutencao`:** duas versões em `Veiculo`, sem parâmetro e com `string pecaSubstituida`.
- **Interface `IImprimivel`:** implementada por `Veiculo` (e, por herança, por todos os veículos), `Frota` e `Locacao`. Em `Veiculo`, `GerarRelatorio()` monta as linhas comuns e chama o método `protected virtual DetalhesRelatorio()`, que cada subclasse sobrescreve.
- **Interface `IEletrico`:** implementada por `CarroEletrico` e `Patinete`. O menu identifica os veículos elétricos com `is IEletrico`.
- **Composição — `CarroEletrico`/`Patinete` ◆ `Bateria`:** a bateria é criada pelo veículo e concentra as validações (autonomia > 0, carga de 0 a 100%) e a recarga, sem duplicar código entre as duas classes.
- **Agregação — `Frota` ◇ `Veiculo`:** os veículos são criados fora da frota e apenas adicionados a ela. Ao serem removidos, continuam existindo.
- **Composição — `Motorista` ◆ `CNH`:** a `CNH` é criada dentro do construtor de `Motorista` e não pode ser trocada por fora. O construtor da `CNH` é `internal`.
- **Associação — `Locacao` → `Motorista` e `Veiculo`:** a locação referencia objetos que existem independentemente dela e rejeita motorista com CNH vencida na data de retirada.
- **Membros estáticos — `ConfiguradorSistema`:** taxa de serviço e contador de veículos compartilhados. `Carro` e `Patinete` registram o veículo no fim do construtor, depois de todas as validações; `CarroEletrico` valida a bateria antes de chamar o construtor de `Carro`, para não ser contado se for inválido.
- **Aplicação — `Program` → `MenuConsole` → `LocadoraService`:** o `Program` só inicializa; o menu lê e exibe; o serviço executa as operações sobre a frota e as locações. Assim a regra de negócio não fica na aplicação de console.

## Situação das classes

| Classe | Sprint | Situação |
|---|---|---|
| `Veiculo` (abstrata) | 1 (polimorfismo na 3, abstrata na 4) | Implementada |
| `Carro` | 1 (sobrescrita na 3) | Implementada |
| `Patinete` (`sealed`) | 3 (`IEletrico` na 4) | Implementada |
| `CarroEletrico` | 4 | Implementada |
| `Bateria` | 4 | Implementada |
| `IImprimivel` | 4 | Implementada |
| `IEletrico` | 4 | Implementada |
| `ConfiguradorSistema` | 2 | Implementada |
| `Frota` | 2 (`IImprimivel` na 4) | Implementada |
| `Motorista` | 2 | Implementada |
| `CNH` | 2 | Implementada |
| `Locacao` | 2 (`IImprimivel` na 4) | Implementada |
| `LocadoraService`, `MenuConsole`, `DadosDemonstracao` | 4 | Implementadas |
| `TestesUrbanDrive` | 4 (testes das Sprints 1 a 4) | Implementada |

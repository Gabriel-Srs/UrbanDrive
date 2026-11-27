using UrbanDrive.ConsoleApp.Interfaces;

namespace UrbanDrive.ConsoleApp.Models
{
    // Herança em dois níveis (Veiculo -> Carro -> CarroEletrico) e veículo elétrico (RF09)
    public class CarroEletrico : Carro, IEletrico
    {
        public const string CombustivelEletrico = "Elétrico";

        // Atributos
        private readonly Bateria bateria;

        // Construtor
        // A bateria é validada antes de chamar a base: o construtor de Carro registra
        // o veículo no contador, então um CarroEletrico inválido não pode chegar até lá
        public CarroEletrico(string identificador, string placa, string modelo, decimal valorBaseDiaria,
                             int quantidadePortas, int autonomiaBateriaKm,
                             int cargaBateriaPercentual = Bateria.CargaMaxima)
            : this(identificador, placa, modelo, valorBaseDiaria, quantidadePortas,
                   new Bateria(autonomiaBateriaKm, cargaBateriaPercentual))
        {
        }

        private CarroEletrico(string identificador, string placa, string modelo, decimal valorBaseDiaria,
                              int quantidadePortas, Bateria bateria)
            : base(identificador, placa, modelo, valorBaseDiaria, quantidadePortas, CombustivelEletrico)
        {
            this.bateria = bateria;
        }

        // Propriedades (IEletrico)
        public int AutonomiaBateriaKm
        {
            get { return bateria.AutonomiaKm; }
            set { bateria.AutonomiaKm = value; }
        }

        public int CargaBateriaPercentual
        {
            get { return bateria.CargaPercentual; }
        }

        // Tipo (membro abstrato de Veiculo, redefinido sobre Carro)
        public override string Tipo
        {
            get { return "Carro elétrico"; }
        }

        // Métodos
        // CalcularAluguel é herdado de Carro (seguro de 5%), sem reescrever a regra

        // IEletrico
        public string Recarregar()
        {
            return bateria.Recarregar($"{Tipo} {Placa}");
        }

        // Dados próprios no relatório: os do Carro e os da bateria
        protected override IEnumerable<string> DetalhesRelatorio()
        {
            foreach (string linha in base.DetalhesRelatorio())
                yield return linha;

            yield return $"Bateria: {bateria}";
        }

        // Exibição
        public override string ToString()
        {
            return $"{base.ToString()} | {bateria}";
        }
    }
}

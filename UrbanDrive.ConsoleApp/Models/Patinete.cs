using UrbanDrive.ConsoleApp.Interfaces;

namespace UrbanDrive.ConsoleApp.Models
{
    // sealed: nenhuma classe pode herdar de Patinete (RF02); elétrico (RF09)
    public sealed class Patinete : Veiculo, IEletrico
    {
        // Desconto de 10% a partir de 3 dias
        public const int DiasMinimosDesconto = 3;
        public const decimal DescontoLongoPeriodo = 0.10m;

        // Atributos
        private readonly Bateria bateria;

        // Construtor
        public Patinete(string identificador, string placa, string modelo, decimal valorBaseDiaria,
                        int autonomiaBateriaKm, int cargaBateriaPercentual = Bateria.CargaMaxima)
            : base(identificador, placa, modelo, valorBaseDiaria)
        {
            bateria = new Bateria(autonomiaBateriaKm, cargaBateriaPercentual);

            // Contador de veículos: só depois de todas as validações
            ConfiguradorSistema.RegistrarVeiculo();
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

        // Tipo (membro abstrato de Veiculo)
        public override string Tipo
        {
            get { return "Patinete"; }
        }

        // Métodos
        // Reaproveita a regra e a validação de Veiculo e aplica o desconto de longo período
        public override decimal CalcularAluguel(int dias)
        {
            decimal valor = base.CalcularAluguel(dias);

            if (dias >= DiasMinimosDesconto)
                valor *= 1 - DescontoLongoPeriodo;

            return valor;
        }

        // IEletrico
        public string Recarregar()
        {
            return bateria.Recarregar($"{Tipo} {Placa}");
        }

        // Dados próprios no relatório (IImprimivel)
        protected override IEnumerable<string> DetalhesRelatorio()
        {
            yield return $"Bateria: {bateria}";
            yield return $"Desconto: {DescontoLongoPeriodo:P0} a partir de {DiasMinimosDesconto} dias";
        }

        // Exibição
        public override string ToString()
        {
            return $"[{Tipo}] {base.ToString()} | {bateria}";
        }
    }
}

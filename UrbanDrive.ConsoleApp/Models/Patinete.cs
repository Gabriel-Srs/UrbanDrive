namespace UrbanDrive.ConsoleApp.Models
{
    // sealed: nenhuma classe pode herdar de Patinete (RF02)
    public sealed class Patinete : Veiculo
    {
        // Desconto de 10% a partir de 3 dias
        public const int DiasMinimosDesconto = 3;
        public const decimal DescontoLongoPeriodo = 0.10m;

        // Atributos
        private int autonomiaBateriaKm;

        // Construtor
        public Patinete(string identificador, string placa, string modelo, decimal valorBaseDiaria,
                        int autonomiaBateriaKm)
            : base(identificador, placa, modelo, valorBaseDiaria)
        {
            AutonomiaBateriaKm = autonomiaBateriaKm;

            // Contador de veículos: só depois de todas as validações
            ConfiguradorSistema.RegistrarVeiculo();
        }

        // Propriedades
        public int AutonomiaBateriaKm
        {
            get { return autonomiaBateriaKm; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(AutonomiaBateriaKm), value,
                        "A autonomia da bateria deve ser maior que zero (km).");

                autonomiaBateriaKm = value;
            }
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

        // Exibição
        public override string ToString()
        {
            return $"[Patinete] {base.ToString()} | Autonomia: {AutonomiaBateriaKm} km";
        }
    }
}

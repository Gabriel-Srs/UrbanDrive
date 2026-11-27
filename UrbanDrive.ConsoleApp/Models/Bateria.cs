namespace UrbanDrive.ConsoleApp.Models
{
    // Composição: usada por Patinete e CarroEletrico, evita duplicar validações e a recarga
    public class Bateria
    {
        public const int CargaMaxima = 100;

        // Atributos
        private int autonomiaKm;
        private int cargaPercentual;

        // Construtor
        public Bateria(int autonomiaKm, int cargaPercentual = CargaMaxima)
        {
            AutonomiaKm = autonomiaKm;
            CargaPercentual = cargaPercentual;
        }

        // Propriedades
        public int AutonomiaKm
        {
            get { return autonomiaKm; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(AutonomiaKm), value,
                        "A autonomia da bateria deve ser maior que zero (km).");

                autonomiaKm = value;
            }
        }

        public int CargaPercentual
        {
            get { return cargaPercentual; }
            private set
            {
                if (value < 0 || value > CargaMaxima)
                    throw new ArgumentOutOfRangeException(nameof(CargaPercentual), value,
                        "A carga da bateria deve estar entre 0 e 100%.");

                cargaPercentual = value;
            }
        }

        // Métodos
        public string Recarregar(string descricaoVeiculo)
        {
            int cargaAnterior = CargaPercentual;
            CargaPercentual = CargaMaxima;
            return $"{descricaoVeiculo} recarregado: bateria de {cargaAnterior}% para {CargaPercentual}% (autonomia de {AutonomiaKm} km).";
        }

        // Exibição
        public override string ToString()
        {
            return $"Autonomia: {AutonomiaKm} km | Carga: {CargaPercentual}%";
        }
    }
}

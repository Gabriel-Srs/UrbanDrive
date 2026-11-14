namespace UrbanDrive.ConsoleApp.Models
{
    // Configurações compartilhadas do sistema (RF07)
    public static class ConfiguradorSistema
    {
        // Taxa padrão: 10%
        public const decimal TaxaServicoPadrao = 0.10m;

        // Atributos
        private static decimal taxaServico = TaxaServicoPadrao;
        private static int totalVeiculosCadastrados = 0;

        // Taxa de serviço (entre 0 e 1)
        public static decimal TaxaServico
        {
            get { return taxaServico; }
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException(nameof(TaxaServico), value,
                        "A taxa de serviço deve estar entre 0 e 1 (ex.: 0,10 para 10%).");

                taxaServico = value;
            }
        }

        // Contador de veículos
        public static int TotalVeiculosCadastrados
        {
            get { return totalVeiculosCadastrados; }
        }

        // Usado pelo construtor de Veiculo
        internal static void RegistrarVeiculo()
        {
            totalVeiculosCadastrados++;
        }
    }
}

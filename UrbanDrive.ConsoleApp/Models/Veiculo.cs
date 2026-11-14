using System.Text.RegularExpressions;

namespace UrbanDrive.ConsoleApp.Models
{
    public class Veiculo
    {
        // Formato da placa (antigo ou Mercosul)
        private static readonly Regex FormatoPlaca = new Regex(@"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$");

        // Atributos
        private string identificador = string.Empty;
        private string placa = string.Empty;
        private string modelo = string.Empty;
        private decimal valorBaseDiaria;

        // Construtor
        public Veiculo(string identificador, string placa, string modelo, decimal valorBaseDiaria)
        {
            Identificador = identificador;
            Placa = placa;
            Modelo = modelo;
            ValorBaseDiaria = valorBaseDiaria;

            // Contador de veículos
            ConfiguradorSistema.RegistrarVeiculo();
        }

        // Propriedades
        public string Identificador
        {
            get { return identificador; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O identificador não pode ser nulo ou vazio.", nameof(Identificador));

                identificador = value.Trim();
            }
        }

        public string Placa
        {
            get { return placa; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("A placa não pode ser nula ou vazia.", nameof(Placa));

                string placaNormalizada = value.Trim().Replace("-", "").ToUpperInvariant();

                if (!FormatoPlaca.IsMatch(placaNormalizada))
                    throw new ArgumentException(
                        $"A placa '{value}' é inválida. Use o padrão antigo (ABC-1234) ou Mercosul (ABC1D23).",
                        nameof(Placa));

                placa = placaNormalizada;
            }
        }

        public string Modelo
        {
            get { return modelo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O modelo não pode ser nulo ou vazio.", nameof(Modelo));

                modelo = value.Trim();
            }
        }

        public decimal ValorBaseDiaria
        {
            get { return valorBaseDiaria; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(ValorBaseDiaria), value,
                        "O valor-base da diária deve ser maior que zero.");

                valorBaseDiaria = value;
            }
        }

        // Exibição
        public override string ToString()
        {
            return $"ID: {Identificador} | Modelo: {Modelo} | Placa: {Placa} | Diária: R$ {ValorBaseDiaria:F2}";
        }
    }
}

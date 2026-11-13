using System.Text.RegularExpressions;

namespace UrbanDrive.ConsoleApp.Models
{
    public class Veiculo
    {
        // Placa no padrão antigo (ABC1234) ou Mercosul (ABC1D23), já sem hífen e em maiúsculas
        private static readonly Regex FormatoPlaca = new Regex(@"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$");

        // Atributos privados (inicializados para eliminar os avisos CS8618)
        private string identificador = string.Empty;
        private string placa = string.Empty;
        private string modelo = string.Empty;
        private decimal valorBaseDiaria;

        // Construtor: passa pelas propriedades para reaproveitar as validações
        public Veiculo(string identificador, string placa, string modelo, decimal valorBaseDiaria)
        {
            Identificador = identificador;
            Placa = placa;
            Modelo = modelo;
            ValorBaseDiaria = valorBaseDiaria;
        }

        // Identificador: só pode ser definido na criação do objeto (set privado)
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

        // Placa: só pode ser definida na criação do objeto (set privado).
        // Aceita "ABC-1234", "abc1234" ou "ABC1D23" e armazena normalizada (ex.: "ABC1234").
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

        // Modelo: pode ser corrigido depois da criação, sempre passando pela validação
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

        // Valor-base da diária: pode ser reajustado, mas precisa ser maior que zero
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

        // Representação textual do veículo. A entidade não escreve no console:
        // quem decide onde exibir é a aplicação (Program).
        public override string ToString()
        {
            return $"ID: {Identificador} | Modelo: {Modelo} | Placa: {Placa} | Diária: R$ {ValorBaseDiaria:F2}";
        }
    }
}

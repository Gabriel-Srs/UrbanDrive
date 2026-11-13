namespace UrbanDrive.ConsoleApp.Models
{
    // Protótipo adiantado da Sprint 3 (herança). As regras específicas entram na Sprint 3.
    public class Carro : Veiculo
    {
        // Atributos privados específicos de Carro
        private int quantidadePortas;
        private string tipoCombustivel = string.Empty;

        // Construtor que repassa os dados comuns para a classe-base usando "base"
        public Carro(string identificador, string placa, string modelo, decimal valorBaseDiaria,
                     int quantidadePortas, string tipoCombustivel)
            : base(identificador, placa, modelo, valorBaseDiaria)
        {
            QuantidadePortas = quantidadePortas;
            TipoCombustivel = tipoCombustivel;
        }

        public int QuantidadePortas
        {
            get { return quantidadePortas; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(QuantidadePortas), value,
                        "A quantidade de portas deve ser maior que zero.");

                quantidadePortas = value;
            }
        }

        public string TipoCombustivel
        {
            get { return tipoCombustivel; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O tipo de combustível não pode ser vazio.", nameof(TipoCombustivel));

                tipoCombustivel = value.Trim();
            }
        }

        // Reaproveita o texto da classe-base em vez de repetir os campos comuns
        public override string ToString()
        {
            return $"[Carro] {base.ToString()} | Portas: {QuantidadePortas} | Combustível: {TipoCombustivel}";
        }
    }
}

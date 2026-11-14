namespace UrbanDrive.ConsoleApp.Models
{
    public class Carro : Veiculo
    {
        // Atributos

        private int quantidadePortas;
        private string tipoCombustivel = string.Empty;

        // Construtor
        public Carro(string identificador, string placa, string modelo, decimal valorBaseDiaria,
                     int quantidadePortas, string tipoCombustivel)
            : base(identificador, placa, modelo, valorBaseDiaria)
        {
            QuantidadePortas = quantidadePortas;
            TipoCombustivel = tipoCombustivel;
        }

        // Propriedades
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

        // Exibição
        public override string ToString()
        {
            return $"[Carro] {base.ToString()} | Portas: {QuantidadePortas} | Combustível: {TipoCombustivel}";
        }
    }
}

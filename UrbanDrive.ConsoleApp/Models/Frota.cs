using System.Text;
using UrbanDrive.ConsoleApp.Interfaces;

namespace UrbanDrive.ConsoleApp.Models
{
    // Agregação: a frota agrupa veículos que existem independentemente dela (RF04)
    public class Frota : IImprimivel
    {
        // Atributos
        private string nome = string.Empty;
        private readonly List<Veiculo> veiculos = new List<Veiculo>();

        // Construtor
        public Frota(string nome)
        {
            Nome = nome;
        }

        // Propriedades
        public string Nome
        {
            get { return nome; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O nome da frota não pode ser nulo ou vazio.", nameof(Nome));

                nome = value.Trim();
            }
        }

        // Lista somente leitura
        public IReadOnlyList<Veiculo> Veiculos
        {
            get { return veiculos.AsReadOnly(); }
        }

        public int Quantidade
        {
            get { return veiculos.Count; }
        }

        // Métodos
        public void AdicionarVeiculo(Veiculo veiculo)
        {
            if (veiculo == null)
                throw new ArgumentNullException(nameof(veiculo), "O veículo não pode ser nulo.");

            if (BuscarPorPlaca(veiculo.Placa) != null)
                throw new ArgumentException(
                    $"Já existe um veículo com a placa {veiculo.Placa} na frota {Nome}.", nameof(veiculo));

            veiculos.Add(veiculo);
        }

        public bool RemoverVeiculo(string placa)
        {
            Veiculo? veiculo = BuscarPorPlaca(placa);

            if (veiculo == null)
                return false;

            veiculos.Remove(veiculo);
            return true;
        }

        public Veiculo? BuscarPorPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return null;

            string placaNormalizada = placa.Trim().Replace("-", "").ToUpperInvariant();

            foreach (Veiculo veiculo in veiculos)
            {
                if (veiculo.Placa == placaNormalizada)
                    return veiculo;
            }

            return null;
        }

        // Relatório (IImprimivel)
        public string GerarRelatorio()
        {
            var relatorio = new StringBuilder();
            relatorio.AppendLine($"=== Relatório da frota {Nome} ===");
            relatorio.AppendLine($"Veículos: {Quantidade}");

            if (Quantidade == 0)
                relatorio.AppendLine("  (nenhum veículo cadastrado)");

            foreach (Veiculo veiculo in veiculos)
                relatorio.AppendLine($"  {veiculo}");

            return relatorio.ToString().TrimEnd();
        }

        // Exibição
        public override string ToString()
        {
            return $"Frota: {Nome} | Veículos: {Quantidade}";
        }
    }
}

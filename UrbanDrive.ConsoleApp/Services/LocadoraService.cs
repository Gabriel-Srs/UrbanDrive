using UrbanDrive.ConsoleApp.Interfaces;
using UrbanDrive.ConsoleApp.Models;

namespace UrbanDrive.ConsoleApp.Services
{
    // Operações da aplicação sobre a frota e as locações (o menu só lê e exibe)
    public class LocadoraService
    {
        // Atributos
        private readonly Frota frota;
        private readonly List<Locacao> locacoes = new List<Locacao>();

        // Construtor
        public LocadoraService(Frota frota)
        {
            this.frota = frota ?? throw new ArgumentNullException(nameof(frota), "A frota não pode ser nula.");
        }

        // Propriedades
        public Frota Frota
        {
            get { return frota; }
        }

        public IReadOnlyList<Locacao> Locacoes
        {
            get { return locacoes.AsReadOnly(); }
        }

        // Cadastro e consulta de veículos
        public void CadastrarVeiculo(Veiculo veiculo)
        {
            frota.AdicionarVeiculo(veiculo);
        }

        public Veiculo ConsultarVeiculo(string placa)
        {
            Veiculo? veiculo = frota.BuscarPorPlaca(placa);

            if (veiculo == null)
                throw new ArgumentException($"Nenhum veículo com a placa '{placa}' na frota.", nameof(placa));

            return veiculo;
        }

        public void RemoverVeiculo(string placa)
        {
            Veiculo veiculo = ConsultarVeiculo(placa);

            foreach (Locacao locacao in locacoes)
            {
                if (locacao.Veiculo == veiculo)
                    throw new ArgumentException($"O veículo {veiculo.Placa} tem locação registrada e não pode ser removido.", nameof(placa));
            }

            frota.RemoverVeiculo(veiculo.Placa);
        }

        // Polimorfismo: cada veículo calcula o próprio aluguel, sem verificar o tipo
        public IReadOnlyList<SimulacaoAluguel> SimularAluguel(int dias)
        {
            var simulacoes = new List<SimulacaoAluguel>();

            foreach (Veiculo veiculo in frota.Veiculos)
            {
                decimal aluguel = veiculo.CalcularAluguel(dias);
                simulacoes.Add(new SimulacaoAluguel(veiculo, dias, aluguel, ConfiguradorSistema.AplicarTaxaServico(aluguel)));
            }

            return simulacoes;
        }

        // Locações (integração com Motorista, CNH e Locacao)
        public Locacao RegistrarLocacao(Motorista motorista, string placa, int dias, DateTime dataRetirada)
        {
            var locacao = new Locacao(motorista, ConsultarVeiculo(placa), dias, dataRetirada);
            locacoes.Add(locacao);
            return locacao;
        }

        // Manutenção (sobrecarga): sem peça ou com a peça substituída
        public string RealizarManutencao(string placa, string? pecaSubstituida)
        {
            Veiculo veiculo = ConsultarVeiculo(placa);

            if (string.IsNullOrWhiteSpace(pecaSubstituida))
                return veiculo.RealizarManutencao();

            return veiculo.RealizarManutencao(pecaSubstituida);
        }

        // Recarga (IEletrico): só veículos elétricos
        public string Recarregar(string placa)
        {
            Veiculo veiculo = ConsultarVeiculo(placa);

            if (veiculo is IEletrico eletrico)
                return eletrico.Recarregar();

            throw new ArgumentException($"O veículo {veiculo.Placa} ({veiculo.Tipo}) não é elétrico.", nameof(placa));
        }

        public IReadOnlyList<IEletrico> ListarEletricos()
        {
            var eletricos = new List<IEletrico>();

            foreach (Veiculo veiculo in frota.Veiculos)
            {
                if (veiculo is IEletrico eletrico)
                    eletricos.Add(eletrico);
            }

            return eletricos;
        }

        // Relatórios (IImprimivel): frota, veículos e locações
        public IReadOnlyList<IImprimivel> ListarImprimiveis()
        {
            var imprimiveis = new List<IImprimivel> { frota };
            imprimiveis.AddRange(frota.Veiculos);
            imprimiveis.AddRange(locacoes);
            return imprimiveis;
        }
    }

    // Resultado da simulação de aluguel de um veículo
    public record SimulacaoAluguel(Veiculo Veiculo, int Dias, decimal Aluguel, decimal TotalComTaxa);
}

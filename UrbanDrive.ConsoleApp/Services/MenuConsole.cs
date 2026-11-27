using System.Globalization;
using UrbanDrive.ConsoleApp.Interfaces;
using UrbanDrive.ConsoleApp.Models;

namespace UrbanDrive.ConsoleApp.Services
{
    // Menu de console (RF10): lê os dados, chama o LocadoraService e exibe o resultado
    public class MenuConsole
    {
        // Atributos
        private readonly LocadoraService locadora;
        private readonly TextReader entrada;
        private readonly TextWriter saida;

        // Construtor: entrada e saída injetadas para permitir testes com digitação simulada
        public MenuConsole(LocadoraService locadora, TextReader entrada, TextWriter saida)
        {
            this.locadora = locadora ?? throw new ArgumentNullException(nameof(locadora));
            this.entrada = entrada ?? throw new ArgumentNullException(nameof(entrada));
            this.saida = saida ?? throw new ArgumentNullException(nameof(saida));
        }

        // Laço principal
        public void Executar()
        {
            saida.WriteLine("=== UrbanDrive - Gestão de Mobilidade Urbana ===");

            while (true)
            {
                ExibirOpcoes();

                string? opcao = entrada.ReadLine();
                if (opcao == null || opcao.Trim() == "0")
                {
                    saida.WriteLine("Até logo!");
                    return;
                }

                saida.WriteLine();
                try
                {
                    ExecutarOpcao(opcao.Trim());
                }
                catch (FimDaEntradaException)
                {
                    saida.WriteLine("Até logo!");
                    return;
                }
                catch (ArgumentException ex)
                {
                    // Mensagem da validação, sem o "(Parameter ...)" do .NET
                    saida.WriteLine($"Erro: {ex.Message.Split(" (Parameter")[0]}");
                }
            }
        }

        private void ExibirOpcoes()
        {
            saida.WriteLine();
            saida.WriteLine($"--- {locadora.Frota} | Veículos cadastrados no sistema: {ConfiguradorSistema.TotalVeiculosCadastrados} | Taxa: {ConfiguradorSistema.TaxaServico:P0} ---");
            saida.WriteLine(" 1. Cadastrar carro");
            saida.WriteLine(" 2. Cadastrar carro elétrico");
            saida.WriteLine(" 3. Cadastrar patinete");
            saida.WriteLine(" 4. Listar veículos");
            saida.WriteLine(" 5. Consultar veículo por placa");
            saida.WriteLine(" 6. Simular aluguel (todos os veículos)");
            saida.WriteLine(" 7. Registrar locação");
            saida.WriteLine(" 8. Realizar manutenção");
            saida.WriteLine(" 9. Recarregar veículo elétrico");
            saida.WriteLine("10. Relatórios");
            saida.WriteLine("11. Remover veículo");
            saida.WriteLine("12. Alterar taxa de serviço");
            saida.WriteLine(" 0. Sair");
            saida.Write("Opção: ");
        }

        private void ExecutarOpcao(string opcao)
        {
            switch (opcao)
            {
                case "1": CadastrarCarro(); break;
                case "2": CadastrarCarroEletrico(); break;
                case "3": CadastrarPatinete(); break;
                case "4": ListarVeiculos(); break;
                case "5": ConsultarVeiculo(); break;
                case "6": SimularAluguel(); break;
                case "7": RegistrarLocacao(); break;
                case "8": RealizarManutencao(); break;
                case "9": Recarregar(); break;
                case "10": ExibirRelatorios(); break;
                case "11": RemoverVeiculo(); break;
                case "12": AlterarTaxa(); break;
                default: saida.WriteLine($"Opção '{opcao}' inválida."); break;
            }
        }

        // 1 a 3: cadastro
        private void CadastrarCarro()
        {
            saida.WriteLine("Cadastro de carro");
            var carro = new Carro(LerTexto("Identificador"), LerTexto("Placa"), LerTexto("Modelo"),
                LerDecimal("Diária (R$)"), LerInteiro("Quantidade de portas"), LerTexto("Combustível"));
            locadora.CadastrarVeiculo(carro);
            saida.WriteLine($"Cadastrado: {carro}");
        }

        private void CadastrarCarroEletrico()
        {
            saida.WriteLine("Cadastro de carro elétrico");
            var carro = new CarroEletrico(LerTexto("Identificador"), LerTexto("Placa"), LerTexto("Modelo"),
                LerDecimal("Diária (R$)"), LerInteiro("Quantidade de portas"),
                LerInteiro("Autonomia da bateria (km)"), LerInteiro("Carga atual da bateria (%)"));
            locadora.CadastrarVeiculo(carro);
            saida.WriteLine($"Cadastrado: {carro}");
        }

        private void CadastrarPatinete()
        {
            saida.WriteLine("Cadastro de patinete");
            var patinete = new Patinete(LerTexto("Identificador"), LerTexto("Placa"), LerTexto("Modelo"),
                LerDecimal("Diária (R$)"), LerInteiro("Autonomia da bateria (km)"),
                LerInteiro("Carga atual da bateria (%)"));
            locadora.CadastrarVeiculo(patinete);
            saida.WriteLine($"Cadastrado: {patinete}");
        }

        // 4 e 5: consulta
        private void ListarVeiculos()
        {
            if (locadora.Frota.Quantidade == 0)
            {
                saida.WriteLine("Nenhum veículo cadastrado.");
                return;
            }

            foreach (Veiculo veiculo in locadora.Frota.Veiculos)
                saida.WriteLine(veiculo);
        }

        private void ConsultarVeiculo()
        {
            Veiculo veiculo = locadora.ConsultarVeiculo(LerTexto("Placa"));
            saida.WriteLine(veiculo.GerarRelatorio());
        }

        // 6: cálculos polimórficos
        private void SimularAluguel()
        {
            int dias = LerInteiro("Quantidade de dias");
            saida.WriteLine($"Simulação de aluguel por {dias} dia(s), taxa de serviço de {ConfiguradorSistema.TaxaServico:P0}:");

            foreach (SimulacaoAluguel simulacao in locadora.SimularAluguel(dias))
            {
                saida.WriteLine($"  {simulacao.Veiculo.Tipo,-15} {simulacao.Veiculo.Placa} | " +
                                $"Aluguel: R$ {simulacao.Aluguel,8:F2} | Total com taxa: R$ {simulacao.TotalComTaxa,8:F2}");
            }
        }

        // 7: locação
        private void RegistrarLocacao()
        {
            saida.WriteLine("Dados do motorista");
            var motorista = new Motorista(LerTexto("Nome"), LerTexto("Número da CNH (11 dígitos)"),
                LerTexto("Categoria da CNH"), LerData("Validade da CNH (dd/mm/aaaa)"));

            Locacao locacao = locadora.RegistrarLocacao(motorista, LerTexto("Placa do veículo"),
                LerInteiro("Quantidade de dias"), DateTime.Today);
            saida.WriteLine(locacao.GerarRelatorio());
        }

        // 8: manutenção (sobrecarga)
        private void RealizarManutencao()
        {
            string placa = LerTexto("Placa");
            string peca = LerTexto("Peça substituída (Enter para manutenção geral)", obrigatorio: false);
            saida.WriteLine(locadora.RealizarManutencao(placa, peca));
        }

        // 9: recarga (IEletrico)
        private void Recarregar()
        {
            IReadOnlyList<IEletrico> eletricos = locadora.ListarEletricos();
            saida.WriteLine($"Veículos elétricos na frota: {eletricos.Count}");

            foreach (IEletrico eletrico in eletricos)
                saida.WriteLine($"  {eletrico}");

            saida.WriteLine(locadora.Recarregar(LerTexto("Placa")));
        }

        // 10: relatórios (IImprimivel)
        private void ExibirRelatorios()
        {
            foreach (IImprimivel imprimivel in locadora.ListarImprimiveis())
            {
                saida.WriteLine(imprimivel.GerarRelatorio());
                saida.WriteLine();
            }
        }

        // 11 e 12
        private void RemoverVeiculo()
        {
            string placa = LerTexto("Placa");
            locadora.RemoverVeiculo(placa);
            saida.WriteLine($"Veículo {placa} removido da frota.");
        }

        private void AlterarTaxa()
        {
            decimal percentual = LerDecimal("Nova taxa de serviço (%)");
            ConfiguradorSistema.TaxaServico = percentual / 100;
            saida.WriteLine($"Taxa de serviço alterada para {ConfiguradorSistema.TaxaServico:P0}.");
        }

        // Leitura dos dados
        private string LerTexto(string rotulo, bool obrigatorio = true)
        {
            saida.Write($"{rotulo}: ");
            string? valor = entrada.ReadLine();

            if (valor == null)
                throw new FimDaEntradaException();

            // Campos obrigatórios vazios são rejeitados pelas validações das entidades
            return obrigatorio ? valor : valor.Trim();
        }

        private int LerInteiro(string rotulo)
        {
            string texto = LerTexto(rotulo);

            if (!int.TryParse(texto.Trim(), out int valor))
                throw new ArgumentException($"'{texto}' não é um número inteiro válido.");

            return valor;
        }

        private decimal LerDecimal(string rotulo)
        {
            string texto = LerTexto(rotulo).Trim();

            // Aceita vírgula ou ponto como separador decimal (ex.: 120,50 ou 120.50)
            if (!decimal.TryParse(texto.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal valor))
                throw new ArgumentException($"'{texto}' não é um valor numérico válido.");

            return valor;
        }

        private DateTime LerData(string rotulo)
        {
            string texto = LerTexto(rotulo).Trim();

            if (!DateTime.TryParseExact(texto, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime data))
                throw new ArgumentException($"'{texto}' não é uma data válida no formato dd/mm/aaaa.");

            return data;
        }

        // Sinaliza que a entrada terminou no meio de uma operação
        private sealed class FimDaEntradaException : Exception
        {
        }
    }
}

using UrbanDrive.ConsoleApp.Models;

namespace UrbanDrive.ConsoleApp
{
    internal class Program
    {
        // Placar dos testes
        private static int aprovados = 0;
        private static int reprovados = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("=== UrbanDrive - Demonstração e testes ===");
            Console.WriteLine();

            TestarSprint1Veiculo();
            TestarSprint2ConfiguradorSistema();
            TestarSprint2Frota();
            TestarSprint2MotoristaCnh();
            TestarSprint2Locacao();
            TestarSprint3Aluguel();
            TestarSprint3Manutencao();
            TestarSprint3Patinete();

            Console.WriteLine();
            Console.WriteLine($"Resultado: {aprovados} aprovados, {reprovados} reprovados.");
        }

        // Sprint 1 - Veiculo
        private static void TestarSprint1Veiculo()
        {
            Console.WriteLine("##### Sprint 1: Veiculo #####");
            Console.WriteLine();

            Console.WriteLine("--- Criação de objetos válidos ---");
            var mobi = new Veiculo("V001", "ABC-1234", "Fiat Mobi", 90.00m);
            var onix = new Carro("V002", "bra2e19", "Chevrolet Onix", 120.00m, 4, "Flex");
            Console.WriteLine(mobi);
            Console.WriteLine(onix);
            Console.WriteLine();

            Console.WriteLine("--- Normalização e alteração válida ---");
            Verificar("Placa ' abc-1234 ' é armazenada como 'ABC1234'",
                new Veiculo("V003", " abc-1234 ", "Fiat Uno", 80m).Placa == "ABC1234");
            Verificar("Placa Mercosul 'bra2e19' é armazenada como 'BRA2E19'", onix.Placa == "BRA2E19");
            mobi.ValorBaseDiaria = 95.50m;
            Verificar("Reajuste da diária para 95,50 é aceito", mobi.ValorBaseDiaria == 95.50m);
            Console.WriteLine();

            Console.WriteLine("--- Validações (devem ser rejeitadas) ---");
            DeveFalhar("Placa vazia", () => new Veiculo("V010", "", "Carro Teste", 50m));
            DeveFalhar("Placa nula", () => new Veiculo("V011", null!, "Carro Teste", 50m));
            DeveFalhar("Placa só com espaços", () => new Veiculo("V012", "   ", "Carro Teste", 50m));
            DeveFalhar("Placa fora do padrão", () => new Veiculo("V013", "12-ABC", "Carro Teste", 50m));
            DeveFalhar("Identificador vazio", () => new Veiculo("", "ABC1234", "Carro Teste", 50m));
            DeveFalhar("Modelo vazio", () => new Veiculo("V014", "ABC1234", "  ", 50m));
            DeveFalhar("Diária zero", () => new Veiculo("V015", "ABC1234", "Carro Teste", 0m));
            DeveFalhar("Diária negativa", () => new Veiculo("V016", "ABC1234", "Carro Teste", -10m));
            DeveFalhar("Alterar modelo para vazio", () => { mobi.Modelo = ""; });
            DeveFalhar("Alterar diária para negativa", () => { mobi.ValorBaseDiaria = -1m; });
            DeveFalhar("Carro sem portas", () => new Carro("V017", "ABC1234", "Carro Teste", 50m, 0, "Flex"));
            DeveFalhar("Carro com placa vazia (validação herdada)", () => new Carro("V018", "", "Carro Teste", 50m, 4, "Flex"));
            Console.WriteLine();
        }

        // Sprint 2 - ConfiguradorSistema
        private static void TestarSprint2ConfiguradorSistema()
        {
            Console.WriteLine("##### Sprint 2: ConfiguradorSistema #####");
            Console.WriteLine();

            Console.WriteLine("--- Contador de veículos (estático) ---");
            int totalAntes = ConfiguradorSistema.TotalVeiculosCadastrados;
            Console.WriteLine($"Veículos cadastrados até agora: {totalAntes}");

            // Sprint 3 (T3.5): o contador passou para Carro e Patinete
            new Patinete("V100", "DEF5678", "Xiaomi Scooter 4", 40m, 35);
            new Carro("V101", "GHI9J01", "Hyundai HB20", 110m, 4, "Flex");
            Verificar("Criar 2 veículos válidos (Carro e Patinete) aumenta o contador em 2",
                ConfiguradorSistema.TotalVeiculosCadastrados == totalAntes + 2);

            int totalAntesDoInvalido = ConfiguradorSistema.TotalVeiculosCadastrados;
            DeveFalhar("Criar veículo com placa vazia é rejeitado",
                () => new Veiculo("V102", "", "Carro Teste", 50m));
            Verificar("Veículo rejeitado não aumenta o contador",
                ConfiguradorSistema.TotalVeiculosCadastrados == totalAntesDoInvalido);
            Console.WriteLine($"Veículos cadastrados agora: {ConfiguradorSistema.TotalVeiculosCadastrados}");
            Console.WriteLine();

            Console.WriteLine("--- Taxa de serviço (estática e compartilhada) ---");
            Verificar("Taxa começa com o valor padrão de 10%",
                ConfiguradorSistema.TaxaServico == ConfiguradorSistema.TaxaServicoPadrao);

            ConfiguradorSistema.TaxaServico = 0.15m;
            Verificar("Alterar a taxa para 15% é aceito",
                ConfiguradorSistema.TaxaServico == 0.15m);

            DeveFalhar("Taxa negativa (-0,10) é rejeitada", () => { ConfiguradorSistema.TaxaServico = -0.10m; });
            DeveFalhar("Taxa acima de 100% (1,50) é rejeitada", () => { ConfiguradorSistema.TaxaServico = 1.50m; });
            Verificar("Após as tentativas inválidas, a taxa continua em 15%",
                ConfiguradorSistema.TaxaServico == 0.15m);

            ConfiguradorSistema.TaxaServico = ConfiguradorSistema.TaxaServicoPadrao;
            Console.WriteLine($"Taxa restaurada para {ConfiguradorSistema.TaxaServico:P0}.");
            Console.WriteLine();
        }

        // Sprint 2 - Frota
        private static void TestarSprint2Frota()
        {
            Console.WriteLine("##### Sprint 2: Frota #####");
            Console.WriteLine();

            Console.WriteLine("--- Criação e adição de veículos ---");
            var frota = new Frota("Frota Centro");
            var gol = new Veiculo("F001", "JKL-2345", "VW Gol", 95m);
            var argo = new Carro("F002", "MNO6P78", "Fiat Argo", 115m, 4, "Flex");

            frota.AdicionarVeiculo(gol);
            frota.AdicionarVeiculo(argo);
            Console.WriteLine(frota);
            foreach (Veiculo veiculo in frota.Veiculos)
                Console.WriteLine($"  {veiculo}");

            Verificar("Frota com 2 veículos adicionados", frota.Quantidade == 2);
            Verificar("Busca por placa 'jkl2345' encontra o Gol", frota.BuscarPorPlaca("jkl2345") == gol);
            Console.WriteLine();

            Console.WriteLine("--- Validações (devem ser rejeitadas) ---");
            DeveFalhar("Frota com nome vazio", () => new Frota(""));
            DeveFalhar("Adicionar veículo nulo", () => frota.AdicionarVeiculo(null!));
            DeveFalhar("Adicionar o mesmo veículo duas vezes", () => frota.AdicionarVeiculo(gol));
            DeveFalhar("Adicionar outro veículo com placa repetida",
                () => frota.AdicionarVeiculo(new Veiculo("F003", "JKL2345", "Outro Gol", 90m)));
            Verificar("Após as tentativas inválidas, a frota continua com 2 veículos", frota.Quantidade == 2);
            Verificar("A lista exposta é somente leitura",
                ((ICollection<Veiculo>)frota.Veiculos).IsReadOnly);
            Console.WriteLine();

            Console.WriteLine("--- Remoção (agregação) ---");
            Verificar("Remover o Gol da frota retorna true", frota.RemoverVeiculo("JKL-2345"));
            Verificar("Frota fica com 1 veículo", frota.Quantidade == 1);
            Verificar("Remover placa inexistente retorna false", !frota.RemoverVeiculo("ZZZ9999"));

            gol.ValorBaseDiaria = 99m;
            Verificar("O Gol continua existindo e utilizável fora da frota",
                gol.Modelo == "VW Gol" && gol.ValorBaseDiaria == 99m);

            var frotaNorte = new Frota("Frota Norte");
            frotaNorte.AdicionarVeiculo(gol);
            Verificar("O Gol removido pode ser adicionado a outra frota", frotaNorte.Quantidade == 1);
            Console.WriteLine($"  {gol}");
            Console.WriteLine();
        }

        // Sprint 2 - Motorista e CNH
        private static void TestarSprint2MotoristaCnh()
        {
            Console.WriteLine("##### Sprint 2: Motorista e CNH #####");
            Console.WriteLine();

            Console.WriteLine("--- T2.1 - Motorista e CNH ---");

            var motorista = new Motorista(
            "João Silva",
            "12345678901",
            "B",
            new DateTime(2027, 10, 15));

            Verificar(
                "Motorista possui CNH válida",
                !motorista.CNH.EstaVencida(DateTime.Now));

            DeveFalhar(
                "CNH com número de 10 dígitos",
                () => new Motorista(
                "João Silva",
                "1234567890",
                "B",
            new DateTime(2027, 10, 15)));

            DeveFalhar(
                "CNH com número de 12 dígitos",
                () => new Motorista(
                "João Silva",
                "123456789012",
                "B",
            new DateTime(2027, 10, 15)));

            DeveFalhar(
                "CNH com letra no número",
                () => new Motorista(
                    "João Silva",
                    "1234567890A",
                    "B",
                    new DateTime(2027, 10, 15)));

            DeveFalhar(
                "CNH com categoria vazia",
                () => new Motorista(
                    "João Silva",
                    "12345678901",
                    "",
                    new DateTime(2027, 10, 15)));

            Verificar(
                "Categoria da CNH é B",
                motorista.CNH.Categoria == "B");

            Console.WriteLine($"Número da CNH: {motorista.CNH.Numero}");
            Console.WriteLine($"Quantidade de dígitos: {motorista.CNH.Numero.Length}");

            Verificar(
                "Número da CNH possui 11 dígitos",
                motorista.CNH.Numero.Length == 11);
            Console.WriteLine();
        }

        // Sprint 2 - Locacao (integração entre os objetos)
        private static void TestarSprint2Locacao()
        {
            Console.WriteLine("##### Sprint 2: Locacao (integração) #####");
            Console.WriteLine();

            var dataRetirada = new DateTime(2026, 11, 14);
            var maria = new Motorista("Maria Souza", "98765432100", "B", new DateTime(2028, 6, 30));
            var kwid = new Veiculo("L001", "QRS-1234", "Renault Kwid", 100m);
            var hb20 = new Carro("L002", "TUV5W67", "Hyundai HB20", 150m, 4, "Flex");

            var frota = new Frota("Frota Locação");
            frota.AdicionarVeiculo(kwid);
            frota.AdicionarVeiculo(hb20);

            Console.WriteLine("--- Locação com veículo buscado na frota ---");
            Veiculo? veiculoEscolhido = frota.BuscarPorPlaca("qrs1234");
            Verificar("Veículo encontrado na frota pela placa", veiculoEscolhido == kwid);

            var locacao = new Locacao(maria, veiculoEscolhido!, 3, dataRetirada);
            Console.WriteLine(locacao);

            Verificar("A locação referencia o mesmo motorista (associação)", locacao.Motorista == maria);
            Verificar("A locação referencia o mesmo veículo da frota (associação)", locacao.Veiculo == kwid);
            Verificar("Total com taxa padrão: 3 dias x R$ 100,00 x 1,10 = R$ 330,00",
                locacao.CalcularValorTotal() == 330.00m);
            Console.WriteLine();

            Console.WriteLine("--- Taxa compartilhada (ConfiguradorSistema) ---");
            ConfiguradorSistema.TaxaServico = 0.20m;
            Verificar("Com taxa de 20%, a mesma locação passa a custar R$ 360,00",
                locacao.CalcularValorTotal() == 360.00m);
            ConfiguradorSistema.TaxaServico = ConfiguradorSistema.TaxaServicoPadrao;
            Verificar("Com a taxa restaurada para 10%, o total volta a R$ 330,00",
                locacao.CalcularValorTotal() == 330.00m);
            Console.WriteLine();

            Console.WriteLine("--- Locação de um Carro (herança) ---");
            var locacaoCarro = new Locacao(maria, hb20, 2, dataRetirada);
            Console.WriteLine(locacaoCarro);
            // Sprint 3: o Carro tem acréscimo de 5% de seguro (antes: R$ 330,00)
            Verificar("Total do Carro: 2 dias x R$ 150,00 x 1,05 x 1,10 = R$ 346,50",
                locacaoCarro.CalcularValorTotal() == 346.50m);
            Verificar("O mesmo motorista pode ter mais de uma locação", locacaoCarro.Motorista == locacao.Motorista);
            Console.WriteLine();

            Console.WriteLine("--- Validade da CNH ---");
            var joao = new Motorista("João Lima", "11122233344", "B", new DateTime(2025, 1, 31));
            DeveFalhar("Motorista com CNH vencida na data de retirada",
                () => new Locacao(joao, kwid, 2, dataRetirada));
            var locacaoNoLimite = new Locacao(joao, kwid, 1, new DateTime(2025, 1, 31));
            Verificar("Retirada no último dia de validade da CNH é aceita", locacaoNoLimite.Dias == 1);
            Console.WriteLine();

            Console.WriteLine("--- Validações (devem ser rejeitadas) ---");
            DeveFalhar("Locação com motorista nulo", () => new Locacao(null!, kwid, 2, dataRetirada));
            DeveFalhar("Locação com veículo nulo", () => new Locacao(maria, null!, 2, dataRetirada));
            DeveFalhar("Locação com 0 dias", () => new Locacao(maria, kwid, 0, dataRetirada));
            DeveFalhar("Locação com dias negativos", () => new Locacao(maria, kwid, -3, dataRetirada));

            Console.WriteLine();
        }

        // Sprint 3 - CalcularAluguel (sobrescrita e polimorfismo)
        private static void TestarSprint3Aluguel()
        {
            Console.WriteLine("##### Sprint 3: CalcularAluguel (polimorfismo) #####");
            Console.WriteLine();

            var veiculo = new Veiculo("A001", "AAA1A11", "Veículo Base", 100m);
            var carro = new Carro("A002", "BBB2B22", "Fiat Cronos", 100m, 4, "Flex");
            var patinete = new Patinete("A003", "CCC3C33", "Xiaomi Scooter 4", 40m, 35);

            Console.WriteLine("--- Regra de cada tipo ---");
            Verificar("Veiculo: R$ 100,00 x 3 dias = R$ 300,00", veiculo.CalcularAluguel(3) == 300.00m);
            Verificar("Carro: R$ 100,00 x 3 dias x 1,05 (seguro) = R$ 315,00", carro.CalcularAluguel(3) == 315.00m);
            Verificar("Patinete: R$ 40,00 x 2 dias = R$ 80,00 (sem desconto)", patinete.CalcularAluguel(2) == 80.00m);
            Verificar("Patinete: R$ 40,00 x 3 dias x 0,90 = R$ 108,00 (desconto a partir de 3 dias)",
                patinete.CalcularAluguel(3) == 108.00m);
            Verificar("Patinete: R$ 40,00 x 4 dias x 0,90 = R$ 144,00", patinete.CalcularAluguel(4) == 144.00m);
            Console.WriteLine();

            Console.WriteLine("--- Polimorfismo: a mesma chamada em uma lista de Veiculo ---");
            var veiculos = new List<Veiculo> { veiculo, carro, patinete };
            var esperados = new List<decimal> { 300.00m, 315.00m, 108.00m };
            for (int i = 0; i < veiculos.Count; i++)
            {
                // Não verifica o tipo: cada objeto executa a própria versão de CalcularAluguel
                decimal valor = veiculos[i].CalcularAluguel(3);
                Console.WriteLine($"  {veiculos[i].GetType().Name,-8} | 3 dias | R$ {valor:F2}");
                Verificar($"{veiculos[i].GetType().Name} na lista calcula R$ {esperados[i]:F2}", valor == esperados[i]);
            }
            Console.WriteLine();

            Console.WriteLine("--- Validação herdada de dias ---");
            DeveFalhar("Carro com 0 dias", () => carro.CalcularAluguel(0));
            DeveFalhar("Carro com dias negativos", () => carro.CalcularAluguel(-1));
            DeveFalhar("Patinete com 0 dias", () => patinete.CalcularAluguel(0));
            DeveFalhar("Patinete com dias negativos", () => patinete.CalcularAluguel(-1));
            Console.WriteLine();

            Console.WriteLine("--- Locação usa o cálculo de cada tipo ---");
            var ana = new Motorista("Ana Costa", "55566677788", "A", new DateTime(2029, 3, 10));
            var locacaoPatinete = new Locacao(ana, patinete, 4, new DateTime(2026, 11, 20));
            Console.WriteLine(locacaoPatinete);
            Verificar("Locação de Patinete: R$ 144,00 x 1,10 (taxa) = R$ 158,40",
                locacaoPatinete.CalcularValorTotal() == 158.40m);
            var locacaoCarro = new Locacao(ana, carro, 3, new DateTime(2026, 11, 20));
            Console.WriteLine(locacaoCarro);
            Verificar("Locação de Carro: R$ 315,00 x 1,10 (taxa) = R$ 346,50",
                locacaoCarro.CalcularValorTotal() == 346.50m);
            Console.WriteLine();
        }

        // Sprint 3 - RealizarManutencao (sobrecarga)
        private static void TestarSprint3Manutencao()
        {
            Console.WriteLine("##### Sprint 3: RealizarManutencao (sobrecarga) #####");
            Console.WriteLine();

            var carro = new Carro("M001", "DDD4D44", "VW Polo", 130m, 4, "Flex");
            var patinete = new Patinete("M002", "EEE5E55", "Segway Ninebot", 45m, 40);

            string geral = carro.RealizarManutencao();
            string comPeca = patinete.RealizarManutencao("bateria");
            Console.WriteLine($"  RealizarManutencao():          {geral}");
            Console.WriteLine($"  RealizarManutencao(\"bateria\"): {comPeca}");

            Verificar("Versão sem parâmetro descreve a manutenção com a placa", geral.Contains("DDD4D44"));
            Verificar("Versão com peça inclui a peça substituída", comPeca.Contains("bateria") && comPeca.Contains("EEE5E55"));
            DeveFalhar("Peça vazia", () => carro.RealizarManutencao(""));
            DeveFalhar("Peça só com espaços", () => carro.RealizarManutencao("   "));
            DeveFalhar("Peça nula", () => carro.RealizarManutencao(null!));
            Console.WriteLine();
        }

        // Sprint 3 - Patinete (herança, sealed e contador)
        private static void TestarSprint3Patinete()
        {
            Console.WriteLine("##### Sprint 3: Patinete (herança e sealed) #####");
            Console.WriteLine();

            var patinete = new Patinete("P001", "fff-6789", "Xiaomi Scooter 4", 40m, 35);
            Console.WriteLine(patinete);

            Verificar("Patinete é sealed (nenhuma classe pode herdar dela)", typeof(Patinete).IsSealed);
            Verificar("Patinete é um Veiculo (herança)", patinete is Veiculo);
            Verificar("Construtor com base(...) inicializa a placa normalizada", patinete.Placa == "FFF6789");
            Verificar("Autonomia da bateria é 35 km", patinete.AutonomiaBateriaKm == 35);
            DeveFalhar("Autonomia zero", () => new Patinete("P002", "GGG7G77", "Patinete Teste", 40m, 0));
            DeveFalhar("Autonomia negativa", () => new Patinete("P003", "GGG7G77", "Patinete Teste", 40m, -10));
            DeveFalhar("Alterar autonomia para zero", () => { patinete.AutonomiaBateriaKm = 0; });
            DeveFalhar("Patinete com placa inválida (validação herdada)",
                () => new Patinete("P004", "12-ABC", "Patinete Teste", 40m, 30));
            Console.WriteLine();

            Console.WriteLine("--- Contador não conta veículo rejeitado pela subclasse (T3.5) ---");
            int total = ConfiguradorSistema.TotalVeiculosCadastrados;
            DeveFalhar("Carro com 0 portas", () => new Carro("P005", "HHH8H88", "Carro Teste", 50m, 0, "Flex"));
            DeveFalhar("Patinete com autonomia 0", () => new Patinete("P006", "III9I99", "Patinete Teste", 40m, 0));
            Verificar("Carro e Patinete rejeitados não aumentam o contador",
                ConfiguradorSistema.TotalVeiculosCadastrados == total);
            new Patinete("P007", "JJJ1J11", "Patinete Válido", 40m, 25);
            Verificar("Patinete válido aumenta o contador em 1",
                ConfiguradorSistema.TotalVeiculosCadastrados == total + 1);
            Console.WriteLine();
        }

        // Auxiliares de teste
        private static void DeveFalhar(string cenario, Action acao)
        {
            try
            {
                acao();
                Registrar(false, cenario, "nenhuma exceção foi lançada");
            }
            catch (ArgumentException ex)
            {
                string mensagem = ex.Message.Split(" (Parameter")[0];
                Registrar(true, cenario, mensagem);
            }
        }

        private static void Verificar(string cenario, bool condicao)
        {
            Registrar(condicao, cenario, condicao ? "ok" : "valor diferente do esperado");
        }

        private static void Registrar(bool passou, string cenario, string detalhe)
        {
            if (passou) aprovados++; else reprovados++;
            Console.WriteLine($"[{(passou ? "OK" : "FALHOU")}] {cenario}: {detalhe}");
        }
    }
}

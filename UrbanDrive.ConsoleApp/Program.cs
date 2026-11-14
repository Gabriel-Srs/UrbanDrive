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

            new Veiculo("V100", "DEF5678", "Renault Kwid", 85m);
            new Carro("V101", "GHI9J01", "Hyundai HB20", 110m, 4, "Flex");
            Verificar("Criar 2 veículos válidos (Veiculo e Carro) aumenta o contador em 2",
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

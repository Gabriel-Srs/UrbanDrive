using UrbanDrive.ConsoleApp.Models;

namespace UrbanDrive.ConsoleApp
{
    internal class Program
    {
        private static int aprovados = 0;
        private static int reprovados = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("=== UrbanDrive - Testes da Sprint 1 ===");
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

            // Cada cenário roda isolado: se um falhar, os demais continuam sendo testados
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
            Console.WriteLine($"Resultado: {aprovados} aprovados, {reprovados} reprovados.");

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
        }

        // Cenário que deve lançar ArgumentException (ou derivada, como ArgumentOutOfRangeException)
        private static void DeveFalhar(string cenario, Action acao)
        {
            try
            {
                acao();
                Registrar(false, cenario, "nenhuma exceção foi lançada");
            }
            catch (ArgumentException ex)
            {
                // Mostra só a primeira linha da mensagem, sem o "(Parameter ...)"
                string mensagem = ex.Message.Split(" (Parameter")[0];
                Registrar(true, cenario, mensagem);
            }
        }

        // Cenário em que uma condição deve ser verdadeira
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

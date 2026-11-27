using System.Globalization;
using System.Text;
using UrbanDrive.ConsoleApp.Services;
using UrbanDrive.ConsoleApp.Testes;

namespace UrbanDrive.ConsoleApp
{
    // Ponto de entrada: só inicializa a aplicação, sem regra de negócio
    internal class Program
    {
        static int Main(string[] args)
        {
            // Acentos corretos no terminal do Windows
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // Valores em reais e datas no padrão brasileiro em qualquer máquina
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

            // dotnet run -- --testes: executa os testes (código de saída 0 se todos passarem)
            if (args.Contains("--testes"))
                return TestesUrbanDrive.Executar() == 0 ? 0 : 1;

            // dotnet run: abre o menu com uma frota de demonstração
            var locadora = new LocadoraService(DadosDemonstracao.CriarFrota());
            new MenuConsole(locadora, Console.In, Console.Out).Executar();
            return 0;
        }
    }
}

using UrbanDrive.ConsoleApp.Testes;

namespace UrbanDrive.ConsoleApp
{
    // Ponto de entrada: só inicializa a aplicação, sem regra de negócio
    internal class Program
    {
        static int Main(string[] args)
        {
            // dotnet run -- --testes
            if (args.Contains("--testes"))
                return TestesUrbanDrive.Executar() == 0 ? 0 : 1;

            return TestesUrbanDrive.Executar() == 0 ? 0 : 1;
        }
    }
}

using UrbanDrive.ConsoleApp.Models;

namespace UrbanDrive.ConsoleApp.Services
{
    // Veículos de exemplo, para o menu já começar com uma frota (demonstração)
    public static class DadosDemonstracao
    {
        public static Frota CriarFrota()
        {
            var frota = new Frota("UrbanDrive Centro");
            frota.AdicionarVeiculo(new Carro("C001", "BRA2E19", "Chevrolet Onix", 120m, 4, "Flex"));
            frota.AdicionarVeiculo(new CarroEletrico("E001", "ELT1C23", "BYD Dolphin", 200m, 4, 400, 60));
            frota.AdicionarVeiculo(new Patinete("P001", "PAT4E56", "Xiaomi Scooter 4", 40m, 35, 20));
            return frota;
        }
    }
}

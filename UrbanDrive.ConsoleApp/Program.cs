using System;
using UrbanDrive.ConsoleApp.Models;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== UrbanDrive - Testes da Sprint 1 ===");

        try
        {
            // Testando criação de um veículo válido
            Veiculo veiculo1 = new Veiculo("V001", "ABC-1234", "Fiat Mobi", 90.00m);
            veiculo1.ExibirDetalhes();

            // Teste de validação: tentar inserir placa vazia (vai disparar a exceção)
            Veiculo veiculoInvalido = new Veiculo("V002", "", "Carro Teste", 50.00m);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro capturado: {ex.Message}");
        }
    }
}
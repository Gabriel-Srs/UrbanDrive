using System;

namespace UrbanDrive.ConsoleApp.Models
{
    public class Carro : Veiculo
    {
        // Atributos privados específicos de Carro
        private int quantidadePortas;
        private string tipoCombustivel;

        // Construtor que repassa os dados base para a classe Pai (Veiculo) usando o 'base'
        public Carro(string identificador, string placa, string modelo, decimal valorBaseDiaria, int quantidadePortas, string tipoCombustivel)
            : base(identificador, placa, modelo, valorBaseDiaria)
        {
            QuantidadePortas = quantidadePortas;
            TipoCombustivel = tipoCombustivel;
        }

        // Propriedade para Quantidade de Portas com validação
        public int QuantidadePortas
        {
            get { return quantidadePortas; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentException("A quantidade de portas deve ser maior que zero.");
                quantidadePortas = value; 
            }
        }

        // Propriedade para Tipo de Combustível com validação
        public string TipoCombustivel
        {
            get { return tipoCombustivel; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O tipo de combustível não pode ser vazio.");
                tipoCombustivel = value; 
            }
        }

        // Polimorfismo: sobrescrevemos o método virtual para exibir os dados específicos do carro
        public override void ExibirDetalhes()
        {
            Console.WriteLine($"[Carro] ID: {Identificador} | Modelo: {Modelo} | Placa: {Placa} | Portas: {QuantidadePortas} | Combustível: {TipoCombustivel} | Diária: R$ {ValorBaseDiaria:F2}");
        }
    }
}
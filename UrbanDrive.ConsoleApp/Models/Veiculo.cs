using System;

namespace UrbanDrive.ConsoleApp.Models
{
    public class Veiculo
    {
        // Atributos privados (Encapsulamento tradicional)
        private string identificador;
        private string placa;
        private string modelo;
        private decimal valorBaseDiaria;

        // Construtor da classe
        public Veiculo(string identificador, string placa, string modelo, decimal valorBaseDiaria)
        {
            Identificador = identificador;
            Placa = placa;
            Modelo = modelo;
            ValorBaseDiaria = valorBaseDiaria;
        }

        // Propriedade pública para o Identificador
        public string Identificador
        {
            get { return identificador; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O identificador não pode ser nulo ou vazio.");
                identificador = value; 
            }
        }

        // Propriedade pública para a Placa (com validação exigida no requisito)
        public string Placa
        {
            get { return placa; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("A placa não pode ser nula ou vazia.");
                placa = value.Trim().ToUpper(); 
            }
        }

        // Propriedade pública para o Modelo
        public string Modelo
        {
            get { return modelo; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O modelo não pode ser nulo ou vazio.");
                modelo = value; 
            }
        }

        // Propriedade pública para o Valor Base da Diária
        public decimal ValorBaseDiaria
        {
            get { return valorBaseDiaria; }
            set 
            {
                if (value < 0)
                    throw new ArgumentException("O valor base da diária não pode ser negativo.");
                valorBaseDiaria = value; 
            }
        }

        // Método virtual para exibir os detalhes (preparado para o polimorfismo futuro)
        public virtual void ExibirDetalhes()
        {
            Console.WriteLine($"ID: {Identificador} | Modelo: {Modelo} | Placa: {Placa} | Diária: R$ {ValorBaseDiaria:F2}");
        }
    }
}
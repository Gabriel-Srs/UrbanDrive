using System;
using UrbanDrive.ConsoleApp.Interfaces;

namespace UrbanDrive.ConsoleApp.Models
{
    public class Locacao : IImprimivel
    {
        // Atributos
        private Motorista motorista = null!;
        private Veiculo veiculo = null!;
        private int dias;
        private DateTime dataRetirada;

        // Construtor
        public Locacao(Motorista motorista, Veiculo veiculo, int dias, DateTime dataRetirada)
        {
            Motorista = motorista;
            Veiculo = veiculo;
            Dias = dias;
            DataRetirada = dataRetirada;
            ValidarCnh();
        }

        // Propriedades
        public Motorista Motorista
        {
            get { return motorista; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Motorista), "O motorista não pode ser nulo.");
                motorista = value;
            }
        }

        public Veiculo Veiculo
        {
            get { return veiculo; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Veiculo), "O veículo não pode ser nulo.");
                veiculo = value;
            }
        }

        public int Dias
        {
            get { return dias; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Dias), "A quantidade de dias deve ser maior que zero.");
                dias = value;
            }
        }

        public DateTime DataRetirada
        {
            get { return dataRetirada; }
            private set { dataRetirada = value; }
        }

        // Métodos
        private void ValidarCnh()
        {
            if (Motorista.CNH.EstaVencida(DataRetirada))
                throw new ArgumentException("A CNH do motorista está vencida na data de retirada.", nameof(DataRetirada));
        }

        // Polimorfismo: cada tipo de veículo calcula o próprio aluguel
        public decimal CalcularValorTotal()
        {
            return Veiculo.CalcularAluguel(Dias) * (1 + ConfiguradorSistema.TaxaServico);
        }

        // Relatório (IImprimivel)
        public string GerarRelatorio()
        {
            return $"=== Relatório da locação ===\n" +
                   $"Motorista: {Motorista.Nome} (CNH {Motorista.CNH.Categoria}, válida até {Motorista.CNH.DataValidade:dd/MM/yyyy})\n" +
                   $"Veículo: {Veiculo.Tipo} {Veiculo.Modelo} | Placa: {Veiculo.Placa}\n" +
                   $"Retirada: {DataRetirada:dd/MM/yyyy} | Dias: {Dias}\n" +
                   $"Aluguel do veículo: R$ {Veiculo.CalcularAluguel(Dias):F2}\n" +
                   $"Taxa de serviço: {ConfiguradorSistema.TaxaServico:P0}\n" +
                   $"Total: R$ {CalcularValorTotal():F2}";
        }

        // Exibição
        public override string ToString()
        {
            return $"Locação | Motorista: {Motorista.Nome} | Placa: {Veiculo.Placa} | Dias: {Dias} | Total: R$ {CalcularValorTotal():F2}";
        }
    }
}
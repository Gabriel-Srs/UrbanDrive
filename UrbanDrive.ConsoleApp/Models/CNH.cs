namespace UrbanDrive.ConsoleApp.Models;

public class CNH
{
    private string numero = string.Empty;
    private string categoria = string.Empty;

    public string Numero
    {
        get => numero;
        private set
        {
            if (string.IsNullOrWhiteSpace(value) ||
    value.Length != 11 ||
    !value.All(char.IsDigit))
            {
                throw new ArgumentException(
                    "O número da CNH deve conter exatamente 11 dígitos.",
                    nameof(Numero));
            }
        }
        
    }
    public string Categoria
    {
        get => categoria;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "A categoria da CNH não pode ser vazia.",
                    nameof(Categoria));
            }

            categoria = value;
        }
    }

    public DateTime DataValidade { get; private set; }

    public CNH(string numero, string categoria, DateTime dataValidade)
    {
        Numero = numero;
        Categoria = categoria;
        DataValidade = dataValidade;
    }

    public bool EstaVencida(DateTime data)
    {
        return data > DataValidade;
    }
}
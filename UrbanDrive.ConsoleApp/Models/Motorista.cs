namespace UrbanDrive.ConsoleApp.Models;

public class Motorista
{
    private string nome = string.Empty;

    public string Nome
    {
        get => nome;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "O nome do motorista não pode ser vazio.",
                    nameof(Nome));
            }

            nome = value;
        }
    }

    public CNH CNH { get; private set; }

    public Motorista(
        string nome,
        string numeroCnh,
        string categoriaCnh,
        DateTime dataValidadeCnh)
    {
        Nome = nome;

        CNH = new CNH(
            numeroCnh,
            categoriaCnh,
            dataValidadeCnh);
    }
}
namespace UrbanDrive.ConsoleApp.Interfaces
{
    // Contrato das entidades que geram relatório (RF08)
    public interface IImprimivel
    {
        // Retorna o relatório como texto, sem escrever no Console
        string GerarRelatorio();
    }
}

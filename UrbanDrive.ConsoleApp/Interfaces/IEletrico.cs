namespace UrbanDrive.ConsoleApp.Interfaces
{
    // Contrato dos veículos elétricos (RF09)
    public interface IEletrico
    {
        int AutonomiaBateriaKm { get; }
        int CargaBateriaPercentual { get; }

        // Recarrega a bateria até 100% e retorna a descrição, sem escrever no Console
        string Recarregar();
    }
}

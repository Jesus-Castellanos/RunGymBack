namespace RunGym.Repositorios.Interfaces
{
    public interface IEmailServiceReposytory
    {
        Task EnviarCorreoAsync(string destinatario, string asunto, string contenido);
    }
}

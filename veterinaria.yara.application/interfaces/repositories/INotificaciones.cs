namespace veterinaria.yara.application.interfaces.repositories
{
    public interface INotificaciones
    {
        Task<bool> NotificacionOfertas(string message);
        Task<bool> NotificacionUsuario(string message, Guid idUsuario);
    }
}

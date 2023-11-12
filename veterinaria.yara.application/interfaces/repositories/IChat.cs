using veterinaria_yara_core.domain.DTOs;

namespace veterinaria_yara_core.application.interfaces.repositories
{
    public interface IChat
    {
        Task Insertar(MensajeDTO idMascota);
    }
}

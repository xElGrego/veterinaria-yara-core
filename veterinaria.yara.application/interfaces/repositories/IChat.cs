using veterinaria.yara.domain.DTOs;

namespace veterinaria.yara.application.interfaces.repositories
{
    public interface IChat
    {
        Task Insertar(MensajeDTO idMascota);
    }
}

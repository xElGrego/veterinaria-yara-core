namespace veterinaria.yara.domain.DTOs
{
    public class MensajeDTO
    {
        public Guid? RemitenteId { get; set; }
        public Guid? DestinatarioId { get; set; }
        public string? Contenido { get; set; }
        public DateTime? FechaEnvio { get; set; }
    }
}

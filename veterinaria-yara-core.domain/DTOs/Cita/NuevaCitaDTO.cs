namespace veterinaria_yara_core.domain.DTOs.Cita
{
    public class NuevaCitaDTO
    {
        public Guid IdCita { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentarios { get; set; } = null!;
        public Guid? IdEstadoCita { get; set; }
        public Guid? IdUsuario { get; set; }
        public Guid? IdMascota { get; set; }
        public Guid? IdTipoCita { get; set; }
    }
}

namespace veterinaria_yara_core.domain.DTOs.Estados.Mascota
{
    public class NuevaMascotaDto
    {
        public Guid IdMascota { get; set; }
        public Guid IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string? Mote { get; set; }
        public int? Edad { get; set; }
        public decimal? Peso { get; set; }
        public Guid IdRaza { get; set; }
    }
}

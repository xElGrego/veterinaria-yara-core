namespace veterinaria.yara.domain.DTOs.Mascota
{
    public class EditarMascotaDTO
    {
        public Guid IdMascota { get; set; }
        public string Nombre { get; set; }
        public string? Mote { get; set; }
        public int? Edad { get; set; }
        public decimal? Peso { get; set; }
        public Guid IdRaza { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}

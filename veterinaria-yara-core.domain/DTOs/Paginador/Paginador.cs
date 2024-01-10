namespace veterinaria_yara_core.domain.DTOs.Paginador
{
    public class Paginador<T>
    {
        public int PaginaActual { get; set; }
        public int RegistrosPorPagina { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
        public string? BusquedaActual { get; set; }
        public string? OrdenActual { get; set; }
        public string? TipoOrdenActual { get; set; }
        public IEnumerable<T>? Resultado { get; set; }
    }
}

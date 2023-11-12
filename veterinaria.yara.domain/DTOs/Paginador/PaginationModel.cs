namespace veterinaria_yara_core.domain.DTOs.Paginador
{
    public class PaginationModel
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Returned { get; set; }
        public int Total { get; set; }
    }

    public class PaginationFilterResponse<T>
    {
        public IList<T> consulta { set; get; }
        public PaginationModel pagination { get; set; }
        public PaginationFilterResponse()
        {
            consulta = new List<T>();
            pagination = new PaginationModel();
        }
    }
}

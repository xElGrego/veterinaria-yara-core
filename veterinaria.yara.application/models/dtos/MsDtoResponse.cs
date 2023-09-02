namespace veterinaria.yara.application.models.dtos
{
    public class MsDtoResponse<T>
    {
        public T data { get; set; }
        public bool error { get; set; }
        public string msgRetorno { get; set; }
        public int code { get; set; }

        public MsDtoResponse(T data)
        {
          
            this.data = data;
            code = 200;
            msgRetorno = "OK";
            error = false;
        }
    }
}

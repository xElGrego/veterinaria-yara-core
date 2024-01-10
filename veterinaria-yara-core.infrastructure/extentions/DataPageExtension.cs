using AutoMapper;
using Microsoft.EntityFrameworkCore;
using veterinaria_yara_core.domain.DTOs.Paginador;

public static class DataPageExtension
{
    public static async Task<Paginador<T>> PaginateSearchAsync<T>(
        this IQueryable<T> query,
        string buscar,
        int pagina,
        int totalPaginas
        )
    {
        Paginador<T> paged = new();
        int _TotalRegistros = 0;
        int _TotalPaginas = 0;

        List<T> consulta = await query.Skip((pagina - 1) * totalPaginas)
                                         .Take(totalPaginas)
                                         .ToListAsync();
        // Número total de páginas de la tabla Customers
        _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / totalPaginas);

        paged = new Paginador<T>()
        {
            RegistrosPorPagina = totalPaginas,
            TotalRegistros = _TotalRegistros,
            TotalPaginas = _TotalPaginas,
            PaginaActual = pagina,
            BusquedaActual = buscar,
            Resultado = consulta
        };

        return paged;
    }


    public static async Task<PaginationFilterResponse<T>> PaginationAsync<S, T>(
        this IQueryable<S> query,
        int startRow,
        int limit,
        IMapper _mapper
        )
    {
        PaginationFilterResponse<T> paged = new PaginationFilterResponse<T>();
        paged.pagination.Limit = limit;
        List<S> consulta = await query.Skip(startRow).Take(limit).ToListAsync();
        paged.consulta = _mapper.Map<List<T>>(consulta);
        var totalItemsCountTask = await query.CountAsync();
        paged.pagination.Total = totalItemsCountTask;
        paged.pagination.Returned = consulta.Count();
        paged.pagination.Offset = startRow;
        return paged;
    }

    public static async Task<PaginationFilterResponse<T>> PaginationAsync<T>(
    this IQueryable<T> query,
    int startRow,
    int limit,
    int totalRegistros,
    IMapper _mapper
    )
    {
        PaginationFilterResponse<T> paged = new PaginationFilterResponse<T>();
        paged.pagination.Limit = limit;
        List<T> consulta = await query.Skip(startRow).Take(limit).ToListAsync();
        paged.consulta = _mapper.Map<List<T>>(consulta);
        //int totalItemsCountTask = await query.CountAsync();
        paged.pagination.Total = totalRegistros;
        paged.pagination.Returned = consulta.Count();
        paged.pagination.Offset = startRow;
        return paged;
    }

}

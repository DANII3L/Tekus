using LibraryDBApi.Models;

namespace ApiRestTekus.Models
{
    public class PagedResult<T>
    {
        //
        // Summary:
        //     Lista de modelo de valores encontrados
        public IEnumerable<T> ListFind { get; set; } = new List<T>();
        
        //
        // Summary:
        //     Total de valores encontrados
        public int? TotalRecords { get; set; } = 0;

        //
        // Summary:
        //     Número de página actual (para paginación)
        public int? PageNumber { get; set; } = 1;

        //
        // Summary:
        //     Tamaño de la página (cantidad de registros por página para paginación)
        public int? PageSize { get; set; } = 10;

        public static PagedResult<T> PaginatedResponse(StoredProcedureResult<IEnumerable<T>> spResult)
        {
            return new PagedResult<T>
            {
                ListFind = spResult.Data ?? new List<T>(),
                TotalRecords = spResult.TotalRecords ?? 0,
                PageNumber = spResult.PageNumber ?? 1,
                PageSize = spResult.PageSize ?? 10
            };
        }
    }
} 

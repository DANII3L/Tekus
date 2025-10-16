using LibraryDBApi.Models;

public interface IData
{
    Task<StoredProcedureResult<IEnumerable<TResult>>> EjecutarProcedimientoAsync<TResult>(string nombreProcedimiento, object parametros) where TResult : new();
    Task<StoredProcedureResult<IEnumerable<TResult>>> EjecutarProcedimientoAsync<TResult>(string procedureName) where TResult : new();
}

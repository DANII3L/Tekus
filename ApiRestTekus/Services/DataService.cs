using LibraryDBApi.Core;
using LibraryDBApi.Models;
using LibStoredProcedureParameters = LibraryDBApi.Models.StoredProcedureParameters;
using LibModelPaginacion = LibraryDBApi.Models.ModelPaginacion;

public class DataService : BaseDataService, IData
{
    private readonly IConfiguration _configuration;
    private readonly string connectionString;

    public DataService(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration.GetConnectionString("DBConnection")!;
    }
    public async Task<StoredProcedureResult<IEnumerable<TResult>>> EjecutarProcedimientoAsync<TResult>(string nombreProcedimiento, object parametros) where TResult : new()
    {
        var libParameters = new LibStoredProcedureParameters
        {
            ConnectionString = this.connectionString,
            ProcedureName = nombreProcedimiento,
            Model = parametros
        };

        return await base.EjecutarProcedimientoAsync<TResult>(libParameters);
    }
    public async Task<StoredProcedureResult<IEnumerable<TResult>>> EjecutarProcedimientoAsync<TResult>(string procedureName) where TResult : new() => await base.EjecutarProcedimientoAsync<TResult>(connectionString, procedureName);
}
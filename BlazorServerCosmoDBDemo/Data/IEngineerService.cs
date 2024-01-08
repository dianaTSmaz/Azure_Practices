namespace BlazorServerCosmoDBDemo.Data
{
    public interface IEngineerService
    {

    
        Task AddEngineer(Engineer engineer);

        Task<List<Engineer>> GetEngineerDetails();
        Task UpsertEngineer(Engineer engineer);
        Task DeleteEngineer(string? id, string? PartitionKey);

        Task<Engineer> GetEngineerDetailsById(string? Id, string? partitionKey);
    }
}

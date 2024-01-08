using Microsoft.Azure.Cosmos;
using System.Configuration;
using System.Linq.Expressions;

namespace BlazorServerCosmoDBDemo.Data
{
    public class EngineerService:IEngineerService
    {

        //iInformation to connect the cosmos DB Service



        //CosmosDbAcconuntUrl and AccountPrymaryKey split the DbCOnnectionString , so you can use the Db Connection String to get more information about the service
        //private readonly string CosmosDBAccountUrl = "https://localhost:8081";
        //private readonly string CosmosDBAccountPrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private readonly string CosmosDbConnectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private readonly string CosmosDbName = "Engineers";
        private readonly string CosmosDbContainerName = "Container1";


        private Container GetContainerClient()
        {
            //Creacion del cliente para establecer la conexión
            var cosmosDbClient = new CosmosClient(CosmosDbConnectionString);

            //Para obtener la base de datos y el contenedor usamos el metodo GetContainer, como primer parametro
            //usa ek nombre de la base de datos y como segundo parametro el nombre del contenedor
            var container = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
            return container;
        }

        public async Task AddEngineer(Engineer engineer) 
        {
            try
            {
                engineer.Id = Guid.NewGuid();
                var container = GetContainerClient();

                //Some methods we can se are create, delete, get,etc
                var response = await container.CreateItemAsync(engineer, new PartitionKey(engineer.Id.ToString()));
                Console.WriteLine(response.StatusCode);
            
            }
            catch(Exception ex) {

                //Console.WriteLine(ex.Message);
                ex.Message.ToString();

                
            }

        }

        //Method to get all the engneers which are in the containers
        /*public async Task<List<Engineer>> GetEngineerDetails() 
        {
            List<Engineer>engineers= new List<Engineer>();
            try
            {
                var container = GetContainerClient();
                var sqlQuery = "SELECT * FROM C";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
                FeedIterator<Engineer> queryResultSetIterator = container.GetItemQueryIterator<Engineer>();

                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<Engineer> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (Engineer engineer in currentResultSet)
                    {
                        engineers.Add(engineer);
                    }
                }


            }
            catch (Exception e) {
                e.Message.ToString();
         
            
           }


            return engineers;

        }*/

        public async Task UpsertEngineer(Engineer engineer) 
        {
            try
            {
                var container = GetContainerClient();
                var updateRes = await container.UpsertItemAsync(engineer, new PartitionKey(engineer.Id.ToString()));
                Console.Write(updateRes.StatusCode);
            }
            catch (Exception ex) {

                throw new Exception("Exception:", ex);
            
            }
        }

        public async Task DeleteEngineer(string ? id, string? PartitionKey) 
        {
            try
            {
                var container = GetContainerClient();
                var response = await container.DeleteItemAsync<Engineer>(id, new PartitionKey(PartitionKey));
            }
            catch (Exception ex) 
            {
                //This excetion is used to clean the stack tree.
                throw new Exception("Exception", ex);
            }
        
        }

        public async Task<List<Engineer>> GetEngineerDetails() 
        {
            List<Engineer> engineers = new List<Engineer>();

            try
            {
                var container = GetContainerClient();
                var sqlQuery = "SELECT * FROM c";
                QueryDefinition querydefinitioon = new QueryDefinition(sqlQuery);
                FeedIterator<Engineer> queryResultSetIterator = container.GetItemQueryIterator<Engineer>(querydefinitioon);

                while (queryResultSetIterator.HasMoreResults)
                {

                    FeedResponse<Engineer> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (Engineer engineer in currentResultSet)
                    {
                        engineers.Add(engineer);
                    }
                }
            }
            catch (Exception ex) 
            {
                ex.Message.ToString();
            }
            return engineers;
        }

        public async Task<Engineer> GetEngineerDetailsById(string? Id, string? partitionKey) 
        {
            try {
                var container  = GetContainerClient();
                ItemResponse<Engineer> response = await container.ReadItemAsync<Engineer>(Id, new PartitionKey(partitionKey));
                return response.Resource;
            }
            catch(Exception ex) {

                throw new Exception("Exception", ex);
            }
        }


       




    }


}

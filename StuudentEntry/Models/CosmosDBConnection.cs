using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StuudentEntry.Models
{

    public class CosmosDBConnection
    {
        private string EndpointUrl = Environment.GetEnvironmentVariable("EndpointUrl");
        private string PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");
        private CosmosClient cosmosClient;
        private Database database;
        private Container container;
        private string databaseId = "Students";
        private string containerId = "Studentcontainer";
        private string markscontainerId = "Markscontainer";

        private async Task CreateDatabaseAsync()
        {
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        }
        private async Task CreateContainerAsync()
        {
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/id");
        }
        private async Task CreateMarksContainerAsync()
        {
            this.container = await this.database.CreateContainerIfNotExistsAsync(markscontainerId, "/Id");
        }
        private async Task AddItemsToContainerAsync(Student s)
        {
                ItemResponse<Student> studentadd = await this.container.CreateItemAsync<Student>(s, new PartitionKey(s.Id));

        }
        private async Task AddMarksToContainerAsync(Marks marks)
        {
            ItemResponse<Marks> studentadd = await this.container.CreateItemAsync<Marks>(marks);

        }
        public async Task GetStartedDemoAsync(Student student, String url,string key)
        {
            this.cosmosClient = new CosmosClient(url, key);
            await this.CreateDatabaseAsync();
            await this.CreateContainerAsync();
            await this.AddItemsToContainerAsync(student);
            //await this.QueryItemsAsync();
        }
        public async Task MarksAdder(string url, string key, Marks marks)
        {
            this.cosmosClient=new CosmosClient(url, key);
            await this.CreateDatabaseAsync();
            await this.CreateMarksContainerAsync();
            await this.AddMarksToContainerAsync(marks);

        }
    }
}
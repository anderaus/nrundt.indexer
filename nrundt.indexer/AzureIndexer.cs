using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace nrundt.indexer
{
    internal class AzureIndexer : IDisposable
    {
        private readonly SearchServiceClient _serviceClient;

        public AzureIndexer(string searchServiceName, string searchServiceAdminApiKey)
        {
            _serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(searchServiceAdminApiKey));
        }

        public void IndexDocuments<T>(string indexName, IEnumerable<T> documents) where T : class
        {
            DeleteIndexIfExists(indexName);
            CreateIndex<T>(indexName);

            var batch = IndexBatch.Upload(documents);
            try
            {
                // Note: it's bad practice to get index client from service client as the admin key will be used. 
                // Should rather create separate key for the various index clients in a real-world app
                var indexResult = _serviceClient.Indexes.GetClient(indexName).Documents.Index(batch);
                Console.WriteLine($"Indexed {indexResult.Results.Count} documents!");
            }
            catch (IndexBatchException e)
            {
                Console.WriteLine(
                    "Failed to index documents: {0}",
                    string.Join(", ", e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key)));
            }
        }

        private void DeleteIndexIfExists(string indexName)
        {
            if (_serviceClient.Indexes.Exists(indexName))
            {
                _serviceClient.Indexes.Delete(indexName);
            }
        }

        private void CreateIndex<T>(string indexName)
        {
            var definition = new Index
            {
                Name = indexName,
                Fields = FieldBuilder.BuildForType<T>()
            };

            _serviceClient.Indexes.Create(definition);
        }

        public void Dispose()
        {
            _serviceClient?.Dispose();
        }
    }
}
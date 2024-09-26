using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Collections.Generic;
using System.Linq;
using Azure.Messaging.EventHubs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Dapper;
using ReceiverEventHub.Infrastructure.Models;

namespace ReceiverEventHub
{
    public static class EventHubConsumerSQLFunction
    {
        private static readonly string SqlConnection = Environment.GetEnvironmentVariable("SqlConnection");

        [FunctionName("RecieveEvents")]
        public static async Task Run([EventHubTrigger("EventHubName", Connection = "EventHubConnection")] EventData[] events, ILogger log)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
                  {
                try
                {
                    var messageBody = System.Text.Encoding.UTF8.GetString(eventData.EventBody);
                    var content = JsonConvert.DeserializeObject<ContentPlayLog>(messageBody);

                    // Replace these two lines with your processing logic.
                    log.LogInformation($"C# Event Hub trigger function processed a message: {messageBody}");
                    /*await Task.Yield()*/

                    Console.WriteLine("Data:", content);
                    //await SaveToSqlServerAsync(ContentPlayLog);
                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
                    exceptions.Add(e);
                    log.LogError($"Exception: {e.Message}");
                }
            }

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }

        public static async Task SaveToSqlServerAsync(Order order)
        {
            using (var connection = new SqlConnection(SqlConnection))
            {
                await connection.OpenAsync();

                var query = @"
                        INSERT INTO Orders (Product, Quantity, CreatedAt)
                        VALUES (@Product, @Quantity, @CreatedAt)";

                var parameters = new
                {
                    Product = order.Product,
                    Quantity = order.Quantity,
                    CreatedAt = order.CreatedAt
                };

                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}

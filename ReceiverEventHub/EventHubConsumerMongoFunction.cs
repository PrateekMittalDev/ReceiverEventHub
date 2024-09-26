//using System;
//using System.Data.SqlClient;
//using System.Threading.Tasks;
//using Microsoft.Azure.WebJobs;
//using System.Collections.Generic;
//using System.Linq;
//using Azure.Messaging.EventHubs;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Dapper;
//using ReceiverEventHub.Infrastructure.Models;
//using MongoDB.Driver;
//using MongoDB.Bson;

//namespace ReceiverEventHub
//{
//    public class EventHubConsumerMongoFunction
//    {
//        private readonly IMongoCollection<Order> _orderCollection;

//        public EventHubConsumerMongoFunction(IMongoClient mongoClient)
//        {
//            var database = mongoClient.GetDatabase("");
//            _orderCollection = database.GetCollection<Order>("orders");
//        }

//        [FunctionName("RecieveEvents")]
//        public static async Task Run([EventHubTrigger("EventHubName", Connection = "EventHubConnection")] EventData[] events, ILogger log)
//        {
//            var exceptions = new List<Exception>();

//            foreach (EventData eventData in events)
//            {
//                try
//                {
//                    var messageBody = System.Text.Encoding.UTF8.GetString(eventData.EventBody);
//                    var order = JsonConvert.DeserializeObject<Order>(messageBody);

//                    // Replace these two lines with your processing logic.
//                    log.LogInformation($"C# Event Hub trigger function processed a message: {messageBody}");
//                    /*await Task.Yield()*/

//                    await SaveToMongoAsync(order);
//                }
//                catch (Exception e)
//                {
//                    // We need to keep processing the rest of the batch - capture this exception and continue.
//                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
//                    exceptions.Add(e);
//                    log.LogError($"Exception: {e.Message}");
//                }
//            }

//            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

//            if (exceptions.Count > 1)
//                throw new AggregateException(exceptions);

//            if (exceptions.Count == 1)
//                throw exceptions.Single();
//        }

//        public async Task SaveToMongoAsync(Order order)
//        {
//            await _orderCollection.InsertOneAsync(order);
//        }
//    }
//}

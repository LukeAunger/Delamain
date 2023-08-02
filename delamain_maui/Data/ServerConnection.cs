using System;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Transports;

namespace delamain_maui.Data
{
    public class ServerConnection
    {
        private static string connectionstring = "http://192.168.1.101:5041";
        private static readonly HttpClient sharedClient = new()
        {
            BaseAddress = new Uri(connectionstring)
        };

        private HubConnection? _connection;
        private string queue = string.Empty;
        private string message = string.Empty;

        public async Task<string> Connect(string key)
        {
            try
            {
                _connection = new HubConnectionBuilder()
                 .WithUrl($"{connectionstring}/QueueUpdate?key={key}")
                 .WithAutomaticReconnect()
                 .Build();
                _connection.On<string>("MessageReceived", (counter) =>
                {
                    queue = counter;
                });

                await _connection.StartAsync();
                return(queue);
            }
            catch (AggregateException e)
            {
                return queue = "Failed connection";
            }
            catch (Exception ex)
            {
               return queue = "Failed connection";
            }
        }


        public bool IsConnected => _connection?.State == HubConnectionState.Connected;
        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync();
            }
        }


        //I have built a controller and will migrate a table in the database to store all hospital location in the uk
        // the httpclient just needs setting up when ready
        public string gethosptials()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var endpoint = new Uri($"{connectionstring}/hospitals");
                    var result = client.GetAsync(endpoint).Result;
                    var Json = result.Content.ReadAsStringAsync().Result;
                    return (Json);
                }
                catch(Exception e)
                {
                    return ("Connection to internet failed backout and try again");
                }
            }
        }

        public string testConnect()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var endpoint = new Uri($"{connectionstring}/ping");
                    var result = client.GetAsync(endpoint).Result;
                    var Json = result.Content.ReadAsStringAsync().Result;
                    return (Json);
                }
                catch(Exception e)
                {
                    return ("Connection to internet failed backout and try again ");
                }
            }
        }

        public async Task<string> call(patient_call item)
        {
            try
            {
                var jsonContent = (JsonSerializer.Serialize(item));
                var client = sharedClient;
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/emergancy", content);
                var responseString = await response.Content.ReadAsStringAsync();
                return (responseString);
            }
            catch(Exception e)
            {
                return (null);
            }
        }
    }
}


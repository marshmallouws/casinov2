using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Casino.Client.Services
{
    public class HubConnector : IAsyncDisposable
    {
        public HubConnection hubConnection { get; set; }

        public  HubConnector(string player)
        { 
        }

        public async Task Test()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:7123/gamehub?playername=Lars")
                .Build();

            await hubConnection.StartAsync();
        }

        public async Task Start()
        {
            await hubConnection.StartAsync();
            hubConnection.On<string, string>("RecieveMessage", async (username, message) =>
            { 
            });

        }

        public async ValueTask DisposeAsync()
        {
            if (hubConnection != null)
            {
                await hubConnection.DisposeAsync();
            }
        }
    }
}

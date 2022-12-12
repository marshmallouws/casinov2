using Casino.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Casino.Client.Services
{

    public delegate Task recieveMessage(string str);

    public class HubConnector
    {
        public event recieveMessage? recieveMessageEvent;
        public HubConnection? hubConnection;
        public string? path;

        public string? test;
        public bool IsConnected => hubConnection?.State == HubConnectionState.Connected;

        // public void NotifyOpponentIsFound() => OpponentIsFound?.Invoke();

        public async Task Connect()
        {
            path = $"http://localhost:7123/gamehub?playername=a";
            hubConnection = new HubConnectionBuilder()
                .WithAutomaticReconnect()
                .Build();

            hubConnection.On<string>("test", (res) =>
            {
                recieveMessageEvent?.Invoke(res);
            });

            await hubConnection.StartAsync();
        }

        public string RecieveMessage(string input)
        {
            return input;
        }


        public async Task Test()
        {
            await hubConnection.SendAsync("test");
        }

        public async Task Start()
        {
            await hubConnection.StartAsync();
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

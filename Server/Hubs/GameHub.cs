using Microsoft.AspNetCore.SignalR;
using Casino.Shared.Models;
using Casino.Server.GameLogic;

namespace Casino.Server.Hubs
{
    public class GameHub : Hub
    {

        private static Dictionary<string, Player> Players = new Dictionary<string, Player>();
        private GamePlay game = new GamePlay();

        public override async Task OnConnectedAsync()
        {

            System.Diagnostics.Debug.WriteLine("This message apears in the debug console in VS");
            string name = string.Empty;
            try
            {
                name = Context.GetHttpContext().Request.Query["playerName"];
                Players.Add(Context.ConnectionId, new Player(name));

            } catch (NullReferenceException e)
            {
                // TODO: Handle null name
                // It should not be possible to play this game without a name.
                // Players that send an empty name should have their browser crashing and burning.
            }

            if (Players.Count == 2)
            {
                // Now there are two players registered in the server, and therefore,
                // we broadcast to them that the game can start.
                await StartGame();
            }
            await base.OnConnectedAsync();
        }

        // With the "Clients" object we can send a <message> to players.
        // A <message> is a handle that the clients can act upon.
        // Below there is the "Others" which broadcast the <message> to everyother
        // client beside the one initating the method.

        public async Task AnnouncePlayer(string username)
        {
            await Clients.Others.SendAsync("Announcement", username);
        }

        public async Task SendMessage(string username, string message)
        {
            await Clients.Others.SendAsync("RecieveMessage", username, message);
        }

        // TODO: Divide clients into groups of two to have multiple players at once
        public async Task StartGame()
        {
            var players = Players.Values.ToList();
            var p1 = players[0];
            var p2 = players[1];

            game.AddPlayers(p1, p2);

            await Clients.All.SendAsync("14", "start the game already");
        }

        public async Task ShowTableCards()
        {
            if(game.GameIsReady())
            {
                var cards = game.DealCards();
                await Clients.All.SendAsync("firstDeal", cards);
            }
        }
    }
}

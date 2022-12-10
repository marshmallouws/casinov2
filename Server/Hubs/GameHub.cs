using Microsoft.AspNetCore.SignalR;
using Casino.Shared.Models;
using Casino.Server.GameLogic;
using System.Text.Json;

namespace Casino.Server.Hubs
{
    public class GameHub : Hub
    {
        //private static readonly ClientConnectionMap<string> _connections = new ClientConnectionMap<string>();
        private static Dictionary<string, Player> players = new Dictionary<string, Player>();
        private static GamePlay game = new GamePlay();

        public override async Task OnConnectedAsync()
        {
            string name = string.Empty;
            try
            {
                name = Context.GetHttpContext().Request.Query["playerName"];
                players.Add(Context.ConnectionId, new Player(name));
                game.AddPlayer(new Player(name));

                System.Diagnostics.Debug.WriteLine("This message apears in the debug console in VS" + name);
            } catch (NullReferenceException e)
            {
                // TODO: Handle null name
                // It should not be possible to play this game without a name.
                // Players that send an empty name should have their browser crashing and burning.
            }

            if (players.Count == 2)
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

        public async Task AnnounceOpponent(string playerName)
        {
            await Clients.Others.SendAsync("Opponent", playerName);
        }

        public async Task SendMessage(string playerName, string message)
        { 
            await Clients.Others.SendAsync("RecieveMessage", playerName, message);
        }

        // TODO: Divide clients into groups of two to have multiple players at once
        public async Task StartGame()
        { 
            await Clients.All.SendAsync("14", "start the game already");
        }
        
        public async Task ShowTableCards()
        {
            await Clients.All.SendAsync("SendTableCards", game.TableCards);
        }

        public async Task TakeTurn(string currentBuild)
        {
            var cur = JsonSerializer.Deserialize<CardBuild>(currentBuild);
            game.CountPoints(cur);
            System.Diagnostics.Debug.WriteLine("------------------" + game.GetPlayerTurn().Name);
            game.incrementPlayerTurn();
            System.Diagnostics.Debug.WriteLine("------------------" + game.GetPlayerTurn().Name);
            await ShowTableCards();

        }

        public async Task SendPlayerTurn()
        {
            players.TryGetValue(Context.ConnectionId, out var player);
            var isMyTurn = player.Name == game.GetPlayerTurn().Name;
            await Clients.Caller.SendAsync("PlayersTurn", isMyTurn);
        }

        public async Task DealPlayerCards()
        {
            var isLastTurn = false;
            var cards = game.DealCards();

            if (game.GetDeckSize() <= 4)
                isLastTurn = true;

            await Clients.Caller.SendAsync("PlayerCards", cards, isLastTurn);
        }
    }
}

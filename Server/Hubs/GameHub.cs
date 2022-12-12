using Microsoft.AspNetCore.SignalR;
using Casino.Shared.Models;
using Casino.Server.GameLogic;
using System.Text.Json;
using Casino.Server.Model;
using System.Numerics;

namespace Casino.Server.Hubs
{
    public class GameHub : Hub
    {
        private static List<Player> lobby1 = new List<Player>();
        private static List<Group> groups = new List<Group>();

        public override async Task OnConnectedAsync()
        {
            string name = string.Empty;
            try
            {
                name = Context.GetHttpContext().Request.Query["playerName"];
                lobby1.Add(new Player(name, Context.ConnectionId));

                //game.AddPlayer(new Player(name));

                System.Diagnostics.Debug.WriteLine("This message apears in the debug console in VS" + name);
            } catch (NullReferenceException e)
            {
                // TODO: Handle null name
                // It should not be possible to play this game without a name.
                // Players that send an empty name should have their browser crashing and burning.
            }

            if (lobby1.Count == 2)
            {
                // Now there are two players registered in the server, and therefore,
                // we broadcast to them that the game can start.
                var groupName = await CreateNewGroup();
                await StartGame(groupName);
            }
            await base.OnConnectedAsync();
        }

        public async Task Test()
        {
            await Clients.Caller.SendAsync("test", "HEJ MED DIG!!!");
        }

        public async Task<string> CreateNewGroup()
        {
            var groupName = System.Guid.NewGuid().ToString();
            foreach (var p in lobby1)
            {
                await Groups.AddToGroupAsync(p.ConnectionId, groupName);
            }
            groups.Add(new Group(groupName, lobby1));
            await SendGroupName(groupName);

            // Reset lobby
            lobby1 = new List<Player>();
            return groupName;
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

            // With the "Clients" object we can send a <message> to players.
            // A <message> is a handle that the clients can act upon.
            // Below there is the "Others" which broadcast the <message> to everyother
            // client beside the one initating the method.

        public async Task AnnounceOpponent(string playerName, string groupName)
        {
            await Clients.OthersInGroup(groupName).SendAsync("Opponent", playerName);
        }

        public async Task SendGroupName(string groupName)
        {
            await Clients.Group(groupName).SendAsync("group", groupName);
        }

        // TODO: Divide clients into groups of two to have multiple players at once
        public async Task StartGame(string groupName)
        {
            await Clients.Group(groupName).SendAsync("14", "start the game already");
        }
        
        public async Task ShowTableCards(string groupName)
        {
            var game = findGameByGroupName(groupName);
            await Clients.Group(groupName).SendAsync("SendTableCards", game.TableCards);
        }

        public async Task ShowMoveToOpponent(CardBuild currentBuild, string groupName)
        {
            await Clients.OthersInGroup(groupName).SendAsync("opponentMove", currentBuild);
        }
        
        public async Task TakeTurn(string currentBuild, string groupName)
        {
            var game = findGameByGroupName(groupName);
            var cur = JsonSerializer.Deserialize<CardBuild>(currentBuild);
            game.CountPoints(cur);

            await ShowMoveToOpponent(cur, groupName);

            game.IncrementPlayerTurn();

            await ShowTableCards(groupName);

        }

        public async Task SendPlayerTurn(string groupName)
        {
            var game = findGameByGroupName(groupName);
            var current = game.GetPlayerTurn();
            var isMyTurn = current.ConnectionId == Context.ConnectionId;
            await Clients.Caller.SendAsync("PlayersTurn", isMyTurn);
        }

        public async Task DealPlayerCards(string groupName)
        {
            var game = findGameByGroupName(groupName);
            var isLastTurn = false;
            var cards = game.DealCards();

            if (game.GetDeckSize() <= 4)
                isLastTurn = true;

            await Clients.Caller.SendAsync("PlayerCards", cards, isLastTurn);
        }

        private GamePlay findGameByGroupName(string groupName)
        {
            return groups.Find(g => g.GroupName.Equals(groupName)).game;
        }
    }
}

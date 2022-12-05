namespace Casino.Shared.Models
{
    public class Game
    {
        // Will increment for each player and restart when all players have played their turn.
        public int PlayerTurn { get; private set; }
        public Dictionary<string, List<Card>> CardBuilds { get; set; }
        public List<Player> Players { get; set; }

        public Game(Dictionary<string, List<Card>> cardBuilds, List<Player> players)
        {
            CardBuilds = cardBuilds;
            Players = players;
            PlayerTurn = 0;
        }

        public void incrementPlayerTurn()
        {
            try
            {
                var next = PlayerTurn + 1;
                var _ = Players[next];
                PlayerTurn = next;
            }
            catch (Exception)
            {
                PlayerTurn = 0;
            }
        }
    }
}

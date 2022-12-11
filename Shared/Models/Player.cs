namespace Casino.Shared.Models
{
    public class Player
    {
        // Only used in server
        public string? ConnectionId { get; set; }

        public string Name { get; set; }
        public List<Card> Points { get; private set; }

        public Player() { }

        public Player(string _name, string _connectionId)
        {
            Name = _name;
            ConnectionId = _connectionId;
            Points = new List<Card>();
        }

        public Player(string _name)
        {
            Name = _name;
            Points = new List<Card>();
        }

        public void AddPoints(List<Card> cardsWon)
        {
            Points.AddRange(cardsWon);
        }
    }
}

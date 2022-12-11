using Casino.Server.GameLogic;
using Casino.Shared.Models;

namespace Casino.Server.Model
{
    public class Group
    { 
        public string GroupName { get; set; }
        public GamePlay game { get; }
        public Group(string _groupName, List<Player> _players)
        {
            GroupName = _groupName;
            game = new GamePlay(_players);
        }   
    }
}

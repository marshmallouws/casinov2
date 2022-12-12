namespace Casino.Client.Services
{
    public delegate string sendMessage(string str);
    public class GameEvents
    {
        event sendMessage sendMessageEvent;
        public GameEvents()
        {
            this.sendMessageEvent += new sendMessage(SendMessage);
        }

        public string SendMessage(string str)
        {
            return "";
        }
    }
}

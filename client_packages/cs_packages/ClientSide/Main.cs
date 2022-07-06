using RAGE;

namespace ClientSide
{
    public class Main : Events.Script
    {
        public Main()
        {
            Events.OnPlayerReady += OnPlayerReady;
        }
        private void OnPlayerReady()
        {
            RAGE.Chat.Output("You joined!");
        }
    }
}

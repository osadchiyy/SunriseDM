using GTANetworkAPI;

namespace ServerSide
{
    public class Main : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStarted()
        {
            NAPI.Util.ConsoleOutput("Server Started!");
        }
    }
}
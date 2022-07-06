using GTANetworkAPI;

namespace ServerSide
{
    internal class Events : Script
    {
        [ServerEvent(Event.PlayerSpawn)]
        private void _OnPlayerSpawn(Player player)
        {
            player.SendNotification("~g~ You spawned");
            NAPI.Util.ConsoleOutput($"Player {player.Name} spawned!");
        }
        [ServerEvent(Event.PlayerEnterVehicle)]
        private void _OnPlayerEnterVehicle(Player player, Vehicle vehicle, sbyte seatID)
        {
            if (vehicle == null) return;
        }
        [ServerEvent(Event.PlayerExitVehicle)]
        private void _OnPlayerExitVehicle(Player player, Vehicle vehicle)
        {
            if(vehicle == null) return;
        }

        [RemoteEvent("CLIENT:SERVER:RepairCar")]
        private void RepairCar(Player player)
        {
            if (player.Vehicle == null)
            {
                player.SendNotification("~r~ You must be in the car! FROM: SERVER");
                return;
            }
            player.Vehicle.Repair();
        }
    }
}

using GTANetworkAPI;
using System;

namespace ServerSide
{
    internal class Commands : Script
    {
        [Command("createcar", Alias = "veh")]
        public void CreateCar(Player player, string vehicleName, int color1, int color2, string numberplate = "HELPER")
        {
            if (player.IsInVehicle)
            {
                player.SendChatMessage("Get out of the car to spawn the vehicle");
                return;
            }
            var vehicleHash = NAPI.Util.GetHashKey(vehicleName);
            var car = NAPI.Vehicle.CreateVehicle(vehicleHash, player.Position, player.Heading, color1, color2);
            car.NumberPlate = numberplate;
            car.Locked = false;
            car.EngineStatus = true;
            player.SetIntoVehicle(car, (int)VehicleSeat.Driver);
            player.SendChatMessage("The car has been successfully spawned");
        }
        [Command("sethp", Alias = "hp")]
        public void SetHp(Player player, int amount)
        {
            player.Health = amount;
            player.SendChatMessage($"Health set to {amount}");
        }
        [Command("setarmor", Alias = "armor")]
        public void SetArmor(Player player, int amount)
        {
            player.Armor = amount;
            player.SendChatMessage($"Armor set to {amount}");
        }
        [Command("healme")]
        public void HealMe(Player player)
        {
            if (player.Health >= 80)
            {
                player.SendNotification("You are so healthy!");
                return;
            }
            player.Health = 100;
            player.SendNotification("You have successfully healed");
        }
        [Command("teleport", Alias = "tp")]
        public void TeleportPlayer(Player player, float x, float y, float z)
        {
            player.Position = new Vector3(x, y, z);
            player.SendChatMessage("Teleport implemented");
        }
        [Command("me", GreedyArg = true)]
        public void TypeMe(Player player, string action)
        {
            player.SendChatMessage($"{player.Name} did that: " + action);
        }
        [Command("setweather")]
        public void SetWeather(Player player, string weather)
        {
            NAPI.World.SetWeather(weather);
            player.SendChatMessage("The weather has been changed to: ~g~" + weather);
        }
        [Command("settime")]
        public void SetTime(Player player, int hours, int minutes = 0, int seconds = 0)
        {
            if (hours >= 24 || minutes >= 60 || seconds >= 60 || hours < 0 || minutes < 0 || seconds < 0)
            {
                player.SendChatMessage("You entered the wrong time");
                return;
            }
            NAPI.World.SetTime(hours, minutes, seconds);
            player.SendChatMessage($"Time successfully changed to ~g~{hours} hour {minutes} minutes {seconds} seconds~w~");
        }
        [Command("giveweapon", Alias = "givegun")]
        public void GiveWeaponPlayer(Player player, WeaponHash weaponType, int ammo)
        {
            player.GiveWeapon(weaponType, ammo);
            player.SendChatMessage("Weapons issued successfully");
        }
        /*
        [Command("repaircar", Alias = "repair")]
        public void RepairCar(Player player)
        {
            if (!player.IsInVehicle)
            {
                player.SendChatMessage("You must be in the car to use this command!");
                return;
            }
            player.Vehicle.Repair();
            player.SendChatMessage("You have successfully repaired the vehicle!");
        }
        */
        [Command("addnpc")]
        public void AddNPC(Player player, PedHash pedHash)
        {
            Vector3 PlayerPos = NAPI.Entity.GetEntityPosition(player);
            Ped John = NAPI.Ped.CreatePed((uint)pedHash, new Vector3(PlayerPos.X + 1f, PlayerPos.Y + 1f, PlayerPos.Z + 1f), 5f, true, false, false, false, 0);
            NAPI.Chat.SendChatMessageToPlayer(player, $"Игрок: ~g~{player.Name} ~w~заспавнил ~g~NPC: {pedHash}!");
        }
        [Command("marker", Alias = "m")]
        public void Marker(Player player, uint markerType)
        {
            NAPI.Marker.CreateMarker(markerType, player.Position, new Vector3(), new Vector3(), 1f, new Color(255, 0, 0, 100), false, player.Dimension);
        }
        private Checkpoint _prevCheckpoint;
        [Command("checkpoint")]
        public void CheckPoint(Player player, uint checkpointType)
        {
            var direction = _prevCheckpoint?.Position ?? player.Position;
            _prevCheckpoint = NAPI.Checkpoint.CreateCheckpoint(checkpointType, player.Position + new Vector3(0f, 0f, -1f), direction, 1f ,new Color(255, 0, 0, 100), player.Dimension);
        }
        [Command("blip")]
        public void Blip(Player player, uint sprite, byte color, string name, bool shortRange)
        {
            NAPI.Blip.CreateBlip(sprite, player.Position, 1f, color, name, 255, 0f, shortRange, 0, player.Dimension);
        }
        [Command("colshape")]
        public void Colshape(Player player, float scale)
        {
            var position = player.Position + new Vector3(0f, 0f, -1f);

            var colShape = NAPI.ColShape.CreateCylinderColShape(position, scale, 2f, player.Dimension);

            colShape.SetData(nameof(GTANetworkAPI.Marker), NAPI.Marker.CreateMarker(1, position, new Vector3(), new Vector3(), scale * 2, new Color(255, 0, 0, 100), false, player.Dimension));

            colShape.OnEntityEnterColShape += OnEntityEnterColShape;
            colShape.OnEntityExitColShape += OnEntityExitColShape;
        }
        private void OnEntityEnterColShape(ColShape colShape, Player player)
        {
            player.SendNotification("~g~ Вы вошли в ColShape!");
        }
        private void OnEntityExitColShape(ColShape colShape, Player player)
        {
            player.SendNotification("~r~ Вы вышли из ColShape!");
        }
        [Command("randomizeme")]
        private void RadomizeMe(Player player)
        {
            player.TriggerEvent("SERVER:CLIENT:RandomizePlayer");
        }
    }
}
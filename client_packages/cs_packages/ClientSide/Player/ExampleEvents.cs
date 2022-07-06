using RAGE;
using RAGE.Elements;
using RAGE.Ui;
using System;

namespace ClientSide.Player
{
    internal class ExampleEvents : Events.Script
    {
        public ExampleEvents()
        {
            /* Events.OnPlayerEnterColshape += OnPlayerEnterColshape;
            Events.OnPlayerExitColshape += OnPlayerExitColshape; */
            Events.Add("SERVER:CLIENT:RandomizePlayer", RandomizePlayer);

            RAGE.Input.Bind(VirtualKeys.F5, true, () =>
            {
                if (RAGE.Elements.Player.LocalPlayer.Vehicle == null)
                {
                    RAGE.Chat.Output("You're not in car! FROM: CLIENT");
                    return;
                }
                Events.CallRemote("CLIENT:SERVER:RepairCar");
            });
        }

        private void RandomizePlayer(object[] args)
        {
            Random rnd = new Random();
            RAGE.Elements.Player.LocalPlayer.SetHeadBlendData(
                rnd.Next(0, 3),
                rnd.Next(0, 3),
                rnd.Next(0, 3),
                rnd.Next(0, 3),
                rnd.Next(0, 3),
                rnd.Next(0, 3),
                0.5f,
                0.5f,
                0.5f,
                false);
        }
        /* private void OnPlayerEnterColshape(Colshape colshape, Events.CancelEventArgs cancel)
        {
            RAGE.Chat.Output("You entered the colshape");
        }
        private void OnPlayerExitColshape(Colshape colshape, Events. CancelEventArgs cancel)
        {
            RAGE.Chat.Output("You exited the colshape");
        } */
    }
}

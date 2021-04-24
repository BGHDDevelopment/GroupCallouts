using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace GroupAttack
{

    [CalloutProperties("Group Attack", "BGHDDevelopment", "1.1.0")]
    public class GroupAttack : Callout
    {
        Ped suspect, suspect2, suspect3, victim;
        public GroupAttack()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));

            ShortName = "Group Attack";
            CalloutDescription = "3 armed suspects are attacking a civilian!";
            ResponseCode = 3;
            StartDistance = 120f;
        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
                        suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            suspect2 = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            suspect3 = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            victim = await SpawnPed(RandomUtils.GetRandomPed(), Location);

            //Suspect 1
            PedData data = new PedData();
            List<Item> items = new List<Item>();
            data.BloodAlcoholLevel = 0.02;
            Item Bottle = new Item {
                Name = "Bottle",
                IsIllegal = false
            };
            items.Add(Bottle);
            data.Items = items;
            Utilities.SetPedData(suspect.NetworkId,data);
            
            //Suspect 2
            PedData data2 = new PedData();
            List<Item> items2 = new List<Item>();
            Item Crowbar = new Item {
                Name = "Crowbar",
                IsIllegal = false
            };
            items2.Add(Crowbar);
            data2.Items = items2;
            Utilities.SetPedData(suspect2.NetworkId,data2);
            
            //Suspect 3
            PedData data3 = new PedData();
            List<Item> items3 = new List<Item>();
            Item GolfClub = new Item {
                Name = "GolfClub",
                IsIllegal = false
            };
            items3.Add(GolfClub);
            data3.Items = items3;
            Utilities.SetPedData(suspect3.NetworkId,data3);
            
            //Victim
            PedData data4 = new PedData();
            List<Item> items4 = new List<Item>();
            Item Purse = new Item {
                Name = "Purse",
                IsIllegal = false
            };
            items4.Add(Purse);
            data4.Items = items4;
            Utilities.SetPedData(victim.NetworkId,data4);

            //Tasks
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            suspect2.AlwaysKeepTask = true;
            suspect2.BlockPermanentEvents = true;
            suspect3.AlwaysKeepTask = true;
            suspect3.BlockPermanentEvents = true;
            victim.AlwaysKeepTask = true;
            victim.BlockPermanentEvents = true;
            
            suspect.AttachBlip();
            suspect2.AttachBlip();
            suspect3.AttachBlip();
            victim.AttachBlip();
            dynamic playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~o~Officer ~b~" + displayName + ", ~o~reports show three individuals are fighting!");

            //SUSPECT 1
            suspect.Weapons.Give(WeaponHash.Bottle, 1, true, true);
            suspect.Task.FightAgainst(victim);
            //SUSPECT 2
            suspect2.Weapons.Give(WeaponHash.Crowbar, 1, true, true);
            suspect2.Task.FightAgainst(victim);
            //SUSPECT 3
            suspect3.Weapons.Give(WeaponHash.GolfClub, 1, true, true);
            suspect3.Task.FightAgainst(victim);
            victim.Task.ReactAndFlee(suspect);
            PedData data1 = await Utilities.GetPedData(suspect.NetworkId);
            string firstname = data1.FirstName;
            PedData data5 = await Utilities.GetPedData(suspect2.NetworkId);
            string firstname2 = data5.FirstName;
            PedData data6 = await Utilities.GetPedData(suspect3.NetworkId);
            string firstname3 = data6.FirstName;
            PedData data7 = await Utilities.GetPedData(victim.NetworkId);
            string firstname4 = data7.FirstName;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~You were not supposed to see that!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname2 + "] ~s~Come back here!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname3 + "] ~s~Your dead!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname4 + "] ~s~Help Me! HELP PLEASE!", 5000);
        }
        private void Notify(string message)
        {
            API.BeginTextCommandThefeedPost("STRING");
            API.AddTextComponentSubstringPlayerName(message);
            API.EndTextCommandThefeedPostTicker(false, true);
        }
        private void DrawSubtitle(string message, int duration)
        {
            API.BeginTextCommandPrint("STRING");
            API.AddTextComponentSubstringPlayerName(message);
            API.EndTextCommandPrint(duration, false);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace GroupGunFight
{
    
    [CalloutProperties("Group Gun Fight", "BGHDDevelopment", "1.0.9")]
    public class GroupGunFight : Callout
    {
        Ped suspect, suspect2, suspect3, suspect4;
        public GroupGunFight()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));

            ShortName = "Group Gun Fight";
            CalloutDescription = "4 armed suspects are fighting!";
            ResponseCode = 3;
            StartDistance = 30f;
        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
            
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~o~Officer ~b~" + displayName + ", ~o~reports show four individuals are shooting at each other!");
            
            suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location + 5, 3);
            suspect2 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 15, 2);
            suspect3 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 25 ,3);
            suspect4 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 21, 5);

            //Suspect 1
            PedData data = new PedData();
            List<Item> items = new List<Item>();
            data.BloodAlcoholLevel = 0.08;
            Item Pistol = new Item {
                Name = "Pistol",
                IsIllegal = false
            };
            items.Add(Pistol);
            data.Items = items;
            Utilities.SetPedData(suspect.NetworkId,data);
            
            //Suspect 2
            PedData data2 = new PedData();
            Utilities.SetPedData(suspect2.NetworkId,data2);
            
            //Suspect 3
            PedData data3 = new PedData();
            Utilities.SetPedData(suspect3.NetworkId,data3);
            
            //Suspect 4
            PedData data4 = new PedData();
            List<Item> items2 = new List<Item>();
            data.BloodAlcoholLevel = 0.08;
            Item Pistol2 = new Item {
                Name = "Pistol",
                IsIllegal = false
            };
            items.Add(Pistol2);
            data4.Items = items2;
            Utilities.SetPedData(suspect4.NetworkId,data4);
            
            //Tasks
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            suspect2.AlwaysKeepTask = true;
            suspect2.BlockPermanentEvents = true;
            suspect3.AlwaysKeepTask = true;
            suspect3.BlockPermanentEvents = true;
            suspect4.AlwaysKeepTask = true;
            suspect4.BlockPermanentEvents = true;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            suspect.AttachBlip();
            suspect2.AttachBlip();
            suspect3.AttachBlip();
            suspect4.AttachBlip();
            suspect.Task.FightAgainst(suspect2);
            suspect.Weapons.Give(WeaponHash.Pistol, 1, true, true);

            suspect2.Task.FightAgainst(suspect3);
            suspect2.Weapons.Give(WeaponHash.Pistol50, 1, true, true);

            suspect3.Task.FightAgainst(suspect4);
            suspect3.Weapons.Give(WeaponHash.CombatPistol, 1, true, true);

            suspect4.Task.FightAgainst(suspect);
            suspect4.Weapons.Give(WeaponHash.Pistol, 1, true, true);
            
            PedData data1 = await Utilities.GetPedData(suspect.NetworkId);
            string firstname = data1.FirstName;
            PedData data2 = await Utilities.GetPedData(suspect2.NetworkId);
            string firstname2 = data2.FirstName;
            PedData data3 = await Utilities.GetPedData(suspect3.NetworkId);
            string firstname3 = data3.FirstName;
            PedData data4 = await Utilities.GetPedData(suspect4.NetworkId);
            string firstname4 = data4.FirstName;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~I hate all of you!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname2 + "] ~s~Die!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname3 + "] ~s~Ahhhh!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname4 + "] ~s~STOP SHOOTING!", 5000);
            API.Wait(6000);
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
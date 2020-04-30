using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CalloutAPI;
using CitizenFX.Core.Native;

namespace GroupAttack
{

    [CalloutProperties("Group Attack", "BGHDDevelopment", "1.0.4", Probability.High)]
    public class GroupAttack : Callout
    {
        Ped suspect, suspect2, suspect3, victim;
        List<object> items = new List<object>();
        List<object> items2 = new List<object>();
        List<object> items3 = new List<object>();
        List<object> items4 = new List<object>();
        public GroupAttack()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitBase(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));

            ShortName = "Group Attack";
            CalloutDescription = "3 armed suspects are attacking a civilian!";
            ResponseCode = 3;
            StartDistance = 120f;
        }

        public async override Task Init()
        {
            OnAccept();

            suspect = await SpawnPed(GetRandomPed(), Location);
            suspect2 = await SpawnPed(GetRandomPed(), Location);
            suspect3 = await SpawnPed(GetRandomPed(), Location);
            victim = await SpawnPed(GetRandomPed(), Location);
            
            suspect.AttachBlip();
            suspect2.AttachBlip();
            suspect3.AttachBlip();
            victim.AttachBlip();
            
            //Suspect 1
            dynamic data = new ExpandoObject();
            data.alcoholLevel = 0.02;
            object Bottle = new {
                Name = "Bottle",
                IsIllegal = true
            };
            items.Add(Bottle);
            data.items = items;
            SetPedData(suspect.NetworkId,data);
            
            //Suspect 2
            dynamic data2 = new ExpandoObject();
            object Crowbar = new {
                Name = "Crowbar",
                IsIllegal = true
            };
            items2.Add(Crowbar);
            data2.items2 = items2;
            SetPedData(suspect2.NetworkId,data2);
            
            //Suspect 3
            dynamic data3 = new ExpandoObject();
            data3.drugsUsed = new bool[] {false,false,true};
            object GolfClub = new {
                Name = "GolfClub",
                IsIllegal = true
            };
            items3.Add(GolfClub);
            data3.items3 = items3;
            SetPedData(suspect3.NetworkId,data3);
            
            //Victim
            dynamic data4 = new ExpandoObject();
            object Purse = new {
                Name = "Purse",
                IsIllegal = false
            };
            items4.Add(Purse);
            data4.items4 = items4;
            SetPedData(victim.NetworkId,data4);

            //Tasks
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            suspect2.AlwaysKeepTask = true;
            suspect2.BlockPermanentEvents = true;
            suspect3.AlwaysKeepTask = true;
            suspect3.BlockPermanentEvents = true;
            victim.AlwaysKeepTask = true;
            victim.BlockPermanentEvents = true;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            dynamic playerData = GetPlayerData();
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
            dynamic data1 = await GetPedData(suspect.NetworkId);
            string firstname = data1.Firstname;
            dynamic data2 = await GetPedData(suspect2.NetworkId);
            string firstname2 = data2.Firstname;
            dynamic data3 = await GetPedData(suspect3.NetworkId);
            string firstname3 = data3.Firstname;
            dynamic data4 = await GetPedData(victim.NetworkId);
            string firstname4 = data4.Firstname;
            DrawSubtitle("~r~[" + firstname + "] ~s~You were not supposed to see that!", 500);
            API.Wait(550);
            DrawSubtitle("~r~[" + firstname2 + "] ~s~Come back here!", 500);
            API.Wait(550);
            DrawSubtitle("~r~[" + firstname3 + "] ~s~Your dead!", 500);
            API.Wait(550);
            DrawSubtitle("~r~[" + firstname4 + "] ~s~Help Me! HELP PLEASE!", 500);
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
        public override void OnCancelBefore()
        {
        }
    }
}
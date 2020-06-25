using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;

namespace GroupGunFight
{
    
    [CalloutProperties("Group Gun Fight", "BGHDDevelopment", "1.0.8")]
    public class GroupGunFight : Callout
    {
        Ped suspect, suspect2, suspect3, suspect4;
        List<object> items = new List<object>();
        List<object> items2 = new List<object>();
        List<object> items3 = new List<object>();
        List<object> items4 = new List<object>();
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
            UpdateData();
        }

        public async override Task OnAccept()
        {
            InitBlip();
            dynamic playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~o~Officer ~b~" + displayName + ", ~o~reports show four individuals are shooting at each other!");
            suspect = await SpawnPed(GetRandomPed(), Location + 5, 3);
            suspect2 = await SpawnPed(GetRandomPed(), Location + 15, 2);
            suspect3 = await SpawnPed(GetRandomPed(), Location + 25 ,3);
            suspect4 = await SpawnPed(GetRandomPed(), Location + 21, 5);

            //Suspect 1
            dynamic data = new ExpandoObject();
            data.alcoholLevel = 0.08;
            object Pistol = new {
                Name = "Pistol",
                IsIllegal = true
            };
            items.Add(Pistol);
            data.items = items;
            Utilities.SetPedData(suspect.NetworkId,data);
            
            //Suspect 2
            dynamic data2 = new ExpandoObject();
            object HeavyPistol = new {
                Name = "Heavy Pistol",
                IsIllegal = true
            };
            items2.Add(HeavyPistol);
            data2.items2 = items2;
            Utilities.SetPedData(suspect2.NetworkId,data2);
            
            //Suspect 3
            dynamic data3 = new ExpandoObject();
            data3.drugsUsed = new bool[] {true,false,false};
            object Meth = new {
                Name = "Bag of meth",
                IsIllegal = true
            };
            object CombatPistol = new {
                Name = "CombatPistol",
                IsIllegal = true
            };
            items3.Add(CombatPistol);
            items3.Add(Meth);
            data3.items3 = items3;
            Utilities.SetPedData(suspect3.NetworkId,data3);
            
            //Suspect 4
            dynamic data4 = new ExpandoObject();
            data4.drugsUsed = new bool[] {true,true,false};
            object Pistol2 = new {
                Name = "Pistol",
                IsIllegal = true
            };
            items4.Add(Pistol2);
            data4.items4 = items4;
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
            
            dynamic data1 = await Utilities.GetPedData(suspect.NetworkId);
            string firstname = data1.Firstname;
            dynamic data2 = await Utilities.GetPedData(suspect2.NetworkId);
            string firstname2 = data2.Firstname;
            dynamic data3 = await Utilities.GetPedData(suspect3.NetworkId);
            string firstname3 = data3.Firstname;
            dynamic data4 = await Utilities.GetPedData(suspect4.NetworkId);
            string firstname4 = data4.Firstname;
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
        public override void OnCancelBefore()
        {
        }
    }
}
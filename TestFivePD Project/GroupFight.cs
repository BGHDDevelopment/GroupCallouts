using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CalloutAPI;
using CitizenFX.Core.Native;

namespace GroupFight
{
    
    [CalloutProperties("Group Fight", "BGHDDevelopment", "1.0.5", Probability.Medium)]
    public class GroupFight : Callout
    {
        Ped suspect, suspect2, suspect3, suspect4, suspect5, suspect6, suspect7, suspect8, suspect9, suspect10;
        List<object> items = new List<object>();

        public GroupFight()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitBase(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));

            ShortName = "Group Fight";
            CalloutDescription = "10 unarmed suspects are fighting each other!";
            ResponseCode = 3;
            StartDistance = 120f;
        }

        public async override Task Init()
        {
            OnAccept();

            suspect = await SpawnPed(GetRandomPed(), Location + 1);
            suspect2 = await SpawnPed(GetRandomPed(), Location - 1);
            suspect3 = await SpawnPed(GetRandomPed(), Location + 2);
            suspect4 = await SpawnPed(GetRandomPed(), Location - 2);
            suspect5 = await SpawnPed(GetRandomPed(), Location + 1);
            suspect6 = await SpawnPed(GetRandomPed(), Location - 1);
            suspect7 = await SpawnPed(GetRandomPed(), Location + 3);
            suspect8 = await SpawnPed(GetRandomPed(), Location - 3);
            suspect9 = await SpawnPed(GetRandomPed(), Location + 1);
            suspect10 = await SpawnPed(GetRandomPed(), Location - 1);

            
            
            //Suspect 1
            dynamic data = new ExpandoObject();
            data.alcoholLevel = 0.08;
            data.drugsUsed = new bool[] {false,false,true};
            SetPedData(suspect.NetworkId,data);
            
            //Suspect 2
            dynamic data2 = new ExpandoObject();
            data2.alcoholLevel = 0.05;
            data2.drugsUsed = new bool[] {true,false,false};
            SetPedData(suspect2.NetworkId,data2);
            
            //Suspect 3
            dynamic data3 = new ExpandoObject();
            data3.alcoholLevel = 0.02;
            data3.drugsUsed = new bool[] {false,false,false};
            SetPedData(suspect3.NetworkId,data3);
            
            //Suspect 4
            dynamic data4 = new ExpandoObject();
            data4.alcoholLevel = 0.00;
            data4.drugsUsed = new bool[] {false,false,false};
            object Cash = new {
                Name = "$500 Cash",
                IsIllegal = false
            };
            items.Add(Cash);
            SetPedData(suspect4.NetworkId,data4);
            
            //Suspect 5
            dynamic data5 = new ExpandoObject();
            data5.alcoholLevel = 0.00;
            data5.drugsUsed = new bool[] {true,true,true};
            SetPedData(suspect5.NetworkId,data5);
            
            //Suspect 6
            dynamic data6 = new ExpandoObject();
            data6.alcoholLevel = 0.20;
            data6.drugsUsed = new bool[] {false,false,false};
            SetPedData(suspect6.NetworkId,data6);
            
            //Suspect 7
            dynamic data7 = new ExpandoObject();
            data7.alcoholLevel = 0.01;
            data7.drugsUsed = new bool[] {false,false,false};
            SetPedData(suspect7.NetworkId,data7);
            
            //Suspect 8
            dynamic data8 = new ExpandoObject();
            data8.alcoholLevel = 0.08;
            data8.drugsUsed = new bool[] {false,false,false};
            SetPedData(suspect8.NetworkId,data8);
            
            //Suspect 9
            dynamic data9 = new ExpandoObject();
            data9.alcoholLevel = 0.00;
            data9.drugsUsed = new bool[] {false,true,false};
            SetPedData(suspect9.NetworkId,data9);
            
            //Suspect 10
            dynamic data10 = new ExpandoObject();
            data10.alcoholLevel = 0.00;
            data10.drugsUsed = new bool[] {false,false,true};
            object Cash2 = new {
                Name = "$500 Cash",
                IsIllegal = false
            };
            items.Add(Cash);
            SetPedData(suspect10.NetworkId,data10);
            
            
            //TASKS
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            suspect2.AlwaysKeepTask = true;
            suspect2.BlockPermanentEvents = true;
            suspect3.AlwaysKeepTask = true;
            suspect3.BlockPermanentEvents = true;
            suspect4.AlwaysKeepTask = true;
            suspect4.BlockPermanentEvents = true;
            suspect5.AlwaysKeepTask = true;
            suspect5.BlockPermanentEvents = true;
            suspect6.AlwaysKeepTask = true;
            suspect6.BlockPermanentEvents = true;
            suspect7.AlwaysKeepTask = true;
            suspect7.BlockPermanentEvents = true;
            suspect8.AlwaysKeepTask = true;
            suspect8.BlockPermanentEvents = true;
            suspect9.AlwaysKeepTask = true;
            suspect9.BlockPermanentEvents = true;
            suspect10.AlwaysKeepTask = true;
            suspect10.BlockPermanentEvents = true;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            dynamic playerData = GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~o~Officer ~b~" + displayName + ", ~o~reports show ten individuals are fighting!");
            suspect.AttachBlip();
            suspect2.AttachBlip();
            suspect3.AttachBlip();
            suspect4.AttachBlip();
            suspect5.AttachBlip();
            suspect6.AttachBlip();
            suspect7.AttachBlip();
            suspect8.AttachBlip();
            suspect9.AttachBlip();
            suspect10.AttachBlip();
            suspect.Task.FightAgainst(suspect2);
            suspect2.Task.FightAgainst(suspect3);
            suspect3.Task.FightAgainst(suspect4);
            suspect4.Task.FightAgainst(suspect5);
            suspect5.Task.FightAgainst(suspect6);
            suspect6.Task.FightAgainst(suspect7);
            suspect7.Task.FightAgainst(suspect8);
            suspect8.Task.FightAgainst(suspect9);
            suspect9.Task.FightAgainst(suspect10);
            suspect10.Task.FightAgainst(suspect);
            
            dynamic data1 = await GetPedData(suspect.NetworkId);
            string firstname = data1.Firstname;
            dynamic data2 = await GetPedData(suspect2.NetworkId);
            string firstname2 = data2.Firstname;
            dynamic data3 = await GetPedData(suspect3.NetworkId);
            string firstname3 = data3.Firstname;
            dynamic data4 = await GetPedData(suspect4.NetworkId);
            string firstname4 = data4.Firstname;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~Come here you!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname2 + "] ~s~I hate you!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname3 + "] ~s~STOP HITTING ME!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname4 + "] ~s~That hurts!", 5000);
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
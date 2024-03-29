﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace GroupFight
{
    
    [CalloutProperties("Group Fight", "BGHDDevelopment", "1.1.0")]
    public class GroupFight : Callout
    {
        Ped suspect, suspect2, suspect3, suspect4, suspect5, suspect6, suspect7, suspect8, suspect9, suspect10;

        public GroupFight()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));

            ShortName = "Group Fight";
            CalloutDescription = "10 unarmed suspects are fighting each other!";
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
                        
            suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            suspect2 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 1);
            suspect3 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            suspect4 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 2);
            suspect5 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            suspect6 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 1);
            suspect7 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 3);
            suspect8 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 3);
            suspect9 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            suspect10 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 1);

            
            
            //Suspect 1
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.08;
            Utilities.SetPedData(suspect.NetworkId,data);
            
            //Suspect 2
            PedData data2 = new PedData();
            data2.BloodAlcoholLevel = 0.05;
            Utilities.SetPedData(suspect2.NetworkId,data2);
            
            //Suspect 3
            PedData data3 = new PedData();
            data3.BloodAlcoholLevel = 0.02;
            Utilities.SetPedData(suspect3.NetworkId,data3);
            
            //Suspect 4
            PedData data4 = new PedData();
            data4.BloodAlcoholLevel = 0.00;
            List<Item> items = new List<Item>();
            Item Cash = new Item {
                Name = "$500 Cash",
                IsIllegal = false
            };
            items.Add(Cash);
            data.Items = items;
            Utilities.SetPedData(suspect4.NetworkId,data4);
            
            //Suspect 5
            PedData data5 = new PedData();
            data5.BloodAlcoholLevel = 0.00;
            Utilities.SetPedData(suspect5.NetworkId,data5);
            
            //Suspect 6
            PedData data6 = new PedData();
            data6.BloodAlcoholLevel = 0.20;
            Utilities.SetPedData(suspect6.NetworkId,data6);
            
            //Suspect 7
            PedData data7 = new PedData();
            data7.BloodAlcoholLevel = 0.01;
            Utilities.SetPedData(suspect7.NetworkId,data7);
            
            //Suspect 8
            PedData data8 = new PedData();
            data8.BloodAlcoholLevel = 0.08;
            Utilities.SetPedData(suspect8.NetworkId,data8);
            
            //Suspect 9
            PedData data9 = new PedData();
            data9.BloodAlcoholLevel = 0.00;
            Utilities.SetPedData(suspect9.NetworkId,data9);
            
            //Suspect 10
            PedData data10 = new PedData();
            List<Item> items2 = new List<Item>();
            Item Cash2 = new Item {
                Name = "$5000 Cash",
                IsIllegal = false
            };
            items2.Add(Cash2);
            data.Items = items;
            Utilities.SetPedData(suspect10.NetworkId,data10);
            
            
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
            PlayerData playerData = Utilities.GetPlayerData();
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
            
            PedData data1 = await Utilities.GetPedData(suspect.NetworkId);
            string firstname = data1.FirstName;
            PedData data12 = await Utilities.GetPedData(suspect2.NetworkId);
            string firstname2 = data12.FirstName;
            PedData data13 = await Utilities.GetPedData(suspect3.NetworkId);
            string firstname3 = data13.FirstName;
            PedData data14 = await Utilities.GetPedData(suspect4.NetworkId);
            string firstname4 = data14.FirstName;
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
    }
}
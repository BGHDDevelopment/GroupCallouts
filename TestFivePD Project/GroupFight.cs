using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CalloutAPI;

namespace GroupFight
{
    
    [CalloutProperties("Group Fight", "BGHDDevelopment", "1.0", Probability.Low)]
    public class GroupFight : Callout
    {
        Ped suspect, suspect2, suspect3, suspect4, suspect5, suspect6, suspect7, suspect8, suspect9, suspect10;
        
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

            suspect = await SpawnPed(GetRandomPed(), Location);
            suspect2 = await SpawnPed(GetRandomPed(), Location);
            suspect3 = await SpawnPed(GetRandomPed(), Location);
            suspect4 = await SpawnPed(GetRandomPed(), Location);
            suspect5 = await SpawnPed(GetRandomPed(), Location);
            suspect6 = await SpawnPed(GetRandomPed(), Location);
            suspect7 = await SpawnPed(GetRandomPed(), Location);
            suspect8 = await SpawnPed(GetRandomPed(), Location);
            suspect9 = await SpawnPed(GetRandomPed(), Location);
            suspect10 = await SpawnPed(GetRandomPed(), Location);

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

        public override void OnStart(Ped player)
        {
            base.OnStart(player);
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
        }
    }
}
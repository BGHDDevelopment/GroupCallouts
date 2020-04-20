using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CalloutAPI;
using CitizenFX.Core.NaturalMotion;

namespace GroupGunFight
{
    
    [CalloutProperties("Group Gun Fight", "BGHDDevelopment", "1.0", Probability.Low)]
    public class GroupGunFight : Callout
    {
        Ped suspect, suspect2, suspect3, suspect4;
        
        public GroupGunFight()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitBase(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));

            ShortName = "Group Gun Fight";
            CalloutDescription = "4 armed suspects are fighting!";
            ResponseCode = 3;
            StartDistance = 30f;
        }

        public async override Task Init()
        {
            OnAccept();

            suspect = await SpawnPed(GetRandomPed(), Location + 5, 3);
            suspect2 = await SpawnPed(GetRandomPed(), Location + 15, 2);
            suspect3 = await SpawnPed(GetRandomPed(), Location + 25 ,3);
            suspect4 = await SpawnPed(GetRandomPed(), Location + 21, 5);

            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            suspect2.AlwaysKeepTask = true;
            suspect2.BlockPermanentEvents = true;
            suspect3.AlwaysKeepTask = true;
            suspect3.BlockPermanentEvents = true;
            suspect4.AlwaysKeepTask = true;
            suspect4.BlockPermanentEvents = true;
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);
            suspect.Task.FightAgainst(suspect2);
            suspect.Weapons.Give(WeaponHash.Pistol, 1, true, true);

            suspect2.Task.FightAgainst(suspect3);
            suspect2.Weapons.Give(WeaponHash.Pistol50, 1, true, true);

            suspect3.Task.FightAgainst(suspect4);
            suspect3.Weapons.Give(WeaponHash.CombatPistol, 1, true, true);

            suspect4.Task.FightAgainst(suspect);
            suspect4.Weapons.Give(WeaponHash.Pistol, 1, true, true);
            
        }
    }
}
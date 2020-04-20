using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CalloutAPI;

namespace GroupAttack
{

    [CalloutProperties("Group Attack", "BGHDDevelopment", "1.0", Probability.High)]
    public class GroupAttack : Callout
    {
        Ped suspect, suspect2, suspect3, victim;

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

            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            victim.AlwaysKeepTask = true;
            victim.BlockPermanentEvents = true;
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);
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
        }
    }
}
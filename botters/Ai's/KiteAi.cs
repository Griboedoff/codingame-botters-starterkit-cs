﻿using System.Linq;

namespace botters
{
    public class KiteAi : IAi
    {
        private readonly Vec enemyTower;
        private readonly Vec stepToBase;

        public KiteAi(AttackDirection ad)
        {
            enemyTower = ad == AttackDirection.Left ? new Vec(100, 540) : new Vec(1820, 540);
            stepToBase = ad == AttackDirection.Left ? new Vec(70, 0) : new Vec(-70, 0);

            Logger.Log(enemyTower.ToString());
        }

        public string GetNextMove(State state, Countdown countdown)
        {
            if (state.RoundType < 0)
                return CommandHelper.SelectHero(HeroType.IronMan);

            var myHero = state.GetMy(UnitType.Hero).First();

            Logger.LogDebug($"Hero {myHero.Pos}");
            var closestToEnemyUnit = state.GetMy()
                .Where(u => u.UnitType != UnitType.Hero)
                .OrderBy(u => u.Pos.DistTo(enemyTower))
                .First();
            Logger.LogDebug($"closestToEnemyUnit {closestToEnemyUnit.Pos}");

            var heroDistToTower = myHero.Pos.DistTo(enemyTower);
            var unitDistToTower = closestToEnemyUnit.Pos.DistTo(enemyTower);

            Logger.LogDebug($"{heroDistToTower} < {unitDistToTower}");
            if (heroDistToTower < unitDistToTower)
                return CommandHelper.Move(closestToEnemyUnit.Pos + stepToBase);

            if (state.GetHis().Any(u => myHero.CanAttack(u)))
            {
                var unitToAttack = state.GetHis(UnitType.Unit)
                    .Where(u => myHero.CanAttack(u))
                    .OrderBy(u => u.UnitType)
//                    .ThenBy(u => u.Health)
                    .FirstOrDefault();
                if (unitToAttack != null)
                    return CommandHelper.Attack(unitToAttack);
            }

            return CommandHelper.Move(closestToEnemyUnit.Pos + stepToBase);
        }
    }
}
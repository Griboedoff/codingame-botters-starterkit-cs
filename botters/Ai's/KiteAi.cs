using System.Linq;

namespace botters
{
    public class KiteAi : AiBase
    {
        private readonly Vec enemyTower;
        private readonly Vec stepToBase;

        public KiteAi(AttackDirection ad, HeroType heroType)
            : base(heroType)
        {
            enemyTower = ad == AttackDirection.Left ? new Vec(100, 540) : new Vec(1820, 540);
            stepToBase = ad == AttackDirection.Left ? new Vec(70, 0) : new Vec(-70, 0);

            Logger.Log(enemyTower.ToString());
        }

        public override string GetNextMove(State state, Countdown countdown)
        {
            var myHero = state.GetMy(UnitType.Hero).First(h => h.HeroType == heroType);
            var unitToDeny = GetUnitToDeny(state);
            if (unitToDeny != null)
            {
                return Deny(unitToDeny);
            }

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
                return Move(closestToEnemyUnit.Pos + stepToBase);

            if (myHero.ItemsOwned < 4)
            {
                var itemToBy = state.InitData.Items.Where(i => i.Damage != 0)
                    .OrderByDescending(i => i.Damage)
                    .FirstOrDefault(i => i.ItemCost <= state.Gold);
                if (itemToBy != null)
                    return CommandHelper.Buy(itemToBy);
            }

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

            return Move(closestToEnemyUnit.Pos + stepToBase);
        }

        private Unit GetUnitToDeny(State state)
        {
            var myHero = state.GetMy(UnitType.Hero).First(h => h.HeroType == heroType);
            var myUnits = state.GetMy(UnitType.Unit);
            var unitToKill = myUnits.FirstOrDefault(unit => myHero.CanAttack(unit) && myHero.CanKill(unit));
            return unitToKill;
        }

        private string Deny(Unit unitToDeny)
        {
            return CommandHelper.Attack(unitToDeny);
        }

        private string Move(Vec to)
        {
            while (to.InRadiusTo(enemyTower, 400))
                to += stepToBase;

            return CommandHelper.Move(to);
        }
    }
}
using System.Linq;

namespace botters
{
    public class StayBehindTowerAI : IAi
    {
        public int Team { get;  }
        private Vec LeftPosition => new Vec(100, 450);
        private Vec RightPosition => new Vec(1820, 450);
        
        public StayBehindTowerAI(int team)
        {
            Team = team;
        }

        public bool ReadyToAttack(Vec heroPosition)
        {
            return Equals(heroPosition, LeftPosition) || Equals(heroPosition, RightPosition);
        }
        
        public string GetNextMove(State state, Countdown countdown)
        {
            if (state.RoundType < 0) return CommandHelper.SelectHero(HeroType.Valkyrie);

            var heroInfo = state.GetMy(UnitType.Hero).First();
            if (!ReadyToAttack(heroInfo.Pos))
                return GotoPosition();

            var unitsInRange = state.GetHis();
            var unitToAttack = unitsInRange.Where(u => u.Pos.InRadiusTo(heroInfo.Pos, heroInfo.AttackRange))
                .OrderBy(u => u.Health).First();

            return CommandHelper.Attack(unitToAttack);
        }

        private string GotoPosition()
        {
            return Team == 0 ? CommandHelper.Move(100, 450) : CommandHelper.Move(1820, 450);
        }
    }
}
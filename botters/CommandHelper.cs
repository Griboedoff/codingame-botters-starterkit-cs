namespace botters
{
    public static class CommandHelper
    {
        public static string SelectHero(HeroType hero) => hero.ToStringValue();

        public static string Wait() => "WAIT";

        public static string Move(int x, int y) => $"MOVE {x} {y}";
        public static string Move(Vec to) => Move(to.X, to.Y);

        public static string Attack(int unitId) => $"ATTACK {unitId}";
        public static string Attack(Unit unit) => Attack(unit.UnitId);

        public static string AttackNearest(UnitType unitType) => $"ATTACK_NEAREST {unitType.ToStringValue()}";

        public static string MoveAttack(int x, int y, int unitId) => $"MOVE_ATTACK {x} {y} {unitId}";
        public static string MoveAttack(Vec to, Unit unit) => MoveAttack(to.X, to.Y, unit.UnitId);

        public static string Buy(string itemName) => $"BUY {itemName}";
        public static string Buy(Item item) => Buy(item.ItemName);

        public static string Sell(string itemName) => $"SELL {itemName}";
        public static string Sell(Item item) => Sell(item.ItemName);
    }
}
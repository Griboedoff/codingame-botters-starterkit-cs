namespace botters
{
    /// <summary>
    /// Случайно прошел им в дерево 2))
    /// </summary>
    public class HulkSmashAi : IAi
    {
        public string GetNextMove(State state, Countdown countdown)
        {
            if (state.RoundType < 0) return "HULK";
            return "ATTACK_NEAREST UNIT";
        }
    }
}
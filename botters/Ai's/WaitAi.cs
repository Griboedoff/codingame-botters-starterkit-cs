namespace botters
{
    public class WaitAi : AiBase
    {
        public WaitAi(HeroType heroType)
            : base(heroType)
        {
        }

        public override string GetNextMove(State state, Countdown countdown) => CommandHelper.Wait();
    }
}
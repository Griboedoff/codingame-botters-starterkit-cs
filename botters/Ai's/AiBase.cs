namespace botters
{
    public abstract class AiBase : IAi
    {
        protected readonly HeroType heroType;

        protected AiBase(HeroType heroType)
        {
            this.heroType = heroType;
        }

        public abstract string GetNextMove(State state, Countdown countdown);
    }
}
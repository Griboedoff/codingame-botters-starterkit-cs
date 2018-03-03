namespace botters
{
    public interface IAi
    {
        string GetNextMove(State state, Countdown countdown);
    }
}
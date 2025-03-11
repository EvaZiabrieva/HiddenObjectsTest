public abstract class BaseState
{
    public abstract bool IsFinished { get; }
    public abstract void Start();
    public abstract void Stop();
}

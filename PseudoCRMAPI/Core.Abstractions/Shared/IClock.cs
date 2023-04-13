namespace Core.Abstractions.Shared
{
    public interface IClock
    {
        DateTime GetNow();
    }
}
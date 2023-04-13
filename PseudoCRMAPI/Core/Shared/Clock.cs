using Core.Abstractions.Shared;

namespace Core.Shared
{
    public class Clock : IClock
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}
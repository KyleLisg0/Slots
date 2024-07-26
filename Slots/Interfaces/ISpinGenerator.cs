using Slots.Models;

namespace Slots.Interfaces
{
    public interface ISpinGenerator
    {
        SpinResult SpinResult(decimal stake);
    }
}

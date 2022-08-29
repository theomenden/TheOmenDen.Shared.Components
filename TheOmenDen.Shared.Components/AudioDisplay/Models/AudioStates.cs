using TheOmenDen.Shared.Enumerations;
namespace TheOmenDen.Shared.Components.AudioDisplay.Models;

internal sealed record AudioStates(String Name, Int32 Id) : EnumerationBase<AudioStates>(Name, Id)
{
    public static readonly AudioStates Playing = new (nameof(Playing), 1);
    public static readonly AudioStates Stopped = new (nameof(Stopped), 2);
    public static readonly AudioStates Paused = new (nameof(Paused), 3);
}

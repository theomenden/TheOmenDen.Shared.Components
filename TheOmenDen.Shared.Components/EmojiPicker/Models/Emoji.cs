namespace TheOmenDen.Shared.Components.EmojiPicker.Models;

public record struct Emoji(String Name, String Display, Int32 Id) : IComparable<Emoji>
{
    public int CompareTo(Emoji other) => Id.CompareTo(other.Id);

    public override int GetHashCode() => HashCode.Combine(Display.GetHashCode(), Id.GetHashCode());

    public override string ToString() => Name;

    public static bool operator <(Emoji left, Emoji right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(Emoji left, Emoji right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(Emoji left, Emoji right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(Emoji left, Emoji right)
    {
        return left.CompareTo(right) >= 0;
    }
}

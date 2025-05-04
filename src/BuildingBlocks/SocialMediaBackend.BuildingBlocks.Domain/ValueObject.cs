namespace SocialMediaBackend.BuildingBlocks.Domain;

public abstract record ValueObject : IEquatable<ValueObject>
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (left is null ^ right is null)
        {
            return false;
        }

        return left?.Equals(right!) != false;
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    protected abstract IEnumerable<object> GetComponents();

    public override int GetHashCode()
    {
        var hash = new HashCode();

        foreach (var component in GetComponents())
        {
            hash.Add(component);
        }

        return hash.ToHashCode();
    }
}

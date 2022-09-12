namespace Panni.Source.Utilities;
public class Optional<T> {
    private T _value;
    public bool IsPresent { get; private set; }

    private Optional() { }

    public static Optional<T> Empty() {
        return new Optional<T>();
    }

    public static Optional<T> Of(T value) {
        var obj = new Optional<T>();
        obj.Set(value);
        return obj;
    }

    public void Set(T value) {
        this._value = value;
        IsPresent = true;
    }

    public T Get() {
        return _value;
    }

    protected bool Equals(Optional<T> other)
    {
        return EqualityComparer<T>.Default.Equals(_value, other._value) && IsPresent == other.IsPresent;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return (obj.GetType() == this.GetType()) && Equals((Optional<T>) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_value, IsPresent);
    }
}
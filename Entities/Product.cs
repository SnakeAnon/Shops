namespace Shops.Entities;
using Shops.Exceptions;

public class Product : IEquatable<Product>
{
    private int _id;

    public Product(string name, int id)
    {
        if (string.IsNullOrWhiteSpace(name) || id <= 0)
            throw new Exception();

        Name = name;
        _id = id;
    }

    public string Name { get; }

    public int Id => _id;

    public bool Equals(Product other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _id == other._id;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Product)obj);
    }

    public override int GetHashCode()
    {
        return _id;
    }
}
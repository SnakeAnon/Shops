using Shops.Exceptions;

namespace Shops.Entities;

public class ProductToBuy : IEquatable<ProductToBuy>
{
    public ProductToBuy(Product product, int count)
    {
        if (product is null)
            throw new ArgumentNullException();
        if (count <= 0)
            throw new NegativeValueException("Incorrect count of product");

        Product = product;
        Count = count;
    }

    public int Count { get; }

    public Product Product { get; }

    public int Id => Product.Id;

    public bool Equals(ProductToBuy other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(Product, other.Product);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ProductToBuy)obj);
    }

    public override int GetHashCode()
    {
        return Product != null ? Product.GetHashCode() : 0;
    }
}
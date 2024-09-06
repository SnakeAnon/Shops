namespace Shops.Entities;
using Shops.Exceptions;

public class ProductInShop : IEquatable<ProductInShop>
{
    public ProductInShop(Product product, decimal price, int count)
    {
        if (product is null)
            throw new ArgumentNullException();

        Product = product;
        Count = count;
        Price = price;
    }

    public int Count { get; private set; }

    public Product Product { get; }

    public decimal Price { get; private set; }

    public int Id => Product.Id;

    public void ChangeCount(int count)
    {
        Count += count;
    }

    public decimal TotalPrice()
    {
        return Count * Price;
    }

    public void ChangePriceInShop(decimal newPrice)
    {
        if (newPrice <= 0) throw new NegativeValueException("Negative value");

        Price = newPrice;
    }

    public void Add(int count)
    {
        if (count <= 0)
            throw new NegativeValueException("NegativeValue");

        Count += count;
    }

    public bool Equals(ProductInShop other)
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
        return Equals((ProductInShop)obj);
    }

    public override int GetHashCode()
    {
        return Product != null ? Product.GetHashCode() : 0;
    }
}
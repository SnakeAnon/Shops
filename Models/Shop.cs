using Shops.Entities;

namespace Shops.Models;

public class Shop : IEquatable<Shop>
{
    private List<ProductInShop> _products;
    private int _id;
    private string _addres;
    private string _name;

    public Shop(string adress, string name, decimal budget, int id)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(adress) || id < 0)
            throw new ArgumentNullException();

        Budget = budget;
        _addres = adress;
        _name = name;
        _id = id;
        _products = new List<ProductInShop>();
    }

    public IReadOnlyCollection<ProductInShop> Products => _products;
    public decimal Budget { get; private set; }

    public int Id => _id;

    public void AddProduct(Product product, decimal price, int count)
    {
        if (product is null)
            throw new ArgumentNullException();

        var productInShop = new ProductInShop(product, price, count);
        _products.Add(productInShop);
        decimal balanceWriteOff = -price * count;
        BalancheChange(balanceWriteOff);
    }

    public void RemoveProduct(ProductInShop product, int count)
    {
        if (product is null)
            throw new ArgumentNullException();

        _products.FirstOrDefault(product).ChangeCount(-count);
    }

    public void ChangePrice(ProductInShop productInShop, decimal newPrice)
    {
        _products.FirstOrDefault(productInShop).ChangePriceInShop(newPrice);
    }

    public void BalancheChange(decimal price)
    {
        Budget += price;
    }

    public bool Equals(Shop other)
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
        return Equals((Shop)obj);
    }

    public override int GetHashCode()
    {
        return _id;
    }
}
using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Service;

public class Service
{
    private readonly List<Shop> _listOfShop = new List<Shop>();
    private readonly List<Product> _listOfProducts = new List<Product>();
    private int _idOfProduct = 0;
    private int _idOfShop = 0;

    public IReadOnlyList<Product> Products => _listOfProducts;
    public IReadOnlyList<Shop> Shops => _listOfShop;

    public Shop AddShop(string adress, string name, int budget)
    {
        if (string.IsNullOrWhiteSpace(adress) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (budget < 0)
            throw new NegativeValueException("Incorrect budget value");

        var newShop = new Shop(adress, name, budget, ++_idOfShop);
        if (_listOfShop.FirstOrDefault(x => x.Equals(newShop)) != null)
            throw new AlreadyExistException("Shop is already exist");
        _listOfShop.Add(newShop);
        return newShop;
    }

    public Product AddProduct(string nameOfProduct)
    {
        if (string.IsNullOrWhiteSpace(nameOfProduct))
            throw new ArgumentNullException();

        var product = new Product(nameOfProduct, ++_idOfProduct);
        if (product.Equals(_listOfProducts.FirstOrDefault()))
            throw new AlreadyExistException("Product is already added");
        _listOfProducts.Add(product);
        return product;
    }

    public void Delivery(Shop shop, Product product, int count, decimal price)
    {
        if (shop is null || product is null)
            throw new ArgumentNullException();
        if (count <= 0 || price <= 0)
            throw new NegativeValueException("Incorrect count or price value");
        if (shop.Budget < price * count)
            throw new LackOfMoneyException("Lack of money");

        if (shop.Products.Any(x => x.Product.Equals(product)))
            shop.Products.SingleOrDefault(x => x.Product.Equals(product)).Add(count);
        else
            shop.AddProduct(product, price, count);
    }

    public decimal BuyProducts(Shop shop, Person person)
    {
        if (shop is null || person is null)
            throw new ArgumentNullException();

        var productsInShop = new List<ProductInShop>();
        foreach (ProductToBuy product in person.Basket)
        {
            productsInShop = shop.Products.Where(x => x.Id.Equals(product.Id)).ToList();
        }

        decimal totalPrice = 0;
        foreach (var product in productsInShop)
        {
            totalPrice += product.Price * product.Count;
            shop.RemoveProduct(product, product.Count);
        }

        person.MakeTransaction(totalPrice);
        return person.Balance;
    }

    public Shop FindCheapest(Person person)
    {
        if (person is null)
            throw new ArgumentNullException();

        var productsInShop = new List<ProductInShop>();
        decimal totalPrice;
        return _listOfShop.MinBy(shop =>
        {
            foreach (ProductToBuy product in person.Basket)
            {
                productsInShop = shop.Products.Where(x => x.Product.Id.Equals(product.Id)).ToList();
            }

            totalPrice = productsInShop.Sum(product => product.Price);
            if (shop != null)
                return totalPrice;
            return 0;
        });
    }
}
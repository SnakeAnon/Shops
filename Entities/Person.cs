using Shops.Exceptions;

namespace Shops.Entities;

public class Person
{
    private List<ProductToBuy> _basket = new List<ProductToBuy>();

    public Person(int balance, string name)
    {
        if (balance < 0)
            throw new NegativeValueException("Incorrect balance value");
        Name = name;
        Balance = balance;
    }

    public IReadOnlyCollection<ProductToBuy> Basket => _basket;

    public decimal Balance { get; private set; }

    public string Name { get; }
    public void AddShoppingList(List<ProductToBuy> shoppingList)
    {
        _basket.AddRange(shoppingList);
    }

    public void MakeTransaction(decimal price)
    {
        if (price > Balance)
            throw new LackOfMoneyException("Lack of money");
        Balance -= price;
    }
}
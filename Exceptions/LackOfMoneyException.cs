namespace Shops.Exceptions;

public class LackOfMoneyException : Exception
{
    public LackOfMoneyException(string message)
        : base(message)
    {
    }
}
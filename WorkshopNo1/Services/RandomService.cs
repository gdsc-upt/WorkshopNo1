namespace WorkshopNo1.Services;

public class RandomService : IRandomService
{
    private int number;
    public RandomService()
    {
        var random = new Random();
        number = random.Next();
    }

    public int GetRandomInt()
    {
        return number;
    }
}
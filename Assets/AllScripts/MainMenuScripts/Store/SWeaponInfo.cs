public class SWeaponInfo
{
    public string name;
    public int accuracy;
    public int distance;
    public int price;

    internal protected static readonly int[] missHitProb = { 2, 2, 3, 4, 5, 6, 7 };

    public SWeaponInfo(string name, int accuracy, int distance, int price)
    {
        this.name = name;
        this.accuracy = accuracy;
        this.distance = distance;
        this.price = price;
    }
    public SWeaponInfo(string name, int accuracy, int distance)
    {
        this.name = name;
        this.accuracy = accuracy;
        this.distance = distance;
    }
}

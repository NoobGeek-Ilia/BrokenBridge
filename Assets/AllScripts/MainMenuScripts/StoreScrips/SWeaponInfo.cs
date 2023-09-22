public class SWeaponInfo
{
    public string name;
    public int accuracy;
    public int distance;
    public int price;

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

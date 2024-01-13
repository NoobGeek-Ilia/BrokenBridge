public class SCharacterInfo
{
    public string name;
    public int hp;
    public int price;
    public SCharacterInfo(string name, int hp)
    {
        this.name = name;
        this.hp = hp;
    }
    public SCharacterInfo(string name, int hp, int price)
    {
        this.name = name;
        this.hp = hp;
        this.price = price;
    }
}
using static BASE;
public class SorcererAttack
{
    public double Damage
    {
        get
        {
            if (Level != null)
            {
                return damage * (1d + 0.01d * Level()); // level * 1%加算
            }
            else
            {
                return damage;
            }
        }
    }
    double damage;
    public IntSync Level;
    public SorcererAttack(double damage)
    {
        this.damage = damage;
    }
}

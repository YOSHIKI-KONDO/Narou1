using static BASE;
public class WarriorAttack
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
    public IntSync Level;
    double damage;
    public WarriorAttack(double damage)
    {
        this.damage = damage;
    }
}

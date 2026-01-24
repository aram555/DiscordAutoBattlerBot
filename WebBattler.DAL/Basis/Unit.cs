namespace WebBattler.DAL.Basis;

public class Unit
{
    public string Name { get; set; }
    public float Health { get; set; }
    public string Weapon {  get; set; }
    public bool IsAlive => Health > 0;

    public Unit(string name, float health, string weapon)
    {
        Name = name;
        Health = health;
        Weapon = weapon;
    }

    public void TakeDamage(float damage)
    {
        if (IsAlive)
        {
            Health -= damage;
        }
    }
}

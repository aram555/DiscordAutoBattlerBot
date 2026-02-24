namespace WebBattler.DAL.Basis;

public class Unit
{
    public string Name { get; set; }
    public float Health { get; set; }
    public string Weapon {  get; set; }
    public float Damage { get; set; }
    public float Armor { get; set; }
    public bool IsAlive => Health > 0;

    public Unit(string name, float health, string weapon, float damage, float armor)
    {
        Name = name;
        Health = health;
        Weapon = weapon;
        Damage = damage;
        Armor = armor;
    }

    public void TakeDamage(float damage)
    {
        if (IsAlive)
        {
            Health -= damage;
        }
    }
}

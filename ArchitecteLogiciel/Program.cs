using System.Reflection;

namespace ArchitecteLogiciel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hero hero = new Hero();
            hero.Loot(new Loup());
            hero.Loot(new Orque());
            hero.Loot(new Dragon());
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public abstract class LootAttribute : Attribute
    {
        public int MaxValue { get; init; }
        public string LootName { get; init; }

        public LootAttribute(string lootName, int maxValue)
        {
            LootName = lootName;
            MaxValue = maxValue;

        }

        public int Quantity { get { return Random.Shared.Next(MaxValue) + 1; } }
    }

    public class LeatherAttribute : LootAttribute
    {
        public LeatherAttribute(string lootName, int maxValue = 4) : base(lootName, maxValue)
        {
            
        }
    }

    public class GoldAttribute : LootAttribute
    {
        public GoldAttribute(int maxValue = 6) : base("pièce(s) d'or", maxValue)
        {
            
        }
    }

    public class GemAttribute : LootAttribute
    {
        public GemAttribute(int maxValue = 3) : base("gemme(s)", maxValue)
        {
            
        }
    }

    class Hero
    {
        public void Loot(Monstre monster)
        {
            Type monsterType = monster.GetType();

            Console.WriteLine($"Je loot un {monsterType.Name}");

            IEnumerable<LootAttribute> lootAttributes = monsterType.GetCustomAttributes<LootAttribute>();

            foreach (LootAttribute attribute in lootAttributes)
            {                
                Console.WriteLine($"Je ramasse {attribute.Quantity} {attribute.LootName}");
            }

            Console.WriteLine();
        }
    }

    class Monstre
    {

    }

    [Leather("cuir(s)")]
    class Loup : Monstre
    {
        
    }

    [Gold]
    class Orque : Monstre
    { 
        
    }

    [Leather("écaille(s)", 25)]
    [Gold(600)]
    [Gem(50)]
    class Dragon : Monstre
    {
    }
}

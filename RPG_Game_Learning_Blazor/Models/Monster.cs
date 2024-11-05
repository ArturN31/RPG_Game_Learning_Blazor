namespace RPG_Game_Learning_Blazor.Models
{
    public class Monster(
        string name,
        string description,
        int level,
        int maxHealth,
        double currentHealth,
        int damage,
        int attackChance,
        int blockChance,
        int critChance,
        double experience,
        string imageName
        )
    {
        public enum AttackTypeEnum
        {
            Melee,
            Magic,
        }

        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public int Level { get; set; } = level;
        public int MaxHealth { get; set; } = maxHealth;
        public double CurrentHealth { get; set; } = currentHealth;
        public int Damage { get; set; } = damage;
        public AttackTypeEnum AttackType { get; set; }
        public int AttackChance { get; set; } = attackChance;
        public int BlockChance { get; set; } = blockChance;
        public int CritChance { get; set; } = critChance;
        public double ExperienceGivenToPlayer { get; set; } = experience;
        public string ImageName { get; set; } = imageName;

        public void SetCurrentHealth(double currentHealth)
        {
            CurrentHealth = currentHealth;
        }

        bool DidAttack()
        {
            Random rnd = new();
            return rnd.NextInt64(1, 100) < AttackChance;
        }

        public bool DidBlock()
        {
            Random rnd = new();
            return rnd.NextInt64(1, 100) < BlockChance;
        }

        bool IsCriticalHit()
        {
            Random rnd = new();
            return rnd.NextInt64(1, 100) < CritChance;
        }

        double CalculateDamage(int Damage, bool isCritical)
        {
            double damageMultiplier = isCritical ? 1.5 : 1;
            Random rnd = new Random();
            return rnd.Next(Damage / 4, Damage) * damageMultiplier;
        }

        public (double, bool) MonsterAttack(Monster Monster)
        {
            if (!DidAttack())
            {
                return (0, false);
            }

            bool isCritical = IsCriticalHit();
            double damageDealt = CalculateDamage(Monster.Damage, isCritical);

            return (damageDealt, isCritical);
        }
    }
}

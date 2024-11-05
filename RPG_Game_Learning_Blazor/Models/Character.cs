using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace RPG_Game_Learning_Blazor.Models
{
    public class Character(
        string name, 
        int level,
        double currentExperience, 
        double maxExperience, 
        double baseHealth, 
        double maxHealth, 
        double currentHealth, 
        double baseMana,
        double maxMana, 
        double currentMana)
    {
        public string Name { get; set; } = name;
        public int Level { get; set; } = level;
        public double CurrentExperience { get; set; } = currentExperience;
        public double MaxExperience { get; set; } = maxExperience;
        public double BaseHealth { get; set; } = baseHealth;
        public double MaxHealth { get; set; } = maxHealth;
        public double CurrentHealth { get; set; } = currentHealth;
        public double BaseMana { get; set; } = baseMana;
        public double MaxMana { get; set; } = maxMana;
        public double CurrentMana { get; set; } = currentMana;

        public void SetName(string name) { Name = name; }
        public void SetLevel(int level) { Level = level; }
        public void SetBaseHealth(double baseHealth) { BaseHealth = baseHealth; }
        public void SetCurrentExperience(double experience) { CurrentExperience = experience; }
        public void SetMaxExperience(double experience) { MaxExperience = experience; }
        public void SetMaxHealth(double health) { MaxHealth = health; }
        public void SetCurrentHealth(double health) { CurrentHealth = health; }
        public void SetBaseMana(double baseMana) { BaseMana = baseMana; }
        public void SetMaxMana(double mana) { MaxMana = mana; }
        public void SetCurrentMana(double mana) { CurrentMana = mana; }

        bool DidAttack(List<Skill> Skills, string TypeOfAttack)
        {
            Skill? Attack = Skills.FirstOrDefault(Skill => Skill.Name == TypeOfAttack);
            Random rnd = new();

            if (Attack == null) return false;
            return rnd.NextInt64(1, 100) < Attack.TriggerChance;
        }

        public bool DidBlock(List<Skill> Skills)
        {
            Skill? Block = Skills.FirstOrDefault(Skill => Skill.Name == "Block");
            Random rnd = new();

            if (Block == null) return false;
            return rnd.NextInt64(1, 100) < Block.TriggerChance;
        }

        bool IsCriticalHit(List<Skill> Skills)
        {
            Skill? Crit = Skills.FirstOrDefault(Skill => Skill.Name == "Critical Hit");
            Random rnd = new();

            if (Crit == null) return false;
            return rnd.NextInt64(1, 100) < Crit.TriggerChance;
        }

        static double CalculateDamage(string TypeOfAttack, int Damage, bool isCritical)
        {
            double damageMultiplier = isCritical ? 1.5 : 1;
            Random rnd = new();

            switch (TypeOfAttack)
            {
                case "Melee Damage":
                    return rnd.Next(Damage / 4, Damage) * damageMultiplier;
                case "Magic Damage":
                    int magicAttack = Damage * 2;
                    return rnd.Next(magicAttack / 2, magicAttack) * damageMultiplier;
                default:
                    throw new ArgumentException("Invalid attack type");
            }
        }

        public (double, bool) PlayerAttack(List<Skill> Skills, string TypeOfAttack)
        {
            //Melee or Magic
            Skill? AttackType = Skills.FirstOrDefault(skill => skill.Name == TypeOfAttack);

            if (!DidAttack(Skills, TypeOfAttack))
            {
                return (0, false);
            }

            if (AttackType != null)
            {
                bool isCritical = IsCriticalHit(Skills);
                double damageDealt = CalculateDamage(TypeOfAttack, AttackType.Level, isCritical);

                return (damageDealt, isCritical);
            } return (0, false);
        }

        public void GiveExperience(Character Character, Monster Monster, StringBuilder playerOutputBuilder)
        {
            double experienceToGive = Monster.ExperienceGivenToPlayer;

            //award experience and check for level up
            if (Character.CurrentExperience + experienceToGive >= Character.MaxExperience)
            {
                //calculate remaining experience after exceeding the limit
                double remainingExperience = Character.CurrentExperience + experienceToGive - MaxExperience;

                //award experience to reach the limit
                Character.SetCurrentExperience(Character.MaxExperience);

                playerOutputBuilder.AppendLine($"<p>{Name} has gained {Math.Round(experienceToGive - remainingExperience, 2)} experience by killing {Monster.Name}.</p>");

                //level up
                SetLevel(++Level);
                playerOutputBuilder.AppendLine($"<p>{Character.Name} has leveled up!</p>");
                playerOutputBuilder.AppendLine($"<p>Advanced to level {Character.Level}.</p>");

                //reset experience
                Character.SetCurrentExperience(0);

                //calculate and set new experience limit (consider using double for MaxExperience)
                SetMaxExperience(Character.MaxExperience * 1.15);

                //increase HP
                double increaseHP = Character.BaseHealth * 0.20;
                Character.SetMaxHealth(Character.MaxHealth + increaseHP);

                //increase MP
                double increaseMP = Character.BaseMana * 0.15;
                Character.SetMaxMana(Character.MaxMana + increaseMP);

                //restore HP to full
                Character.SetCurrentHealth(Character.MaxHealth);

                //restore MP to full
                Character.SetCurrentMana(Character.MaxMana);

                //award remaining experience after level up
                GiveExperience(Character, new Monster(
                    Monster.Name,
                    Monster.Description,
                    Monster.Level,
                    Monster.MaxHealth,
                    Monster.CurrentHealth,
                    Monster.Damage,
                    Monster.AttackChance,
                    Monster.BlockChance,
                    Monster.CritChance,
                    remainingExperience,
                    Monster.ImageName
                ), playerOutputBuilder);
            }
            else
            {
                //award experience without exceeding the limit
                Character.SetCurrentExperience(Character.CurrentExperience + experienceToGive);
                playerOutputBuilder.AppendLine($"<p>{Name} has gained {Math.Round(experienceToGive, 2)} experience after killing {Monster.Name}.</p>");
            }
        }
    }
}

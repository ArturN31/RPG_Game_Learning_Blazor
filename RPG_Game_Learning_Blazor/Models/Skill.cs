using Microsoft.AspNetCore.Components;
using System.Text;

namespace RPG_Game_Learning_Blazor.Models
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        public double CurrentExperience { get; set; }
        public double MaxExperience { get; set; }
        public double TriggerChance { get; set; }
        public double MaxTriggerChance { get; set; }
        public double ScalingFactor { get; set; }

        public Skill(
            string name,
            string description,
            int level,
            double currentExperience,
            double maxExperience,
            double triggerChance,
            double maxTriggerChance,
            double scalingFactor
        )
        {
            Name = name;
            Description = description;
            Level = level;
            CurrentExperience = currentExperience;
            MaxExperience = maxExperience;
            TriggerChance = triggerChance;
            MaxTriggerChance = maxTriggerChance;
            ScalingFactor = scalingFactor;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetLevel(int level)
        {
            Level = level;
        }

        public void SetCurrentExperience(double experience)
        {
            CurrentExperience = experience;
        }

        public void SetMaxExperience(double experience)
        {
            MaxExperience = experience;
        }

        public void SetTriggerChance(double triggerChance)
        {
            TriggerChance = triggerChance;
        }

        public void SetMaxTriggerChance(double maxTriggerChance)
        {
            MaxTriggerChance = maxTriggerChance;
        }

        public void SetScalingFactor(double scalingFactor)
        {
            ScalingFactor = scalingFactor;
        }

        public void GiveExperience(Skill usedSkill, double experienceToGive, StringBuilder outputBuilder)
        {
            //given + current experience exceeds the max experience limit
            //level up
            if (usedSkill.CurrentExperience + experienceToGive >= usedSkill.MaxExperience)
            {
                //experience required to level up
                double remindingExperience = usedSkill.MaxExperience - usedSkill.CurrentExperience;

                //level up
                usedSkill.SetLevel(++usedSkill.Level);

                //reset experience
                usedSkill.SetCurrentExperience(0);

                //calculate and set new experience limit
                usedSkill.SetMaxExperience(usedSkill.MaxExperience * 1.15);

                //increase the trigger chance
                if (usedSkill.Name == "Melee Attack" || 
                    usedSkill.Name == "Magic Attack" || 
                    usedSkill.Name == "Block" || 
                    usedSkill.Name == "Critical Hit" &&
                    usedSkill.TriggerChance < usedSkill.MaxTriggerChance)
                {
                    usedSkill.SetTriggerChance(usedSkill.TriggerChance + (usedSkill.TriggerChance * usedSkill.ScalingFactor / 4));
                }

                //output message
                outputBuilder.AppendLine($"<p>{usedSkill.Name} has gained {Math.Round(remindingExperience, 2)} experience.</p>"
                  + $"<p>Your skill has leveled up!</p>"
                  + $"<p>{usedSkill.Name} advanced to level {usedSkill.Level}.</p>");

                double experienceToGiveAfterLevelUp = experienceToGive - remindingExperience;

                if (experienceToGiveAfterLevelUp > 0)
                {
                    //award remaining experience after level up
                    GiveExperience(usedSkill, experienceToGiveAfterLevelUp, outputBuilder);
                }
            }
            else
            {
                //award experience without exceeding the limit
                usedSkill.SetCurrentExperience(usedSkill.CurrentExperience + experienceToGive);
                outputBuilder.AppendLine($"<p>{usedSkill.Name} has gained {Math.Round(experienceToGive, 2)} experience.</p>");
            }
        }
    }
}

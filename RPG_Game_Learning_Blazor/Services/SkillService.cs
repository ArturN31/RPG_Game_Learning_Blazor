using Microsoft.AspNetCore.Components;
using RPG_Game_Learning_Blazor.Models;
using System.Reflection.Emit;

namespace RPG_Game_Learning_Blazor.Services
{
    public class SkillService : SingletonBase
    {
        private List<Models.Skill> _skills = new List<Models.Skill>();

        public List<Models.Skill> Skills
        {
            get => _skills;
        }

        public void AddSkill(Models.Skill skill)
        {
            _skills.Add(skill);
        }

        public void RemoveSkill(Models.Skill skill)
        {
            _skills.Remove(skill);
        }

        public void RemoveAllSkills()
        {
            _skills.Clear();
        }

        public Models.Skill GetSkillByName(string name)
        {
            return _skills.FirstOrDefault(s => s.Name == name);
        }
    }
}

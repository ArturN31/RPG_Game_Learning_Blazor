namespace RPG_Game_Learning_Blazor.Services
{
    public class CharacterService
    {
        private Models.Character? _character;

        public Models.Character Character
        {
            get => _character ?? new Models.Character("", 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        public void CreateCharacter(
            string name, 
            int level, 
            int currentExperience, 
            int maxExperience, 
            double baseHealth,
            double maxHealth, 
            double currentHealth, 
            double baseMana,
            double maxMana, 
            double currentMana)
        {
            _character = new Models.Character(
                name, 
                level, 
                currentExperience, 
                maxExperience, 
                baseHealth,
                maxHealth, 
                currentHealth, 
                baseMana,
                maxMana, 
                currentMana);
        }

        public void RemoveCharacter()
        {
            _character = null;
        }

        public bool CharacterExists()
        {
            return _character != null ? true : false;
        }
    }
}

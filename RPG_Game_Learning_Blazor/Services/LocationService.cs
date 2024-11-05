using RPG_Game_Learning_Blazor.Models;

namespace RPG_Game_Learning_Blazor.Services
{
    public class LocationService
    {
        private List<Models.Location> _locations = new();

        public List<Models.Location> Locations
        {
            get => _locations;
        }

        public void AddLocation(Models.Location location)
        {
            _locations.Add(location);
        }

        public void RemoveLocation(Models.Location location)
        {
            _locations.Remove(location);
        }

        public void RemoveAllLocations()
        {
            _locations.Clear();
        }

        public Models.Location GetLocationByName(string name)
        {
            return _locations.FirstOrDefault(s => s.Name == name);
        }

        public void RemoveMonsterFromLocation(string LocationName, string MonsterName)
        {
            foreach (Location location in _locations)
            {
                if (location.Name == LocationName)
                {
                    foreach(Monster monster in location.CurrentMonsters)
                    {
                        if (monster.Name == MonsterName) {
                            location.CurrentMonsters.Remove(monster);
                            break;
                        }
                    }
                }
            }
        }

        public void RespawnMonstersInLocation(Location location)
        {
            Location locationToUpdate = _locations.FirstOrDefault(l => l.Name == location.Name);

            // Respawn all monsters by copying DefaultMonsters
            locationToUpdate?.CurrentMonsters.AddRange(locationToUpdate.DefaultMonsters.Select(monster => new Monster(
                monster.Name,
                monster.Description,
                monster.Level,
                monster.MaxHealth,
                monster.CurrentHealth,
                monster.Damage,
                monster.AttackChance,
                monster.BlockChance,
                monster.CritChance,
                monster.ExperienceGivenToPlayer,
                monster.ImageName
            )));
        }
    }
}

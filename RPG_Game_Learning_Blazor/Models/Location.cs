namespace RPG_Game_Learning_Blazor.Models
{
    public class Location(
        string name,
        string description,
        bool respawningMonsters,
        int defaultLocationRespawnTimer,
        int currentLocationRespawnTimer,
        List<Monster> defaultMonsters,
        List<Monster> currentMonsters,
        string imageName
        )
    {
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public bool RespawningMonsters { get; set; } = respawningMonsters;
        public int DefaultLocationRespawnTimer { get; set; } = defaultLocationRespawnTimer;
        public int CurrentLocationRespawnTimer { set; get; } = currentLocationRespawnTimer;
        public List<Monster> DefaultMonsters { get; set; } = defaultMonsters;
        public List<Monster> CurrentMonsters { get; set; } = currentMonsters;
        public string ImageName { get; set; } = imageName;

        public void SetRespawningMonsters( bool respawningMonsters ) { RespawningMonsters = respawningMonsters; }
        public void SetCurrentLocationRespawnTimer( int currentLocationRespawnTimer ) { CurrentLocationRespawnTimer = currentLocationRespawnTimer; }
        public void SetCurrentMonsters( List<Monster> currentMonsters ) { CurrentMonsters = currentMonsters; }
    }
}

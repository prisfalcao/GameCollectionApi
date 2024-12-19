namespace GameCollectionApi
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Developer { get; set; }
        public string Condition { get; set; }
    }
}

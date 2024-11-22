namespace RPSGame.Domain
{
    public class Player
    {
        public string Name { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Draws { get; set; }

        public Player(string _name) 
        {
            Name = _name;
        }

        public Player() 
        {
            Name = "";
        }
    }
}

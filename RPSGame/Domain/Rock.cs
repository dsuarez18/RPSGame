namespace RPSGame.Domain
{
    public class Rock : Choice
    {
        public Rock() : base("Rock")
        {      
        }

        public override Result compareToPaper()
        {
            return Result.Lose;
        }

        public override Result compareToScissors()
        {
            return Result.Win;
        }

        public override Result compareToRock()
        {
            return Result.Draw;
        }
    }
}

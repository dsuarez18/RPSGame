namespace RPSGame.Domain
{
    public class Scissors : Choice
    {
        public Scissors() : base("Scissors"){ }

        public override Result compareToPaper()
        {
            return Result.Win;
        }

        public override Result compareToScissors()
        {
            return Result.Draw;
        }

        public override Result compareToRock()
        {
            return Result.Lose;
        }
    }
}

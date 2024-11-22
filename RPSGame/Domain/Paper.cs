namespace RPSGame.Domain
{
    public class Paper : Choice
    {
        public Paper() :base("Paper") { }

        public override Result compareToPaper()
        {
            return Result.Draw;
        }

        public override Result compareToScissors()
        {
            return Result.Lose;
        }

        public override Result compareToRock()
        {
            return Result.Win;
        }
    }
}

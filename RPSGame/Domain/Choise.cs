namespace RPSGame.Domain
{
    public abstract class Choice
    {
        private readonly string _name;

        protected Choice(string name)
        {
            _name = name;
        }

        protected Choice() { }

        public abstract Result compareToRock();
        public abstract Result compareToPaper();
        public abstract Result compareToScissors();

        public string GetName() {
            return _name;
        }
    }
}

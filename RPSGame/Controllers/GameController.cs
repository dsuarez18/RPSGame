using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using RPSGame.Domain;

namespace RPSGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        private static int player1Rounds = 0;
        private static int player2Rounds = 0;
        private static int player1Wins = 0;
        private static int player2Wins = 0;
        private static int roundsPlayed = 0;
        private static List<Round> rounds = [];
        private static List<Choice> choices = [new Rock(), new Paper(), new Scissors()];

        [HttpGet("choices")]
        public IActionResult GetChoices()
        {
            return Ok(new
            {
                Choices = choices.Select(choice => choice.GetName())
            });
        }

        [HttpPost("play")]
        public IActionResult Play([FromBody] JsonObject data)
        {
            Result result = GetGameResult((string)data["player1choice"], (string)data["player2choice"]);
            roundsPlayed++;
            Player winner = new();
            if (result.Equals(Result.Win))
            {
                player1Rounds++;
                winner = new(((string?)data["player1name"]));
            }
            else if (result.Equals(Result.Lose))
            {
                player2Rounds++;
                winner = new(((string?)data["player2name"]));
            } else
            {
                winner = new("Draw");
            }
            Round round = new(roundsPlayed, winner);
            rounds.Add(round);

            if (player1Rounds == 3 || player2Rounds == 3)
            {

                if (player1Rounds > player2Rounds)
                {
                    player1Wins++;
                }
                else
                {
                    player2Wins++;
                }
                player1Rounds = 0;
                player2Rounds = 0;
                roundsPlayed = 0;
                rounds.Clear();
                return Ok(new
                {
                    Result = winner.Name,
                    Message = winner.Name + " is the new EMPEROR!",
                    Player1Wins = player1Wins,
                    Player2Wins = player2Wins,
                });
            }


            return Ok(new
            {
                Result = winner.Name,
                Player1Rounds = player1Rounds,
                Player2Rounds = player2Rounds,
                RoundsPlayed = roundsPlayed,
                Rounds = rounds
            });
        }

        private Result GetGameResult(string player1choice, string player2choice)
        {
            return ChoiceComparator(player1choice,player2choice);
        }

        private Choice ChoiceFactory(string choice)
        {
            return choice switch
            {
                "Rock" => new Rock(),
                "Paper" => new Paper(),
                "Scissors" => new Scissors(),
                _ => throw new NotImplementedException(),
            };
        }

        private Result ChoiceComparator(string player1Choice, string player2Choice)
        {
            Choice player1 = ChoiceFactory(player1Choice);
            return player2Choice switch
            {
                "Rock" => player1.compareToRock(),
                "Paper" => player1.compareToPaper(),
                "Scissors" => player1.compareToScissors(),
                _ => throw new NotImplementedException(),
            };
        }

    }

    //public class Player
    //{
    //    public string Name { get; set; }
    //    public string Choice { get; set; }

    //    public Player() {
    //        this.Name = "";
    //        this.Choice = "";
    //    }
    //    public Player(string name, string choice) { 
    //        this.Name = name;
    //        this.Choice = choice;
    //    }
    //}

    public class Round
    {
        public Player Winner { get; set; } = new Player();
        public int RoundNumber { get; set; }

        public Round()
        {
            this.RoundNumber = 0;
            this.Winner = new Player();
        }
        public Round(int roundNumber, Player player)
        {
            this.RoundNumber = roundNumber;
            this.Winner = player;
        }
    }

}

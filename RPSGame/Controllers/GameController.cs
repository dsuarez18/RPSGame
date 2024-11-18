using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace RPSGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        public enum Choice
        {
            Piedra,
            Papel,
            Tijera
        }

        private static int player1Rounds = 0;
        private static int player2Rounds = 0;
        private static int player1Wins = 0;
        private static int player2Wins = 0;
        private static int roundsPlayed = 0;
        private static string[,] winnerMatrix = new string[Enum.GetNames(typeof(Choice)).Length, Enum.GetNames(typeof(Choice)).Length];
        private static List<Round> rounds = [];

        public GameController()
        {
            LoadWinnerMatrix();
        }

        private static void LoadWinnerMatrix()
        {
            for (int i = 0; i < Enum.GetNames(typeof(Choice)).Length; i++)
            {
                for (int j = 0; j < Enum.GetNames(typeof(Choice)).Length; j++)
                {
                    if (i == j)
                        winnerMatrix[i,j] = "Draw";
                    else if ((i == 0 && j == 2) || (i == 1 && j == 0) || (i == 2 && j == 1))
                        winnerMatrix[i,j] = "Win";
                    else
                        winnerMatrix[i,j] = "Lose";
                }
            }
        }

        [HttpGet("choices")]
        public IActionResult GetChoices()
        {
            return Ok(new
            {
                Choices = Enum.GetValues(typeof(Choice))
            });
        }

        [HttpPost("play")]
        public IActionResult Play([FromBody] JsonObject data)
        {
            string result = GetGameResult((int)data["player1choice"], (int)data["player2choice"]);
            roundsPlayed++;
            Player winner = new();
            if (result == "Win")
            {
                player1Rounds++;
                winner = new(((string?)data["player1name"]), ((int)data["player1choice"]));
            }
            else if (result == "Lose")
            {
                player2Rounds++;
                winner = new(((string?)data["player2name"]), ((int)data["player2choice"]));
            } else
            {
                winner = new("Draw",0);
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
                    Result = result,
                    Message = winner.Name + " is the new EMPEROR!",
                    Player1Wins = player1Wins,
                    Player2Wins = player2Wins,
                });
            }


            return Ok(new
            {
                Result = result,
                Player1Rounds = player1Rounds,
                Player2Rounds = player2Rounds,
                RoundsPlayed = roundsPlayed,
                Rounds = rounds
            });
        }

        private static string GetGameResult(int player1choice, int player2choice)
        {
            return winnerMatrix[player1choice,player2choice];
        }

    }

    public class Player
    {
        public string Name { get; set; }
        public int Choice { get; set; }

        public Player() {
            this.Name = "";
            this.Choice = 0;
        }
        public Player(string name, int choice) { 
            this.Name = name;
            this.Choice = choice;
        }
    }

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

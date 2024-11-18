import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'
import { FormsModule } from '@angular/forms'
import axios from 'axios';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [
    CommonModule,
    FormsModule
  ]
})

export class AppComponent implements OnInit {
  winnerMatrix: string[][] = [];
  player1Name: string = "";
  player2Name: string = "";
  player1Choice: number = 0;
  player2Choice: number = 0;
  player1Wins: number = 0;
  player2Wins: number = 0;
  player1Rounds: number = 0;
  player2Rounds: number = 0;
  roundNumber: number = 1;
  roundResult: string = "";
  roundEnds: boolean = false;
  rounds: any[] = [];
  currentPlayer: boolean = true;
  newGame: boolean = true;
  choices: number[] = [];
  gameOver: boolean = false;
  endMessage: string = "";

  async ngOnInit() {
     await this.getChoices();
  }

  async getChoices() {
    try {
      const response = await axios.get('http://localhost:5109/api/game/choices');
      this.choices = response.data.choices || [];
    } catch (error) {
      console.error('Choices could not be obtained');
    }
  }

  startGame() {
    this.newGame = false;
  }

  goToMain() {
    this.gameOver = false;
    this.newGame = true;
  }

  onSelected() {
    let choice = (<HTMLSelectElement>document.getElementById("choiseSelect")).value;
    if (this.currentPlayer) {
      this.player1Choice = parseInt(choice);
    } else {
      this.player2Choice = parseInt(choice);
    }
  }

  checkPlay() {
    if ((<HTMLSelectElement>document.getElementById("choiseSelect")).value != "-1") {
      if (this.currentPlayer) {
        this.currentPlayer = !this.currentPlayer;
      } else {
        this.play(this.player1Name, this.player1Choice, this.player2Name, this.player2Choice)
      }
      (<HTMLSelectElement>document.getElementById("choiseSelect")).value = "-1";
    }
  }

  nextRound() {
    this.roundEnds = false;
  }

  async play(name1: string, choice1: number, name2: string, choice2: number) {
    try {
      const response = await axios.post('http://localhost:5109/api/game/play', {
        player1name: name1,
        player1choice: choice1,
        player2name: name2,
        player2choice: choice2
      }, {
        headers: {
          "Content-Type": "application/json"
        }
      });

      this.roundResult = response.data.result;
      this.currentPlayer = !this.currentPlayer;
      if (response.data.message) {
        this.player1Wins = response.data.player1Wins;
        this.player2Wins = response.data.player2Wins;
        this.gameOver = true;
        this.roundNumber = 1;
        this.rounds = [];
        this.endMessage = response.data.message;
      } else {
        this.player1Rounds = response.data.player1Rounds;
        this.player2Rounds = response.data.player2Rounds;
        this.roundNumber++;
        this.roundEnds = true;
        this.rounds = response.data.rounds;
      }

    } catch (error) {
      console.error('Error playing', error);
    }
  }

}

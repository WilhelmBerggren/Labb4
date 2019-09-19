﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Labb4
{
    public class Game
    {
        private Player player;
        private Level level;
        private int mapWidth;
        private int mapHeight;

        public int MapWidth { get => mapWidth; }
        public int MapHeight { get => mapHeight; }
        public Player Player { get => player; }
        public Level Level { get => level; }

        private List<KeyValuePair<string, int>> scores;

        public Game()
        {
            this.scores = new List<KeyValuePair<string, int>>();
        }

        public void Start()
        {
            this.player = new Player(this, 2, 2, 0);
            this.mapWidth = Console.WindowWidth / 2;
            this.mapHeight = Console.WindowHeight - 1;
            this.level = new Level(this);
            GameLoop();
        }

        private void GameLoop()
        {
            Console.Clear();
            Console.CursorVisible = false;
            while (player.Moves < 100)
            {
                Draw();
                WaitForInput();
            }
            Console.Clear();
            GameOver();
            Console.ReadKey();
        }

        private void GameOver()
        {
            Console.WriteLine("Insert name:");
            string userInput = Console.ReadLine();
            string name = ScoreNameCriterias(userInput, out name);

            Console.Clear();
            Console.WriteLine(
                "====================================\n" +
                "             SCORELIST               " +
                "\n===================================="
                );

            scores.Add(new KeyValuePair<string, int>(name, level.rooms.Count));
            Console.WriteLine($"Name:\t\t\tFinal floor:");
            foreach (var i in scores.OrderByDescending(score => score.Value))
            {
                Console.WriteLine($"{i.Key}\t\t\t{i.Value}");
            }
            Console.WriteLine("\n\nPress any key to restart...");
            Console.ReadKey(true);
            Console.Clear();
            Start();
        }

        private string ScoreNameCriterias(string userInput, out string name)
        {
            if (string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput))
                name = "Anonymous";

            else if (userInput.Length > 15)
                name = userInput.Remove(14);

            else
                name = userInput;
            return name;
        }

        private void WaitForInput()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    player.Move(0, -1);
                    break;
                case ConsoleKey.A:
                    player.Move(-1, 0);
                    break;
                case ConsoleKey.S:
                    player.Move(0, +1);
                    break;
                case ConsoleKey.D:
                    player.Move(+1, 0);
                    break;
            }
        }
        private double DistanceFromPlayer(int x, int y)
        {
            return Math.Sqrt(Math.Pow((player.PosX - x), 2) + Math.Pow((player.PosY - y), 2));
        }
        private void Draw()
        {
            for (int row = 0; row < mapWidth; row++)
            {
                for (int column = 0; column < mapHeight; column++)
                {
                    Tile currentTile = level.currentRoom.Map[row, column];
                    char c = currentTile.Representation;

                    if (DistanceFromPlayer(row, column) < 2.5)
                    {
                        currentTile.Visible = true;
                    }

                    if(!currentTile.Visible)
                    {
                        c = ' ';
                    }

                    if (player.PosX == row && player.PosY == column)
                        c = 'X';

                    PrintChar(c, currentTile.Color, row, column);
                }
            }
            Console.Write($"Moves: {player.Moves}, rooms: {level.rooms.Count}");
        }
        private void PrintChar(char c, ConsoleColor consoleColor, int x, int y)
        {
            Console.SetCursorPosition(x * 2, y);
            Console.ForegroundColor = consoleColor;
            Console.Write(c + " ");
        }
    }
}

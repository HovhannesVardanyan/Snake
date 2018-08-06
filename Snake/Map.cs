using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Map
{
    private char[][] map;
    private Random generator;
    private int BonusX = -1;
    private int BonusY = -1;
   

    public (int a, int b) Dimension { get { return (map.Length,map[0].Length); } }
    
    public Map(int a,int b)
    {
        map = new char[a][];
        generator = new Random();
        for (int i = 0; i < a; i++)
        {
            map[i] = new char[b];
            for (int j = 0; j < b; j++)
                map[i][j] = '-';
        }
    }

    public void Draw(Snake snake)
    {
        DrawBonus();
        DrawSnake(snake);
        DisplayMap();
    }
    public void RemoveTail(Snake snake)
    {
        int[] x = snake.X;
        int[] y = snake.Y;

        map[x[snake.Length - 1]][y[snake.Length - 1]] = 'O';

        InsertCharacter(x[snake.Length - 1], y[snake.Length - 1], ConsoleColor.Green, 'O');

        map[x[0]][y[0]] = '-';

        InsertCharacter(x[0], y[0], ConsoleColor.DarkBlue, '-');
    }
    public (int x, int y) GenerateBonus(Snake snake)
    {
        RemoveBonus();
        var gen = GenerateCordinates();
        while (!IsProperPlaceToPutABonus(gen.x,gen.y,snake))
        {
             gen = GenerateCordinates();
        }
        BonusX = gen.x;
        BonusY = gen.y;
        return gen;

    }
    private void DrawBonus()
    {
        if (BonusX >= 0 && BonusY >= 0)
        {
            map[BonusX][BonusY] = '*';
            InsertCharacter(BonusX, BonusY, ConsoleColor.Red, '*');

        }
    }
    private void RemoveBonus()
    {
        if(BonusX >=0 && BonusY >= 0)
            InsertCharacter(BonusX, BonusY, ConsoleColor.DarkBlue, '-');

        BonusX = -1;
        BonusY = -1;
      
    }
    private (int x, int y) GenerateCordinates()
        => (this.generator.Next(0, map.Length), this.generator.Next(0, map[0].Length));
    private void DrawSnake(Snake snake)
    {
        int[] x = snake.X;
        int[] y = snake.Y;
        map[x[snake.Length - 1]][y[snake.Length - 1]] = snake.Direction;

        InsertCharacter(x[snake.Length - 1], y[snake.Length - 1], ConsoleColor.Yellow, snake.Direction);

    }
    private bool IsProperPlaceToPutABonus(int x, int y,Snake snake)
    {
        for (int i = 0; i < snake.Length; i++)
            if (x == snake.X[i] && y == snake.Y[i])
                return false;
        return true;
    }
    private void InsertCharacter(int x, int y,ConsoleColor color,char ch)
    {
        Console.SetCursorPosition(2*y, x);
        Console.ForegroundColor = color;
        Console.Write(ch);
    }
    private void DisplayMap()
    {
        Console.Clear();
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[0].Length; j++)
            {
                if (map[i][j] == 'O')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(map[i][j] + " ");
                }
                else if (map[i][j] == '>' || map[i][j] == '<' || map[i][j] == 'v' || map[i][j] == '^')
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(map[i][j] + " ");
                }

                else if (map[i][j] == '*')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(map[i][j] + " ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(map[i][j] + " ");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine();
        }
    }
    public void Refresh(Snake snake)
    {
        DrawBonus();
        DrawSnake(snake);

    }

}


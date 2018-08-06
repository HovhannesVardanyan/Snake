using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



class Game
{
    private Map map;
    private Snake snake;
    
    public Game(int a, int b)
    {
        map = new Map(a,b);
        snake = new Snake(a*b);
    }
    private void Regulations()
    {
        Console.SetWindowSize(83, 28);
        Console.SetBufferSize(100, 50);
    }
    public void Start()
    {
        //Regulations();
        this.map.Draw(snake);
        ConsoleKeyInfo keyinfo = Console.ReadKey();
        ConsoleKeyInfo keyinfox = keyinfo;
        bool lost = false;
        var gen = map.GenerateBonus(snake);
        bool won = false;
        

        while (true)
        {
            if (lost || won)
                break;
            if (Console.KeyAvailable)
            {
                keyinfo = Console.ReadKey();

                if (keyinfo.Key == ConsoleKey.R)
                    Restart();

                int index = (int)keyinfo.Key - (int)keyinfox.Key;
                if ( !(index == 2 || index == -2 ))
                    keyinfox = keyinfo;

            }
           
            map.RemoveTail(snake);
            switch (snake.Move(keyinfox.Key, map.Dimension.a, map.Dimension.b, gen.x, gen.y))
            {
                case Status.OK:
                    // nothing to do here
                    break;
                case Status.AteBonus:
                    won = !snake.Elongate();
                    if (!won)
                        gen = map.GenerateBonus(snake);
                    break;
                case Status.AteMyself:
                    lost = true;
                    break;
               
                default:
                    break;
                    
            }
            this.map.Refresh(snake);
            System.Threading.Thread.Sleep(200 - 40*(snake.Length/15));
        }
        Console.SetCursorPosition(0, map.Dimension.a);
        if (lost)
        {
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You lost!");

            
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You won");
        }



        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Press R to restart or E to exit");
        while (true)
        {

            
            keyinfo = Console.ReadKey();
            if (keyinfo.Key == ConsoleKey.R)
                Restart();
            else if (keyinfo.Key == ConsoleKey.E)
                break;
            Console.SetCursorPosition(0, Console.CursorTop);
        }
       
        
    }
    public void Restart()
    {
        map = new Map(map.Dimension.a, map.Dimension.b);
        snake = new Snake(map.Dimension.a*map.Dimension.b);
        Start();
    }
}


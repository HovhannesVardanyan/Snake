using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum Status {
    OK,
    AteBonus,
    AteMyself,
   
}

class Snake
{
   
    public int Length { get; private set; }
    public int[] X { get; private set; }
    public int[] Y { get; private set; }
    private ConsoleKey dir;

    public char Direction {
        get {
            char head = '>';
            switch (dir)
            {
                case ConsoleKey.LeftArrow:
                    head = '<';
                    break;
                case ConsoleKey.UpArrow:
                    head = '^';
                    break;
                case ConsoleKey.RightArrow:
                    head = '>';
                    break;
                case ConsoleKey.DownArrow:
                    head = 'v';
                    break;

            }
            return head;
        }
    }


    // constructor
    public Snake(int dim)
    {

        dir = ConsoleKey.RightArrow;
        this.Length = 1;
        X = new int[dim];
        Y = new int[dim];
    }
   
    // Actions
    public Status Move(ConsoleKey key, int a, int b,int xBonus, int yBonus)
    {
        dir = key;
        CutTheTale();
        int xo =  X[Length - 1];
        int yo =  Y[Length - 1];
        
        switch (key)
        {
            case ConsoleKey.LeftArrow:
                
                yo = (yo  - 1) % (b);
                break;
            case ConsoleKey.UpArrow:
                xo = (xo - 1) % a;
                break;
            case ConsoleKey.RightArrow:
                yo = (yo + 1) % (b);
                break;
            case ConsoleKey.DownArrow:
                xo = (xo + 1) % a;
                break;
    
        }

        if (xo < 0)
            xo += a;
        if (yo < 0)
            yo += b;

        X[Length - 1] = xo;
        Y[Length - 1] = yo;
      
        if (xBonus == xo && yBonus == yo)
            return Status.AteBonus;

        for (int i = 0; i < this.Length - 1; i++)
            if (xo == X[i] && yo == Y[i])
                    return Status.AteMyself;

        return Status.OK;
    }
    public bool Elongate()
    {
        if(Length >= 60)
        {
            return false;
        }
        Length++;

        X[Length - 1] = X[Length - 2];
        Y[Length - 1] = Y[Length - 2];

        return true;
        
    }
    public void ReturnBothEnds(out int x0, out int y0, out int xl, out int yl)
    {
        x0 = this.X[0];
        y0 = this.Y[0];
        xl = this.X[this.Length - 1];
        yl = this.Y[this.Length - 1];
    }


    // inner Actions
    private void CutTheTale()
    {
        for (int i = 0; i < this.Length - 1; i++)
        {
            this.X[i] = this.X[i + 1];
            this.Y[i] = this.Y[i + 1];
        }
    }

}


using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public struct Coordinates
{
    public readonly int column;
    public readonly int row;

    public Coordinates(int column, int row)
    { 
        this.column = column;
        this.row = row;
    }

    public static Coordinates zero => new Coordinates(0, 0);
    public static Coordinates left => new Coordinates(-1, 0);
    public static Coordinates right => new Coordinates(1, 0);
    public static Coordinates up => new Coordinates(0, -1);
    public static Coordinates down => new Coordinates(0, 1);

    public static Coordinates[] mooreNeightborhood =
    {
        new Coordinates(-1,-1),
        new Coordinates(0,-1),
        new Coordinates(1,-1),
        new Coordinates(-1,0),
        new Coordinates(1,0),
        new Coordinates(-1,1),
        new Coordinates(0,1),
        new Coordinates(1,1),
    };

    public static Coordinates[] vonNeumannNeighborhood =
    {
        new Coordinates(0,-1),
        new Coordinates(-1,0),
        new Coordinates(1,0),
        new Coordinates(0,1)
    };

    public static Coordinates FromVector2(Vector2 vector) => new Coordinates((int)Math.Round(vector.X), (int)Math.Round(vector.Y));
    public Vector2 ToVector2 => new Vector2(column, row);


    public static Coordinates operator -(Coordinates a, Coordinates b)
        => new Coordinates(a.column - b.column, a.row - b.row);

    public static Coordinates operator +(Coordinates a, Coordinates b)
        => new Coordinates(a.column+b.column, a.row+b.row);
    
    public static Coordinates operator *(int scalar, Coordinates a)
        => new Coordinates(a.column*scalar, a.row*scalar);
    
    public static Coordinates operator *(Coordinates a, int scalar)
        => new Coordinates(a.column*scalar, a.row*scalar);

    public static Coordinates operator -(Coordinates a)
        => new Coordinates(-a.column, -a.row);

    public static double distance(Coordinates a, Coordinates b)
    {
        int deltaX = a.column - b.column;
        int deltaY = a.row - b.row;
        return Math.Sqrt(deltaX*deltaX + deltaY*deltaY);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is Coordinates other && column == other.column && row == other.row;

    public static bool operator ==(Coordinates a, Coordinates b)
        => a.Equals(b);
    public static bool operator !=(Coordinates a, Coordinates b)
        => !a.Equals(b);

    public static Coordinates Random(int maxColumn, int maxRow)
    {
        Random random = new Random();
        int column = random.Next(0,maxColumn);
        int row = random.Next(0,maxRow);
        return new Coordinates(column, row);
    }
    public override int GetHashCode() => HashCode.Combine(column, row);
    
    public static double GetAngle(Coordinates coordinates)
    {
        Vector2 a = coordinates.ToVector2;
        return Math.Atan2(0 - a.Y, 0 - a.X) * (180 / Math.PI);
    }

    public static int GetAngle(Coordinates previous, Coordinates current, Coordinates next)
    {
        int id = 0;
        /*if (current + Coordinates.left == next || current + Coordinates.left == previous) id += 1;
        if (current + Coordinates.up == next || current + Coordinates.up == previous) id += 2;
        if (current + Coordinates.right == next || current + Coordinates.right == previous) id += 4;
        if (current + Coordinates.down == next || current + Coordinates.down == previous) id += 8;
        */
        if (current + Coordinates.left == previous && current + Coordinates.up == next) id = 45;
        if (current + Coordinates.left == previous && current + Coordinates.right == next) id = 90; //
        if (current + Coordinates.left == previous && current + Coordinates.down == next) id = 135; //
        if (current + Coordinates.up == previous && current + Coordinates.left == next) id = 225;
        if (current + Coordinates.up == previous && current + Coordinates.right == next) id = 135;
        if (current + Coordinates.up == previous && current + Coordinates.down == next) id = 180; 
        if (current + Coordinates.right == previous && current + Coordinates.left == next) id = 270; //
        if (current + Coordinates.right == previous && current + Coordinates.up == next) id = 270; //
        if (current + Coordinates.right == previous && current + Coordinates.down == next) id = 225;
        if (current + Coordinates.down == previous && current + Coordinates.left == next) id = 315;
        if (current + Coordinates.down == previous && current + Coordinates.up == next) id = 0; //
        if (current + Coordinates.down == previous && current + Coordinates.right == next) id = 45;

        return id;
    }

    public static int GetAngle(Coordinates previous, Coordinates current)
    {
        int id = 0;
        if (current + Coordinates.left == previous) id += 1;
        if (current + Coordinates.up == previous) id += 2;
        if (current + Coordinates.right == previous) id += 4;
        if (current + Coordinates.down == previous) id += 8;

        switch (id)
        {
            case 1:
                id = 0;
                break;
            case 2:
                id = 90;
                break;
            case 4:
                id = 180;
                break;
            case 8:
                id = 270;
                break;
            default:
                id = 0;
                break;
        }
        return id;
    }

    public override string ToString() => $"({column},{row})";
}


﻿using System;
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
    

    public override string ToString() => $"({column},{row})";
}


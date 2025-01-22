using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum EnumSnakeElement
{
    Head,
    Element,
    Queue
}

public class SnakeElement
{
    public EnumSnakeElement Type { get; private set; }

    public SnakeElement(EnumSnakeElement snakeElement)
    {
        Type = snakeElement;
    }
}

using System;
using System.Security.AccessControl;
using Raylib_cs;

namespace raycasting;


public class RectangleObs : IObstacle
{
    public int x;
    public int y;
    public int width;
    public int height;
    public const string type = "rect";
    public RectangleObs(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }
    public bool CheckForCollision(int x, int y)
    {
        return x+2 > this.x && x<= this.x + width && y+2 > this.y && y <= this.y + height;
    }

    public void Draw(Image img, Color clr)
    {
        Raylib.ImageDrawRectangle(ref img, this.x, this.y, this.width, this.height, clr);
    }
}
using System;
using System.Security.AccessControl;
using Raylib_cs;

namespace raycasting;


public class CircleObs : IObstacle
{

    public int x;
    public int y;
    public int radius;

    public const string type = "circle";
    public CircleObs(int x, int y , int radius)
    {
        this.x = x;
        this.y =y;
        this.radius = radius;
    }
    public bool CheckForCollision(int x, int y)
    {
        int rel_x = x - this.x;
        int rel_y = y - this.y;
        return rel_x * rel_x + rel_y * rel_y <= radius * radius;
    }

    public void Draw(Image img, Color clr)
    {
        Raylib.ImageDrawCircle(ref img, this.x,this.y,this.radius,clr);
    }
}
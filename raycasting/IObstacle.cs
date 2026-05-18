using System;
using System.Security.AccessControl;
using Raylib_cs;

namespace raycasting;

public interface IObstacle
{
    public const string type = "";
    bool CheckForCollision(int x, int y);

    void Draw(Image img, Color clr);
}
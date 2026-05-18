using System;
using System.Security.AccessControl;
using Raylib_cs;

namespace raycasting;

class Program
{
    static List<IObstacle> obstacles = new List<IObstacle>();
    const int raysCount = 800;
   // static Circle obstacle = new Circle { x = 600, y = 300, radius = 70};
      static CircleObs obstacle = new CircleObs(600,300,70);
      static RectangleObs obstacle1 = new RectangleObs(800,400,50,70);
      static RectangleObs obstacle2 = new RectangleObs(300,300,50,30);
      
    struct Circle  //currently useless
    {
        public int x;
        public int y;
        public int radius;
    }
    
    static void DrawRays(int x, int y, Image img) //draws rays from a point
    {
        for(int i = 0; i < raysCount; i++)
        {
            bool end = false;
            double rel_y = y;
            double rel_x = x;

            while(!end)
            {
                rel_x += Math.Cos(((double)i/raysCount)*2*Math.PI);
                rel_y += Math.Sin(((double)i/raysCount)*2*Math.PI);
               // Console.WriteLine(((double)i/raysCount)*2*Math.PI);
                 if(rel_y >= img.Height || rel_x >= img.Width || rel_y < 0 || rel_x < 0) end = true;
                 foreach(IObstacle obstacle in obstacles)
                 {
                    if(obstacle.CheckForCollision((int)rel_x,(int)rel_y))  end = true;
                 }
                 Raylib.ImageDrawPixelV(ref img,new System.Numerics.Vector2((float)rel_x,(float)rel_y),Color.White);
                // Raylib.ImageDrawRectangle(ref img,(int)rel_x,(int)rel_y,3,3,Color.White);
                
            }
            //Console.WriteLine(i);
           
        }
    }
    static void Main(string[] args)
    {
        bool removed_obs = false;
        obstacles.Add(obstacle);
        obstacles.Add(obstacle1);
        obstacles.Add(obstacle2);
        int obstacle_v = 1;
        int windowWidth = 1000;
        int windowHight = 800;
       // Circle circle = new Circle { x = 400, y = 300, radius = 50 };
       CircleObs raysSource = new CircleObs(400,300,50);
       Raylib.InitWindow(windowWidth, windowHight, "Raycasting");
       Raylib.SetTargetFPS(60);
       Image img = Raylib.GenImageColor(windowWidth, windowHight, Color.Black);
       Raylib.ImageDrawCircle(ref img, obstacle.x, obstacle.y, obstacle.radius, Color.Red);
       Texture2D texture = Raylib.LoadTextureFromImage(img);
       while(!Raylib.WindowShouldClose())
       {

            Raylib.ImageClearBackground(ref img, Color.Black);
            
            if(Raylib.IsMouseButtonPressed(MouseButton.Right))
            {
                int mouseX = Raylib.GetMouseX();
                int mouseY = Raylib.GetMouseY();
                foreach(IObstacle obstacle in obstacles)
                {
                    if(obstacle.CheckForCollision(mouseX, mouseY))
                    {
                        obstacles.Remove(obstacle);
                        removed_obs = true;
                        break;
                    }
                }
                if(!removed_obs)
                obstacles.Add(new RectangleObs(mouseX,mouseY,20,20));
                removed_obs = false;
            }

            if(Raylib.IsMouseButtonDown(MouseButton.Left)) //source dragging
            {
                int mouseX = Raylib.GetMouseX();
                int mouseY = Raylib.GetMouseY();
                raysSource.x = mouseX;
                raysSource.y = mouseY;
                
                //texture = Raylib.LoadTextureFromImage(img);
            }
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            if(obstacle.radius + obstacle.y >= windowHight) // obstacle circle movement
            {
                obstacle_v *= -1;
                obstacle.y = windowHight - obstacle.radius;
            }
            if(obstacle.y - obstacle.radius <= 0)
            {
                 obstacle_v *= -1;
                 obstacle.y = obstacle.radius;
            }
            obstacle.y += (int)(obstacle_v*Raylib.GetFrameTime()*60);
           
            foreach(IObstacle obstacle in obstacles) //draws osbtacles
            {
                obstacle.Draw(img, Color.DarkBlue);
            }

            DrawRays(raysSource.x, raysSource.y,img);
            Raylib.ImageDrawCircle(ref img, raysSource.x, raysSource.y, raysSource.radius, Color.Yellow); //draws source

            texture = Raylib.LoadTextureFromImage(img);
            Raylib.DrawTexture(texture, 0, 0, Color.White);
           Raylib.EndDrawing();
           Raylib.UnloadTexture(texture);
           
       }
       Raylib.CloseWindow();
    }
}

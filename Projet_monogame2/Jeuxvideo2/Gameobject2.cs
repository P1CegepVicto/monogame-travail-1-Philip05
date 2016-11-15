using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeuxvideo2
{
    class Gameobject2
    {
        public bool estvivant; // si le personnage est vivant (condition).
        public Texture2D sprite; //C'est une photo.
        public Vector2 position;
        public Vector2 vitesse;
        public int vieennemi = 5;
        public Rectangle RectCollision = new Rectangle();
       
     


        public Rectangle GetRect()
        {
            RectCollision.X = (int)this.position.X;
            RectCollision.Y = (int)this.position.Y;
            RectCollision.Width = this.sprite.Width;
            RectCollision.Height = this.sprite.Height;

            return RectCollision;
        }
    }
}

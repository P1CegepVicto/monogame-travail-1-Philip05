using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projet_monogame3
{
    class Gameobject
    {
        public bool estvivant;
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 vitesse;
        public Vector2 direction;
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

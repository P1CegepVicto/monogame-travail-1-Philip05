using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeuxvideo1
{
    class GameObject
    {
        public bool estvivant; // si le personnage est vivant (condition).
        public Texture2D sprite; //C'est une photo.
        public Vector2 position;
        public Vector2 vitesse;
    }
}

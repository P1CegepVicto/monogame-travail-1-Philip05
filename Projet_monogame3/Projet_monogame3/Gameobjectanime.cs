using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Projet_monogame3
{
    class Gameobjectanime
    {
        public Texture2D sprite;
        public Vector2 vitesse;
        public Vector2 direction;
        public bool estvivant;
        public Rectangle position;
        public Vector2 position1;
        public Rectangle spriteAfficher;
        public int cpt = 0;
        public Rectangle RectCollision1 = new Rectangle();
        //États
        public enum etats { attentedroite, attentegauche, rundroite, rungauche,runhaut,runbas,attentehaut,attentebas };
        public etats objetstate;
        //compteur
        private int compteur = 0;
        //Gesion des tableaux
        //courrir droite
        public int runState = 0;
        public int nbEtatsRun = 6;
        public Rectangle[] TabRunDroite = {
        new Rectangle(403,30,65,65),
        new Rectangle(333,30,65,65),
        new Rectangle(263,30,65,65),
        new Rectangle(193,30,65,65),
        new Rectangle(123,30,65,65),
        new Rectangle(71,30,65,65)};

        //Courrir gauche
        public Rectangle[] TabRunGauche = {
            new Rectangle(68,95,63,65),
            new Rectangle(128,95,63,65),
            new Rectangle(188,95,63,65),
            new Rectangle(258,95,63,65),
            new Rectangle(328,95,63,65),
            new Rectangle(398,95,63,65)};

        public int WaitState = 0;
        public Rectangle[] TabAttenteDroite =
         {
            new Rectangle(195,160,65,65)
         };
       public Rectangle[] TabAttenteGauche =
        {
            new Rectangle(195,226,65,65)
        };
        public Rectangle[] TabAttenteHaut =
        {
            new Rectangle(35,310,63,65)
        };
        public Rectangle[] TabAttenteBas =
        {
             new Rectangle(78,310,63,65)
        };

        public int runhautstate = 0;
        public int nbetatshaut = 2;
        public Rectangle[] TabRunHaut = {
            new Rectangle(35,310,63,65),
            new Rectangle(78,310,63,65)};

        public Rectangle GetRectSonic()
        {
            RectCollision1 = position;
            

            return RectCollision1;
        }

    }

}


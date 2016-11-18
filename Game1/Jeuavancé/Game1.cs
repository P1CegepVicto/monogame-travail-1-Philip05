using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Jeuavancé
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Gameobject Hero;
        Gameobject ExplosionEnnemi;
        Gameobject Ennemi;
        Rectangle fenêtre;
        Gameobject projectilehero;
        Gameobject laser;
        Texture2D Background;
        Random de1 = new Random();
        Gameobject[] tableauennemi;
        Gameobject[] projectilehéro;
        

        
       
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
           
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ToggleFullScreen(); //ou this.apply.graphicschanges.
            fenêtre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);// TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Ennemi = new Gameobject();
            //Ennemi.estvivant = true;
            //Ennemi.position.X = 1350;
            //Ennemi.position.Y = 0;

            laser = new Gameobject();
            laser.estvivant = true;

            Hero = new Gameobject();
            Hero.estvivant = true;
            Hero.position.X = 30;
            Hero.position.Y = 950;

            projectilehero = new Gameobject();
            projectilehero.estvivant = true;


            tableauennemi = new Gameobject[8];
            for(int i =0; i < tableauennemi.Length; i++)
            {
                tableauennemi[i] = new Gameobject();
                tableauennemi[i].direction.X = de1.Next(5,15);
                tableauennemi[i].direction.Y = de1.Next(1, 8);
                tableauennemi[i].position.Y = 0;
                tableauennemi[i].position.X = 1300;             
                tableauennemi[i].estvivant = true;
            }

            ExplosionEnnemi = new Gameobject();
            ExplosionEnnemi.estvivant = true;



            #region Load fichiers

            for(int g =0; g<tableauennemi.Length;g++)
            {
             tableauennemi[g].sprite = Content.Load<Texture2D>("Ennemi5.png");
            }
           
            laser.sprite = Content.Load<Texture2D>("Laser.png");
            Hero.sprite = Content.Load<Texture2D>("Hero2.png");
            projectilehero.sprite = Content.Load<Texture2D>("Projectilehero2.png");
            Background = Content.Load<Texture2D>("Background.jpg");
            ExplosionEnnemi.sprite = Content.Load<Texture2D>("explosion2.png");
            #endregion

        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
                                 
            #region lancer laser ennemi
            //for (int j = 0; j < tableauennemi.Length; j++)
            //{
            //    if (laser.estvivant == true)
            //    {
            //        laser.position = tableauennemi[j].position;
            //        laser.vitesse.X = -10;
            //        laser.estvivant = false;
            //    }
            //    if (laser.position.X == 0)
            //    {
            //        laser.estvivant = true;
            //    }
            //}

            #endregion
            #region Déplacements Hero

            if (Hero.estvivant == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    Hero.position.Y += -10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    Hero.position.Y += 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    Hero.position.X += 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    Hero.position.X += -10;
                }
            }
            #endregion
            #region Surface ecran hero
            if (Hero.estvivant ==true)
            {
                if(Hero.position.X <= fenêtre.Left)
                {
                    Hero.position.X = fenêtre.Left;
                }
                if (Hero.position.X > fenêtre.Right - Hero.sprite.Height)
                {
                    Hero.position.X = fenêtre.Right - Hero.sprite.Height;
                }
                if (Hero.position.Y > fenêtre.Bottom - Hero.sprite.Width)
                {
                    Hero.position.Y = fenêtre.Bottom- Hero.sprite.Width;
                }
                if (Hero.position.Y < fenêtre.Top - Hero.sprite.Width)
                {
                    Hero.position.Y = fenêtre.Top - Hero.sprite.Width;
                }
            }
            #endregion
            #region Si appuie space Hero lance missile
            if (Hero.estvivant == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if(projectilehero.estvivant ==true)
                    {
                        projectilehero.position = Hero.position;
                        projectilehero.vitesse.X = 40;
                        projectilehero.estvivant = false;
                    }

                }
                if(projectilehero.position.X > fenêtre.Right)
                {
                    projectilehero.position = Hero.position;
                    projectilehero.estvivant = true;
                }
            }
            #endregion

            
            for (int n =0; n<tableauennemi.Length;n++)
            {
                if (tableauennemi[n].position.Y > fenêtre.Bottom)
                {
                    tableauennemi[n].direction.X = de1.Next(1, 10);
                    tableauennemi[n].direction.Y = de1.Next(1, 8);
                    tableauennemi[n].position.X = de1.Next(1000, 1700);
                    tableauennemi[n].position.Y = 0;
                    tableauennemi[n].estvivant = true;
                }

            }

            UpdateHero();
            UpdateEnnemi();
            UpdateProjectileEnnemi();
           


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

       public void UpdateHero()
        {
            if(Hero.GetRect().Intersects(laser.GetRect()))
            {
                Hero.estvivant = false;
            }
        }
        public void UpdateEnnemi()
        {
            for (int a = 0; a < tableauennemi.Length; a++)
            {
                tableauennemi[a].position.X += tableauennemi[a].direction.X;
                tableauennemi[a].position.Y += tableauennemi[a].direction.Y;
                tableauennemi[a].vitesse.Y = 5;
                tableauennemi[a].vitesse.X = -10;


                if (tableauennemi[a].GetRect().Intersects(projectilehero.GetRect()))
                {
                    tableauennemi[a].estvivant = false;
                    ExplosionEnnemi.position = tableauennemi[a].position;
                    ExplosionEnnemi.vitesse.X = 2;
                    ExplosionEnnemi.vitesse.Y = 3;
                }
            }
        }
        public void UpdateProjectileEnnemi()
        {
            for (int l = 0; l < tableauennemi.Length; l++)
            {
                if (Hero.GetRect().Intersects(tableauennemi[l].GetRect()))
                {
                    Hero.estvivant = false;
                }
            }
        }
        
       
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(Background, fenêtre, Color.White);
             #region Dessin ennemi si vivant + laser ennemi vivant            
            for(int b =0; b< tableauennemi.Length; b++)
            {
                if (tableauennemi[b].estvivant == true)
                {
                    spriteBatch.Draw(tableauennemi[b].sprite, tableauennemi[b].position+=tableauennemi[b].vitesse, Color.White);
                    
                    //if (laser.estvivant == false)
                    //{
                    //    spriteBatch.Draw(laser.sprite, laser.position += laser.vitesse, Color.White);
                    //}
                }             
            }
            #endregion
            #region Dessin explosion ennemi
            for (int s =0; s<tableauennemi.Length;s++)
            {
                if(tableauennemi[s].estvivant ==false)
                {
                    spriteBatch.Draw(ExplosionEnnemi.sprite, ExplosionEnnemi.position += ExplosionEnnemi.vitesse, Color.White);
                    tableauennemi[s].estvivant = true;
                    
                }
            }
            #endregion
            #region Dessin Hero + projectile hero vivant

            if (Hero.estvivant == true)
            {
                spriteBatch.Draw(Hero.sprite, Hero.position += Hero.vitesse, Color.White);
                if(projectilehero.estvivant ==false)
                {
                    spriteBatch.Draw(projectilehero.sprite, projectilehero.position += projectilehero.vitesse, Color.White);
                }
            }
            #endregion

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

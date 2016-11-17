using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        Gameobject Ennemi;
        Rectangle fenêtre;
        Gameobject projectilehero;
        Gameobject laser;
        Texture2D Background;
       





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
            this.graphics.ApplyChanges(); //ou this.apply.graphicschanges.
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

            Ennemi = new Gameobject();
            Ennemi.estvivant = true;
            Ennemi.position.X = 1350;
            Ennemi.position.Y = 0;

            laser = new Gameobject();
            laser.estvivant = true;

            Hero = new Gameobject();
            Hero.estvivant = true;
            Hero.position.X = 30;
            Hero.position.Y = 950;

            projectilehero = new Gameobject();
            projectilehero.estvivant = true;

           


            //Load fichiers

            Ennemi.sprite = Content.Load<Texture2D>("Ennemi5.png");
            laser.sprite = Content.Load<Texture2D>("Laser.png");
            Hero.sprite = Content.Load<Texture2D>("Hero2.png");
            projectilehero.sprite = Content.Load<Texture2D>("Projectilehero2.png");
            Background = Content.Load<Texture2D>("Background.jpg");
            

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
            #region Deplacement ennemi                   
            //Déplacement ennemi

            if (Ennemi.estvivant == true)
               
            {
                if (Ennemi.position.Y == 0  && Ennemi.vitesse.Y != -10)
                {
                    Ennemi.vitesse.Y = 10;                   
                }
                if(Ennemi.position.Y >= fenêtre.Bottom- Ennemi.sprite.Height )
                {
                    Ennemi.vitesse.Y = -10;              
                }
                if(Ennemi.position.Y ==0 && Ennemi.vitesse.Y != 10)
                {
                    Ennemi.vitesse.Y = 10;
                }
            }

            #endregion
            #region lancer laser ennemi
            if (laser.estvivant==true)
            {              
                    laser.position = Ennemi.position;
                    laser.vitesse.X = -10;
                    laser.estvivant = false; 
            }
            if(laser.position.X ==0)
            {
                laser.estvivant = true;
            }

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
                if (Hero.position.X > fenêtre.Right - Ennemi.sprite.Height)
                {
                    Hero.position.X = fenêtre.Right - Ennemi.sprite.Height;
                }
                if (Hero.position.Y > fenêtre.Bottom - Ennemi.sprite.Width)
                {
                    Hero.position.Y = fenêtre.Bottom- Ennemi.sprite.Width;
                }
                if (Hero.position.Y < fenêtre.Top - Ennemi.sprite.Width)
                {
                    Hero.position.Y = fenêtre.Top - Ennemi.sprite.Width;
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
                        projectilehero.vitesse.X = 10;
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
            if(Ennemi.GetRect().Intersects(projectilehero.GetRect()))
            {
                Ennemi.estvivant = false;
            }               
        }
        public void UpdateProjectileEnnemi()
        {
            if(Hero.GetRect().Intersects(Ennemi.GetRect()))
            {
                Hero.estvivant = false;
            }
        }
       
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(Background, fenêtre, Color.White);
            //Déplacer ennemi fenêtre
            #region Dessin ennemi + laser ennemi vivant
            if (Ennemi.estvivant== true)
            {
                spriteBatch.Draw(Ennemi.sprite, Ennemi.position += Ennemi.vitesse, Color.White);

                if (laser.estvivant == false)
                {
                    spriteBatch.Draw(laser.sprite, laser.position += laser.vitesse, Color.White);
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

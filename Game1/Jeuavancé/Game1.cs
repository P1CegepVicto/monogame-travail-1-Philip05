using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
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
        Rectangle fenêtre;
        Gameobject laser;
        Texture2D Background;
        Random de1 = new Random();
        Gameobject[] tableauennemi;
        Gameobject[] projectilehéro;
        SoundEffect son1;
        SoundEffectInstance bombe;
        SoundEffect son;
        SoundEffectInstance Sonlaser;
        Gameobject explosionhero;
        SpriteFont font;
        SpriteFont fonttotaltime;
        int compteurexplohero=0;
        int compteur=0;
        int cptennemi = 0;     
        int v = 0;



        
       



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

            laser = new Gameobject();
            laser.estvivant = true;

            Hero = new Gameobject();
            Hero.estvivant = true;
            Hero.position.X = 30;
            Hero.position.Y = 950;

            projectilehéro = new Gameobject[10];
            for (int y = 0; y < projectilehéro.Length; y++)
            {
                projectilehéro[y] = new Gameobject();
                projectilehéro[y].estvivant = true;
                projectilehéro[y].position = Hero.position;
            }

            tableauennemi = new Gameobject[10];
            for (int i = 0; i < tableauennemi.Length; i++)
            {
                tableauennemi[i] = new Gameobject();
                tableauennemi[i].direction.X = de1.Next(5, 15);
                tableauennemi[i].direction.Y = de1.Next(1, 8);
                tableauennemi[i].position.Y = 0;
                tableauennemi[i].position.X = 1300;
                tableauennemi[i].estvivant = true;
            }

            ExplosionEnnemi = new Gameobject();
            ExplosionEnnemi.estvivant = true;
            explosionhero = new Gameobject();
            explosionhero.estvivant = false;

            #region Load fichiers

            for (int g = 0; g < tableauennemi.Length; g++)
            {
                tableauennemi[g].sprite = Content.Load<Texture2D>("Ennemi5.png");
            }

            laser.sprite = Content.Load<Texture2D>("Laser.png");
            Hero.sprite = Content.Load<Texture2D>("Hero2.png");

            for (int l = 0; l < projectilehéro.Length; l++)
            {
                projectilehéro[l].sprite = Content.Load<Texture2D>("Laser.png");
            }

            Background = Content.Load<Texture2D>("Background.jpg");
            ExplosionEnnemi.sprite = Content.Load<Texture2D>("explosion2.png");
            explosionhero.sprite = Content.Load<Texture2D>("explosion3.png");
            //sons
            son = Content.Load<SoundEffect>("Sounds\\sonlaser");
            Sonlaser = son.CreateInstance();
            son1 = Content.Load<SoundEffect>("Sounds\\Bombe");
            bombe = son1.CreateInstance();
           Song spacemusic  = Content.Load<Song>("Sounds\\Spacemusic");
            MediaPlayer.Play(spacemusic);
            

            //Spritefont

            font = Content.Load<SpriteFont>("font");
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
            if (Hero.estvivant == true)
            {
                if (Hero.position.X <= fenêtre.Left)
                {
                    Hero.position.X = fenêtre.Left;
                }
                if (Hero.position.X > fenêtre.Right - Hero.sprite.Height)
                {
                    Hero.position.X = fenêtre.Right - Hero.sprite.Height;
                }
                if (Hero.position.Y > fenêtre.Bottom - Hero.sprite.Width)
                {
                    Hero.position.Y = fenêtre.Bottom - Hero.sprite.Width;
                }
                if (Hero.position.Y < fenêtre.Top - Hero.sprite.Width)
                {
                    Hero.position.Y = fenêtre.Top - Hero.sprite.Width;
                }
            }
            #endregion
            #region Si appuie space Hero lance missile
            if (Hero.estvivant == true)
            {    compteur++;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                 {                  
                    
                    if (v < projectilehéro.Length )
                    {
                        if (projectilehéro[v].estvivant == true && compteur­ >=10)
                        {
                            projectilehéro[v].position = Hero.position;
                            projectilehéro[v].vitesse.X = 40;
                            projectilehéro[v].estvivant = false;
                            Sonlaser.Play();
                            compteur = 0;
                            v++;
                        }                                             
                    }
                    else
                    {
                        v = 0;

                        for (int y = 0; y < projectilehéro.Length; y++)
                        {
                            projectilehéro[y].estvivant = true;                           
                        }
                    }                   
                }
            }
            
            #endregion
            for (int n = 0; n < tableauennemi.Length; n++)
            {
                  if (tableauennemi[n].position.Y > fenêtre.Bottom)
                    {
                        
                        tableauennemi[n].position.X = de1.Next(100, 1700);
                        tableauennemi[n].position.Y = 0;
                        tableauennemi[n].estvivant = true;                                          
                }
            }       
            UpdateEnnemi();
            UpdateProjectileEnnemi();
            #region Si hero mort reprend position initiale;
            if (Hero.estvivant == false)
            {
                if (compteurexplohero == 0)
                {
                    explosionhero.estvivant = true;
                    explosionhero.position = Hero.position;
                    compteurexplohero++;
                }
            }

            #endregion

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
         public void UpdateEnnemi()
        {     
                for (int a = 0; a < tableauennemi.Length; a++)
                {
                if (cptennemi == 5)
                {
                    tableauennemi[a].position.X += tableauennemi[a].direction.X;
                    tableauennemi[a].position.Y += tableauennemi[a].direction.Y;
                    tableauennemi[a].vitesse.Y = 5;
                    tableauennemi[a].vitesse.X = -10;
                    cptennemi = 0;
                }
                    cptennemi++;
                for (int b = 0; b < projectilehéro.Length; b++)
                {
                    if (tableauennemi[a].GetRect().Intersects(projectilehéro[b].GetRect()))
                    {
                        tableauennemi[a].estvivant = false;
                        ExplosionEnnemi.position = tableauennemi[a].position;
                        ExplosionEnnemi.vitesse.X = 2;
                        ExplosionEnnemi.vitesse.Y = 3;
                    }
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
                    explosionhero.vitesse.X = 4;
                    explosionhero.vitesse.Y = 4;
                    explosionhero.estvivant = true;
                    bombe.Play();
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
            #region Dessin ennemi si vivant             
            for (int b = 0; b < tableauennemi.Length; b++)
            {
                if (tableauennemi[b].estvivant == true)
                {                  
                        spriteBatch.Draw(tableauennemi[b].sprite, tableauennemi[b].position += tableauennemi[b].vitesse, Color.White);                    
                }
            }
            #endregion
            #region Dessin explosion ennemi
            for (int s = 0; s < tableauennemi.Length; s++)
            {
                if (tableauennemi[s].estvivant == false)
                {
                    spriteBatch.Draw(ExplosionEnnemi.sprite, ExplosionEnnemi.position += ExplosionEnnemi.vitesse, Color.White);
                }
            }
            #endregion
            #region Dessin Hero + projectile hero vivant et mort

            if (Hero.estvivant == true)
            {
             
                spriteBatch.Draw(Hero.sprite, Hero.position += Hero.vitesse, Color.White);
                for (int c = 0; c < projectilehéro.Length; c++)
                {
                    if (projectilehéro[c].estvivant == false)
                    {
                        spriteBatch.Draw(projectilehéro[c].sprite, projectilehéro[c].position += projectilehéro[c].vitesse, Color.White);
                        
                    }
                }
            }
            if(Hero.estvivant ==false)
            {
                if (explosionhero.estvivant ==true)
                {
                    spriteBatch.Draw(explosionhero.sprite, explosionhero.position += explosionhero.vitesse, Color.White);
                }

                if (explosionhero.position.Y > fenêtre.Bottom)
                {
                    Hero.estvivant = true;
                    Hero.position.X = 25;
                    Hero.position.Y = 600;
                    compteurexplohero = 0;
                    explosionhero.estvivant = true;                  
                }
            }

            spriteBatch.DrawString(font,gameTime.TotalGameTime.Minutes.ToString()+", " + gameTime.TotalGameTime.Seconds.ToString() + ", " + gameTime.TotalGameTime.Milliseconds.ToString() + "", new Vector2(100, 100), Color.White);
            #endregion
        
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;

namespace Jeuxvideo2
{
      /// <summary>
                                        /// This is the main type for your game.
                                        /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Gameobject2 Mario; //Nom du héro dans le jeux.
        Rectangle fenetre;
        Gameobject2 Ennemi; //Ennemi.
        Texture2D background;
        Gameobject2 projectile;
       Gameobject2 explosion;
        Gameobject2 sonlaser;
        SoundEffect son;
        SoundEffectInstance laser;
        
       


        //Game object: position, image










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
            // TODO: Add your initialization logic here

            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ToggleFullScreen(); //ou this.apply.graphicschanges.
            fenetre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        { 
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Mario = new Gameobject2();
            Mario.estvivant = true;
            Mario.position.X = 25;
            Mario.position.Y = 900;
            //Ennemi
            Ennemi = new Gameobject2();
            Ennemi.estvivant = true;
            Ennemi.position.X = fenetre.Top;
            Ennemi.position.Y = fenetre.Top;
            //Projectile
            projectile = new Gameobject2();
            projectile.estvivant = true;
            projectile.position = Ennemi.position;
           //Explosion
            explosion = new Gameobject2();
            explosion.estvivant = true;

            //Ajouter l'image.
            //**** Aller dans advences et cliquer sur copy to output et copy if newer
            Mario.sprite = Content.Load<Texture2D>("spaceship1.png");
            projectile.sprite = Content.Load<Texture2D>("projectile1.png");
            Ennemi.sprite = Content.Load<Texture2D>("ennemi1.png");
            background = Content.Load<Texture2D>("Background.png");
            explosion.sprite = Content.Load<Texture2D>("explosion2.png");
            //son = Content.Load<SoundEffect>("Sounds\\Laser.mp3");
            //laser = son.CreateInstance();


            //Charger un son chanson, il faut changer le type avec le nom entre guillemet.


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
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

            //Déplacament clavier Mario

            if (Mario.estvivant == true)
            { 
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Mario.position.X += 8;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Mario.position.X -= 8;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Mario.position.Y -= 8;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Mario.position.Y += 8;
            }

            // Limiter espace de jeu

            if (Mario.position.X < fenetre.Left)
            {
                Mario.position.X = fenetre.Left;

            }

            if (Mario.position.X > fenetre.Right - Mario.sprite.Width)
            {
                    Mario.position.X = fenetre.Right - Mario.sprite.Width;
            }

            if (Mario.position.Y > fenetre.Bottom - Mario.sprite.Height)
            {
                Mario.position.Y = fenetre.Bottom - Mario.sprite.Height;
            }

            if (Mario.position.Y < fenetre.Top)
            {
                Mario.position.Y = fenetre.Top;
            }
         }

            //update Mario,vitesse.

            UpdateMario();
            UpdateVitesse();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        //si ennemi touche projectile       
        public void UpdateMario()
        {
            Mario.position += Mario.vitesse;

            if (Mario.GetRect().Intersects(projectile.GetRect()))

            {
                Mario.estvivant = false;
            }

        }

        public void UpdateVitesse()
        {
            if (Ennemi.estvivant == true)
            { 
               if (Ennemi.position.X < fenetre.Width - Ennemi.sprite.Width && Ennemi.vitesse.X != -10)
                {
                    Ennemi.vitesse.X = 10;
                }

                if (Ennemi.position.X >= fenetre.Width - Ennemi.sprite.Width)
                {
                    Ennemi.vitesse.X = -10;
                }

                if (Ennemi.position.X == 0 && Ennemi.vitesse.X != 10)
                {
                    Ennemi.vitesse.X = 10;
                }

                Ennemi.position += Ennemi.vitesse;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Les codes de dessin. Code entre begin et end.
            spriteBatch.Begin();
            //il y a plusieur possibilité d'affichage avec .draw.


            spriteBatch.Draw(background, fenetre, Color.White);

            if (Mario.estvivant == true)
            {
                spriteBatch.Draw(Mario.sprite, Mario.position, Color.White);
            }

            if (Mario.estvivant == false)
            {
                spriteBatch.Draw(explosion.sprite, explosion.position += explosion.vitesse, Color.White);               
                explosion.vitesse.X = 2;
                explosion.vitesse.Y = 2;
                explosion.position += explosion.vitesse;
            }

            else
            {
                explosion.position = Mario.position;
            }

            if (projectile.position.Y >=  Ennemi.position.Y)
            {
                         
                spriteBatch.Draw(projectile.sprite, projectile.position += projectile.vitesse, Color.White);
                projectile.vitesse.Y = 10;
                projectile.position += projectile.vitesse;
                
                //laser.Play();
            }

            if(projectile.position.Y > fenetre.Bottom)
            {
                projectile.position = Ennemi.position;
            }

            if (Ennemi.estvivant == true)
            {
                spriteBatch.Draw(Ennemi.sprite, Ennemi.position, Color.White);
            }

            if (Ennemi.estvivant == false)
            {
                spriteBatch.Draw(explosion.sprite, explosion.position, Color.White);
                Ennemi.vitesse.X = 2;
                Ennemi.vitesse.Y = 2;
                Ennemi.position += Ennemi.vitesse;
                explosion.position.X = Ennemi.position.X;
                explosion.position.Y = Ennemi.position.Y;
                explosion.position += explosion.vitesse;
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

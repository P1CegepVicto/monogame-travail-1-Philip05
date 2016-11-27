using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projet_monogame3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keys = new KeyboardState();
        KeyboardState PreviousKeys = new KeyboardState();
        Gameobjectanime Sonic;
        Rectangle fenetre;
        Gameobject projectilehero;
        Texture2D background1;
        Texture2D background2;
        Texture2D background3;
        Texture2D background4;





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

            



            #region Anime Sonic
            Sonic = new Gameobjectanime();
            Sonic.direction = Vector2.Zero;
            Sonic.vitesse.X = 2;
            Sonic.estvivant = true;
            Sonic.objetstate = Gameobjectanime.etats.attentedroite;
            Sonic.position = new Rectangle(80, 250, 65, 65);
            
           

            projectilehero = new Gameobject();
            projectilehero.estvivant = false;
            projectilehero.position = Sonic.direction;

            background1 = Content.Load<Texture2D>("background1.jpg");
            background2 = Content.Load<Texture2D>("background6.jpg");
            Sonic.sprite = Content.Load<Texture2D>("spritesheet.png");
            projectilehero.sprite = Content.Load<Texture2D>("projectilehero.png");

            #endregion

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
            #region Anime Sonic controles
            keys = Keyboard.GetState();
            Sonic.position.X += (int)(Sonic.vitesse.X * Sonic.direction.X);

            if(keys.IsKeyDown(Keys.D))
            {
                Sonic.direction.X = 2;
                Sonic.objetstate = Gameobjectanime.etats.rundroite;
            }
            if(keys.IsKeyUp(Keys.D)&&PreviousKeys.IsKeyDown(Keys.D))
            {
                Sonic.direction.X = 0;
                Sonic.objetstate = Gameobjectanime.etats.attentedroite;
            }
            if (keys.IsKeyDown(Keys.A))
            {
                Sonic.direction.X = -2;
                Sonic.objetstate = Gameobjectanime.etats.rungauche;
            }
            if (keys.IsKeyUp(Keys.A) && PreviousKeys.IsKeyDown(Keys.A))
            {
                Sonic.direction.X = 0;
                Sonic.objetstate = Gameobjectanime.etats.attentegauche;
            }

            if (Sonic.objetstate ==Gameobjectanime.etats.rundroite)
            {
                Sonic.spriteAfficher = Sonic.TabRunDroite[Sonic.runState];
            }
            if(Sonic.objetstate ==Gameobjectanime.etats.rungauche)
            {
                Sonic.spriteAfficher = Sonic.TabRunGauche[Sonic.runState];
            }
            if (Sonic.objetstate == Gameobjectanime.etats.attentedroite)
            {
                Sonic.spriteAfficher = Sonic.TabAttenteDroite[Sonic.WaitState];
            }
            if (Sonic.objetstate == Gameobjectanime.etats.attentegauche)
            {
                Sonic.spriteAfficher = Sonic.TabAttenteGauche[Sonic.WaitState];
            }
                     
         
                Sonic.cpt++;
                if (Sonic.cpt == 8)
                {
                    Sonic.runState++;
                    if (Sonic.runState == Sonic.nbEtatsRun)
                    {
                        Sonic.runState = 0;
                    }
                    Sonic.cpt = 0;
                }
            

            PreviousKeys = keys;

            #endregion

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (Sonic.estvivant == true)
                {
                    projectilehero.estvivant = true;
                    projectilehero.position = Sonic.direction;
                    projectilehero.vitesse.X = 5;
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            #region dessiner Sonic
            if (Sonic.estvivant == true)
            {
                spriteBatch.Draw(Sonic.sprite, Sonic.position, Sonic.spriteAfficher, Color.White);
            }
                    

            #endregion
            if(projectilehero.estvivant ==true)
            {
                spriteBatch.Draw(projectilehero.sprite, projectilehero.position += projectilehero.vitesse, Color.White);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

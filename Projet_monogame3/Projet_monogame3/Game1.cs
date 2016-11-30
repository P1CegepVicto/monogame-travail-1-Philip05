using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        Gameobject[] projectilehero;
        Gameobject background1;
        Gameobject background2;
        Texture2D background3;
        Texture2D background4;
        Gameobject[] Ghosts;
        Gameobject[] vampire;
        Random de1 = new Random();
        int h = 0;
        int cpttemps = 0;
        
       
        
        




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

            Ghosts = new Gameobject[10];
            for(int i =0; i<Ghosts.Length;i++)
            {
                Ghosts[i] = new Gameobject();
                Ghosts[i].direction.X = de1.Next(1, 8);
                Ghosts[i].direction.Y = de1.Next(1, 5);
                Ghosts[i].position.X = 1300;
                Ghosts[i].position.Y = 0;
                Ghosts[i].estvivant = true;
            }

            #region projectilehero
            projectilehero = new Gameobject[250];
            for(int a=0; a<projectilehero.Length;a++)
            {
                projectilehero[a] = new Gameobject();
                projectilehero[a].estvivant = false;
            }

            for (int d =0; d<projectilehero.Length;d++)
            {
                projectilehero[d].sprite = Content.Load<Texture2D>("blueball.png");
            }
            #endregion




            #region Anime Sonic
            Sonic = new Gameobjectanime();
            Sonic.direction = Vector2.Zero;
            Sonic.vitesse.X = 1;
            Sonic.estvivant = true;
            Sonic.objetstate = Gameobjectanime.etats.attentedroite;
            Sonic.position = new Rectangle(80, 250, 65, 65);
            
           

           

            background1 = new Gameobject();
            background1.sprite = Content.Load<Texture2D>("background1.jpg");
            //background2.sprite = Content.Load<Texture2D>("background6.jpg");
            Sonic.sprite = Content.Load<Texture2D>("spritesheet.png");

            
            
            for(int a =0; a<Ghosts.Length;a++)
            {
                Ghosts[a].sprite = Content.Load<Texture2D>("Ghost2.png");
            }

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
                Sonic.direction.X = 1;
                Sonic.objetstate = Gameobjectanime.etats.rundroite;
                background1.position.X -= 3;
                
            }
            if(keys.IsKeyUp(Keys.D)&&PreviousKeys.IsKeyDown(Keys.D))
            {
                Sonic.direction.X = 0;
                Sonic.objetstate = Gameobjectanime.etats.attentedroite;
            }
            if (keys.IsKeyDown(Keys.A))
            {
                Sonic.direction.X = -1;
                Sonic.objetstate = Gameobjectanime.etats.rungauche;
                background1.position.X += 3;
            }
            if (keys.IsKeyUp(Keys.A) && PreviousKeys.IsKeyDown(Keys.A))
            {
                Sonic.direction.X = 0;
                Sonic.objetstate = Gameobjectanime.etats.attentegauche;
              
            }
            if(keys.IsKeyDown(Keys.W))
            {
                Sonic.direction.Y = 1;
                Sonic.objetstate = Gameobjectanime.etats.runhaut;
                background1.position.Y += 3;
            }
            if(keys.IsKeyUp(Keys.W)&&PreviousKeys.IsKeyDown(Keys.W))
            {
                Sonic.direction.Y = 0;
                Sonic.objetstate = Gameobjectanime.etats.attentehaut;
            }
            if (keys.IsKeyDown(Keys.S))
            {
                Sonic.direction.Y = -1;
                Sonic.objetstate = Gameobjectanime.etats.runhaut;
                background1.position.Y -= 3;
            }
            if (keys.IsKeyUp(Keys.S) && PreviousKeys.IsKeyDown(Keys.S))
            {
                Sonic.direction.Y = 0;
                Sonic.objetstate = Gameobjectanime.etats.attentebas;
            }
            #region position ecran sonic

            if (Sonic.position.X < fenetre.Width)
            {
                Sonic.position.X =200;
            }
            #endregion

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
            if(Sonic.objetstate == Gameobjectanime.etats.runhaut)
            {
                Sonic.spriteAfficher = Sonic.TabRunHaut[Sonic.runhautstate];
            }
            if (Sonic.objetstate == Gameobjectanime.etats.attentebas)
            {
                Sonic.spriteAfficher = Sonic.TabAttenteBas[Sonic.WaitState];
            }
            if (Sonic.objetstate == Gameobjectanime.etats.attentehaut)
            {
                Sonic.spriteAfficher = Sonic.TabAttenteHaut[Sonic.WaitState];
            }


            Sonic.cpt++;
                if (Sonic.cpt == 8)
                {
                    Sonic.runState++;
                    Sonic.runhautstate++;
                    if (Sonic.runState == Sonic.nbEtatsRun || Sonic.runhautstate == Sonic.nbetatshaut)
                    {
                        Sonic.runState = 0;
                        Sonic.runhautstate = 0;
                    }
                    Sonic.cpt = 0;
                }
            


            PreviousKeys = keys;

            #endregion
            if (Sonic.estvivant == true)
            {
                cpttemps++;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {

                    if (Sonic.estvivant == true && projectilehero[h].estvivant == false && cpttemps >= 8)
                    {
                        projectilehero[h].estvivant = true;
                        if(Sonic.objetstate ==Gameobjectanime.etats.attentegauche)
                        {
                            projectilehero[h].vitesse.X = -10;
                            projectilehero[h].position.X = Sonic.position.X -10 ;
                            projectilehero[h].position.Y = Sonic.position.Y ;
                        }
                        if (Sonic.objetstate == Gameobjectanime.etats.attentedroite)
                        {
                            projectilehero[h].vitesse.X = 10;
                            projectilehero[h].position.X = Sonic.position.X + 50;
                            projectilehero[h].position.Y = Sonic.position.Y - 1;
                        }
                        if(Sonic.objetstate ==Gameobjectanime.etats.attentehaut)
                        {
                            projectilehero[h].vitesse.Y = -10;
                            projectilehero[h].position.X = Sonic.position.X + 50;
                            projectilehero[h].position.Y = Sonic.position.Y - 1;
                        }
                        if (Sonic.objetstate == Gameobjectanime.etats.attentebas)
                        {
                            projectilehero[h].vitesse.Y = 10;
                            projectilehero[h].position.X = Sonic.position.X + 50;
                            projectilehero[h].position.Y = Sonic.position.Y - 1;
                        }
                        cpttemps = 0;
                        h++;
                    }
                    if (h >= projectilehero.Length)
                    {
                        h = 0;
                    }
                }

                for (int k = 0; k < projectilehero.Length; k++)
                {
                    if (projectilehero[k].position.X >= fenetre.Width)
                    {
                        projectilehero[k].position.X = Sonic.position.X;
                        projectilehero[k].position.Y = Sonic.position.Y;
                        projectilehero[k].estvivant = false;
                    }
                }             
            }
            for (int g = 0; g < Ghosts.Length; g++)
                if (Ghosts[g].estvivant == true)
                {
                    {
                        Ghosts[g].vitesse.X = 5;
                        Ghosts[g].vitesse.Y = 5;
                        Ghosts[g].estvivant = false;
                    }
                }
            //Background

            //if(background1.position.X <0)
            //{
            //    background2.position.X = background1.position.X + background1.sprite.Width;
            //}
            //if(background1.position.X >0)
            //{
            //    background2.position.X = background1.position.X - background1.sprite.Width;
            //}

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
            spriteBatch.Draw(background1.sprite,background1.position);
            

            //Background

            #region dessiner Sonic
            if (Sonic.estvivant == true)
            {
                spriteBatch.Draw(Sonic.sprite, Sonic.position, Sonic.spriteAfficher, Color.White);
            }


            #endregion
            #region projectilehero
            for (int j = 0; j < projectilehero.Length; j++)
            {
                if (projectilehero[j].estvivant == true)
                {
                    spriteBatch.Draw(projectilehero[j].sprite, projectilehero[j].position += projectilehero[j].vitesse, Color.YellowGreen);
                }
            }
            #endregion
            #region dessiner ghost
            for(int y=0; y<Ghosts.Length;y++)
            {
                if(Ghosts[y].estvivant ==false)
                {
                    spriteBatch.Draw(Ghosts[y].sprite, Ghosts[y].position += Ghosts[y].vitesse, Color.White);
                }
            }
            #endregion


            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

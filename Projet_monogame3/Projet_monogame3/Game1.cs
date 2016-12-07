using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
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
        Gameobject ToucheGhost;
        Random de1 = new Random();
        SpriteFont font;
        SoundEffect son;
        int Gameover=5;
        SoundEffectInstance scream;
        int cptviesonic = 5;
        int h = 0;
        int cpttemps = 0;
        int cptennemi = 0;
        int cptback = 0;
        int cpttoucheennemi = 0;
        
       
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
            son = Content.Load<SoundEffect>("Sounds\\scream"); 
            scream = son.CreateInstance();
            Song song = Content.Load<Song>("Sounds\\Song");
            MediaPlayer.Play(song);
            Ghosts = new Gameobject[10];
            for(int c =0; c < Ghosts.Length;c++)
            {
                Ghosts[c] = new Gameobject();
                Ghosts[c].estvivant = true;
                Ghosts[c].direction.X = de1.Next(1,5);
                Ghosts[c].direction.Y= de1.Next(1,5);
            }

            #region projectilehero

            

            projectilehero = new Gameobject[25];
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
            Sonic.estvivant = true;
            Sonic.objetstate = Gameobjectanime.etats.attentedroite;
            Sonic.position = new Rectangle(80, 250, 65, 65);

            background1 = new Gameobject();
            background1.sprite = Content.Load<Texture2D>("background7.jpg");
            background2 = new Gameobject();
            Sonic.sprite = Content.Load<Texture2D>("spritesheet.png");
            background2.sprite = Content.Load<Texture2D>("background7.jpg");
            if (cptback ==0)
            {
                background3 = Content.Load<Texture2D>("begin2.png");
                font = Content.Load<SpriteFont>("Font");
            }

            for (int l = 0; l < Ghosts.Length;l++)
            {
                Ghosts[l].sprite = Content.Load<Texture2D>("Ghost4.png");
            }

            ToucheGhost = new Gameobject();
            ToucheGhost.estvivant = false;
            ToucheGhost.sprite = Content.Load<Texture2D>("ghosttouch.png");


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
            if (cptback !=0 && Gameover !=0)
            {
                #region Anime Sonic controles
                keys = Keyboard.GetState();
                Sonic.position.X += (int)(Sonic.vitesse.X * Sonic.direction.X);

                if (keys.IsKeyDown(Keys.D))
                {

                    Sonic.direction.X = 10;
                    Sonic.position.X += 10;
                    Sonic.objetstate = Gameobjectanime.etats.rundroite;
                    background1.position.X -= 3;
                    background2.position.X -= 3;

                }
                if (keys.IsKeyUp(Keys.D) && PreviousKeys.IsKeyDown(Keys.D))
                {
                    Sonic.direction.X = 0;     
                    Sonic.objetstate = Gameobjectanime.etats.attentedroite;
                }
                if (keys.IsKeyDown(Keys.A))
                {
                    Sonic.direction.X = -4;
                    Sonic.position.X -= 10;
                    Sonic.objetstate = Gameobjectanime.etats.rungauche;
                    background1.position.X += 3;
                    background2.position.X += 3;
                }
                if (keys.IsKeyUp(Keys.A) && PreviousKeys.IsKeyDown(Keys.A))
                {
                    Sonic.direction.X = 0;
                    Sonic.objetstate = Gameobjectanime.etats.attentegauche;

                }
                if (keys.IsKeyDown(Keys.W))
                {
                    Sonic.direction.Y = 1;
                    Sonic.position.Y -= 5;
                    Sonic.objetstate = Gameobjectanime.etats.runhaut;
                    background1.position.Y += 3;
                    background2.position.Y += 3;
                }
                if (keys.IsKeyUp(Keys.W) && PreviousKeys.IsKeyDown(Keys.W))
                {
                    Sonic.direction.Y = 0;
                    Sonic.objetstate = Gameobjectanime.etats.attentehaut;
                }
                if (keys.IsKeyDown(Keys.S))
                {
                    Sonic.direction.Y = -1;
                    Sonic.position.Y += 5;
                    Sonic.objetstate = Gameobjectanime.etats.runhaut;
                    background1.position.Y -= 3;
                    background2.position.Y -= 3;
                }
                if (keys.IsKeyUp(Keys.S) && PreviousKeys.IsKeyDown(Keys.S))
                {
                    Sonic.direction.Y = 0;
                    Sonic.objetstate = Gameobjectanime.etats.attentebas;
                }
                #region position ecran sonic

                if (Sonic.position.X < fenetre.Width)
                {
                    Sonic.position.X = 200;
                }
                #endregion

                if (Sonic.objetstate == Gameobjectanime.etats.rundroite)
                {
                    Sonic.spriteAfficher = Sonic.TabRunDroite[Sonic.runState];

                }
                if (Sonic.objetstate == Gameobjectanime.etats.rungauche)
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
                if (Sonic.objetstate == Gameobjectanime.etats.runhaut)
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

                #region Projectilehero
                if (Sonic.estvivant == true)
                {
                    cpttemps++;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {

                        if (Sonic.estvivant == true && projectilehero[h].estvivant == false && cpttemps >= 8)
                        {
                            projectilehero[h].estvivant = true;
                            if (Sonic.objetstate == Gameobjectanime.etats.attentegauche)
                            {
                                projectilehero[h].vitesse.X = -10;
                                projectilehero[h].position.X = Sonic.position.X - 10;
                                projectilehero[h].position.Y = Sonic.position.Y;
                            }
                            if (Sonic.objetstate == Gameobjectanime.etats.attentedroite)
                            {
                                projectilehero[h].vitesse.X = 10;
                                projectilehero[h].position.X = Sonic.position.X + 50;
                                projectilehero[h].position.Y = Sonic.position.Y - 1;
                            }
                            if (Sonic.objetstate == Gameobjectanime.etats.attentehaut)
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
                #endregion
                #region ghost
                for (int v = 0; v < Ghosts.Length; v++)
                {
                    if (Ghosts[v].position.Y > fenetre.Bottom)
                    {
                        Ghosts[v].estvivant = false;
                        Ghosts[v].position.X = de1.Next(0, 3000);
                        Ghosts[v].position.Y = 0;
                    }
                }
            }

            UpdateToucheEnnemi();
            UpdateToucheSonic();

            //for(int p=0;p<Ghosts.Length;p++)
            //{
            //    if(Ghosts[p])
            //}
            #endregion
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                cptback++;
                }

                cptennemi++;
                updateennemi();


            //Background

            if (background1.position.X < 0)
            {
                background2.position.X = background1.position.X + background1.sprite.Width;
            }
            if (background1.position.X > 0)
            {
                background2.position.X = background1.position.X - background1.sprite.Width;
            }
          
             if (background1.position.Y> 0)
            {
                background2.position.Y = 0;
                background1.position.Y = 0;
            }
            if (background1.position.Y < fenetre.Height-background1.sprite.Height)
            {
                background2.position.Y = fenetre.Bottom-background1.sprite.Height;
                background1.position.Y = fenetre.Bottom-background1.sprite.Height;
            }


            base.Update(gameTime);
        }
        public void UpdateToucheSonic()
        {
            for (int i = 0; i < Ghosts.Length; i++)
            {
                if (Sonic.GetRectSonic().Intersects(Ghosts[i].GetRect()))
                {
                    
                }
            }
        }
        public void updateennemi()
        {
            for (int g = 0; g < Ghosts.Length; g++)
            {
                if (cptennemi == 10 && Ghosts[g].estvivant==true)
                {
                    Ghosts[g].position.Y = 0;
                    Ghosts[g].position.X =de1.Next(100,1500);
                    Ghosts[g].position.X += Ghosts[g].direction.X;
                    Ghosts[g].position.Y += Ghosts[g].direction.Y;
                    Ghosts[g].vitesse.X = de1.Next(-3,5);
                    Ghosts[g].vitesse.Y = de1.Next(1, 8);
                    Ghosts[g].estvivant = false;
                    cptennemi = 0;
                }
            }
        }
        public void UpdateToucheEnnemi()
        {
            for (int k = 0; k < projectilehero.Length; k++)
            {
                for (int i = 0; i < Ghosts.Length; i++)
                {
                    if (projectilehero[k].GetRect().Intersects(Ghosts[i].GetRect()))
                    {
                        Ghosts[i].estvivant = true;
                        ToucheGhost.estvivant = false;
                        ToucheGhost.vitesse.X = -2;
                        ToucheGhost.position = Ghosts[i].position;
                        cpttoucheennemi += 5;
                        Gameover--;
                        //scream.Play();

                    }
                }
            }
        }
            
               
        
  
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            if (cptback ==0)
            {
                spriteBatch.Draw(background3,fenetre,Color.White);
                spriteBatch.DrawString(font, "Press enter to start the game", new Vector2(100,500), Color.White);
            }
            if (cptback!=0 && Gameover != 0)
            {
                spriteBatch.Draw(background1.sprite, background1.position);
                spriteBatch.Draw(background2.sprite, background2.position);
                //spriteBatch.DrawString(font,cptviesonic.ToString(), new Vector2(100, 200), Color.White);
                spriteBatch.DrawString(font, gameTime.TotalGameTime.Minutes.ToString() + ", " + gameTime.TotalGameTime.Seconds.ToString() + ", " + gameTime.TotalGameTime.Milliseconds.ToString() + "", new Vector2(100, 100), Color.White);
                spriteBatch.DrawString(font, cpttoucheennemi.ToString(), new Vector2(700, 100), Color.White);

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
                for (int y = 0; y < Ghosts.Length; y++)
                {
                    if (Ghosts[y].estvivant == false)
                    {
                        spriteBatch.Draw(Ghosts[y].sprite, Ghosts[y].position += Ghosts[y].vitesse, Color.White);
                    }
                }
            
            if(ToucheGhost.estvivant==false)
            {
                spriteBatch.Draw(ToucheGhost.sprite, ToucheGhost.position+= ToucheGhost.vitesse, Color.White);
                    
                }
        }
           
            #endregion

            //if(Gameover <= 0)
            //{
            //    spriteBatch.DrawString(font, "GameOver", new Vector2(100, 500), Color.White);
            //}


            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

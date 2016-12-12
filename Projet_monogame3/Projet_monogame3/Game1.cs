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
        Gameobject background4;
        Gameobject[] Ghosts;
        Gameobject[] vampire;
        Gameobject ToucheGhost;
        Random de1 = new Random();
        SpriteFont font;
        SoundEffect son;
        int Gameover=10;
        SoundEffectInstance scream;
        float seconds;
        int h = 0;
        int cpttemps = 0;
        int cptennemi = 0;
        int cptback = 0;
        int cpttoucheennemi = 0;
        int ennemivivant = 250;
        
        
       
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
            Ghosts = new Gameobject[50];
            for(int c =0; c < Ghosts.Length;c++)
            {
                Ghosts[c] = new Gameobject();
                Ghosts[c].estvivant = true;
                Ghosts[c].direction.X = de1.Next(1,5);
                Ghosts[c].direction.Y= de1.Next(1,5);
                Ghosts[c].sprite = Content.Load<Texture2D>("Ghost4.png");
            }

            #region projectilehero

            

            projectilehero = new Gameobject[200];
            for(int a=0; a<projectilehero.Length;a++)
            {
                projectilehero[a] = new Gameobject();
                projectilehero[a].estvivant = false;
                projectilehero[a].sprite = Content.Load<Texture2D>("blueball.png");
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

           
                background4 = new Gameobject();
                background4.sprite = Content.Load<Texture2D>("1.jpg");
                font = Content.Load<SpriteFont>("Font");
            
            if (cptback ==0)
            {
                background3 = Content.Load<Texture2D>("begin2.png");
                font = Content.Load<SpriteFont>("Font");
            }

            

            ToucheGhost = new Gameobject();
            ToucheGhost.estvivant = true;
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
                #region anime Sonic

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
                #endregion



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
                                projectilehero[h].vitesse.X = -15;
                                projectilehero[h].position.X = Sonic.position.X - 10;
                                projectilehero[h].position.Y = Sonic.position.Y;
                            }
                            if (Sonic.objetstate == Gameobjectanime.etats.attentedroite)
                            {
                                projectilehero[h].vitesse.X = 15;
                                projectilehero[h].position.X = Sonic.position.X + 50;
                                projectilehero[h].position.Y = Sonic.position.Y - 1;
                            }
                            if (Sonic.objetstate == Gameobjectanime.etats.attentehaut)
                            {
                                projectilehero[h].vitesse.Y = -15;
                                projectilehero[h].position.X = Sonic.position.X + 50;
                                projectilehero[h].position.Y = Sonic.position.Y - 1;
                            }
                            if (Sonic.objetstate == Gameobjectanime.etats.attentebas)
                            {
                                projectilehero[h].vitesse.Y = 15;
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
                        if (projectilehero[k].position.X > fenetre.Right || projectilehero[k].position.X < fenetre.Left)
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
                    if (Ghosts[v].position.Y > fenetre.Width || Ghosts[v].position.Y > fenetre.Height)
                    {
                        Ghosts[v].estvivant = false;
                        Ghosts[v].position.X = de1.Next(0, 1200);
                        Ghosts[v].position.Y = 0;
                    }
                }
                cptennemi++;
                updateennemi();
                UpdateToucheEnnemi();
                UpdateToucheSonic();
            }

            

            //for(int p=0;p<Ghosts.Length;p++)
            //{
            //    if(Ghosts[p])
            //}
            #endregion
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                cptback++;
                }
            
            if(Keyboard.GetState().IsKeyDown(Keys.P))
            {
                Gameover = 5;
                cptback = 0;
                cpttoucheennemi = 0;
                font = Content.Load<SpriteFont>("font");
                for (int y =0; y<Ghosts.Length;y++)
                {
                    Ghosts[y].estvivant = false;
                }
            }
            if(cpttoucheennemi ==200)
            {
                Gameover++;
                cpttoucheennemi = 0;
       
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                for (int g = 0; g < projectilehero.Length; g++)
                {
                    projectilehero[g].estvivant = false;
                }
            }

                #region limites Sonic + Background + projectile hero           
                if (Sonic.position.Y < 0)
            {
                Sonic.position.Y = 0;
            }
            if (Sonic.position.Y >= fenetre.Bottom-Sonic.spriteAfficher.Height)
            {
                Sonic.position.Y = fenetre.Bottom-Sonic.spriteAfficher.Height;
            }

            //Background

            if (background1.position.X < 0)
            {
                background2.position.X = background1.position.X + background1.sprite.Width;
            }
            if (background1.position.X > 0)
            {
                background2.position.X = background1.position.X - background1.sprite.Width;
            }
            if (background2.position.X < 0)
            {
                background1.position.X = background2.position.X + background2.sprite.Width;
            }
            if (background2.position.X > 0)
            {
                background1.position.X = background2.position.X - background2.sprite.Width;
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

          
            if(ennemivivant ==100)
            {
                for (int j=0;j< Ghosts.Length;j++)
                {
                    Ghosts[j].estvivant = true;
                }
            }
            #endregion

            

            base.Update(gameTime);
        }
        public void UpdateToucheSonic()
        {
            for (int i = 0; i < Ghosts.Length; i++)
            {
                if (Sonic.GetRectSonic().Intersects(Ghosts[i].GetRect())&& !Ghosts[i].estvivant)
                {
                    Ghosts[i].estvivant = true;
                    Gameover--;
                }
            }
        }
        public void updateennemi()
        {
            for (int g = 0; g < Ghosts.Length; g++)
            {
                if (cptennemi == 20 && Ghosts[g].estvivant==true)
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
                    if (projectilehero[k].GetRect().Intersects(Ghosts[i].GetRect()) && !Ghosts[i].estvivant)
                    {
                        Ghosts[i].estvivant = true;
                        ToucheGhost.estvivant = false;
                        ToucheGhost.vitesse.X = -3;
                        ToucheGhost.vitesse.Y = 3;
                        ToucheGhost.position = Ghosts[i].position;
                        cpttoucheennemi += 5;
                        scream.Play();
                        ennemivivant--;
                        
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
            if (cptback != 0 && Gameover != 0)
            {
                spriteBatch.Draw(background1.sprite, background1.position);
                spriteBatch.Draw(background2.sprite, background2.position);
                //spriteBatch.DrawString(font,cptviesonic.ToString(), new Vector2(100, 200), Color.White);
                spriteBatch.DrawString(font, gameTime.TotalGameTime.Minutes.ToString() + ", " + gameTime.TotalGameTime.Seconds.ToString() + ", " + gameTime.TotalGameTime.Milliseconds.ToString() + "", new Vector2(100, 50), Color.OrangeRed);
                spriteBatch.DrawString(font, " Points: " + cpttoucheennemi.ToString(), new Vector2(600, 50), Color.OrangeRed);
                spriteBatch.DrawString(font, " Vies: " + Gameover.ToString(), new Vector2(1000, 50), Color.OrangeRed);
                spriteBatch.DrawString(font, "Press R to recharge your gun", new Vector2(1200, 50), Color.OrangeRed);

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

                if (ToucheGhost.estvivant == false)
                {
                    spriteBatch.Draw(ToucheGhost.sprite, ToucheGhost.position += ToucheGhost.vitesse, Color.White);

                }


                #endregion
            }
            if (Gameover ==0)
                {
                    spriteBatch.Draw(background4.sprite, fenetre, Color.White);
                    spriteBatch.DrawString(font, "GAME OVER", new Vector2(900, 500), Color.OrangeRed);
                    spriteBatch.DrawString(font, "Press p to start again or escape to quit", new Vector2(700, 600), Color.OrangeRed);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

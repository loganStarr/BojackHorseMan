using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO.Ports;

namespace Hello
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SerialPort port;
        Sprite player;
        float X = 0;
        float Y = 0;
        float XSpeed = 2;
        float YSpeed = 2;
        int updatesSinceLast = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = (700);
            graphics.PreferredBackBufferHeight = (700);
            graphics.ApplyChanges();
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
            player = new Sprite(Content.Load<Texture2D>("Hi"), new Vector2(100, 100), new Vector2(100, 100));
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



            updatesSinceLast++;
            // TODO: Add your update logic here
            port = new SerialPort();

            port.PortName = "COM5";
            port.BaudRate = 115200;
            try
            {
                port.Open();
                if (port.IsOpen)
                {
                    string b = "";
                    //int amount = port.BytesToRead;
                    int x = 0;
                    while (x < 100)
                    {
                        b += (char)port.ReadChar();
                        x++;
                    }
                    //char[] b = new char[amount];
                   /* for (int i = 0; i < amount; i++)
                    {
                        b[i] = (char)port.ReadChar();
                    }   */                 
                    
                    string Context = "";
                    bool doRead = false;
                    for (int i = 0; i < b.Length; i++)
                    {
                        if (b[i] == 'R')
                        {
                            doRead = true;
                            Context = "";
                        }

                        else if (doRead)
                        {
                            if (b[i] == 'X' || b[i] == 'Y')
                            {
                                if (b[i] == 'X' && Context != "")
                                {
                                    X = int.Parse(Context);
                                    Context = "";
                                }
                                else if (Context != "")
                                {
                                    Y = int.Parse(Context);
                                }
                                doRead = false;
                            }
                            else if (b[i] >= '0' && b[i] <= '9' && doRead)
                            {
                                Context += b[i];
                            }
                            else if (b[i] != '\n')
                            {
                                Context = "";
                            }
                        }
                    }
                }
                port.Close();
            }
            catch
            {
                port.Close();
            }

            if (player.Postion.Y >= GraphicsDevice.Viewport.Height - player.Size.Y && (Y - 512) / 128 > 0)
            {
                YSpeed = 0;
            }
            else if (player.Postion.Y <= 0 && (Y - 512) / 128 < 0)
            {
                YSpeed = 0;
            }
            else
            {
                YSpeed = 2;
            }
            if (player.Postion.X >= GraphicsDevice.Viewport.Width - player.Size.X && (X - 512) / 128 > 0)
            {
                XSpeed = 0;
            }
            else if (player.Postion.X <= 0 && (X - 512) / 128 < 0)
            {
                XSpeed = 0;
            }
            else
            {
                XSpeed = 2;
            }
            if (X >= 400 && X <= 600)
            {
                X = 512;
            }
            if (Y >= 400 && Y <= 600)
            {
                Y = 512;
            }
            player.Postion.X += XSpeed * (X - 512) / 128;
            player.Postion.Y += YSpeed * (Y - 512) / 128;
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
            // TODO: Add your drawing code here
            player.Draw(spriteBatch);
            #region Don't open
            //          #      # _ Why did you open this. Your a bad person, it told you not to open. Why did you open it?
            //
            //           ______

            #endregion
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

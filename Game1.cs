﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace geekGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D[] players;
        Texture2D player;
        Texture2D playerRight;
        Texture2D playerLeft;
        Texture2D playerBack;
        int playerIndex;
        

        //a timer that stores miliseconds
        float timer;
        //threshold for timer
        int threshold;
        //stores the sprites you want to draw
        Rectangle[] sourceRectangles;
        // These bytes tell the spriteBatch.Draw() what sourceRectangle to display.
        byte previousAnimationIndex;
        byte currentAnimationIndex;
        KeyboardState kstate = Keyboard.GetState();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            timer = 0;
            // Set an initial threshold of 250ms, you can change this to alter the speed of the animation (lower number = faster animation).
            threshold = 150;
            // Three sourceRectangles contain the coordinates of geeks's three down-facing sprites on the charaset.
            sourceRectangles = new Rectangle[3];
            sourceRectangles[0] = new Rectangle(3, 0, 25, 32); //standing still
            sourceRectangles[1] = new Rectangle(35, 0, 25, 32);//right sprint
            //sourceRectangles[3] = new Rectangle(0, 32, 25, 32); //standing still
            sourceRectangles[2] = new Rectangle(35, 32, 25, 32);//left sprite.

            players = new Texture2D[4];
            

            // This tells the animation to start on the left-side sprite.
            previousAnimationIndex = 2;
            currentAnimationIndex = 0;

            playerIndex = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            player = Content.Load<Texture2D>("geekWalkingForward");
            playerRight = Content.Load<Texture2D>("geekWalkingRight");
            playerLeft = Content.Load<Texture2D>("geekWalkingRight");
            playerBack = Content.Load<Texture2D>("geekWalkingBackward");
            players[0] = player;
            players[1] = playerBack;
            players[2] = playerRight;
            players[3] = playerLeft;





            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //updating which sprite is needed
            // Check if the timer has exceeded the threshold.
            if (timer > threshold)
            {
                // If Geek is in the middle sprite of the animation.
                if (currentAnimationIndex == 0)
                {
                    // If the previous animation was the left-side sprite, then the next animation should be the right-side sprite.
                    if (previousAnimationIndex == 2)
                    {
                        currentAnimationIndex = 1;
                    }
                    else
                    // If not, then the next animation should be the left-side sprite.
                    {
                        currentAnimationIndex = 2;
                    }
                    // Track the animation.
                    previousAnimationIndex = currentAnimationIndex;
                }
                // If Geek was not in the middle sprite of the animation, he should return to the middle sprite.
                else
                {
                    currentAnimationIndex = 0;
                }
                // Reset the timer.
                timer = 0;
            }
            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            //keyboard stuff
            if (kstate.IsKeyDown(Keys.Up))
            {
                playerIndex = 1;

            }
            else if (kstate.IsKeyDown(Keys.Down))
            {
                playerIndex = 0;
            }
            else if (kstate.IsKeyDown(Keys.Right))
            {
                playerIndex = 2;
            }
            else if (kstate.IsKeyDown(Keys.Left))
            {
                playerIndex = 3;
            }
            else
                playerIndex = 0;




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            //Just drawing the Sprite
            _spriteBatch.Draw(player, new Vector2(100, 100), Color.White);

            //source rectangle for sprite sheet
            //Rectangle sourceRectangle = new Rectangle(0, 32, 25, 32);


            //forward animation
            _spriteBatch.Draw(player, new Vector2(300, 100), sourceRectangles[currentAnimationIndex], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            //public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);

            //backward animation 
            _spriteBatch.Draw(playerBack, new Vector2(400, 100), sourceRectangles[currentAnimationIndex], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

            //right animation 
            _spriteBatch.Draw(playerRight, new Vector2(500, 100), sourceRectangles[currentAnimationIndex], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            //left animation 
            _spriteBatch.Draw(playerRight, new Vector2(600, 100), sourceRectangles[currentAnimationIndex], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.FlipHorizontally, 0f);

            _spriteBatch.Draw(players[playerIndex], new Vector2(300, 200), sourceRectangles[currentAnimationIndex], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);




            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
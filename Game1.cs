using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace geekGame
{
    public class Game1 : Game
    {

        //changing this to understand version control better!
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D player;
        Texture2D playerRight;
        Texture2D playerLeft;
        Texture2D playerBack;

        Texture2D barrierColorful;

        Vector2[] barrierPos;
        

       
        
        bool movingLeft;
        bool movingRight;
        bool movingForward;
        bool movingDown;

        bool personHit;

        Vector2 playerPos;
        

        float playerSpeed;
        

        //a timer that stores miliseconds
        float timer;
        //threshold for timer
        int threshold;
        //stores the sprites you want to draw
        Rectangle[] sourceRectangles;
        // These bytes tell the spriteBatch.Draw() what sourceRectangle to display.
        byte previousAnimationIndex;
        byte currentAnimationIndex;
        KeyboardState currentState;
        KeyboardState previousState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //player position 
            playerPos = new Vector2(100,100);

            timer = 0;
            // Set an initial threshold of 250ms, you can change this to alter the speed of the animation (lower number = faster animation).
            threshold = 150;
            // Three sourceRectangles contain the coordinates of geeks's three down-facing sprites on the charaset.
            sourceRectangles = new Rectangle[3];
            sourceRectangles[0] = new Rectangle(3, 0, 25, 32); //standing still
            sourceRectangles[1] = new Rectangle(35, 0, 25, 32);//right sprint
            //sourceRectangles[3] = new Rectangle(0, 32, 25, 32); //standing still
            sourceRectangles[2] = new Rectangle(35, 32, 25, 32);//left sprite.
            barrierPos = new Vector2[3];

            barrierPos[0] = new Vector2(0, 0);
            barrierPos[1] = new Vector2(0, 200);
            barrierPos[2] = new Vector2(0, 400);

            personHit = false;







            // This tells the animation to start on the left-side sprite.
            previousAnimationIndex = 2;
            currentAnimationIndex = 0;

            movingDown = false;
            movingForward = false;
            movingLeft = false;
            movingRight = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            player = Content.Load<Texture2D>("geekWalkingForward");
            playerRight = Content.Load<Texture2D>("geekWalkingRight");
            playerLeft = Content.Load<Texture2D>("geekWalkingRight");
            playerBack = Content.Load<Texture2D>("geekWalkingBackward");

            barrierColorful = Content.Load<Texture2D>("barrierColorful");
           





            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
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
            if (currentState.IsKeyDown(Keys.Up))
            {
                movingDown = true;
                playerPos += new Vector2(0, -2);

            }
            else if (currentState.IsKeyDown(Keys.Down))
            {
                playerPos += new Vector2(0, 2);
                movingForward = true;
            }
            else if (currentState.IsKeyDown(Keys.Right))
            {
                playerPos += new Vector2(2, 0);
                movingRight = true;
            }
            else if (currentState.IsKeyDown(Keys.Left))
            {
                playerPos += new Vector2(-2, 0);
                movingLeft = true;
            }
            else
            {
                movingDown = false;
                movingForward = false;
                movingLeft = false;
                movingRight = false;
            }


            //for( int i = 0; i< barrierPos.Length; i++)
            //{
                Rectangle blockRectangle = new Rectangle((int)barrierPos[2].X, (int)barrierPos[2].Y, 64, 64);
               
                if (sourceRectangles[0].Intersects(blockRectangle) || sourceRectangles[1].Intersects(blockRectangle)|| sourceRectangles[2].Intersects(blockRectangle))
                {
                    personHit = true;
                }
                //else
                //{
                //    personHit = false;
                //}
                    
            //}


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (personHit)
            {
                GraphicsDevice.Clear(Color.Red);
            }
            else 
                GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            //Just drawing the Sprite
            //_spriteBatch.Draw(player, new Vector2(100, 100), Color.White);


            // old code for sprite drawing
            #region
            //source rectangle for sprite sheet
            //Rectangle sourceRectangle = new Rectangle(0, 32, 25, 32);

            ////forward animation
            //_spriteBatch.Draw(player, new Vector2(300, 100), sourceRectangles[currentAnimationIndex], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            ////public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);

            ////backward animation 
            //_spriteBatch.Draw(playerBack, new Vector2(400, 100), sourceRectangles[currentAnimationIndex], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

            ////right animation 
            //_spriteBatch.Draw(playerRight, new Vector2(500, 100), sourceRectangles[currentAnimationIndex], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            ////left animation 
            //_spriteBatch.Draw(playerRight, new Vector2(600, 100), sourceRectangles[currentAnimationIndex], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.FlipHorizontally, 0f);
            #endregion

            if (movingDown)
            {
                _spriteBatch.Draw(playerBack, playerPos, sourceRectangles[currentAnimationIndex], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            }
            else if (movingForward)
            {
                _spriteBatch.Draw(player, playerPos, sourceRectangles[currentAnimationIndex], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            }
            else if (movingRight)
            {
                _spriteBatch.Draw(playerRight, playerPos, sourceRectangles[currentAnimationIndex], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            }
            else if (movingLeft)
            {
                _spriteBatch.Draw(playerRight, playerPos, sourceRectangles[currentAnimationIndex], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.FlipHorizontally, 0f);
            }
            else
                _spriteBatch.Draw(player, playerPos, sourceRectangles[0], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

            for (int b = 0; b < barrierPos.Length; b++)
            {
                _spriteBatch.Draw(barrierColorful, barrierPos[b], new Rectangle(0,0,64, 64), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

            }




            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mono_house_defense
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HouseDefenseGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Texture2D SkeletonTexture { get; set; }

        public HouseDefenseGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            SkeletonTexture = Content.Load<Texture2D>("Skeleton/skeleton_walk");
        }
       
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Rectangle sourceRectangle = new Rectangle(0, 0, 22, 33); 
            Vector2 position = new Vector2(100, 100);
            Vector2 origin = new Vector2(0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(SkeletonTexture, position, sourceRectangle, Color.White, 0.0f, origin, 3, SpriteEffects.None, 0.0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

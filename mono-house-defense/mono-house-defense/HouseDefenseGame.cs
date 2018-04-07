using mono_house_defense.Characters;
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
        Skeleton skeleton = new Skeleton();

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
            skeleton.LoadAllFrames(CharacterAction.Walk, SkeletonTexture, numberOfFramesInSpriteSheet: 13);
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

            Vector2 position = new Vector2(100, 100);

            

            spriteBatch.Begin();

            skeleton.Walk(spriteBatch, position);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

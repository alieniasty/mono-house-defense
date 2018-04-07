using mono_house_defense.Characters;
using mono_house_defense.Characters.Abstractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mono_house_defense
{
    public class HouseDefenseGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Skeleton skeleton;

        public Texture2D SkeletonTexture { get; set; }

        public HouseDefenseGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            skeleton = new Skeleton();
        }

        
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            SkeletonTexture = Content.Load<Texture2D>("Skeleton/skeleton_attack");

            skeleton.LoadAllFrames(
                CharacterAction.Fight, 
                SkeletonTexture, 
                numberOfFramesInSpriteSheet: 18, 
                dimensions: new Vector2(43, 37));

            SkeletonTexture = Content.Load<Texture2D>("Skeleton/skeleton_walk");

            skeleton.LoadAllFrames(
                CharacterAction.Walk, 
                SkeletonTexture, 
                numberOfFramesInSpriteSheet: 13, 
                dimensions: new Vector2(22, 33));

            SkeletonTexture = Content.Load<Texture2D>("Skeleton/skeleton_hit");

            skeleton.LoadAllFrames(
                CharacterAction.Hit, 
                SkeletonTexture, 
                numberOfFramesInSpriteSheet: 8, 
                dimensions: new Vector2(30, 32));

            SkeletonTexture = Content.Load<Texture2D>("Skeleton/skeleton_die");

            skeleton.LoadAllFrames(
                CharacterAction.Die, 
                SkeletonTexture, 
                numberOfFramesInSpriteSheet: 15, 
                dimensions: new Vector2(33, 32));
        }
       
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            skeleton.UpdateAnimation(CharacterAction.Hit);
            skeleton.UpdatePosition(gameTime, speed: 3.2f);
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            skeleton.Hit(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

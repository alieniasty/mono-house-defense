using System;
using System.Collections.Generic;
using mono_house_defense.Characters;
using mono_house_defense.Characters.Abstractions;
using mono_house_defense.Loaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mono_house_defense
{
    public class HouseDefenseGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Random random = new Random();

        private List<Skeleton> skeletons;
        private List<Bandit> bandits;
        private List<Knight> knights;

        public Texture2D SkeletonTexture { get; set; }
        public Texture2D BanditTexture { get; set; }
        public Texture2D KnightTexture { get; set; }
        public Texture2D Background { get; set; }

        public HouseDefenseGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            skeletons = new List<Skeleton>
            {
                new Skeleton(millisecondsPerFrame:50, initialPosition: new Vector2(0, 380), speed: (float)random.NextDouble()*10),
                new Skeleton(millisecondsPerFrame:50, initialPosition: new Vector2(0, 380), speed: (float)random.NextDouble()*10),
                new Skeleton(millisecondsPerFrame:50, initialPosition: new Vector2(0, 380), speed: (float)random.NextDouble()*10),
                new Skeleton(millisecondsPerFrame:50, initialPosition: new Vector2(0, 380), speed: (float)random.NextDouble()*10),
            };

            bandits = new List<Bandit>
            {
                new Bandit(millisecondsPerFrame:50, initialPosition: new Vector2(0, 330), speed: (float)random.NextDouble()*10),
                new Bandit(millisecondsPerFrame:50, initialPosition: new Vector2(0, 330), speed: (float)random.NextDouble()*10),
                new Bandit(millisecondsPerFrame:50, initialPosition: new Vector2(0, 330), speed: (float)random.NextDouble()*10),
                new Bandit(millisecondsPerFrame:50, initialPosition: new Vector2(0, 330), speed: (float)random.NextDouble()*10),
            };

            knights = new List<Knight>
            {
                new Knight(millisecondsPerFrame:50, initialPosition: new Vector2(0, 365), speed: (float)random.NextDouble()*10),
                new Knight(millisecondsPerFrame:50, initialPosition: new Vector2(0, 365), speed: (float)random.NextDouble()*10),
                new Knight(millisecondsPerFrame:50, initialPosition: new Vector2(0, 365), speed: (float)random.NextDouble()*10),
                new Knight(millisecondsPerFrame:50, initialPosition: new Vector2(0, 365), speed: (float)random.NextDouble()*10),
            }; 
        }

        
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1680;
            graphics.PreferredBackBufferHeight = 1050;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Background = Content.Load<Texture2D>("Background/background");

            var loader = new AnimationsLoader(Content);

            foreach (var bandit in bandits)
            {
                loader.LoadBandit(BanditTexture, bandit);
            }

            foreach (var knight in knights)
            {
                loader.LoadKnight(KnightTexture, knight);
            }

            foreach (var skeleton in skeletons)
            {
                loader.LoadSkeleton(SkeletonTexture, skeleton);
            }
        }

        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var bandit in bandits)
            {
                bandit.UpdateAnimation(CharacterAction.Walk, gameTime);
                bandit.UpdatePosition(gameTime);
            }

            foreach (var knight in knights)
            {
                knight.UpdateAnimation(CharacterAction.Walk, gameTime);
                knight.UpdatePosition(gameTime);
            }

            foreach (var skeleton in skeletons)
            {
                skeleton.UpdateAnimation(CharacterAction.Walk, gameTime);
                skeleton.UpdatePosition(gameTime);
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();


            spriteBatch.Draw(
                Background, 
                new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), 
                Color.White);

            foreach (var bandit in bandits)
            {
                bandit.Walk(spriteBatch);
            }

            foreach (var knight in knights)
            {
                knight.Walk(spriteBatch);
            }

            foreach (var skeleton in skeletons)
            {
                skeleton.Walk(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

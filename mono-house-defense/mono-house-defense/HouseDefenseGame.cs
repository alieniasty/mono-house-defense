using System;
using System.Collections.Generic;
using System.Linq;
using mono_house_defense.Characters;
using mono_house_defense.Characters.Abstractions;
using mono_house_defense.Characters.Factories.Astractions;
using mono_house_defense.Loaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mono_house_defense
{
    public class HouseDefenseGame : Game
    {
        #region Fields

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private List<Skeleton> skeletons;
        private List<Bandit> bandits;
        private List<Knight> knights;
        private Explosion playerShot;

        private SoundEffectInstance singleShotSoundEffect;
        private MouseState lastMouseState;

        private Texture2D background;
        private Texture2D scope;
        private Texture2D playerHouse;

        private bool isEligibleToShoot = false;

        #endregion

        public HouseDefenseGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            #region Initialize content

            const int skeletonsVerticalPosition = 410;
            const int banditsVerticalPosition = 390;
            const int knightsVerticalPosition = 400;
            const int playerShotVerticalPosition = 385;

            var random = new Random();

            skeletons = CharacterFactoryBase.Create<Skeleton>(
                numberOfCharacters: 10,
                millisecondsPerFrame: 50,
                initialPosition: new Vector2(0, skeletonsVerticalPosition));

            bandits = CharacterFactoryBase.Create<Bandit>(
                numberOfCharacters: 0,
                millisecondsPerFrame: 50,
                initialPosition: new Vector2(0, banditsVerticalPosition));

            knights = CharacterFactoryBase.Create<Knight>(
                numberOfCharacters: 10,
                millisecondsPerFrame: 50,
                initialPosition: new Vector2(0, knightsVerticalPosition));

            playerShot = CharacterFactoryBase.Create<Explosion>(
                numberOfCharacters: 1,
                millisecondsPerFrame: 10,
                initialPosition: new Vector2(graphics.GraphicsDevice.Viewport.Width - 110, playerShotVerticalPosition)).Single();
            
            background = Content.Load<Texture2D>("Background/background");
            scope = Content.Load<Texture2D>("Miscellanous/scope");
            playerHouse = Content.Load<Texture2D>("Houses/House_1");
            singleShotSoundEffect = Content.Load<SoundEffect>("Sounds/snipershot").CreateInstance();

            #endregion

            #region Load animations

            var loader = new AnimationsLoader(Content);

            foreach (var bandit in bandits)
            {
                loader.LoadBandit(bandit);
            }

            foreach (var knight in knights)
            {
                loader.LoadKnight(knight);
            }

            foreach (var skeleton in skeletons)
            {
                loader.LoadSkeleton(skeleton);
            }

            loader.LoadExplosion(playerShot);

                #endregion

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            #region Update

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var currentMouseState = Mouse.GetState();

            if (currentMouseState.LeftButton == ButtonState.Pressed 
                && lastMouseState.LeftButton == ButtonState.Released
                && singleShotSoundEffect.State == SoundState.Stopped)
            {
                isEligibleToShoot = true;
            }

            lastMouseState = currentMouseState;

            var characterPositionBorder = GraphicsDevice.Viewport.Width - 120;

            foreach (var bandit in bandits)
            {
                bandit.Update(gameTime, characterPositionBorder, currentMouseState);
            }

            foreach (var knight in knights)
            {
                knight.Update(gameTime, characterPositionBorder, currentMouseState);
            }

            foreach (var skeleton in skeletons)
            {
                skeleton.Update(gameTime, characterPositionBorder, currentMouseState);
            }

            if (isEligibleToShoot)
            {
                singleShotSoundEffect.Play();
                playerShot.UpdateFrames(gameTime, CharacterAction.Fight);

                if (playerShot.AnimationIsPlaying == false)
                {
                    isEligibleToShoot = false;
                }
            }

            base.Update(gameTime);

            #endregion
        }
        
        protected override void Draw(GameTime gameTime)
        {
            #region Draw

            spriteBatch.Begin();

            spriteBatch.Draw(
                background, 
                new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), 
                Color.White);

            spriteBatch.Draw(
                playerHouse, 
                new Rectangle(GraphicsDevice.Viewport.Width - 90, GraphicsDevice.Viewport.Height - 130, 100, 100), 
                Color.White);

            foreach (var bandit in bandits)
            {
                bandit.Draw(spriteBatch);
            }

            foreach (var knight in knights)
            {
                knight.Draw(spriteBatch);
            }

            foreach (var skeleton in skeletons)
            {
                skeleton.Draw(spriteBatch);
            }

            if (isEligibleToShoot)
            {
                playerShot.Draw(spriteBatch);
            }

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront);

            spriteBatch.Draw(
                scope,
                new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 39, 39),
                Color.White);

            spriteBatch.End();

            base.Draw(gameTime);

            #endregion
        }
    }
}

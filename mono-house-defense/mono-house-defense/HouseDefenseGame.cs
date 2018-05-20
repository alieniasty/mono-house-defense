using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using mono_house_defense.Characters;
using mono_house_defense.Characters.Abstractions;
using mono_house_defense.DTO;
using mono_house_defense.Factories;
using mono_house_defense.Loaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mono_house_defense
{
    public class HouseDefenseGame : Game
    {
        #region Gameplay Fields

        private GraphicsDeviceManager _graphics;
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
        private SpriteFont font;

        private bool isEligibleToShoot = false;

        private GameState state;
        private LevelState levelState = new LevelState();

        #endregion

        #region Menu Fields

        private Texture2D playButtonTexture;
        private Rectangle recPlayButton;
        private Color playButtonColor = Color.White;
        private Rectangle cursor;

        private bool gameplayReady = false;
        private bool gameplayStarted = false;

        #endregion

        public HouseDefenseGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            //_graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            switch (state)
            {
                case GameState.Play:
                    LoadGameplay();
                    IsMouseVisible = false;
                    break;
                case GameState.Menu:
                    LoadMenu();
                    IsMouseVisible = true;
                    break;
                default:
                    state = GameState.Menu;
                    LoadMenu();
                    IsMouseVisible = true;
                    break;
            }

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        private void LoadGameplay()
        {
            #region Initialize content

            var random = new Random();

            skeletons = CharacterFactoryBase.Create<Skeleton>(
                numberOfCharacters: levelState.NumberOfSkeletons,
                millisecondsPerFrame: 50,
                initialPosition: new Vector2(levelState.SkeletonsHorizontalPosition, levelState.SkeletonsVerticalPosition));

            bandits = CharacterFactoryBase.Create<Bandit>(
                numberOfCharacters: levelState.NumberOfBandits,
                millisecondsPerFrame: 50,
                initialPosition: new Vector2(levelState.BanditsHorizontalPosition, levelState.BanditsVerticalPosition));

            knights = CharacterFactoryBase.Create<Knight>(
                numberOfCharacters: levelState.NumberOfKnights,
                millisecondsPerFrame: 50,
                initialPosition: new Vector2(levelState.KnightsHorizontalPosition, levelState.KnightsVerticalPosition));

            playerShot = CharacterFactoryBase.Create<Explosion>(
                numberOfCharacters: 1,
                millisecondsPerFrame: 10,
                initialPosition: new Vector2(levelState.PlayerShotHorizontalPosition, levelState.PlayerShotVerticalPosition)).Single();

            background = Content.Load<Texture2D>("Background/background");
            scope = Content.Load<Texture2D>("Miscellanous/scope");
            playerHouse = Content.Load<Texture2D>("Houses/House_1");
            font = Content.Load<SpriteFont>("Miscellanous/arcadeclassic");
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

            gameplayReady = true;

            #endregion
        }

        private void LoadMenu()
        {
            playButtonTexture = Content.Load<Texture2D>("Miscellanous/PlayButton");
            recPlayButton = new Rectangle(820, 450, 300, 200);
        }

        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case GameState.Play:
                    if (gameplayReady)
                    {
                        UpdateGameplay(gameTime);
                        gameplayStarted = true;
                    }
                    break;
                case GameState.Menu:
                    UpdateMenu(gameTime);
                    break;
            }
            
        }

        private void UpdateMenu(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            cursor.X = mouseState.X; cursor.Y = mouseState.Y;

            if ((recPlayButton.Intersects(cursor)))
            {
                playButtonColor = Color.Green;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    playButtonColor = Color.Red;
                    GraphicsDevice.Clear(Color.Black);
                    state = GameState.Play;
                    LoadContent();
                }
            }
            else
                playButtonColor = Color.White;
        }

        private void UpdateGameplay(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Score.Instance.Killed >= levelState.TotalNumberOfCharacters)
            {
                GraphicsDevice.Clear(Color.Black);
                state = GameState.Menu;
                gameplayReady = false;
                gameplayStarted = false;
                LoadContent();
            }

            var currentMouseState = Mouse.GetState();

            if (currentMouseState.LeftButton == ButtonState.Pressed
                && lastMouseState.LeftButton == ButtonState.Released
                && singleShotSoundEffect.State == SoundState.Stopped)
            {
                isEligibleToShoot = true;
            }

            var aimState = new AimState(currentMouseState, isEligibleToShoot);
            lastMouseState = currentMouseState;

            var characterPositionBorder = GraphicsDevice.Viewport.Width - 120;

            foreach (var bandit in bandits)
            {
                bandit.Update(gameTime, characterPositionBorder, aimState);
            }

            foreach (var knight in knights)
            {
                knight.Update(gameTime, characterPositionBorder, aimState);
            }

            foreach (var skeleton in skeletons)
            {
                skeleton.Update(gameTime, characterPositionBorder, aimState);
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
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (state)
            {
                case GameState.Play:
                    if (gameplayStarted)
                    {
                        DrawGameplay();
                    }
                    break;
                case GameState.Menu:
                    DrawMenu();
                    break;;
            }

            base.Draw(gameTime);
        }

        private void DrawMenu()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(playButtonTexture, recPlayButton, playButtonColor);
            spriteBatch.End();
        }

        private void DrawGameplay()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(
                background,
                new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
                Color.White);

            spriteBatch.Draw(
                playerHouse,
                new Rectangle(levelState.PlayerHousePosition, levelState.PlayerHouseDimensions),
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

            spriteBatch.DrawString(font, Score.Instance.Value, levelState.ScorePosition, Color.MonoGameOrange, 0, new Vector2(), 4, SpriteEffects.None, 0);

            spriteBatch.Draw(
                scope,
                new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 39, 39),
                Color.White);

            spriteBatch.End();
        }
    }
}

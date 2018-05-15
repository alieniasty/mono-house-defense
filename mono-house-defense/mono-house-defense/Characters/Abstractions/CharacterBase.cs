using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mono_house_defense.Characters.Abstractions
{
    public abstract class CharacterBase 
    {
        private float timeSinceLastFrame;
        private float _millisecondsPerFrame;
        private float _speed;
        private string _currentFrame;

        public bool AnimationIsPlaying = false;

        private Dictionary<string, DrawableFrame> _drawableFramesDictionary = new Dictionary<string, DrawableFrame>();
        protected Frame Frame;
        protected Vector2 Position;
        private Vector2 _dimensions;
        private CharacterAction _state;

        public CharacterBase(float millisecondsPerFrame, Vector2 position, float speed)
        {
            _millisecondsPerFrame = millisecondsPerFrame;
            Position = position;
            _speed = speed;
        }

        protected virtual void AddFrame(string frame, Texture2D texture, Rectangle sourceRectangle, SpriteEffects spriteEffects)
        {
            _drawableFramesDictionary.Add(frame, new DrawableFrame
            {
                Texture2D = texture,
                SourceRectangle = sourceRectangle,
                SpriteEffects = spriteEffects
            });
        }

        public virtual void LoadAllFrames(CharacterAction action, Texture2D texture, int numberOfColumns, int numberOfRows, Vector2 dimensions)
        {
            _dimensions = dimensions;
            SetMaxFrameIndexes(action, numberOfColumns * numberOfRows);

            int frameIndex = 0;

            for (var row = 0; row < numberOfRows; row++)
            {
                var frameBeginning = 0;

                for (var column = 0; column < numberOfColumns; column++)
                {
                    var sourceRectangle = new Rectangle(frameBeginning, row * (int)dimensions.Y, (int)dimensions.X, (int)dimensions.Y);
                    AddFrame($"{action.ToString()}_{frameIndex}", texture, sourceRectangle, SpriteEffects.None);
                    frameBeginning += (int)dimensions.X;
                    frameIndex++;
                }
            }

        }

        /*Set max frame index with -1 because index in dictionary starts at zero.*/
        protected virtual void SetMaxFrameIndexes(CharacterAction action, int numberOfFramesInSpriteSheet)
        {
            switch (action)
            {
                case CharacterAction.Walk:
                    Frame.WalkFrameMaxIndex = numberOfFramesInSpriteSheet - 1;
                    break;
                case CharacterAction.Die:
                    Frame.DieFrameMaxIndex = numberOfFramesInSpriteSheet - 1;
                    break;
                case CharacterAction.Fight:
                    Frame.FightFrameMaxIndex = numberOfFramesInSpriteSheet - 1;
                    break;
                case CharacterAction.Hit:
                    Frame.HitFrameMaxIndex = numberOfFramesInSpriteSheet - 1;
                    break;
            }
        }

        public virtual void UpdateFrames(GameTime gameTime, CharacterAction action)
        {
            AnimationIsPlaying = true;
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > _millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                switch (action)
                {
                    case CharacterAction.Walk:

                        if (Frame.WalkFrameIndex == Frame.WalkFrameMaxIndex)
                        {
                            Frame.WalkFrameIndex = 0;
                            AnimationIsPlaying = false;
                        }

                        Frame.WalkFrameIndex++;
                        break;

                    case CharacterAction.Die:

                        if (Frame.DieFrameIndex == Frame.DieFrameMaxIndex)
                        {
                            AnimationIsPlaying = false;
                        }

                        if (Frame.DieFrameIndex < Frame.DieFrameMaxIndex)
                        {
                            Frame.DieFrameIndex++;
                        }
                        break;

                    case CharacterAction.Fight:

                        if (Frame.FightFrameIndex == Frame.FightFrameMaxIndex)
                        {
                            Frame.FightFrameIndex = 0;
                            AnimationIsPlaying = false;
                        }

                        Frame.FightFrameIndex++;
                        break;

                    case CharacterAction.Hit:

                        if (Frame.HitFrameIndex == Frame.HitFrameMaxIndex)
                        {
                            Frame.HitFrameIndex = 0;
                            AnimationIsPlaying = false;
                        }

                        Frame.HitFrameIndex++;
                        break;
                }
            }
        }

        public virtual void Update(GameTime gameTime, int border, MouseState mouse, bool isAnimation = false)
        {
            if (isAnimation == false)
            {
                SetState(gameTime, border, mouse);
            }
            else
            {
                _state = CharacterAction.Fight;
            }
            SetCurrentFrame();
            UpdateFrames(gameTime, _state);
        }

        private void SetCurrentFrame()
        {
            switch (_state)
            {
                case CharacterAction.Walk:
                    _currentFrame = $"{_state}_{Frame.WalkFrameIndex}";
                    break;
                case CharacterAction.Die:
                    _currentFrame = $"{_state}_{Frame.DieFrameIndex}";
                    break;
                case CharacterAction.Fight:
                    _currentFrame = $"{_state}_{Frame.FightFrameIndex}";
                    break;
                case CharacterAction.Hit:
                    _currentFrame = $"{_state}_{Frame.HitFrameIndex}";
                    break;
            }
        }

        private void SetState(GameTime gameTime, int border, MouseState mouse)
        {
            if (Position.X < border && _state != CharacterAction.Die)
            {
                var position = Position;
                position.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds / _speed;
                Position = position;

                _state = CharacterAction.Walk;
            }

            if (Position.X >= border && _state != CharacterAction.Die)
            {
                _state = CharacterAction.Fight;
            }

            if (HitBoxTriggered(mouse))
            {
                _state = CharacterAction.Die;
            }
        }

        private bool HitBoxTriggered(MouseState mouse)
        {
            if (mouse.X > Position.X && mouse.X < Position.X + _dimensions.X && mouse.LeftButton == ButtonState.Pressed)
            {
                return true;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, string frame, float rotation, float scale)
        {
            if (_drawableFramesDictionary.Count == 0)
            {
                throw new KeyNotFoundException("DrawableCharactersBase dictionary must contain values to be drawn.");
            }

            spriteBatch.Draw(
                _drawableFramesDictionary[frame].Texture2D, 
                Position, 
                _drawableFramesDictionary[frame].SourceRectangle, 
                Color.White, 
                rotation, 
                new Vector2(0,0), 
                scale, 
                _drawableFramesDictionary[frame].SpriteEffects, 
                layerDepth: 0.0f);
        }

        public void Draw(SpriteBatch spriteBatch, float rotation, float scale)
        {
            if (_drawableFramesDictionary.Count == 0)
            {
                throw new KeyNotFoundException("DrawableCharactersBase dictionary must contain values to be drawn.");
            }

            spriteBatch.Draw(
                _drawableFramesDictionary[_currentFrame].Texture2D,
                Position,
                _drawableFramesDictionary[_currentFrame].SourceRectangle,
                Color.White,
                rotation,
                new Vector2(0, 0),
                scale,
                _drawableFramesDictionary[_currentFrame].SpriteEffects,
                layerDepth: 0.0f);
        }
    }
}

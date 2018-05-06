using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono_house_defense.Characters.Abstractions
{
    public abstract class CharacterBase 
    {
        private float timeSinceLastFrame;
        private float _millisecondsPerFrame;
        private float _speed;

        public bool AnimationIsPlaying = false;

        protected Dictionary<string, DrawableCharacter> DrawableCharactersBase = new Dictionary<string, DrawableCharacter>();
        protected Frame Frame;
        protected Vector2 Position;

        public abstract void Walk(SpriteBatch spriteBatch);
        public abstract void Fight(SpriteBatch spriteBatch);
        public abstract void Hit(SpriteBatch spriteBatch);
        public abstract void Die(SpriteBatch spriteBatch);

        public CharacterBase(float millisecondsPerFrame, Vector2 position, float speed)
        {
            _millisecondsPerFrame = millisecondsPerFrame;
            Position = position;
            _speed = speed;
        }

        protected virtual void AddFrame(string frame, Texture2D texture, Rectangle sourceRectangle, SpriteEffects spriteEffects)
        {
            DrawableCharactersBase.Add(frame, new DrawableCharacter
            {
                Texture2D = texture,
                SourceRectangle = sourceRectangle,
                SpriteEffects = spriteEffects
            });
        }

        public virtual void LoadAllFrames(CharacterAction action, Texture2D texture, int numberOfColumns, int numberOfRows, Vector2 dimensions)
        {
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

        public virtual void UpdateAnimation(CharacterAction action, GameTime gameTime)
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
                            Frame.DieFrameIndex = 0;
                            AnimationIsPlaying = false;

                        }

                        Frame.DieFrameIndex++;
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

        public virtual void UpdatePosition(GameTime gameTime)
        {
            var position = Position;
            position.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds / _speed;
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch, string frame, float rotation, float scale)
        {
            if (DrawableCharactersBase.Count == 0)
            {
                throw new KeyNotFoundException("DrawableCharactersBase dictionary must contain values to be drawn.");
            }

            spriteBatch.Draw(
                DrawableCharactersBase[frame].Texture2D, 
                Position, 
                DrawableCharactersBase[frame].SourceRectangle, 
                Color.White, 
                rotation, 
                new Vector2(0,0), 
                scale, 
                DrawableCharactersBase[frame].SpriteEffects, 
                layerDepth: 0.0f);
        }
    }
}

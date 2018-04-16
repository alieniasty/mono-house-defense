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

        protected Dictionary<string, DrawableCharacter> DrawableCharactersBase = new Dictionary<string, DrawableCharacter>();
        protected Frame Frame;
        protected Vector2 _initialPosition;
        private float _speed;

        public abstract void Walk(SpriteBatch spriteBatch);
        public abstract void Fight(SpriteBatch spriteBatch);
        public abstract void Hit(SpriteBatch spriteBatch);
        public abstract void Die(SpriteBatch spriteBatch);

        public CharacterBase(float millisecondsPerFrame, Vector2 initialPosition, float speed)
        {
            _millisecondsPerFrame = millisecondsPerFrame;
            _initialPosition = initialPosition;
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

        public virtual void LoadAllFrames(CharacterAction action, Texture2D skeletonTexture, int numberOfFramesInSpriteSheet, Vector2 dimensions)
        {
            SetMaxFrameIndexes(action, numberOfFramesInSpriteSheet);

            var frameBeginning = 0;

            for (var i = 0; i < numberOfFramesInSpriteSheet; i++)
            {
                var sourceRectangle = new Rectangle(frameBeginning, 0, (int)dimensions.X, (int)dimensions.Y);
                AddFrame($"{action.ToString()}_{i}", skeletonTexture, sourceRectangle, SpriteEffects.None);
                frameBeginning += (int)dimensions.X;
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
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > _millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                switch (action)
                {
                    case CharacterAction.Walk:
                        if (Frame.WalkFrameIndex == Frame.WalkFrameMaxIndex) Frame.WalkFrameIndex = 0;
                        Frame.WalkFrameIndex++;
                        break;
                    case CharacterAction.Die:
                        if (Frame.DieFrameIndex == Frame.DieFrameMaxIndex) Frame.DieFrameIndex = 0;
                        Frame.DieFrameIndex++;
                        break;
                    case CharacterAction.Fight:
                        if (Frame.FightFrameIndex == Frame.FightFrameMaxIndex) Frame.FightFrameIndex = 0;
                        Frame.FightFrameIndex++;
                        break;
                    case CharacterAction.Hit:
                        if (Frame.HitFrameIndex == Frame.HitFrameMaxIndex) Frame.HitFrameIndex = 0;
                        Frame.HitFrameIndex++;
                        break;
                }
            }
        }

        public virtual void UpdatePosition(GameTime gameTime)
        {
            var position = _initialPosition;
            position.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds / _speed;
            _initialPosition = position;
        }

        public void Draw(SpriteBatch spriteBatch, string frame, Vector2 position, float rotation, int scale)
        {
            if (DrawableCharactersBase.Count == 0)
            {
                throw new KeyNotFoundException("DrawableCharactersBase dictionary must contain values to be drawn.");
            }

            spriteBatch.Draw(
                DrawableCharactersBase[frame].Texture2D, 
                position, 
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

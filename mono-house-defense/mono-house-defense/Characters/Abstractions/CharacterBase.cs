using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono_house_defense.Characters.Abstractions
{
    public abstract class CharacterBase
    {
        protected Dictionary<string, DrawableCharacter> DrawableCharactersBase = new Dictionary<string, DrawableCharacter>();

        protected Frame frame;
        protected Vector2 Position { get; set; }

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
                    frame.WalkFrameMaxIndex = numberOfFramesInSpriteSheet - 1;
                    break;
                case CharacterAction.Die:
                    frame.DieFrameMaxIndex = numberOfFramesInSpriteSheet - 1;
                    break;
                case CharacterAction.Fight:
                    frame.FightFrameMaxIndex = numberOfFramesInSpriteSheet - 1;
                    break;
                case CharacterAction.Hit:
                    frame.HitFrameMaxIndex = numberOfFramesInSpriteSheet - 1;
                    break;
            }
        }

        public virtual void UpdateAnimation(CharacterAction action)
        {
            switch (action)
            {
                case CharacterAction.Walk:
                    if (frame.WalkFrameIndex == frame.WalkFrameMaxIndex) frame.WalkFrameIndex = 0;
                    frame.WalkFrameIndex++;
                    break;
                case CharacterAction.Die:
                    if (frame.DieFrameIndex == frame.DieFrameMaxIndex) frame.DieFrameIndex = 0;
                    frame.DieFrameIndex++;
                    break;
                case CharacterAction.Fight:
                    if (frame.FightFrameIndex == frame.FightFrameMaxIndex) frame.FightFrameIndex = 0;
                    frame.FightFrameIndex++;
                    break;
                case CharacterAction.Hit:
                    if (frame.HitFrameIndex == frame.HitFrameMaxIndex) frame.HitFrameIndex = 0;
                    frame.HitFrameIndex++;
                    break;
            }
        }

        public virtual void UpdatePosition(GameTime gameTime, float speed)
        {
            var position = Position;
            position.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds / speed;
            Position = position;
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

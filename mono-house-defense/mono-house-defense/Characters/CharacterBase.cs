using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono_house_defense.Characters
{
    public abstract class CharacterBase
    {
        protected Dictionary<string, DrawableCharacter> DrawableCharactersBase = new Dictionary<string, DrawableCharacter>();

        public abstract void AddFrame(string frame, Texture2D texture, Rectangle sourceRectangle, SpriteEffects spriteEffects);
        public abstract void LoadAllFrames(Texture2D skeletonTexture, int numberOfFramesInSpriteSheet);

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

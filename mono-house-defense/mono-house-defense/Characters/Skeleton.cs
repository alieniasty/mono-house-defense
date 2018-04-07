using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono_house_defense.Characters
{
    public class Skeleton : CharacterBase
    {
        public Dictionary<string, int> FramesMetadata { get; set; }

        public override void AddFrame(string frame, Texture2D texture, Rectangle sourceRectangle, SpriteEffects spriteEffects)
        {
            base.DrawableCharactersBase.Add(frame, new DrawableCharacter
            {
                Texture2D = texture,
                SourceRectangle = sourceRectangle,
                SpriteEffects = spriteEffects
            });
        }

        public override void LoadAllFrames(CharacterAction action, Texture2D skeletonTexture, int numberOfFramesInSpriteSheet)
        {
            UpdateFramesMetadata(action.ToString(), numberOfFramesInSpriteSheet);

            var frameBeginning = 0;

            for (var i = 0; i < numberOfFramesInSpriteSheet; i++)
            {
                var sourceRectangle = new Rectangle(frameBeginning, 0, 22, 33);
                AddFrame($"{action.ToString()}_{i}", skeletonTexture, sourceRectangle, SpriteEffects.None);
                frameBeginning += 22;
            }
            
        }

        private void UpdateFramesMetadata(string framesPartialName, int numberOfFramesInSpriteSheet)
        {
            FramesMetadata.Add(framesPartialName, numberOfFramesInSpriteSheet);
        }

        public void Walk(SpriteBatch spriteBatch, Vector2 position)
        {
            foreach (var key in FramesMetadata.Keys.Where(k => k.Equals(CharacterAction.Walk.ToString())))
            {
                
            }
        }
    }

}

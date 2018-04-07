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
        public override void AddFrame(string frame, Texture2D texture, Rectangle sourceRectangle, SpriteEffects spriteEffects)
        {
            base.DrawableCharactersBase.Add(frame, new DrawableCharacter
            {
                Texture2D = texture,
                SourceRectangle = sourceRectangle,
                SpriteEffects = spriteEffects
            });
        }

        public override void LoadAllFrames(Texture2D skeletonTexture, int numberOfFramesInSpriteSheet)
        {
            var frameBeginning = 0;
            for (var i = 0; i < numberOfFramesInSpriteSheet; i++)
            {
                var sourceRectangle = new Rectangle(frameBeginning, 0, 22, 33);
                AddFrame($"walk_{i}", skeletonTexture, sourceRectangle, SpriteEffects.None);
                frameBeginning += 22;
            }
            
        }
    }

}

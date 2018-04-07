using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono_house_defense.Characters.Abstractions
{
    public class DrawableCharacter
    {
        public Texture2D Texture2D { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mono_house_defense.Characters.Abstractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono_house_defense.Characters
{
    public class Sorcerer : CharacterBase
    {
        private const int _scale = 1;

        public Sorcerer(float millisecondsPerFrame, Vector2 position, float speed) : base(millisecondsPerFrame, position, speed)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CorrectAnimationChange();

            base.Draw(
                spriteBatch,
                rotation: 0.0f,
                scale: _scale);
        }

        public void CorrectAnimationChange()
        {
            if (State == CharacterAction.Fight && PositionChanged == true)
            {
                Position.Y -= 80;
                PositionChanged = false;
            }
        }
    }
}

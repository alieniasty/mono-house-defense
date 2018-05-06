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
    public class Explosion : CharacterBase
    {
        private const float _scale = 0.3f;

        public Explosion(float millisecondsPerFrame, Vector2 position, float speed) 
            : base(millisecondsPerFrame, position, speed)
        {
        }

        public override void Walk(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Fight(SpriteBatch spriteBatch)
        {
            base.Draw(
                spriteBatch,
                frame: $"{CharacterAction.Fight}_{Frame.FightFrameIndex}",
                rotation: 0.0f,
                scale: _scale);
        }

        public override void Hit(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Die(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}

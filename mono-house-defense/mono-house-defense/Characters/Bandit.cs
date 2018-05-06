using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using mono_house_defense.Characters.Abstractions;
using Microsoft.Xna.Framework;

namespace mono_house_defense.Characters
{
    public class Bandit : CharacterBase
    {
        private const float _scale = 1;

        public Bandit(float millisecondsPerFrame, Vector2 position, float speed) 
            : base(millisecondsPerFrame, position, speed)
        {
        }

        public override void Die(SpriteBatch spriteBatch)
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

        public override void Walk(SpriteBatch spriteBatch)
        {
            base.Draw(
                spriteBatch,
                frame: $"{CharacterAction.Walk}_{Frame.WalkFrameIndex}",
                rotation: 0.0f,
                scale: _scale);
        }
    }
}

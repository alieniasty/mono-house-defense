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
    public class Knight : CharacterBase
    {
        public Knight(float millisecondsPerFrame, Vector2 initialPosition, float speed) 
            : base(millisecondsPerFrame, initialPosition, speed)
        {
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
                position: _initialPosition,
                rotation: 0.0f,
                scale: 2);
        }

        public override void Fight(SpriteBatch spriteBatch)
        {
            base.Draw(
                spriteBatch,
                frame: $"{CharacterAction.Fight}_{Frame.FightFrameIndex}",
                position: _initialPosition,
                rotation: 0.0f,
                scale: 2);
        }

        public override void Die(SpriteBatch spriteBatch)
        {
            base.Draw(
                spriteBatch,
                frame: $"{CharacterAction.Die}_{Frame.DieFrameIndex}",
                position: _initialPosition,
                rotation: 0.0f,
                scale: 2);
        }
    }
}

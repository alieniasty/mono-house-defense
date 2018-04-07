using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using mono_house_defense.Characters.Abstractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono_house_defense.Characters
{
    public class Skeleton : CharacterBase
    {
        public void Walk(SpriteBatch spriteBatch)
        {
            base.Draw(
                spriteBatch, 
                frame: $"{CharacterAction.Walk}_{frame.WalkFrameIndex}", 
                position: Position, 
                rotation: 0.0f, 
                scale: 2);
        }

        public void Fight(SpriteBatch spriteBatch)
        {
            base.Draw(
                spriteBatch,
                frame: $"{CharacterAction.Fight}_{frame.FightFrameIndex}",
                position: Position,
                rotation: 0.0f,
                scale: 2);
        }

        public void Hit(SpriteBatch spriteBatch)
        {
            base.Draw(
                spriteBatch,
                frame: $"{CharacterAction.Hit}_{frame.HitFrameIndex}",
                position: Position,
                rotation: 0.0f,
                scale: 2);
        }

        public void Die(SpriteBatch spriteBatch)
        {
            base.Draw(
                spriteBatch,
                frame: $"{CharacterAction.Die}_{frame.DieFrameIndex}",
                position: Position,
                rotation: 0.0f,
                scale: 2);
        }
    }

}

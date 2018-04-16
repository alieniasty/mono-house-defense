﻿using System;
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
        public Skeleton(float millisecondsPerFrame, Vector2 initialPosition, float speed) 
            : base(millisecondsPerFrame, initialPosition, speed)
        {
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

        public override void Hit(SpriteBatch spriteBatch)
        {
            base.Draw(
                spriteBatch,
                frame: $"{CharacterAction.Hit}_{Frame.HitFrameIndex}",
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

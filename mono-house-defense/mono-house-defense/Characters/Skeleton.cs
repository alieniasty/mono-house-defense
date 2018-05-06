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
        private const float _scale = 1;

        public Skeleton(float millisecondsPerFrame, Vector2 position, float speed) 
            : base(millisecondsPerFrame, position, speed)
        {
        }

        public override void Walk(SpriteBatch spriteBatch)
        {
            base.Draw(
                spriteBatch,
                frame: $"{CharacterAction.Walk}_{Frame.WalkFrameIndex}",
                rotation: 0.0f,
                scale: _scale);
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
            base.Draw(
                spriteBatch,
                frame: $"{CharacterAction.Hit}_{Frame.HitFrameIndex}",
                rotation: 0.0f,
                scale: _scale);
        }

        public override void Die(SpriteBatch spriteBatch)
        {
            base.Draw(
                spriteBatch,
                frame: $"{CharacterAction.Die}_{Frame.DieFrameIndex}",
                rotation: 0.0f,
                scale: _scale);
        }
    }

}

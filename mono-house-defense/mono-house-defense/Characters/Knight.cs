﻿using System;
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
        private const float _scale = 2;

        public Knight(float millisecondsPerFrame, Vector2 position, float speed) 
            : base(millisecondsPerFrame, position, speed)
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
                Position.Y -= 65;
                PositionChanged = false;
            }
        }
    }
}

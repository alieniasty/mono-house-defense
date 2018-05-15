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
        private const float _scale = 1;

        public Skeleton(float millisecondsPerFrame, Vector2 position, float speed) 
            : base(millisecondsPerFrame, position, speed)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(
                spriteBatch,
                rotation: 0.0f,
                scale: _scale);
        }
    }

}

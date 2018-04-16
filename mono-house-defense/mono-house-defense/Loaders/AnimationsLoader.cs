using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mono_house_defense.Characters;
using mono_house_defense.Characters.Abstractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace mono_house_defense.Loaders
{
    public class AnimationsLoader
    {
        private ContentManager _content;

        public AnimationsLoader(ContentManager content)
        {
            _content = content;
        }

        public void LoadSkeleton(Texture2D skeletonTexture, Skeleton skeleton)
        {
            skeletonTexture = _content.Load<Texture2D>("Skeleton/skeleton_attack");

            skeleton.LoadAllFrames(
                CharacterAction.Fight,
                skeletonTexture,
                numberOfFramesInSpriteSheet: 18,
                dimensions: new Vector2(43, 37));

            skeletonTexture = _content.Load<Texture2D>("Skeleton/skeleton_walk");

            skeleton.LoadAllFrames(
                CharacterAction.Walk,
                skeletonTexture,
                numberOfFramesInSpriteSheet: 13,
                dimensions: new Vector2(22, 33));

            skeletonTexture = _content.Load<Texture2D>("Skeleton/skeleton_hit");

            skeleton.LoadAllFrames(
                CharacterAction.Hit,
                skeletonTexture,
                numberOfFramesInSpriteSheet: 8,
                dimensions: new Vector2(30, 32));

            skeletonTexture = _content.Load<Texture2D>("Skeleton/skeleton_die");

            skeleton.LoadAllFrames(
                CharacterAction.Die,
                skeletonTexture,
                numberOfFramesInSpriteSheet: 15,
                dimensions: new Vector2(33, 32));
        }

        public void LoadBandit(Texture2D banditTexture, Bandit bandit)
        {
            banditTexture = _content.Load<Texture2D>("Bandit/bandit_attack");

            bandit.LoadAllFrames(
                CharacterAction.Fight,
                banditTexture,
                numberOfFramesInSpriteSheet: 7,
                dimensions: new Vector2(80, 80));

            banditTexture = _content.Load<Texture2D>("Bandit/bandit_run");

            bandit.LoadAllFrames(
                CharacterAction.Walk,
                banditTexture,
                numberOfFramesInSpriteSheet: 8,
                dimensions: new Vector2(80, 80));
        }

        public void LoadKnight(Texture2D knightTexture, Knight knight)
        {
            knightTexture = _content.Load<Texture2D>("Knight/knight_fight");

            knight.LoadAllFrames(
                CharacterAction.Fight,
                knightTexture,
                numberOfFramesInSpriteSheet: 10,
                dimensions: new Vector2(80, 80));

            knightTexture = _content.Load<Texture2D>("Knight/knight_walk");

            knight.LoadAllFrames(
                CharacterAction.Walk,
                knightTexture,
                numberOfFramesInSpriteSheet: 8,
                dimensions: new Vector2(42, 42));

            knightTexture = _content.Load<Texture2D>("Knight/knight_die");

            knight.LoadAllFrames(
                CharacterAction.Die,
                knightTexture,
                numberOfFramesInSpriteSheet: 9,
                dimensions: new Vector2(42, 42));
        }
    }
}

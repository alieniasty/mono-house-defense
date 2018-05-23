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

        public void LoadSkeleton(Skeleton skeleton)
        {
            var skeletonTexture = _content.Load<Texture2D>("Skeleton/skeleton_attack");

            skeleton.LoadAllFrames(
                CharacterAction.Fight,
                skeletonTexture,
                numberOfColumns: 18,
                numberOfRows: 1,
                dimensions: new Vector2(43, 37));

            skeletonTexture = _content.Load<Texture2D>("Skeleton/skeleton_walk");

            skeleton.LoadAllFrames(
                CharacterAction.Walk,
                skeletonTexture,
                numberOfColumns: 13,
                numberOfRows: 1,
                dimensions: new Vector2(22, 33));

            skeletonTexture = _content.Load<Texture2D>("Skeleton/skeleton_hit");

            skeleton.LoadAllFrames(
                CharacterAction.Hit,
                skeletonTexture,
                numberOfColumns: 8,
                numberOfRows: 1,
                dimensions: new Vector2(30, 32));

            skeletonTexture = _content.Load<Texture2D>("Skeleton/skeleton_die");

            skeleton.LoadAllFrames(
                CharacterAction.Die,
                skeletonTexture,
                numberOfColumns: 15,
                numberOfRows: 1,
                dimensions: new Vector2(33, 32));
        }

        public void LoadBandit(Bandit bandit)
        {
            var banditTexture = _content.Load<Texture2D>("Bandit/bandit_attack");

            bandit.LoadAllFrames(
                CharacterAction.Fight,
                banditTexture,
                numberOfColumns: 7,
                numberOfRows: 1,
                dimensions: new Vector2(80, 80));

            banditTexture = _content.Load<Texture2D>("Bandit/bandit_attack");

            bandit.LoadAllFrames(
                CharacterAction.Die,
                banditTexture,
                numberOfColumns: 7,
                numberOfRows: 1,
                dimensions: new Vector2(80, 80));

            banditTexture = _content.Load<Texture2D>("Bandit/bandit_run");

            bandit.LoadAllFrames(
                CharacterAction.Walk,
                banditTexture,
                numberOfColumns: 8,
                numberOfRows: 1,
                dimensions: new Vector2(80, 80));
        }

        public void LoadKnight(Knight knight)
        {
            var knightTexture = _content.Load<Texture2D>("Knight/knight_fight");

            knight.LoadAllFrames(
                CharacterAction.Fight,
                knightTexture,
                numberOfColumns: 10,
                numberOfRows: 1,
                dimensions: new Vector2(80, 80));

            knightTexture = _content.Load<Texture2D>("Knight/knight_walk");

            knight.LoadAllFrames(
                CharacterAction.Walk,
                knightTexture,
                numberOfColumns: 8,
                numberOfRows: 1,
                dimensions: new Vector2(42, 42));

            knightTexture = _content.Load<Texture2D>("Knight/knight_die");

            knight.LoadAllFrames(
                CharacterAction.Die,
                knightTexture,
                numberOfColumns: 9,
                numberOfRows: 1,
                dimensions: new Vector2(42, 42));

            knightTexture = _content.Load<Texture2D>("Knight/knight_die");

            knight.LoadAllFrames(
                CharacterAction.Hit,
                knightTexture,
                numberOfColumns: 9,
                numberOfRows: 1,
                dimensions: new Vector2(42, 42));
        }

        public void LoadExplosion(Explosion explosion)
        {
            var explosionTexture = _content.Load<Texture2D>("Explosions/explosion_5");

            explosion.LoadAllFrames(
                CharacterAction.Fight,
                explosionTexture,
                numberOfColumns: 8,
                numberOfRows: 1,
                dimensions: new Vector2(256, 274));
        }

        public void LoadSorcerers(Sorcerer sorcerer)
        {
            var sorcererTexture = _content.Load<Texture2D>("Sorcerer/sorcerer_attack");

            sorcerer.LoadAllFrames(
                CharacterAction.Fight,
                sorcererTexture,
                numberOfColumns: 10,
                numberOfRows: 1,
                dimensions: new Vector2(200, 200));

            sorcererTexture = _content.Load<Texture2D>("Sorcerer/sorcerer_slide");

            sorcerer.LoadAllFrames(
                CharacterAction.Walk,
                sorcererTexture,
                numberOfColumns: 8,
                numberOfRows: 7,
                dimensions: new Vector2(100, 100));

            sorcererTexture = _content.Load<Texture2D>("Sorcerer/sorcerer_die");

            sorcerer.LoadAllFrames(
                CharacterAction.Die,
                sorcererTexture,
                numberOfColumns: 4,
                numberOfRows: 4,
                dimensions: new Vector2(100, 100));
        }
    }
}

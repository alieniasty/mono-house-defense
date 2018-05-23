using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace mono_house_defense.DTO
{
    public class LevelState
    {
        public int LevelNumber { get; set; } = 0;

        public int SkeletonsVerticalPosition = 930;
        public int BanditsVerticalPosition = 890;
        public int KnightsVerticalPosition = 920;
        public int PlayerShotVerticalPosition = 885;
        public int SorcerersVerticalPosition = 900;

        public int SkeletonsHorizontalPosition = -1;
        public int BanditsHorizontalPosition = -1;
        public int KnightsHorizontalPosition = -1;
        public int PlayerShotHorizontalPosition = 1690;
        public int SorcerersHorizontalPosition = 200;

        public int NumberOfSkeletons = 7;
        public int NumberOfBandits = 3;
        public int NumberOfKnights = 5;
        public int NumberOfSorcerers = 0;

        public bool SorcererReady { get; set; } = false;
        public bool GameplayIsStarting { get; set; }

        public int TotalNumberOfCharacters
        {
            get { return NumberOfBandits + NumberOfKnights + NumberOfSkeletons + NumberOfSorcerers; }
        }

        public Vector2 ScorePosition { get; set; } = new Vector2(940, 100);
        public Point PlayerHouseDimensions { get; set; } = new Point(180, 180);
        public Point PlayerHousePosition { get; set; } = new Point(1740, 830);
    }
}

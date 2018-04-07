namespace mono_house_defense.Characters.Abstractions
{
    public struct Frame
    {
        public int WalkFrameIndex { get; set; }
        public int FightFrameIndex { get; set; }
        public int HitFrameIndex { get; set; }
        public int DieFrameIndex { get; set; }

        public int WalkFrameMaxIndex { get; set; }
        public int FightFrameMaxIndex { get; set; }
        public int HitFrameMaxIndex { get; set; }
        public int DieFrameMaxIndex { get; set; }
    }
}

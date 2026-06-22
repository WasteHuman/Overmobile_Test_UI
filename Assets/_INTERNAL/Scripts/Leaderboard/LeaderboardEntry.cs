namespace Leaderboard
{
    public readonly struct LeaderboardEntry
    {
        public readonly int Rank;
        public readonly string Name;
        public readonly int Score;

        public LeaderboardEntry(int rank, string name, int score)
        {
            Rank = rank;
            Name = name;
            Score = score;
        }
    }
}
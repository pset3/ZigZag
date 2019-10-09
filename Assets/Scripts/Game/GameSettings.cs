using System;

public partial class Game
{
    [Serializable]
    public partial class GameSettings
    {
        public enum Difficult
        {
            Easy,
            Medium,
            Hard
        }

        public enum BonusSpawn
        {
            Random,
            Orderly
        }

        public Difficult difficult;
        public BonusSpawn bonusSpawn;
    }
}
using System;

public class PlayerData
{
    public int score;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            if (value != score)
            {
                score = value;
                OnScoreChanged();
            }
        }
    }

    public event Action OnScoreChanged = () => { };
}
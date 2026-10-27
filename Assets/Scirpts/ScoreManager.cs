using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static System.Action<uint> OnScoreUpdate;

    public uint Score { get; private set; }
    public uint BestScore { get; private set; }

    public void LoadBestScore()
    {
        BestScore = 0;
    }

    internal void AddPoint()
    {
        Score += 1;
        if(Score > BestScore)
            BestScore = Score;
        OnScoreUpdate?.Invoke(Score);
    }
    public void ResetPoints()
    {
        Score = 0;
        OnScoreUpdate?.Invoke(Score);
    }
}

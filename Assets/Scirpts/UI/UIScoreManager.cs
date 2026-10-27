using System;
using UnityEngine;
using TMPro;

public class UIScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;

    public void Open()
    {
        ScoreManager.OnScoreUpdate += OnScoreUpdate;
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
        ScoreManager.OnScoreUpdate -= OnScoreUpdate;
    }

    private void OnScoreUpdate(uint score)
    {
        scoreLabel.text = score.ToString();
    }
}

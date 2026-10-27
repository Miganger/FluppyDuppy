using UnityEngine;
using TMPro;

public class UISummaryManager : UIPanel
{
    [SerializeField] private TextMeshProUGUI pointsPanel;
    [SerializeField] private TextMeshProUGUI bestPointsPanel;

    public override void Open()
    {
        pointsPanel.text = GameManager.Instance.ScoreManager.Score.ToString();
        bestPointsPanel.text = GameManager.Instance.ScoreManager.BestScore.ToString();

        base.Open();
    }
}

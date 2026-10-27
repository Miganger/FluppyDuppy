using System;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public UITransitionController TransitionController => transitionController;
        public UIScoreManager ScoreManager => scoreManager;
        public UISummaryManager SummaryManager => summaryPanel;
        public UIPanel PreparePanel => preparePanel;
        

        [Header("References")]
        [SerializeField] private GameObject titlePanel;
        [SerializeField] private UISummaryManager summaryPanel;
        [SerializeField] private UIPanel preparePanel;
        [SerializeField] private UITransitionController transitionController;
        [SerializeField] private UIScoreManager scoreManager;
        [Header("Controllers")]
        [SerializeField] AudioSource buttonAudioSource;

        public void OpenTitlePanel()
        {
            titlePanel.SetActive(true);
        }
        public void CloseTitlePanel()
        {
            titlePanel.SetActive(false);
        }
        public void StartNewGameButton()
        {
            buttonAudioSource.Play();
            GameManager.Instance.GameStateMachine.ChangeState(Enums.EGameState.Prepare);
        }
        public void OpenMainMenu()
        {
            buttonAudioSource.Play();
            GameManager.Instance.GameStateMachine.ChangeState(Enums.EGameState.MainMenu);
        }
    }
}
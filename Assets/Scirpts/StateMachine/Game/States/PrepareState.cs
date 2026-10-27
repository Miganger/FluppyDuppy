using StateMachine.Game;
using System.Collections;
using UI;
using UnityEngine;

namespace StateMachine.Game.States
{
    public class PrepareState : GameState
    {
        protected override IEnumerator Enter()
        {
            UITransitionController transitionController = GameManager.Instance.UIManager.TransitionController;

            GameManager.Instance.PlayerController.ShowPlayer();
            GameManager.Instance.GroundMovement.EnableMovement();
            GameManager.Instance.BackgroundMovement.EnableMovement();

            GameManager.Instance.UIManager.ScoreManager.Open();
            GameManager.Instance.ScoreManager.ResetPoints();

            GameManager.Instance.UIManager.PreparePanel.Open();

            transitionController.PlayTransitionOut();
            yield return new WaitUntil(() => transitionController.TransitionOutEnd);

        }

        protected override IEnumerator Exit()
        {
            GameManager.Instance.UIManager.PreparePanel.Close();
            GameManager.Instance.UIManager.SummaryManager.Close();
            return base.Exit();
        }
    }
}
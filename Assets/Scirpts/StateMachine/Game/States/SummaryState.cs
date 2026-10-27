using System.Collections;
using UI;
using UnityEngine;

namespace StateMachine.Game.States
{
    public class SummaryState : GameState
    {
        protected override IEnumerator Enter()
        {
            GameManager.Instance.UIManager.SummaryManager.Open();
            return base.Enter();
        }

        protected override IEnumerator Exit()
        {
            GameManager.Instance.UIManager.TransitionController.PlayTransitionIn();
            yield return new WaitUntil(() => GameManager.Instance.UIManager.TransitionController.TransitionInEnd);
            GameManager.Instance.ObstacleManager.ClearObjects();
            GameManager.Instance.PlayerController.Prepare();
            GameManager.Instance.UIManager.SummaryManager.Close();
        }
    }
}
using System.Collections;
using UnityEngine;

namespace StateMachine.Game.States
{
    public class MainMenuState : GameState
    {
        protected override IEnumerator Enter()
        {
            GameManager.Instance.UIManager.OpenTitlePanel();
            GameManager.Instance.PlayerController.HidePlayer();
            UITransitionController transitionController = GameManager.Instance.UIManager.TransitionController;
            transitionController.PlayTransitionOut();
            yield return new WaitUntil(() => transitionController.TransitionOutEnd);
        }

        protected override IEnumerator Exit()
        {
            UITransitionController transitionController = GameManager.Instance.UIManager.TransitionController;
            transitionController.PlayTransitionIn();
            yield return new WaitUntil(() => transitionController.TransitionInEnd);
            GameManager.Instance.UIManager.CloseTitlePanel();
        }
    }
}
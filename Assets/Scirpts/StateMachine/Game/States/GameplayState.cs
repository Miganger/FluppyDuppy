using Player;
using System.Collections;
using UnityEngine;

namespace StateMachine.Game.States
{
    public class GameplayState : GameState
    {
        protected override IEnumerator Enter()
        {
            GameManager.Instance.DifficultyController.ResetDifficulty();

            GameManager.Instance.ObstacleManager.EnableSpawning();
            GameManager.Instance.DifficultyController.StartLogic();

            return base.Enter();
        }

        protected override IEnumerator Exit()
        {
            GameManager.Instance.UIManager.ScoreManager.Close();

            GameManager.Instance.DifficultyController.Stop();

            GameManager.Instance.GroundMovement.DisableMovement();
            GameManager.Instance.BackgroundMovement.DisableMovement();
            GameManager.Instance.ObstacleManager.DisableSpawning();
            return base.Exit();
        }
    }
}
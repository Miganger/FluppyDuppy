using Effects;
using Player;
using StateMachine.Game;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public EffectsManager EffectsManager => effectsManager;
    public DifficultyController DifficultyController => difficultyController;
    public GameStateMachine GameStateMachine => gameStateMachine;
    public UIManager UIManager => uiManager;
    public RepeatingMovement GroundMovement => groundMovement;
    public RepeatingMovement BackgroundMovement => backgroundMovement;
    public ObstacleManager ObstacleManager => obstacleManager;
    public PlayerController PlayerController => playerController;
    public ScoreManager ScoreManager => scoreManager;

    [Header("References")]
    [SerializeField] private RepeatingMovement groundMovement;
    [SerializeField] private RepeatingMovement backgroundMovement;
    [SerializeField] private ObstacleManager obstacleManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EffectsManager effectsManager;
    [SerializeField] private DifficultyController difficultyController;
    [SerializeField] private GameStateMachine gameStateMachine;
    [SerializeField] private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager.LoadBestScore();
        gameStateMachine.RegisterStates();
    }
}

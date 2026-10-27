
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [Header("Difficulty Settings")]
    [SerializeField] private float timeToMaxDifficulty = 60f;

    [Header("Spawn Delay")]
    [SerializeField] private float startSpawnDelay = 3f;
    [SerializeField] private float minSpawnDelay = 0.8f;

    [Header("Gap Offset")]
    [SerializeField] private float startGapOffset = 0f;
    [SerializeField] private float maxGapOffset = 2f;

    [Header("Map speed")]
    [SerializeField] private float startSpeed = 0f;
    [SerializeField] private float maxSpeed = 2f;

    public float SpawnDelay { get; private set; }
    public float GapOffset { get; private set; }
    public float Speed { get; private set; }

    public float DifficultyProgress { get; private set; }

    private float elapsedTime;
    private bool isEnable = false;

    private void Start()
    {
        ResetDifficulty();
    }

    public void Stop()
    {
        isEnable = false;
    }

    private void Update()
    {
        if (!isEnable)
            return;

        UpdateDifficulty();
    }

    private void UpdateDifficulty()
    {
        elapsedTime += Time.deltaTime;

        DifficultyProgress = Mathf.Clamp01(
            elapsedTime / Mathf.Max(0.01f, timeToMaxDifficulty)
        );

        SpawnDelay = Mathf.Lerp(
            startSpawnDelay,
            minSpawnDelay,
            DifficultyProgress);

        GapOffset = Mathf.Lerp(
            startGapOffset,
            maxGapOffset,
            DifficultyProgress);

        Speed = Mathf.Lerp(
            startSpeed,
            maxSpeed,
            DifficultyProgress);
    }

    public void ResetDifficulty()
    {
        elapsedTime = 0f;
        DifficultyProgress = 0f;

        SpawnDelay = startSpawnDelay;
        GapOffset = startGapOffset;
        Speed = startSpeed;
    }
    public void StartLogic()
    {
        isEnable = true;
    }
}
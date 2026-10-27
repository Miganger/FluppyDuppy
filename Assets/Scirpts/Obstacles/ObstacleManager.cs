using System.Threading;
using UnityEngine;

public class ObstacleManager : PooledMonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float maxYRange = 2f;
    [SerializeField] private float maxYRangeChange = 1.5f;

    private float timer = 0;
    private bool spawningEnable = false;

    private float currentY = .1f;

    public void EnableSpawning()
    {
        spawningEnable = true;
    }
    public void DisableSpawning()
    {
        spawningEnable = false;
        foreach (Transform t in transform)
        {
            if (!t.TryGetComponent(out ObstacleObject obstacleObject))
                continue;

            obstacleObject.StopMovement();
        }
    }
    public void ClearObjects()
    {
        foreach (Transform t in transform)
        {
            if (!t.TryGetComponent(out PooledObject pooledObject))
                continue;
            if (pooledObject.gameObject.activeSelf)
                pooledObject.ReturnToPool();
        }
    }

    private void Update()
    {
        if (!spawningEnable)
            return;

        timer += Time.deltaTime;

        if (timer >= GameManager.Instance.DifficultyController.SpawnDelay)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    private void SpawnObstacle()
    {
        var obsObject = Get(0) as ObstacleObject;
        obsObject.Setup(GetObjstaclePosition());
        currentY = obsObject.transform.localPosition.y;
    }
    private Vector3 GetObjstaclePosition()
    {
        var randY = Random.Range(0, maxYRangeChange);
        int direction = Random.Range(0, 2) == 0 ? -1 : 1;
        var newY = currentY + (randY * direction);

        if (newY > maxYRange || newY < -maxYRange)
            newY = currentY + (randY * -direction);

        return new Vector3(0f, newY, 0f);
    }
}
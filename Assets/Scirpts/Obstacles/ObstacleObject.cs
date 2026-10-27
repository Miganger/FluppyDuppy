using System;
using UnityEngine;

public class ObstacleObject : PooledObject
{
    [SerializeField] private Transform upperObstacle;
    [SerializeField] private Transform bottomObstacle;
    [SerializeField] private AudioSource audioSource;

    private bool movementEnabled = false;
    private float defaultHeight;

    private void Awake()
    {
        defaultHeight = upperObstacle.localPosition.y;
    }

    internal void Setup(Vector3 position)
    {
        transform.localPosition = position;

        var y = defaultHeight - GameManager.Instance.DifficultyController.GapOffset;
        upperObstacle.localPosition = new Vector3(0, y, 0);
        bottomObstacle.localPosition = new Vector3(0, -y, 0);

        movementEnabled = true;
    }

    internal void StopMovement()
    {
        movementEnabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.Play();
        GameManager.Instance.ScoreManager.AddPoint();
    }

    private void FixedUpdate()
    {
        if (!gameObject.activeSelf)
            return;

        if(!movementEnabled) 
            return;

        var pos = transform.localPosition;
        pos.x -= Time.fixedDeltaTime * GameManager.Instance.DifficultyController.Speed;
        transform.localPosition = pos;

        if (transform.localPosition.x <= -13f)
        {
            ReturnToPool();
        }
    }
}

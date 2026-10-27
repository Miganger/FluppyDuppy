using System.Collections.Generic;
using UnityEngine;

public class RepeatingMovement : MonoBehaviour
{
    [SerializeField] private bool isBackground;
    [SerializeField] private List<Transform> chunks;
    [SerializeField] private float chunkWidth;

    private bool movementEnable = false;


    public void EnableMovement()
    {
        movementEnable = true;
    }
    public void DisableMovement()
    {
        movementEnable = false;
    }

    private void FixedUpdate()
    {
        if (!movementEnable)
            return;

        foreach(Transform t in chunks)
        {
            var speed = GameManager.Instance.DifficultyController.Speed;
            if (isBackground)
                speed *= .2f;

            var chunkPos = t.localPosition;
            chunkPos.x -= Time.fixedDeltaTime * speed;
            t.localPosition = chunkPos;

            if (t.localPosition.x <= -chunkWidth)
            {
                t.localPosition = new Vector3(chunkWidth, 0, 0);
            }
        }
    }
}

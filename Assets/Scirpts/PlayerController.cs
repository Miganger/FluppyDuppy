using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Input Actions")]
        [SerializeField] private InputActionReference jumpAction;

        [Header("Settings")]
        [SerializeField] private float jumpPower;
        [SerializeField] private float gravityScale = 1.5f;

        [Header("References")]
        [SerializeField] private Transform spriteTransform;
        [SerializeField] private CircleCollider2D circleCollider;
        [SerializeField] private AudioSource jumpAudio;
        [SerializeField] private AudioSource deathAudio;

        [Header("Collision settings")]
        [SerializeField] private string collisionTag;

        [Header("Rotation Angles")]
        [SerializeField] private float maxUpAngle = 35f;
        [SerializeField] private float maxDownAngle = -75f;

        [Header("Velocity Limits")]
        [SerializeField] private float maxRiseSpeed = 5f;
        [SerializeField] private float maxFallSpeed = 8f;

        [Header("Rotation Smoothness")]
        [SerializeField] private float upSmoothTime = 0.08f;
        [SerializeField] private float downSmoothTime = 0.25f;

        private float rotationVelocity;
        private Rigidbody2D rb;
        private Vector3 spawnPosition;

        private void Awake()
        {
            spawnPosition = transform.position;
            rb = GetComponent<Rigidbody2D>();
            DisableMovement();
        }

        public void ShowPlayer()
        {
            gameObject.SetActive(true);
        }
        public void HidePlayer()
        {
            gameObject.SetActive(false);
        }

        public void EnableMovement()
        {
            Debug.Log($"Player.EnableMovement");
            circleCollider.enabled = true;
            transform.position = spawnPosition;
            rb.linearVelocity = Vector3.zero;
            rb.gravityScale = gravityScale;
        }
        public void DisableMovement()
        {
            rb.gravityScale = 0f;
        }

        private void RotateBird()
        {
            float velocityY = rb.linearVelocity.y;
            float targetAngle;

            if (velocityY >= 0f)
            {
                float t = Mathf.Clamp01(velocityY / maxRiseSpeed);
                targetAngle = Mathf.Lerp(0f, maxUpAngle, t);
            }
            else
            {
                float t = Mathf.Clamp01(-velocityY / maxFallSpeed);
                targetAngle = Mathf.Lerp(0f, maxDownAngle, t);
            }

            float currentAngle = spriteTransform.localEulerAngles.z;
            float smoothTime = velocityY >= 0f
                ? upSmoothTime
                : downSmoothTime;

            float newAngle = Mathf.SmoothDampAngle(
                currentAngle,
                targetAngle,
                ref rotationVelocity,
                smoothTime
            );

            spriteTransform.localRotation =
                Quaternion.Euler(0f, 0f, newAngle);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag.Equals(collisionTag))
            {
                GameManager.Instance.EffectsManager.PlayEffect(collision.contacts[0].point, Enums.EEffect.Puff);
                Death();
            }
        }

        private void Death()
        {
            deathAudio.Play();
            circleCollider.enabled = false;
            rb.AddForceY(6f, ForceMode2D.Impulse);
            GameManager.Instance.GameStateMachine.ChangeState(Enums.EGameState.Summary);
        }

        private void Update()
        {
            if (jumpAction.action.triggered)
            {
                if (GameManager.Instance.GameStateMachine.CurrentStateID == Enums.EGameState.Prepare)
                {
                    GameManager.Instance.GameStateMachine.ChangeState(Enums.EGameState.Gameplay);
                    EnableMovement();
                }
                else if (GameManager.Instance.GameStateMachine.CurrentStateID != Enums.EGameState.Gameplay)
                {
                    return;
                }
                Jump();
            }
        }

        private void Jump()
        {
            if (transform.position.y >= 3.5f)
                return;

            jumpAudio.Play();

            if (rb.linearVelocityY < 0)
            {
                rb.linearVelocity = new Vector2(0f, jumpPower);
            }
            else
            {
                rb.AddForceY(jumpPower, ForceMode2D.Impulse);
            }
        }

        private void LateUpdate()
        {
            if (transform.position.y < -5f && rb.gravityScale != 0)
            {
                rb.gravityScale = 0;
                rb.linearVelocity = Vector3.zero;
            }

            RotateBird();
        }

        internal void Prepare()
        {
            circleCollider.enabled = true;
            transform.position = spawnPosition;
            rb.linearVelocity = Vector3.zero;
            rb.gravityScale = 0;
        }
    }

}
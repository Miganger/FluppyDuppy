using Enums;
using UnityEngine;

namespace Effects
{
    public class EffectObject : PooledObject
    {
        public bool IsActive => gameObject.activeSelf;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public virtual void Play(Vector3 position, EEffect type)
        {
            transform.position = position;
            animator.SetTrigger(type.ToString());
        }
    }
}
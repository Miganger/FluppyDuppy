using Enums;
using UnityEngine;

namespace Effects
{
    public class EffectsManager : PooledMonoBehaviour
    {
        internal void PlayEffect(Vector3 position, EEffect effectType)
        {
            switch (effectType)
            {
                case EEffect.Puff:
                case EEffect.Hit:
                    var effect = Get(0) as EffectObject;
                    effect.Play(position, effectType);
                    break;
            }
        }
    }
}
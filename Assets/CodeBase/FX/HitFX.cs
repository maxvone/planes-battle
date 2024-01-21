using DG.Tweening;
using UnityEngine;

namespace CodeBase.FX
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class HitFX : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void Play()
        {
            _spriteRenderer.DOFade(0, 0);
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_spriteRenderer.DOFade(1, 0.1f));
            sequence.Append(_spriteRenderer.DOFade(0, 0.1f));
            sequence.SetLoops(3);
            
            _spriteRenderer.DOFade(0, 0);
        }
    }
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class UIHeart : MonoBehaviour
    {
        [SerializeField] private Image _fullHeart;
        [SerializeField] private Image _flash;
        private Sequence _sequence;


        public void MakeFull() => 
            _fullHeart.enabled = true;

        public void MakeEmpty()
        {
            _fullHeart.enabled = false;
            if (_sequence !=null && _sequence.IsPlaying()) 
                StopFlash();
        }

        public void StartFlash()
        {
            _flash.DOFade(0, 0);
            
            _sequence = DOTween.Sequence();
            _sequence.Append(_flash.DOFade(0.5f, 0.25f));
            _sequence.Append(_flash.DOFade(0.5f, 0.25f));
            _sequence.SetLoops(-1);
        }

        private void StopFlash()
        {
            _sequence.Kill();
            _flash.DOFade(0, 0);
        }
    }
}

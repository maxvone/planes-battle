using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class UIHealth : MonoBehaviour
    {
        [SerializeField] private UIHeart _uiHeart;

        private List<UIHeart> _uiHearts = new();
        private float _maxHp;

        public void Construct(float maxHp)
        {
            _maxHp = maxHp;
            InitHearts();
        }

        private void InitHearts()
        {
            for (int i = 0; i < _maxHp; i++) 
                _uiHearts.Add(Instantiate(_uiHeart, transform));
        }

        public void SetValue(float currentHp)
        {
            DisableAllHearts();
            EnableHeartsAccordingToHeroHp(currentHp);
            StartBlinking(currentHp);
        }

        private void DisableAllHearts()
        {
            foreach (UIHeart heart in _uiHearts)
                heart.MakeEmpty();
        }

        private void EnableHeartsAccordingToHeroHp(float currentHp)
        {
            for (int i = 0; i < currentHp; i++)
                _uiHearts[i].MakeFull();
        }
        
        private void StartBlinking(float currentHp)
        {
            if (currentHp <= _maxHp / 3)
                _uiHearts[(int)currentHp - 1].StartFlash();
        }

    }
}

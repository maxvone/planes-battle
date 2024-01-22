using System;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class UIScore : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        private void Start() => 
            ResetScore();

        private void ResetScore() => 
            SetValue(0);

        public void SetValue(int newValue) => 
            _scoreText.text = newValue.ToString();
    }
}

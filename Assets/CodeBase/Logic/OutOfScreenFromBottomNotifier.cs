using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public class OutOfScreenFromBottomNotifier : MonoBehaviour
    {
        public event Action MovedOutOfScreen;
        private bool _invoked;
        private float _screenBottomInWorldCoords;

        private void Start()
        {
            _screenBottomInWorldCoords = Camera.main.ScreenToWorldPoint(
                new Vector3(0, Screen.height, 0)).y * -1;
        }

        public void Reset() =>
            _invoked = false;

        private void Update()
        {
            if (!_invoked && transform.position.y < _screenBottomInWorldCoords)
            {
                MovedOutOfScreen?.Invoke();
                _invoked = true;
            }
        }
    }
}

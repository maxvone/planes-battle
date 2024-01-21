using System;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Infrastructure.Factory
{
    public class HeroBullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private IObjectPool<HeroBullet> _bulletsPool;
        private bool _released;

        public void Construct(IObjectPool<HeroBullet> bulletsPool) => 
            _bulletsPool = bulletsPool;

        private void OnEnable() => _released = false;


        private void Update() => 
            Move();

        private void Move() => 
            transform.position += (Vector3)Vector2.up * _speed * Time.deltaTime;

        private void OnTriggerEnter2D(Collider2D col) => 
            Release();

        private void OnBecameInvisible() => 
            Release();

        private void Release()
        {
            if (_released) return;
            _released = true;
            _bulletsPool.Release(this);
        }
    }
}
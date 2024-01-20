using System;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Infrastructure.Factory
{
    public class HeroBullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private IObjectPool<HeroBullet> _bulletsPool;

        public void Construct(IObjectPool<HeroBullet> bulletsPool) => 
            _bulletsPool = bulletsPool;

        private void Update()
        {
            Move();
        }

        private void Move() => 
            transform.position += (Vector3)Vector2.up * _speed * Time.deltaTime;

        private void OnBecameInvisible()
        {
            _bulletsPool.Release(this);
        }
    }
}
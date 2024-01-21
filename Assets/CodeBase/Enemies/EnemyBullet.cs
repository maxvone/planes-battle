using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Transform _heroTransform;
        private Vector3 _directionToMove;
        private EnemyBulletLauncher _enemyBulletPool;

        

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
            SetupDirection();
        }

        private void SetupDirection()
        {
            _directionToMove = (_heroTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(_directionToMove.y, _directionToMove.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public void SetPool(EnemyBulletLauncher enemyBulletPool) => 
            _enemyBulletPool = enemyBulletPool;

        private void Update() => 
            Move();

        private void Move() => 
            transform.position += _directionToMove * _speed * Time.deltaTime;

    }
}
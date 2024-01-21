using System;
using System.Collections;
using CodeBase.Enemies;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(EnemyHealth))]
  public class EnemyDeath : MonoBehaviour
  {
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private OutOfScreenFromBottomNotifier _outOfScreenFromBottomNotifier;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private IGameFactory _gameFactory;
    private EnemyPoolSpawner _enemiesPool;
    private ExplosionsSpawner _explosionPool;
    private GameObject _explosion;

    public event Action Happened;


    private void OnEnable()
    {
      _health.HealthChanged += OnHealthChanged;
      _outOfScreenFromBottomNotifier.MovedOutOfScreen += Destroy;
    }

    private void OnDisable()
    {
      _health.HealthChanged -= OnHealthChanged;
      _outOfScreenFromBottomNotifier.MovedOutOfScreen -= Destroy;
    }
    
    public void Construct(IGameFactory gameFactory, ExplosionsSpawner explosionPool)
    {
      _gameFactory = gameFactory;
      _explosionPool = explosionPool;
    }

    public void SetPool(EnemyPoolSpawner enemiesPool) => 
      _enemiesPool = enemiesPool;

    private void OnHealthChanged()
    {
      if (_health.Current <= 0)
        Die();
    }

    private void Die()
    {
      DisableVisuals();
      SpawnDeathFx();
      DestroyTimer();
      Happened?.Invoke();
    }

    private void DisableVisuals() => 
      _spriteRenderer.enabled = false;

    private void SpawnDeathFx()
    {
      _explosion = _gameFactory.CreateExplosion(transform.position);
      _explosion.transform.SetParent(transform);
    }

    private async void DestroyTimer()
    {
      await UniTask.Delay(1000);
      Destroy();
    }

    private void Destroy()
    {
      if (_explosion != null)
      {
        _explosionPool.Release(_explosion);
        _explosion = null;
      }
      
      _enemiesPool.Release(gameObject);
    }

  }
}
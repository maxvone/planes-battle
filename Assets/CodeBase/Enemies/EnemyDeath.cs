using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Enemies
{
  [RequireComponent(typeof(EnemyHealth))]
  public class EnemyDeath : MonoBehaviour
  {
    public event Action Happened;
    
    [SerializeField] private int _scorePoints;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private OutOfScreenFromBottomNotifier _outOfScreenFromBottomNotifier;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private IGameFactory _gameFactory;
    private EnemyPoolSpawner _enemiesPool;
    private ExplosionsSpawner _explosionPool;
    private GameObject _explosion;
    private IScoreCounter _scoreCounter;

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
    
    public void Construct(IGameFactory gameFactory, ExplosionsSpawner explosionPool, IScoreCounter scoreCounter)
    {
      _gameFactory = gameFactory;
      _explosionPool = explosionPool;
      _scoreCounter = scoreCounter;
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
      _scoreCounter.Add(_scorePoints);
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
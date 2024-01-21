using System;
using System.Collections;
using CodeBase.Enemies;
using CodeBase.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(EnemyHealth))]
  public class EnemyDeath : MonoBehaviour
  {
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private GameObject _deathFx;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private IGameFactory _gameFactory;
    private EnemyPoolSpawner _enemiesPool;
    private ExplosionsSpawner _explosionPool;
    private GameObject _explosion;
    private bool _destroyed;

    public event Action Happened;


    private void OnEnable()
    {
      _destroyed = false;
      _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable() => 
      _health.HealthChanged -= OnHealthChanged;

    public void SetPool(EnemyPoolSpawner enemiesPool) => 
      _enemiesPool = enemiesPool;

    private void Update() => 
      ReleaseIfOutOfScreen();

    private void ReleaseIfOutOfScreen()
    {
      if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y && !_destroyed)
        StartCoroutine(DestroyTimer());
    }

    public void Construct(IGameFactory gameFactory, ExplosionsSpawner explosionPool)
    {
      _gameFactory = gameFactory;
      _explosionPool = explosionPool;
    }

    private void OnHealthChanged()
    {
      if (_health.Current <= 0)
        Die();
    }

    private void Die()
    {
      _health.HealthChanged -= OnHealthChanged;
      DisableVisuals();
      SpawnDeathFx();
      StartCoroutine(DestroyTimer());
      Happened?.Invoke();
    }

    private void DisableVisuals() => 
      _spriteRenderer.enabled = false;

    private void SpawnDeathFx()
    {
      _explosion = _gameFactory.CreateExplosion(transform.position);
      _explosion.transform.SetParent(transform);
    }

    private IEnumerator DestroyTimer()
    {
      yield return new WaitForSeconds(3);

      if (_explosion != null) 
        _explosionPool.Release(_explosion);
      
      _explosion = null;
      _enemiesPool.Release(gameObject);
      _destroyed = true;
    }

  }
}
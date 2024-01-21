using System;
using System.Collections;
using CodeBase.Enemies;
using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(EnemyHealth))]
  public class EnemyDeath : MonoBehaviour
  {

    [SerializeField] private EnemyHealth _health;
    [SerializeField] private GameObject _deathFx;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private IGameFactory _gameFactory;

    public event Action Happened;

    private void Start() => 
      _health.HealthChanged += OnHealthChanged;

    private void OnDestroy() => 
      _health.HealthChanged -= OnHealthChanged;

    public void Construct(IGameFactory gameFactory) => 
      _gameFactory = gameFactory;

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
      GameObject explosion = _gameFactory.CreateExplosion(transform.position);
      explosion.transform.SetParent(transform);
    }

    private IEnumerator DestroyTimer()
    {
      yield return new WaitForSeconds(3);
      Destroy(gameObject);
    }

  }
}
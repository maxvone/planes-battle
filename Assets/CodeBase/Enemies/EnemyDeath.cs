using System;
using System.Collections;
using CodeBase.Enemies;
using UnityEngine;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(EnemyHealth))]
  public class EnemyDeath : MonoBehaviour
  {

    [SerializeField] private EnemyHealth _health;
    [SerializeField] private GameObject _deathFx;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public event Action Happened;

    private void Start()
    {
      _health.HealthChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
      _health.HealthChanged -= OnHealthChanged;
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
      GameObject deathFx = Instantiate(_deathFx, gameObject.transform);
      deathFx.transform.position = transform.position;
    }

    private IEnumerator DestroyTimer()
    {
      yield return new WaitForSeconds(3);
      Destroy(gameObject);
    }
  }
}
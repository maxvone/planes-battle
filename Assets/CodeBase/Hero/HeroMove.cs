using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
  public class HeroMove : MonoBehaviour 
  {
    [SerializeField] private float _speed;
    
    private IInputService _inputService;

    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Update()
    {
      Vector3 movementDirection = Vector2.zero;

      if (HasInput())
        Move();
    }

    private bool HasInput() => 
      _inputService != null && _inputService.Axis.sqrMagnitude > Mathf.Epsilon;

    private void Move()
    {
      Vector3 movementDirection = _inputService.Axis;
      movementDirection.z = 0;
      movementDirection.Normalize();

      Vector3 targetPosition = transform.position + movementDirection * _speed * Time.deltaTime;
      transform.position = targetPosition;
    }
  }
}
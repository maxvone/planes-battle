using CodeBase.Infrastructure.Factory;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        private IInputService _inputService;
        private IGameFactory _gameFactory;

        public void Construct(IInputService inputService, IGameFactory gameFactory)
        {
            _inputService = inputService;
            _gameFactory = gameFactory;
        }

        private void Update()
        {
            if (CanAttack())
                Attack();
        }

        private bool CanAttack() => 
            _inputService != null && _inputService.IsAttackButtonDown();

        private void Attack() => 
            _gameFactory.CreateHeroBullet(transform.position);
    }
}
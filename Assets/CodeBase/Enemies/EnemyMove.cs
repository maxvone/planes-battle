using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private void Update() =>
        Move();

    private void Move() =>
        transform.position += (Vector3)Vector2.down * _speed * Time.deltaTime;
}
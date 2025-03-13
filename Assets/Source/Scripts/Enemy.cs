using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Poolable _poolable;
    [SerializeField] private float _moveSpeed = 5f;
    
    private Vector3 _moveDirection;
    
    public void Initialize(Spawner spawner)
    {
        _poolable.Initialize(spawner);
    }
    
    public void SetMoveDirection(Vector3 direction)
    {
        _moveDirection = direction.normalized; 
        transform.rotation = Quaternion.LookRotation(_moveDirection);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_moveDirection * (_moveSpeed * Time.deltaTime), Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        _poolable.ReturnObjectToPool();
    }
}
using UnityEngine;

public class EnemySpawner : Spawner
{
    private Vector2 _minMaxRandom = new Vector2(-1, 1);

    public override void InitilizePoolable(Poolable poolable)
    {
        if (poolable.TryGetComponent(out Enemy enemy))
        {
            enemy.Initialize(this);
        }
        else
        {
            Debug.LogError("Уээ. Ошибка.");
        }
    }

    public override void ActionOnGet(Poolable poolable)
    {
        if (poolable.TryGetComponent(out Enemy enemy))
        {
            enemy.SetMoveDirection(SetRandomMoveDirection());
        }
        
        base.ActionOnGet(poolable);
    }

    private Vector3 SetRandomMoveDirection()
    {
        float xValue;
        float zValue;
        float yValue = 0;
        
        xValue = Random.Range(_minMaxRandom.x, _minMaxRandom.y);
        zValue = Random.Range(_minMaxRandom.x, _minMaxRandom.y);

        Vector3 direction = new Vector3(xValue, yValue, zValue);
            
        return direction;
    }
}
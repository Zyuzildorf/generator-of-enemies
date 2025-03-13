using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] private Poolable _template;
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 15;

    private ObjectPool<Poolable> _pool;

    public abstract void InitilizePoolable(Poolable poolable);
    
    public void ReleasePoolable(Poolable poolable)
    {
        _pool.Release(poolable);
    }
    
    public virtual void ActionOnGet(Poolable poolable)
    {
        poolable.gameObject.transform.position = ChooseRandomSpawnPoint();
        poolable.gameObject.SetActive(true);
    }
    
    private void Awake()
    {
        _pool = new ObjectPool<Poolable>(
            createFunc: () => CreatePoolable(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetObject), 0.0f, _repeatRate);
    }
    
    private Poolable CreatePoolable()
    {
        Poolable poolable = Instantiate(_template);
        InitilizePoolable(poolable);

        return poolable;
    }
    
    private void GetObject()
    {
        _pool.Get();
    }
    
    private Vector3 ChooseRandomSpawnPoint()
    {
        Vector3 spawnPointPosition;

        spawnPointPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;

        return spawnPointPosition;
    }
}
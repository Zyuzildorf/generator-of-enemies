using UnityEngine;

public class Poolable : MonoBehaviour
{
    private Spawner _spawner;
    
    public void ReturnObjectToPool()
    {
        _spawner.ReleasePoolable(this);
    }
    
    public void Initialize(Spawner spawner)
    {
        _spawner = spawner;
    }
}
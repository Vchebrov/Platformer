using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _prefab; 
    [SerializeField] private int _instanceCount = 5; 
    [SerializeField] private float _yPositionLimit = -1f; 

    private ObjectPool<Transform> _pool;
    private float _minSpawnXPosition = -28f;
    private float _maxSpawnXPosition = 28f;

    private void Awake()
    {
        _pool = new ObjectPool<Transform>(
            createFunc: InitiateObject,
            actionOnGet: ActivateObject,
            actionOnRelease: DeactivateObject,
            actionOnDestroy: obj => Destroy(obj.gameObject), 
            collectionCheck: true,
            defaultCapacity: 5,
            maxSize: 10 
        );
    }

    private void Start()
    {
        CreateInstances();
    }
   
    private Transform InitiateObject()
    {
        if (_prefab == null)
        {
            Debug.LogError("Префаб не назначен в инспекторе!");
            return null;
        }

        Transform instance = Instantiate(_prefab);
        ICollectible collectible = instance.GetComponent<ICollectible>();

        if (collectible != null)
        {
            collectible.Collected += OnObjectCollected; 
        }
        else
        {
            Debug.LogWarning($"Объект {instance.name} не реализует ICollectible!");
        }

        instance.gameObject.SetActive(false);
        return instance;
    }

    private void ActivateObject(Transform obj)
    {
        if (obj == null) return; 

        obj.position = GenerateRandomPosition();
        obj.gameObject.SetActive(true);
    }
    
    private void DeactivateObject(Transform obj)
    {
        if (obj == null) return;

        ICollectible collectible = obj.GetComponent<ICollectible>();
        
        if (collectible != null)
        {
            collectible.Collected -= OnObjectCollected; 
        }

        obj.gameObject.SetActive(false);
    }

    private void OnObjectCollected(ICollectible collectedObject)
    {
        if (collectedObject == null) return;

        _pool.Release(collectedObject.GetTransform());
    }
    
    private void CreateInstances()
    {
        for (int i = 0; i < _instanceCount; i++)
        {
            _pool.Get();
        }
    }
   
    private Vector2 GenerateRandomPosition()
    {
        return new Vector2(
            Random.Range(_minSpawnXPosition, _maxSpawnXPosition), _yPositionLimit);
    }
}
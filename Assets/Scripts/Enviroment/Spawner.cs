using Enviroment;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ItemsToCollect _prefab; 
    [SerializeField] private int _instanceCount = 5;
    [SerializeField] private float _yPositionLimit = -1f;

    private ObjectPool<ItemsToCollect> _pool;  
    private float _minSpawnXPosition = -28f;
    private float _maxSpawnXPosition = 28f;

    private void Awake()
    {
        _pool = new ObjectPool<ItemsToCollect>(
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

    private ItemsToCollect InitiateObject()
    {
        if (_prefab == null)
        {
            Debug.LogError("Префаб отсутствует!");
            return null;
        }

        ItemsToCollect instance = Instantiate(_prefab);
        
        instance.OnCollected += OnObjectCollected;

        instance.gameObject.SetActive(false);
        return instance;
    }

    private void ActivateObject(ItemsToCollect obj)
    {
        if (obj == null) return;

        obj.transform.position = GenerateRandomPosition();
        obj.gameObject.SetActive(true);
    }

    private void DeactivateObject(ItemsToCollect obj)
    {
        if (obj == null) return;

        obj.OnCollected -= OnObjectCollected;
        obj.gameObject.SetActive(false);
    }

    private void OnObjectCollected(ItemsToCollect collectedObject)
    {
        if (collectedObject == null) return;
        
        _pool.Release(collectedObject);
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
            Random.Range(_minSpawnXPosition, _maxSpawnXPosition), 
            _yPositionLimit);
    }
}
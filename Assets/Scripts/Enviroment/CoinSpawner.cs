using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform _coinPrefab;
    [SerializeField] private int _coinsNumber = 5;
    [SerializeField] private float _yPositionLimit = -1f;
    [SerializeField] private Hero _hero;
    
    private ObjectPool<Transform> _pool;
    private float _minSpawnXPosition = -28f;
    private float _maxSpawnXPosition = 28f;

    private void Awake()
    {
        _pool = new ObjectPool<Transform>(
            createFunc: () => InitiateCoin(),
            actionOnGet: (obj) => ActivateCoin(obj),
            actionOnRelease: (obj) => RemoveFromScene(obj),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: 5,
            maxSize: 5
        );
    }
    
    private void OnEnable()
    {
        if (_hero != null)
        {
            _hero.Collected += OnRemoveCoin; 
        }
        else
        {
            Debug.LogError("CoinSpawner: _hero не задан в инспекторе!");
        }
    }

    private void Start()
    {
        CreateCoins();
    }

    private void OnDisable()
    {
        if (_hero != null)
        {
            _hero.Collected -= OnRemoveCoin; 
        }
    }

    private Transform InitiateCoin()
    {
        return Instantiate(_coinPrefab);
    }

    private void RemoveFromScene(Transform obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnRemoveCoin(Transform coinTransform)
    {
        Debug.Log("Removing coin");
        _pool.Release(coinTransform);
    }

    private void CreateCoins()
    {
        for (int i = 0; i < _coinsNumber; i++)
        {
            _pool.Get();
        }
    }

    private void ActivateCoin(Transform obj)
    {
        obj.position = GenerateRandomPosition();
        obj.gameObject.SetActive(true);
    }

    private Vector2 GenerateRandomPosition()
    {
        return new Vector2(Random.Range(_minSpawnXPosition, _maxSpawnXPosition), _yPositionLimit);
    }
}
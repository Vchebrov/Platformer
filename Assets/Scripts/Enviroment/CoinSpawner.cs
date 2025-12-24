using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private int _coinsNumber = 5;
    [SerializeField] private float _yPositionLimit = -1f;
    [SerializeField] private Hero _hero;
    
    private ObjectPool<Coin> _pool;
    private float _minSpawnXPosition = -28f;
    private float _maxSpawnXPosition = 28f;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => InitiateCoin(),
            actionOnGet: (obj) => ActivateCoin(obj),
            actionOnRelease: (obj) => RemoveFromScene(obj),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: 5,
            maxSize: 5
        );
    }

    private void Start()
    {
        CreateCoins();
    }

    private Coin InitiateCoin()
    {
        Coin coin = Instantiate(_coinPrefab);
        coin.Collected += OnRemoveCoin;
        return coin;
    }

    private void RemoveFromScene(Coin obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnRemoveCoin(Coin coin)
    {
        _pool.Release(coin);
    }

    private void CreateCoins()
    {
        for (int i = 0; i < _coinsNumber; i++)
        {
            _pool.Get();
        }
    }

    private void ActivateCoin(Coin obj)
    {
        obj.transform.position = GenerateRandomPosition();
        obj.gameObject.SetActive(true);
    }

    private Vector2 GenerateRandomPosition()
    {
        return new Vector2(Random.Range(_minSpawnXPosition, _maxSpawnXPosition), _yPositionLimit);
    }
}
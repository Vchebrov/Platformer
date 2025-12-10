using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(AudioSource))]
public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform _coinPrefab;
    [SerializeField] private int _coinsNumber = 5;
    [SerializeField] private float _yPositionLimit = -1f;
    [SerializeField] private CoinCollector _collector;

    private ObjectPool<Transform> _pool;
    private AudioSource _audioSource;
    private float _minSpawnXPosition = -28f;
    private float _maxSpawnXPosition = 28f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _pool = new ObjectPool<Transform>(
            createFunc: () => FillByCoins(),
            actionOnGet: (obj) => InitializeGetAction(obj),
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

    private void OnEnable()
    {
        if (_collector != null)
        {
            _collector.Touched += OnRemoveCoin; 
        }
        else
        {
            Debug.LogError("CoinSpawner: _collector не задан в инспекторе!");
        }
    }

    private void OnDisable()
    {
        if (_collector != null)
        {
            _collector.Touched -= OnRemoveCoin; 
        }
    }

    private Transform FillByCoins()
    {
        Transform coin = Instantiate(_coinPrefab);

        return coin;
    }

    private void RemoveFromScene(Transform obj)
    {
        obj.gameObject.SetActive(false);
        _audioSource.PlayOneShot(_audioSource.clip);
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

    private void InitializeGetAction(Transform obj)
    {
        obj.position = InitiateCoinPosition();
        obj.gameObject.SetActive(true);
    }

    private Vector2 InitiateCoinPosition()
    {
        return new Vector2(Random.Range(_minSpawnXPosition, _maxSpawnXPosition), _yPositionLimit);
    }
}
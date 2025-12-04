using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(AudioSource))]
public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private int _coinsNumber = 5;
    [SerializeField] private float _yPositionLimit = -1f;

    private ObjectPool<GameObject> _pool;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _pool = new ObjectPool<GameObject>(
            createFunc: () => FillByCoins(),
            actionOnGet: (obj) => ActionOnGet(obj),
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

    private GameObject FillByCoins()
    {
        GameObject coin = Instantiate(_coinPrefab);
        CoinCollector monitor = coin.GetComponent<CoinCollector>();

        if (monitor != null)
        {
            monitor.Touched += OnRemoveCoin;
        }

        return coin;
    }

    private void RemoveFromScene(GameObject obj)
    {
        obj.SetActive(false);
        _audioSource.PlayOneShot(_audioSource.clip);
        CoinCollector monitor = obj.GetComponent<CoinCollector>();
        if (monitor != null)
        {
            monitor.Touched -= OnRemoveCoin;
        }
    }

    private void OnRemoveCoin(Transform transform)
    {
        Debug.Log("Removing coin");
        _pool.Release(transform.gameObject);
    }

    private void CreateCoins()
    {
        for (int i = 0; i < _coinsNumber; i++)
        {
            _pool.Get();
        }
    }

    private void ActionOnGet(GameObject obj)
    {
        obj.transform.position = InitiateCoinPosition();
        obj.SetActive(true);
    }

    private Vector2 InitiateCoinPosition()
    {
        return new Vector2(Random.Range(-28f, 28f), _yPositionLimit);
    }
}
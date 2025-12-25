using UnityEngine;
using UnityEngine.Pool;

public class MedicalKitSpawner : MonoBehaviour
{
    [SerializeField] private MedicalKit _medKitPrefab;
    [SerializeField] private int _kitNumber = 2;
    [SerializeField] private float _yPositionLimit = -3f;
    
    private ObjectPool<MedicalKit> _pool;
    private float _minSpawnXPosition = -28f;
    private float _maxSpawnXPosition = 28f;

    private void Awake()
    {
        _pool = new ObjectPool<MedicalKit>(
            createFunc: () => InitiateKit(),
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

    private MedicalKit InitiateKit()
    {
        MedicalKit medKit = Instantiate(_medKitPrefab);
        medKit.Collected += OnRemoveCoin;
        return medKit;
    }

    private void RemoveFromScene(MedicalKit medKit)
    {
        medKit.gameObject.SetActive(false);
        medKit.Collected -= OnRemoveCoin;
    }

    private void OnRemoveCoin(MedicalKit medKit)
    {
        _pool.Release(medKit);
    }

    private void CreateCoins()
    {
        for (int i = 0; i < _kitNumber; i++)
        {
            _pool.Get();
        }
    }

    private void ActivateCoin(MedicalKit obj)
    {
        obj.transform.position = GenerateRandomPosition();
        obj.gameObject.SetActive(true);
    }

    private Vector2 GenerateRandomPosition()
    {
        return new Vector2(Random.Range(_minSpawnXPosition, _maxSpawnXPosition), _yPositionLimit);
    }
}
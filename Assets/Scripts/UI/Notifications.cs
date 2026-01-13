using TMPro;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _finalNotice;
    [SerializeField] private Health _health;

    private void Awake()
    {
        _finalNotice.gameObject.SetActive(false);
    }
    
    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        _finalNotice.gameObject.SetActive(true);
    }
}

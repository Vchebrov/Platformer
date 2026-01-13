using TMPro;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _healthIndicator;

    
    private void OnEnable()
    {
        _health.InitialHealthSet += OnInitialHealthValue;
        _health.HealthChanged += OnUpdateHealthValue;
    }

    private void OnDisable()
    {
        _health.InitialHealthSet -= OnInitialHealthValue;
    }

    private void OnInitialHealthValue(float health)
    {
        _healthIndicator.text = health.ToString();
    }

    private void OnUpdateHealthValue(float health)
    {
        _healthIndicator.text = health.ToString();
    }
}

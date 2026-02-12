using TMPro;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _healthIndicator;

    
    private void OnEnable()
    {
        _health.InitialValueSet += OnInitialValueValue;
        _health.ValueChanged += OnUpdateValueValue;
    }

    private void OnDisable()
    {
        _health.InitialValueSet -= OnInitialValueValue;
        _health.ValueChanged -= OnUpdateValueValue;
    }

    private void OnInitialValueValue(float health)
    {
        _healthIndicator.text = health.ToString();
    }

    private void OnUpdateValueValue(float health)
    {
        _healthIndicator.text = health.ToString();
    }
}

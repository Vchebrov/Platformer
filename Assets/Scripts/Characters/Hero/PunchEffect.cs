
using UnityEngine;

public class PunchEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    public void Create()
    {
        var effect = Instantiate(_effect, transform.position, Quaternion.identity);
        effect.Play();
        Destroy(effect.gameObject, effect.main.duration);
    }
}

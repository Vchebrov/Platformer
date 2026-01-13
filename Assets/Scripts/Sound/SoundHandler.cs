using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _coinCollection;
    
    private WaitForSeconds _stepDelay;

    public void PlaySound(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
    }

    public void CollectCoin()
    {
        _audioSource.PlayOneShot(_coinCollection);
    }
}

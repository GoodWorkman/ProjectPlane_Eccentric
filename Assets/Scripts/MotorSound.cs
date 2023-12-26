using UnityEngine;

public class MotorSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    private PlayerHealth _playerHealth;
    
    private bool _isShouldPlaying = true;

    private void Awake()
    {
        _playerHealth = GetComponentInParent<PlayerHealth>();
    }

    private void Start()
    {
        _playerHealth.OnPlayerDie += StopSound;
    }

    private float _minSoundValue = .05f;
    private float _maxSoundValue = .3f;
    private float _soundChangeSpeed = 1f;
    private float _targetVolume;

    private void Update()
    {
        if (!_isShouldPlaying) return;
        
       _targetVolume = Mathf.Clamp(Input.GetAxis("Vertical"), _minSoundValue, _maxSoundValue);

       _audioSource.volume = Mathf.Lerp(_audioSource.volume, _targetVolume, _soundChangeSpeed * Time.deltaTime);
       
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        _playerHealth.OnPlayerDie -= StopSound;
    }

    private void StopSound()
    {
        _isShouldPlaying = false;
        
        Debug.Log("stop sound");
        
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}

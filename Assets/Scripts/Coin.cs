using System;
using UnityEngine;

public class Coin : MonoBehaviour, ISpawnable
{
    [SerializeField] private AudioSource _audioSource;
    
    public Action<Coin> OnCoinCollected;
    
    private bool _isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive) return;
        _isActive = false;
    
        if (other.attachedRigidbody.GetComponent<PlayerMover>())
        {
            OnCoinCollected?.Invoke(this);
            
            _audioSource.Play();
            
            Destroy(gameObject, _audioSource.clip.length);
        }
    }

    public GameObject CreateInstance(Vector3 position, Transform container)
    {
        Coin coin = Instantiate(this, position, Quaternion.identity, container);

        return coin.gameObject;
    }
}

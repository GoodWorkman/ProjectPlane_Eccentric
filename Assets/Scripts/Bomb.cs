using System;
using UnityEngine;

public class Bomb : MonoBehaviour, ISpawnable
{
    [SerializeField] private AudioSource _audioSource;
    
    public Action<Bomb> OnBombCollected;

    private bool _isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (_isActive == false)
        {
            return;
        }

        _isActive = false;
        
        if (other.attachedRigidbody.GetComponent<PlayerMover>())
        {
            OnBombCollected?.Invoke(this);
            _audioSource.Play();
            Destroy(gameObject, _audioSource.clip.length);
        }
    }

    public GameObject CreateInstance(Vector3 position, Transform container)
    {
        Bomb bomb = Instantiate(this, position, Quaternion.identity, container);

        return bomb.gameObject;
    }
}

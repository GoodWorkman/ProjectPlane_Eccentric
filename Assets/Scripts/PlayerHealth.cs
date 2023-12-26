using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Action OnPlayerDie;
    public Action<int> OnLifesChanged;
    
    private ObjectsCounter _objectsCounter;
    private int _lifes = 5;
    
    public int Lifes => _lifes;
    
    private void Awake()
    {
        _objectsCounter = FindObjectOfType<ObjectsCounter>();
    }

    private void Start()
    {
        _objectsCounter.OnBombDestroyed += ReduseLife;
    }

    private void ReduseLife()
    {
        _lifes--;
        
        OnLifesChanged?.Invoke(_lifes);

        if (_lifes == 0)
        {
            OnPlayerDie?.Invoke();
        }
    }

    private void Die()
    {
        
    }

    private void OnDestroy()
    {
        _objectsCounter.OnBombDestroyed -= ReduseLife;
    }
}

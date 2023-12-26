using TMPro;
using UnityEngine;

public class UIChecker : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private ObjectsCounter _objectsCounter;

    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _livesText;

    [SerializeField] private GameObject _winObject;
    [SerializeField] private GameObject _loseObject;

    [SerializeField] private AudioSource _winSound;
    [SerializeField] private AudioSource _loseSound;

    private void Awake()
    {
        _objectsCounter = FindObjectOfType<ObjectsCounter>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Start()
    {
        _objectsCounter.OnCoinsCountChanged += UpdateCoinsText;
        _objectsCounter.OnAllCoinsCollected += Win;

        _playerHealth.OnLifesChanged += UpdateLivesText;
        _playerHealth.OnPlayerDie += Lose;
        
        UpdateLivesText(_playerHealth.Lifes); // если так не сделать, то 42 строка только после собранной бомбы меняется
    }

    private void UpdateCoinsText(int remainingsCoins)
    {
        _coinsText.text = "Осталось собрать: " + remainingsCoins;
    }

    private void UpdateLivesText(int remainingsLives)
    {
        _livesText.text = "Жизней: " + remainingsLives; // почему-то если в инспекторе поле текста пустое, то текст с жизнями появляется только после 1 собранной бомбы

    }

    private void Win()
    {
        Time.timeScale = 0;
        
        _winSound.Play();
        _winObject.SetActive(true);
    }

    private void Lose()
    {
        Time.timeScale = 0;
        
        _loseSound.Play();
        _loseObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _objectsCounter.OnCoinsCountChanged -= UpdateCoinsText;
        _objectsCounter.OnAllCoinsCollected -= Win;

        _playerHealth.OnLifesChanged -= UpdateLivesText;
        _playerHealth.OnPlayerDie -= Lose;
    }
}

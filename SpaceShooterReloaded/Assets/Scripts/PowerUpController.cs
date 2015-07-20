using UnityEngine;
using System.Collections;

public class PowerUpController: MonoBehaviour
{

    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private GameObject _playerExplosion;
    [SerializeField]
    private int _scoreValue;
    [SerializeField] private int _powerUpDuration;
    private GameController _gameController;

    void Start()
    {
        var gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject == null) return;
        _gameController = gameControllerObject.GetComponent<GameController>();
        if (_gameController == null)
            Debug.Log("Cannot find 'GameController' Script");

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary")) return;
        Instantiate(_explosion, transform.position, transform.rotation);
        _gameController.Player.SetPowerUpState(_powerUpDuration); 
        if (other.CompareTag("Player"))
        {
            Instantiate(_playerExplosion, other.transform.position, other.transform.rotation);
            _gameController.GameOver();
        }

        _gameController.AddScore(_scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }


}

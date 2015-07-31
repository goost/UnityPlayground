using UnityEngine;
using System.Collections;

public class HazardController : MonoBehaviour
{

    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _playerExplosion;
    [SerializeField] private GameObject _explosionBig;
    [SerializeField] private int _scoreValue;
    private GameController _gameController;

    void Start()
    {
        if (Random.value < 0.1)
        {
            gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
            _scoreValue *= 2;
        }
        var gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject == null) return;
        _gameController = gameControllerObject.GetComponent<GameController>();
        if (_gameController == null)
            Debug.Log("Cannot find 'GameController' Script");

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary")) return;
        if (_scoreValue != 10)
            {
                Instantiate(_explosionBig, other.transform.position, other.transform.rotation);
            }
            else
            {
                Instantiate(_explosion, other.transform.position, other.transform.rotation);
            }

        if (other.CompareTag("Player"))
        {
            Instantiate(_playerExplosion, other.transform.position, other.transform.rotation);
            _gameController.GameOver();
        }
        if (other.CompareTag("Bolt"))
        {
            _gameController.AddScore(_scoreValue);
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

    
}

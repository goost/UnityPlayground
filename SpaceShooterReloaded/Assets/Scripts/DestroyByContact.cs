using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
    [SerializeField] private GameObject _playerExplosion;
    [SerializeField] private int _score;
    private GameController _gameController;
    [SerializeField] private GameObject _explosion;

    public void Start()
    {
        var gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject == null) return;
        _gameController = gameControllerObject.GetComponent<GameController>();
        if (_gameController == null)
            Debug.Log("Cannot find 'GameController' Script");
    }



    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary")|| other.CompareTag("Enemy") || other.CompareTag("EnemyBolt")) return; 
        if (other.CompareTag("Bolt"))
        {           
            _gameController.AddScore(_score);
        }
        if (other.CompareTag("Player"))
        {
            Instantiate(_playerExplosion, other.transform.position, other.transform.rotation);
            _gameController.GameOver();
        }
        if (_explosion != null)
            Instantiate(_explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestroyByBoundary : MonoBehaviour
{
    [SerializeField] private Text _passCounterText;
    [SerializeField] private int _passCounter;
    private GameController _gameController;

    void Start()
    {
        var gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject == null) return;
        _gameController = gameControllerObject.GetComponent<GameController>();
        if (_gameController == null)
            Debug.Log("Cannot find 'GameController' Script");
        UpdatePassCount();
    }

    public void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
        if (other.CompareTag("Bolt") || other.CompareTag("EnemyBolt")) return;
        _passCounter = _passCounter == 0 ? 0:_passCounter-1;
        UpdatePassCount();
        
    }


    private void UpdatePassCount()
    {
        _passCounterText.text =_passCounter+" Units still alive";
        GameOverCheck();
    }

    private void GameOverCheck()
    {
        if (_passCounter == 0)
        {
            _gameController.GameOver();
            
        }
    }
}

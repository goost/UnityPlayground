using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject[] _hazard;
    [SerializeField] private GameObject _powerUp;
    [SerializeField] private Vector3 _spawnValues;
    [SerializeField] private int _hazardCount;
    [SerializeField] private double _powerUpChance;
    [SerializeField] private float _spawnWait;
    [SerializeField] private float _startWait;
    [SerializeField] private float _waveWait;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _restartText;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private AudioMixerSnapshot _gameOverSnapshot;
    [SerializeField] private AudioMixerSnapshot _mainSnapshot;
    public PlayerController Player { get; private set; }

    private bool _gameOver;
    private bool _restart;

    private int _score;

    void Start()
    {
        _score = 0;
        _gameOver = false;
        _restart = false;
        _restartText.text = "";
        _gameOverText.text = "";
        _mainSnapshot.TransitionTo(0.01f);
        UpdateScore();
        InvokeRepeating("spawnHazard", _startWait, _spawnWait);
        Invoke("incrementVars", _hazardCount * _spawnWait + _startWait+1);
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (Player == null)
        {
            Debug.LogError("Cannot find PlayerController Script.");
        }
        // StartCoroutine(spawnWaves());
    }

    void Update()
    {
        if (_restart && Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    private IEnumerator spawnWaves()
    {   
        yield return new WaitForSeconds(_startWait);
        var spawnWait = _spawnWait;
        var hazardCount = _hazardCount;
        while (!_gameOver)
        {
            for (var i = 0; i < hazardCount; i++)
            {
                var spawnPosition = new Vector3(Random.Range(-_spawnValues.x, _spawnValues.x), 0, _spawnValues.z);
                var spawnRotation = Quaternion.identity;
                Instantiate(
                    Random.value < _powerUpChance ? 
                        _powerUp :
                        _hazard[Random.Range(0, _hazard.Length)], 
                    spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            spawnWait *= 0.9f;
            hazardCount = (int)(hazardCount * 1.5);
            yield return new WaitForSeconds(_waveWait);

            /*if (_gameOver)
            {
                _restartText.text = "Press 'R' for restart!";
                _restart = true;
                break;
            }*/
        }
    }

    private void incrementVars()
    {
        CancelInvoke("spawnHazard");
        _spawnWait *= 0.9f;
        _hazardCount = (int)(_hazardCount * 1.5);
        InvokeRepeating("spawnHazard", _waveWait, _spawnWait);
        Invoke("incrementVars", _hazardCount * _spawnWait + _waveWait+1);
        
    }

    private void spawnHazard()
    {
            var spawnPosition = new Vector3(Random.Range(-_spawnValues.x, _spawnValues.x), 0, _spawnValues.z);
            var spawnRotation = Quaternion.identity;
            Instantiate(
                    Random.value < _powerUpChance ?
                        _powerUp :
                        _hazard[Random.Range(0, _hazard.Length)],
                    spawnPosition, spawnRotation);
    }

    public void GameOver()
    {
        CancelInvoke("spawnHazard");
        CancelInvoke("incrementVars");
        Player.SetGameOverState();
        _gameOverSnapshot.TransitionTo(2.5f);
        _gameOverText.text = "Game Over!";
        _gameOver = true;
        _restartText.text = "Press 'R' for Restart!";
        _restart = true;
    }

    public void AddScore(int ns)
    {
        _score += ns;
        UpdateScore();
    }

    private void UpdateScore()
    {
        _scoreText.text = "Score: " + _score;
    }

}

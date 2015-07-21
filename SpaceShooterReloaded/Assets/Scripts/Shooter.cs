using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
    [SerializeField]
    private Transform _shotSpawn;
    [SerializeField]
    private Transform _shotSpawnLeft;
    [SerializeField]
    private Transform _shotSpawnRight;

    [SerializeField] private GameObject _shot;
	// Use this for initialization
	void Start ()
	{
	    InvokeRepeating(Random.value < 0.2 ? "ShootTwo" : "ShootOne", 1, 2);
	}

    private void ShootOne()
	{
        Instantiate(_shot, _shotSpawn.position, _shotSpawn.rotation);
	}
    
    private void ShootTwo()
	{
        Instantiate(_shot, _shotSpawnRight.position, _shotSpawnRight.rotation);
        Instantiate(_shot, _shotSpawnLeft.position, _shotSpawnLeft.rotation);
	}
}

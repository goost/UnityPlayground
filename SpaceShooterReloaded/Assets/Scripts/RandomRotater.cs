using UnityEngine;
using System.Collections;

public class RandomRotater : MonoBehaviour
{

    [SerializeField] private float _tumble;
	// Use this for initialization
	void Start ()
	{
	    GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere*_tumble;
	}
	
}

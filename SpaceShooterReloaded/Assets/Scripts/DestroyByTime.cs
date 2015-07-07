using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{

    [SerializeField] private float _lifeTime;
	// Use this for initialization
	void Start () {
	    Destroy(gameObject, _lifeTime);
	}
	
}

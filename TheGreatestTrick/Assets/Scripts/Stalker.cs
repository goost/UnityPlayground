using UnityEngine;
using System.Collections;

public class Stalker : MonoBehaviour {

    //Stalker Camera should focus on target, e.a. target should always be in center
    [SerializeField] private Transform _target;
	
	void Update () {
        transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
	}
}

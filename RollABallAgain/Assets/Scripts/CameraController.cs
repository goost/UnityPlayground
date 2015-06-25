using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject Player;
	private Vector3 _offset;

	// Use this for initialization
	void Start () {
		_offset = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = Player.transform.position + _offset;
	}
}

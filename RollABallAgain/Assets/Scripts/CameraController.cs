using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject _player;
	private Vector3 _offset;

	// Use this for initialization
	void Start () {
		_offset = transform.position - _player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = _player.transform.position + _offset;
	}
}

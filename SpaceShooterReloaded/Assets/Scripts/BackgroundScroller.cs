using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour
{

    [SerializeField] private float _scrollSpeed ;
    [SerializeField] private int _tileSizeZ;
    private Vector3 _startPosition;
	// Use this for initialization
	void Start ()
	{
	    _startPosition = transform.position;
	}
	
	// Update is called once per frame
    private void Update()
    {
        var newPosition = Mathf.Repeat(Time.time*_scrollSpeed, _tileSizeZ);
        transform.position = _startPosition + Vector3.forward*newPosition;
    }
}


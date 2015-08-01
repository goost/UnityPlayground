using UnityEngine;
using System.Collections;
using System.Net.Sockets;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothing = 5f;
    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - _target.position;
    }

    void FixedUpdate()
    {
        var targetCam = _target.position + _offset;
        transform.position = Vector3.Slerp(transform.position, targetCam,_smoothing*Time.deltaTime); 
    }


}

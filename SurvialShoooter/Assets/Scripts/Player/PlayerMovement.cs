using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed = 6f;
    Vector3 _movement;
    Animator _anim;
    Rigidbody _rb;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        _movement.Set(h, 0, v);
        _movement = _movement.normalized*_speed*Time.deltaTime;
       _rb.MovePosition(transform.position + _movement); 
    }

    void Turning()
    {
        var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (!Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) return;
        var playerToMouse = floorHit.point - transform.position;
        playerToMouse.y = 0;
        var newRotation = Quaternion.LookRotation(playerToMouse);
        _rb.MoveRotation(newRotation);
    }

    void Animating(float h, float v)
    {
        var walking = h != 0f || v != 0f;
        _anim.SetBool("IsWalking", walking);
    }
}

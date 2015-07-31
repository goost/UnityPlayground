using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float ZMin;
    public float ZMax;
    public float XMin;
    public float XMax;
}

public class PlayerController : MonoBehaviour
{
    private enum State {Normal, Powerup, GameOver}
    public float Speed;
    public float Tilt;
    public Boundary Boundary;
    public GameObject Shot;
    public float FireRate;

    private float _nextFire;
    [SerializeField]
    private Transform _shotSpawn;
    [SerializeField]
    private Transform _shotSpawnLeft;
    [SerializeField]
    private Transform _shotSpawnRight;

    private State _state;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _state = State.Normal;
    }

    void Update()
    {
        
        if (Input.GetButton("Fire1") && Time.time > _nextFire )
        {
            switch (_state)
            {
                case State.Normal:
                    Instantiate(Shot, _shotSpawn.position, _shotSpawn.rotation);
                    break;
                case State.Powerup:
                    Instantiate(Shot, _shotSpawnLeft.position, _shotSpawnLeft.rotation);
                    Instantiate(Shot, _shotSpawnRight.position, _shotSpawnRight.rotation);
                   // Instantiate(Shot, new Vector3(_shotSpawn.position.x, _shotSpawn.position.y, _shotSpawn.position.z + 2), _shotSpawn.rotation);
                    break;
                case State.GameOver:
                    break;
            }
            _nextFire = Time.time + FireRate;
        }
    }

    void FixedUpdate()
    {
        if (_state == State.GameOver) return;
        var moveHorrizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        _rigidbody.velocity = new Vector3(moveHorrizontal * Speed, 0f, moveVertical * Speed);
        _rigidbody.position = new Vector3
            (
                Mathf.Clamp(_rigidbody.position.x, Boundary.XMin, Boundary.XMax),
                0f,
                Mathf.Clamp(_rigidbody.position.z, Boundary.ZMin, Boundary.ZMax)
            );
        _rigidbody.rotation = Quaternion.Euler(0f, 0f, _rigidbody.velocity.x * -Tilt);
    }

    private void SetNormalState()
    {
        _state = State.Normal;
    }
    public void SetPowerUpState(int duration)
    {
        CancelInvoke("SetNormalState");
        _state = State.Powerup;
        Invoke("SetNormalState", duration);
    }

    public void SetGameOverState()
    {   
        _rigidbody.Sleep();
        _state = State.GameOver;
    }
}

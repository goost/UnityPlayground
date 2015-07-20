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
    private enum State {NORMAL, POWERUP}
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
    private AudioSource _audio;
    private Rigidbody _rigidbody;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _state = State.NORMAL;
    }

    void Update()
    {
        
        if (Input.GetButton("Fire1") && Time.time > _nextFire )
        {
            switch (_state)
            {
                case State.NORMAL:
                    Instantiate(Shot, _shotSpawn.position, _shotSpawn.rotation);
                    break;
                case State.POWERUP:
                    Instantiate(Shot, _shotSpawnLeft.position, _shotSpawnLeft.rotation);
                    Instantiate(Shot, _shotSpawnRight.position, _shotSpawnRight.rotation);
                   // Instantiate(Shot, new Vector3(_shotSpawn.position.x, _shotSpawn.position.y, _shotSpawn.position.z + 2), _shotSpawn.rotation);
                    break;
            }
            _nextFire = Time.time + FireRate;
            
            _audio.Play();
        }
    }

    void FixedUpdate()
    {
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
        _state = State.NORMAL;
    }
    public void SetPowerUpState(int duration)
    {
        CancelInvoke("SetNormalState");
        _state = State.POWERUP;
        Invoke("SetNormalState", duration);
    }
}

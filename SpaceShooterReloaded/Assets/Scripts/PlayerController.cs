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
    public float Speed;
    public float Tilt;
    public Boundary Boundary;
    public GameObject Shot;
    public float FireRate;

    private float _nextFire;
    [SerializeField]
    private Transform _shotSpawn;

    void Update()
    {
        
        if (Input.GetButton("Fire1") && Time.time > _nextFire )
        {
            _nextFire = Time.time + FireRate;
            Instantiate(Shot, _shotSpawn.position, _shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }

    void FixedUpdate()
    {
        var moveHorrizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        GetComponent<Rigidbody>().velocity = new Vector3(moveHorrizontal* Speed,0f,moveVertical*Speed);
        GetComponent<Rigidbody>().position  = new Vector3
            (
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, Boundary.XMin, Boundary.XMax),
                0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, Boundary.ZMin, Boundary.ZMax)
            );
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0f, 0f, GetComponent<Rigidbody>().velocity.x*-Tilt);
    }
}

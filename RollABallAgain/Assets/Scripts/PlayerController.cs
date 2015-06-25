using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float Speed;
    public Text CounText;
    public Text WinText;

	private Rigidbody _rb;
    private int _count;
    private const float JumpSpeed = 6.0f;

    void Start()
	{
	    _count = 0;
		_rb = GetComponent<Rigidbody> ();
	    SetCountText();
	    WinText.text = "";
	}
	void FixedUpdate () {
		var moveHorizontal = Input.GetAxis ("Horizontal");
		var moveVertical = Input.GetAxis ("Vertical");
        var velocity = new Vector3();

        if (Input.GetButtonDown("Jump")  && IsOnGround())
	    {
           // Debug.Log("Should Jump");
	       _rb.velocity =  new Vector3(0, JumpSpeed, 0);
	       // velocity.y = JumpSpeed;
	    }

        var movement = new Vector3(moveHorizontal, 0, moveVertical);
	    //velocity.x = moveHorizontal * Speed;
	    //velocity.z = moveVertical * Speed;
	    //_rb.velocity = velocity;
	    _rb.AddForce(movement * Speed);
	    //_rb.velocity = movement*Speed;
	}

    private bool IsOnGround()
    {
        //Debug.Log("Is on Ground Test");
        return Physics.Raycast(transform.position, -Vector3.up, .6f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("PickUp")) return;
        other.gameObject.GetComponent<AudioSource>().Play();
        other.gameObject.SetActive(false);
        _count++;
        SetCountText();
        Destroy(other.gameObject);
    }

    void SetCountText()
    {
        CounText.text = "Count: " + _count;
        if (_count >= 16)
        {
            WinText.text = "You WIN!";
        }
    }
}

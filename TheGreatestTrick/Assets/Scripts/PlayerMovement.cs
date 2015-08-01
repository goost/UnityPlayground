using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //Enum for player state machine
    //NOTE(goost) Overkill, a few simple bools should be sufficient
    private enum PositionState { GROUND,AIR,AIR_DOUBLE}

    //const values (Animator Strings, Tags, etc)
    private const string IsOnGround = "IsOnGround";
    private const string Movement = "Movement";
    private const string HorizontalAxis = "Horizontal";


    //Setable in Editor
    [SerializeField] private float _movementVelocity;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _overlapRadius;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundMask;
    
    private PositionState _posState;
    private Rigidbody2D _rb;
    private AudioSource _jumpEffect;
    private Animator _anim;
    private bool _isFacingRight;
    private bool _isGrounded;
    

    //Awaks to init oneself, Start to setup links
	void Awake ()
	{
	    _rb = GetComponent<Rigidbody2D>();
	    _anim = GetComponent<Animator>();
	    _jumpEffect = GetComponent<AudioSource>();
        _posState = PositionState.GROUND;
        _isFacingRight = true;
	}
	
	// FixedUpdate -> movement is physic dependend
	void FixedUpdate ()
	{
	    GroundControl();

        //NOTE(goost) Or GetAxis()?
	    var h = Input.GetAxisRaw(HorizontalAxis);
        _rb.velocity = new Vector2(h*_movementVelocity, _rb.velocity.y);
        _anim.SetFloat(Movement, Mathf.Abs(h));

        if (_isFacingRight && h < 0)
	        FlipSides();
        else if (!_isFacingRight && h > 0)
            FlipSides();
	}

    private void GroundControl()
    {
        //Check a small area around the players foot, if there are any collision
        //if true, then we are grounded
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _overlapRadius, _groundMask);
        _anim.SetBool(IsOnGround, _isGrounded);
        if (_isGrounded)
        {
            _posState = PositionState.GROUND;
        }
        else
        {
            //NOTE(goost) One more reason why state machine is maybe overkill....
            _posState = _posState == PositionState.AIR_DOUBLE ? _posState : PositionState.AIR;
        }
    }

    //Checking for continous input is fine in FixedUpdate, but
    //one-time events could get missed -> check jump here
    void Update()
    {
        if (!Input.GetButtonDown("Jump")) return;
        switch (_posState)
        {
            case PositionState.AIR_DOUBLE:
                break;
            case PositionState.AIR:
                Jump();
                _posState = PositionState.AIR_DOUBLE;
                break;
            case PositionState.GROUND:
                Jump();
                _posState = PositionState.AIR;
                break;
        }
    }

    //Flips Sprite, essentially mirrors it
    private void FlipSides()
    {
        _isFacingRight = !_isFacingRight;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Jump()
    {
        _jumpEffect.Play();
        _anim.SetBool(IsOnGround, false);
        _rb.AddForce(new Vector2(0, _jumpForce));
        Logger.Log(Debug.Log, "Player Should Jump");
    }
}

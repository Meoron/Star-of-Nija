using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0F;
    [SerializeField]
    private float _jumpForce = 5.0F;
    [SerializeField]
    private int _ExtraJump = 1;
    [SerializeField]
    private string folderEventSoundStepFMOD;

    private DamageableController _damageableController;
    private Animator _unitAnimator;
    private SurfaceCheck _surfaceCheck;
    protected new Rigidbody2D rigidbody;
    protected Transform unitTransform;

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public float JumpForce
    {
        get { return _jumpForce; }
        set { _jumpForce = value; }
    }

    public Animator UnitAnimator
    {
        set { _unitAnimator = value; }
        get { return _unitAnimator; }
    }

    public DamageableController DamageableController
    { get { return _damageableController; } }

    protected SurfaceCheck SurfaceChech
    { get { return _surfaceCheck; } }

    public Transform UnitTransform
    { get { return unitTransform; } }


    private void Awake()
    {
        _surfaceCheck = gameObject.GetComponent<SurfaceCheck>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        UnitAnimator = gameObject.GetComponent<Animator>();
        unitTransform = gameObject.GetComponent<Transform>();
        _damageableController = gameObject.GetComponent<DamageableController>();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public virtual void Moving(float xAxisDirection, float speed)
    {
        Vector3 direction = transform.right * xAxisDirection;
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, Time.deltaTime * speed);
    }

    public virtual void Jump()
    {
        if (_surfaceCheck.OnGround == true)
            _ExtraJump = 1;

        if (_ExtraJump > 0)
        {
            rigidbody.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
            _ExtraJump--;
        }
    }

    public void RollUnit(float tagetPositionX)
    {
        if (tagetPositionX / transform.localScale.x < 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    protected void PlaySoudOfStep()
    {
        FMODUnity.RuntimeManager.PlayOneShot(folderEventSoundStepFMOD);
    }

   
}



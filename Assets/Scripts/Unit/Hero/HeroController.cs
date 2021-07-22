using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(UserController))]
[RequireComponent(typeof(SurfaceCheck))]
[RequireComponent(typeof(DamageableController))]
[RequireComponent(typeof(WeaponSelectionController))]
public class HeroController : Unit,  IMovable, ILeaping
{
    [SerializeField]
    protected float _speed = 1.0F;
    [SerializeField]
    private float _jumpForce = 5.0F;
    [SerializeField]
    private int _ExtraJump = 1;

    private Animator _unitAnimator;
    private Rigidbody2D _rigidbody;
    private SurfaceCheck _surfaceCheck;
    private float _horizontalAccelerationOfStrafe = 0.05f;

    private AimOnGUI aimPosition;
    private GameObject _aim;
   
    //public Vector3 MousePositionOnCamera { set { _mousePositionOnCamera = value; } }

    public float Speed
    { get { return _speed; } }

    public Animator UnitAnimator
    { get { return _unitAnimator; } }


    private void Start()
    {
        _aim = GameObject.Find("Aim");
        _rigidbody = GetComponent<Rigidbody2D>();
        _unitAnimator = GetComponent<Animator>();
        _surfaceCheck = GetComponent<SurfaceCheck>();
        aimPosition = _aim.GetComponent<AimOnGUI>();
    }

    private void FixedUpdate()
    {
        Strafe(Input.GetAxis("Horizontal"),_speed, _horizontalAccelerationOfStrafe);
        RollUnit(GetDirectionMouseOnCameraAboutPlayer());
    }

    public void MovingInUserDirection(float xAxisDirection)
    {
        Vector3 direction = transform.right * xAxisDirection;
        Moving(direction);
    }

    public void Jump()
    {
        if (_surfaceCheck.OnGround == true)
            _ExtraJump = 1;

        if (_ExtraJump > 0)
        {
            _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            _ExtraJump--;
        }
    }

    public void Crouch(float horizontalAxis)
    {
        _rigidbody.velocity = transform.right * horizontalAxis * _speed/2;
    }

    public void Strafe(float horizontalAxis, float speed, float horizontalAccelerationOfStrafe)
    {
        _rigidbody.AddForce(transform.right * horizontalAxis * horizontalAccelerationOfStrafe, ForceMode2D.Impulse);
    }

    public void SetStateMovingAnimation(float horizontalAxis, float verticalAxis)
    {
        if (_surfaceCheck.OnGround==false)
            verticalAxis = 1f;
        horizontalAxis = horizontalAxis * GetDirectionMouseOnCameraAboutPlayer();
        _unitAnimator.SetFloat("horizontalStateMoving", horizontalAxis);
        _unitAnimator.SetFloat("verticalStateMoving", verticalAxis);
    }

    public IEnumerator StartPlayingReloadHeroAnimation()
    {
        float timeHeroReloadAnimation = 1.5f;
        _unitAnimator.SetBool("ActiavationRealoadWeaponAnimation", true); //Value true activate hero weapon animation
        yield return new WaitForSeconds(timeHeroReloadAnimation);
        _unitAnimator.SetBool("ActiavationRealoadWeaponAnimation", false);
    }

    private float GetDirectionMouseOnCameraAboutPlayer()
    {
        return transform.position.x < aimPosition.MouseWorldPosition.x ? 1 : -1;
    }
    public void Moving(Vector3 direction)
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, Time.deltaTime * _speed);
    }
}

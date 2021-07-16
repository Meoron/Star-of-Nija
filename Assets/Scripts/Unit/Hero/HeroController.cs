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
public class HeroController : Unit
{
    private float horizontalAccelerationOfStrafe = 0.05f;
    private Vector3 mousePositionOnCamera;

    private AimOnGUI aimPosition;
    private GameObject Aim;
   
    public Vector3 MousePositionOnCamera { set { mousePositionOnCamera = value; } }



    private void Start()
    { 
        Aim = GameObject.Find("Aim");
        aimPosition = Aim.GetComponent<AimOnGUI>();
    }

    private void FixedUpdate()
    {
        Strafe(Input.GetAxis("Horizontal"),Speed, horizontalAccelerationOfStrafe);
    }
   


    public void Crouch(float horizontalAxis)
    {
        rigidbody.velocity = transform.right * horizontalAxis * Speed/2;
    }

    public void Strafe(float horizontalAxis, float speed, float horizontalAccelerationOfStrafe)
    {
        rigidbody.AddForce(transform.right * horizontalAxis * horizontalAccelerationOfStrafe, ForceMode2D.Impulse);
    }

   

    public void SetStateAnimation(float horizontalAxis, float verticalAxis)
    {
        if (SurfaceChech.OnGround==false)
            verticalAxis = 1f;
        horizontalAxis = horizontalAxis * (mousePositionOnCamera.x / Math.Abs(mousePositionOnCamera.x));
        UnitAnimator.SetFloat("horizontalStateMoving", horizontalAxis);
        if(verticalAxis<=0)
            UnitAnimator.SetFloat("verticalStateMoving", verticalAxis);
    }

    public IEnumerator StartPlayingReloadHeroAnimation()
    {
        float timeHeroReloadAnimation = 1.5f;
        UnitAnimator.SetBool("ActiavationRealoadWeaponAnimation", true); //Value true activate hero weapon animation
        yield return new WaitForSeconds(timeHeroReloadAnimation);  
        UnitAnimator.SetBool("ActiavationRealoadWeaponAnimation", false);
    }
}

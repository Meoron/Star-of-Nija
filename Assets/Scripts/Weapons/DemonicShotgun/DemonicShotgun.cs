using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class DemonicShotgun : Weapon
{
    [SerializeField]
    private ParticleSystem _catridgeParticlePrefab;
    
    private bool _canShot= true;
    private float _timeOfReaload = 0.8f;
    private float _timeBeforeStartReloadWeaponAnimation = 0.3f;

    private void Start()
    {
        weaponAnimator.SetFloat("stateReload", 0);
    }

    private void FixedUpdate()
    {
        StartShooting(ref HeroSelectionWeaponController.AmmoWeapons[WeaponSlot]);
    }

    protected override void StartShooting(ref int ammo)
    {
        if (Input.GetButton("Fire1") && !Input.GetButton("Fire2") && _canShot == true && ammo>0)
        {
            Shoot(CalculationDelayTimeBetweenShots(fireRate), ref ammo, reduceNumberBulletsPerShot);
            if (ammo % 2 == 0)
            {
                StartCoroutine(PlayingWeaponReloadAnimation());
            }
        }

        if (Input.GetButton("Fire2") && !Input.GetButton("Fire1") && _canShot == true && ammo > 0)
        {
            AlternativeShoot(CalculationDelayTimeBetweenShots(alternativeFireRate), ref ammo, alternativeReduceNumberBulletsPerShot);
            StartCoroutine(PlayingWeaponReloadAnimation());
        }
    }



    private IEnumerator PlayingWeaponReloadAnimation()
    {
        _canShot = false;
        PlayingHeroReloadAnimation();
        yield return new WaitForSeconds(_timeBeforeStartReloadWeaponAnimation);
        weaponAnimator.SetFloat("stateReload", 1);
        yield return new WaitForSeconds(_timeOfReaload);
        weaponAnimator.SetFloat("stateReload", 0);
        yield return new WaitForSeconds(0.38f);
        _canShot = true;
    }
}



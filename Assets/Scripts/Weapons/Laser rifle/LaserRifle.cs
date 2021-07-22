using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserRifle : Weapon
{
    [SerializeField]
    private float timeBeforeAlternativeAttack = 1f;
    [SerializeField]
    private GameObject laserVFX;
    private float stopwatch;


    private void FixedUpdate()
    {
        StartShooting(ref HeroSelectionWeaponController.AmmoWeapons[WeaponSlot]);
    }

    protected override void StartShooting(ref int ammo)
    {
        if (Input.GetButton("Fire1") && !Input.GetButton("Fire2") && ammo > 0)
        {
            Shoot(CalculationDelayTimeBetweenShots(fireRate), reduceNumberBulletsPerShot);
        }

        if (Input.GetButton("Fire2") && !Input.GetButton("Fire1") && ammo > 0)
        {
            stopwatch += Time.deltaTime;
            if (stopwatch >= timeBeforeAlternativeAttack)
            {
                laserVFX.SetActive(true);
                AlternativeShoot(CalculationDelayTimeBetweenShots(alternativeFireRate), alternativeReduceNumberBulletsPerShot);
            }
        }

        else
        {
            stopwatch = 0;
            laserVFX.SetActive(false);
        }
    }

    protected override void AlternativeShoot(float alternativeFireRate, int reduceNumberAmmoPerShot)
    {
        Shot(alternativeBulletPrefab, alternativeNumberOfBullets, alternativeBulletScatter, alternativeFireRate, actionAfterAlternativeShot, alternatineFolderEventSoundShot);
        HeroSelectionWeaponController.ChangeСurrentValueAmmo(-reduceNumberAmmoPerShot, WeaponSlot);
        if (HeroSelectionWeaponController.AmmoWeapons[WeaponSlot] <= 0)
            laserVFX.SetActive(false);
    }
}


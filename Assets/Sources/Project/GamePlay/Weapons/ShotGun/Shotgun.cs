using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shotgun : Weapon
{
    private void FixedUpdate()
    {
        StartShooting(ref HeroSelectionWeaponController.AmmoWeapons[WeaponSlot]);
    }
}

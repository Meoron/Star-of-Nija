using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFG : Weapon
{
    private void FixedUpdate()
    {
        StartShooting(ref HeroSelectionWeaponController.AmmoWeapons[WeaponSlot]);
    }
}

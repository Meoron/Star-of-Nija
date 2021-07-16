using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticalRifle : Weapon
{
    private void FixedUpdate()
    {
        StartShooting(ref HeroSelectionWeaponController.AmmoWeapons[WeaponSlot]);
    }
}

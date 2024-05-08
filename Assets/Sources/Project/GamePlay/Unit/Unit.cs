using System.Collections;
using UnityEngine;

interface IAttackedMelee{
	IEnumerator MeleeAttack();
}

interface IAttackedRange{
	IEnumerator RangeAttack();
}

interface IMovable{
	void Moving(Vector3 Direction);
}

interface ILeaping{
	void Jump();
}
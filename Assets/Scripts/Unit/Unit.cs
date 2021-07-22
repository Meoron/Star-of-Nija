using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IHaveAIUnit
{
    void SetAIUnit();
    void FollowToPlayer();
}

interface IAttackedMelee
{
    IEnumerator MeleeAttack();
}

interface IAttackedRange
{
    IEnumerator RangeAttack();
}

interface IMovable
{
    void Moving(Vector3 Direction);
}

interface ILeaping
{
    void Jump();
}

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    protected string folderEventSoundStepFMOD;



    protected void RollUnit(float tagetPositionX)
    {
        if (tagetPositionX / transform.localScale.x < 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    protected void PlaySoudOfStep()
    {
        FMODUnity.RuntimeManager.PlayOneShot(folderEventSoundStepFMOD);
    }  
}



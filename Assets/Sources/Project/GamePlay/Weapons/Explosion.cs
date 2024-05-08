using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DamageController))]
public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float radius=1f;
    [SerializeField]
    private bool drawRadiusExplosion=false;
    [SerializeField]
    private float force=1f;
    [SerializeField]
    private UnityEvent actionAfterExplosion;
    // Update is called once per frame

    private void Start()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(Collider2D objCollider in colliders)
        {
            Rigidbody2D objRigidbody = objCollider.GetComponent<Rigidbody2D>();
            if(objRigidbody!=null)
            objRigidbody.AddExplosionForce(force, this.transform.position, radius);
        }
        actionAfterExplosion.Invoke();
    }

    

    private void OnDrawGizmos()
    {
        if(drawRadiusExplosion==true)
        Gizmos.DrawSphere(transform.position, radius);
    }
}

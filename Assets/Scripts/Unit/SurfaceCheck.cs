using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceCheck : MonoBehaviour
{
    [Tooltip("The Layers which represent gameobjects that the Character Controller can be grounded on.")]
    public LayerMask groundedLayerMask;
    [Tooltip("The Layers which represent gameobjects that the Character Controller can be grounded on.")]
    public LayerMask wallLayerMask;
    [SerializeField]
    private float groundCheckerPosition = 0f;
    [SerializeField]
    private float wallCheckerPosition = 0f;
    [SerializeField]
    private float groundCheckerRadius = 1.2f;
    [SerializeField]
    private float wallCheckerRadius = 1.2f;
    [SerializeField]
    private bool EnableDrawingCheckerRadius = true;
    
    //for result
    private bool onGround;
    private bool onWall;

    public bool OnGround
    {
        set {onGround=value;}
        get { return onGround; }
    }
    public bool OnWall
    {
        set { onWall = value; }
        get { return onWall; }
    }
    //
    private GameObject unit;
    private Transform unitTransform; 

    private void Awake()
    {
        OnGround = false;
        OnWall = false;
        unitTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        CheckGroun(unitTransform, groundCheckerPosition, wallCheckerPosition, groundCheckerRadius, wallCheckerRadius, groundedLayerMask, wallLayerMask);
    }

    protected void CheckGroun(Transform unitTransform, float lowerPointOfUnit, float upperPointOfUnit, float groundCheckerRadius, 
                              float wallCheckerRadius, LayerMask groundLayer, LayerMask wallLayer)
    {
        Vector2 gorundCheckerPosition = new Vector2(unitTransform.position.x, unitTransform.position.y - lowerPointOfUnit);
        OnGround = Physics2D.OverlapCircle(gorundCheckerPosition, groundCheckerRadius, groundLayer);

        Vector2 wallCheckerPosition = new Vector2(unitTransform.position.x, unitTransform.position.y + upperPointOfUnit);
        OnWall = Physics2D.OverlapCircle(wallCheckerPosition, wallCheckerRadius, wallLayer);

        if (EnableDrawingCheckerRadius)
        {
            Debug.DrawRay(gorundCheckerPosition, -Vector2.up * groundCheckerRadius, Color.green);
            Debug.DrawRay(wallCheckerPosition, Vector2.right * wallCheckerRadius, Color.blue);
        }
    }
}
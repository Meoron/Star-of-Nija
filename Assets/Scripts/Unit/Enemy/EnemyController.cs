using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DamageableController))]
[RequireComponent(typeof(Collider2D))]
public class EnemyController : Unit
{

    //[SerializeField]
    //private Transform transformPlayer = null;
    [SerializeField]
    private float distanceToPlayer;
    [SerializeField]
    private float viewRadius = 7f;
    [SerializeField]
    private float distanceToMeleeAttack = 3f;
    [SerializeField]
    private float distanceToRangeAttack = 3f;

    [SerializeField]
    private bool isHaveMeleeAttack = false;
    

    private Vector3 direction;
    private Vector3 firstPosition;
    private float currentSpeed = 0;
    private float acceleration = 0.1f;
    private GameObject player;
    private DamageableController playerHealth;
    private EnemyMeleeController meleeController;
    private bool stateFollowingToPlayer = false;
    private bool statePatrol = false;
    public bool canMoving = true;
    public bool canHit = true;
    

    public float DistanceToPlayer {get{return distanceToPlayer;}}
    public float DistanceToMeleeAttack {get{ return distanceToMeleeAttack; } }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        UnitAnimator = GetComponent<Animator>();
        player = GameObject.Find("Doomslayer");
        playerHealth = player.GetComponent<DamageableController>();
        if (isHaveMeleeAttack == true)
            meleeController = GetComponent<EnemyMeleeController>();


    }

    private void FixedUpdate()
    {
        SetStateAnimotion(currentSpeed);
        LookingForPlayer();
        if (stateFollowingToPlayer == false)
        StartCoroutine("Patrol");
        if (stateFollowingToPlayer == true && canMoving == true )
            MovingToPlayer();
        if (isHaveMeleeAttack == true && canHit == true && playerHealth.CurrentHealth > 0)
        {
            meleeController.StartCoroutine("MeleeAttack");
        }
    }

    // Update is called once per frame
    void Update()
    {
        DistanceDetermination(); 
    }

   
    private void MovingToPlayer()
    {  
        direction = player.transform.position - transform.position; //Vector to player
        if (distanceToPlayer > distanceToMeleeAttack)
        {
            Walk(direction.normalized.x);
        }
        else
            currentSpeed = Mathf.Lerp(currentSpeed, 0, acceleration);
    }

    private void Patrol()
    {
        if (statePatrol == false) // Set direction for patrol
        {
            again:                                      
            direction.x = Random.Range(-1, 2);          
            if (direction.x == 0)
                goto again;
            StartCoroutine("CanPatrol");
            statePatrol = true;
        }
        //UnitAnimator._speed = 0.5f;
        if(statePatrol == true && direction.x != 0)
        Walk(direction.normalized.x);               //Movement in a given direction 
        else
           currentSpeed = Mathf.Lerp(currentSpeed, 0, acceleration);    //Smooth stop
    }

    private IEnumerator CanPatrol() //Delay for more realistic behavior
    {
        yield return new WaitForSeconds(Random.Range(2, 5));
        direction.x = 0;
        yield return new WaitForSeconds(Random.Range(0, 6));
        statePatrol = false;
    }

    private void Walk(float direction)
    {
        currentSpeed = Mathf.Lerp(currentSpeed, Speed, acceleration);
        Moving(direction, currentSpeed);
        RollUnit(direction);
    }

    private void LookingForPlayer()
    {
        if (distanceToPlayer <= viewRadius)
        {
            stateFollowingToPlayer = true;
        }
        else
        {
            stateFollowingToPlayer = false;
        }
    }

    private IEnumerator RangeAttack()
    {
        yield return new WaitForSeconds(1f);
    }

    public void SetStateAnimotion(float MovingStateAnimation)
    {
        UnitAnimator.SetFloat("AnimationMovingBlendTree", MovingStateAnimation);
    }

    private void DistanceDetermination()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
    }
}

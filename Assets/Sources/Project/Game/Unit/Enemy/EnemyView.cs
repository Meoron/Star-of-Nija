using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DamageableController))]
[RequireComponent(typeof(Collider2D))]
public class EnemyView : MonoBehaviour{
	[SerializeField] protected float viewRadius = 7f;

	protected float distanceToPlayer;
	protected string stateAI;
	protected Vector3 direction;
	protected float currentSpeed = 0;
	protected float acceleration = 0.1f;
	protected static GameObject player;
	protected Animator _unitAnimator;
	protected Rigidbody2D _rigidbody;

	public Animator UnitAnimator{
		get{ return _unitAnimator; }
	}

	public float DistanceToPlayer{
		get{ return distanceToPlayer; }
	}

	// Start is called before the first frame update
	void Awake(){
		_rigidbody = gameObject.GetComponent<Rigidbody2D>();
		_unitAnimator = gameObject.GetComponent<Animator>();
	}

	public void SetStateAnimotion(float MovingStateAnimation){
		_unitAnimator.SetFloat("AnimationMovingBlendTree", MovingStateAnimation);
	}
}
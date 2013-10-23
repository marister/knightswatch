using UnityEngine;
using System.Collections;

//TODO  decide if to break this logicly?
//TODO  shit load of stuff that enemies are still missing like health bars,
//		scrolling text, better AI?, deal damage logic to make sense, experience gold systems?
//		loot? unique behavior such as bosses?
//TODO	use some enum status to determine what state this enemy is right now?
//		i.e. { ATTACKING, FROZEN, SLOWED, WALKING, RUNNING, DEAD } etc...
//TODO	generic death animations
//TODO  get ALL enemies (current known enemies) specs decided on BEFORE continuing

/**
 * Base enemy class.
 * Currently this class holds all of enemy logic
 * all enemies should inherit from this base class.
 * All methods that might needs overwriting for specific behavior (like animations?)
 * should override the virtual methods.
 */ 
public class Enemy : MonoBehaviour, DamageDealer, DamageReciever {
	public float ememyMovementSpeed;
	public float healthPoints;
	public float damage;
	public float attackCooldown;
	public int experiencePoints;
	public int baseGold;
	
	////// AI
	public float distanceToAttackTower;
	public float distanceToAttackPlayer;
	public float attackRadius;
		
	protected Transform target;
	protected Vector3 direction;
	protected bool isDead = false;
	protected float i;
	protected float attackCooldownLeft = 0;
	protected CharacterController controller;
	
	///// 2d plain collider for a seperate plain???
	private SphereCollider collider; //for the 2d collider
	public float enemyColliderRadius = 2.5f;
	
	protected virtual void Start(){
		initializeDamageTakenListener();
		create2dCollider();
	}
	
	protected void create2dCollider(){
		collider = gameObject.AddComponent("SphereCollider") as SphereCollider;
		collider.isTrigger = true;
		collider.radius = enemyColliderRadius;
	}
	
	public void initializeDamageTakenListener(){
		Messenger<DamageEventParams>.AddListener("damage dealt", onDamageTaken);
	}
	
	protected void Update(){
		if(isDead){
			return;
		}
		
		GameObject currentTarget = getCurrentTarget();
		collider.center = new Vector3(0, GlobalConfig.plainHeight, 0);
		
		if(needsToAdvance(currentTarget.transform.position)){			
			move(currentTarget.transform);
		} else {
			attack(currentTarget);
		}
	}	
	
	protected void move(Transform currentTarget){
		controller = GetComponent<CharacterController>();
		
		forceWalkAnimation(); //if he is moving he should be walking

		transform.LookAt(currentTarget.transform); //face current target
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		controller.SimpleMove(forward * ememyMovementSpeed);
	}
	
	// This function should start handeling AI
	protected GameObject getCurrentTarget(){
		GameObject tower = GameObject.FindGameObjectWithTag("MainTower");
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
		float distanceToTower = Vector3.Distance(tower.transform.position, transform.position);
		
		if(distanceToTower < distanceToAttackTower){
			return tower;
		} else if (distanceToPlayer < distanceToAttackPlayer) {
			return player;
		}
		
		//if not within forced tower range or no players near by just advance to tower?
		return tower;
	}
	

	 // Determines wether the player should advance or attack	 
	protected bool needsToAdvance(Vector3 targetPosition){	
		float distanceToTarget = Vector3.Distance(targetPosition, transform.position);
		if(distanceToTarget > attackRadius){
			return true;
		}
		return false;
	}
	
	protected void attack(GameObject currentTarget){
		if(currentTarget == null || gameObject == null){
			return;
		}
		
		if(attackCoolDownOff()){
			playAttackAnimation();
			dealDamage(currentTarget);
		}
	}
	
	public virtual void dealDamage(GameObject currentTarget){
		DamageEventParams damageEventParams = new DamageEventParams();
		damageEventParams.damage = damage;
		damageEventParams.source = gameObject;
		damageEventParams.target = currentTarget;
		
		Messenger<DamageEventParams>.Broadcast("damage dealt", damageEventParams);
	}
	
	public void onDamageTaken(DamageEventParams damageEventParams){
		if(gameObject == null){
			return;	
		}
		
		if(damageEventParams.target == gameObject){
			//TODO currently dies when taking a hit, should be changed to health management...
			die();
		}
	}
	
	private bool attackCoolDownOff(){
		attackCooldownLeft -= Time.deltaTime; //update time has passed
		if(attackCooldownLeft <= 0){
			attackCooldownLeft = attackCooldown; //reset cooldown
			return true;
		}
		return false;
	}
	
	void die(){
		Messenger<DamageEventParams>.RemoveListener("damage dealt", onDamageTaken);
		isDead = true;
		playDeathAnimation();
		Destroy(gameObject.collider);
		Destroy(gameObject, 3);
	}
	
	//These methods should be overwridden by each enemy (for sure)
	protected virtual void playAttackAnimation(){}
	protected virtual void playDeathAnimation(){}
	protected virtual void forceWalkAnimation(){}
	protected virtual void initializeEnemyMovement(){}
}

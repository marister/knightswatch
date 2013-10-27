//TODO add short global cooldown???
//TODO parallel class to take care of an attack while its in motion??
//TODO check collision on impact? (through seperate class as mentioned above)

using UnityEngine;
using System.Collections;

/**
  * This is class is in charge of the player prefab attacks. 
  * Helps deterimne if a player hit an enemy object
  * Implements DamageDealer which sends out an event of damage
  * with the current target and the player stats for attacking
 */
public class AvatarAttackManager : MonoBehaviour, DamageDealer {
	public float basePlayerDamage;
	public float qAbilityCooldown;
	public float qAbilityDistance = 100f;
	
	protected float qAbilityCooldownLeft = 0;

    private GameObject currentTarget; //refrence to current target if exists.

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		qAbilityTick();
		//listen to input and fire
		if(Input.GetKeyDown ("q") && qAbilityOffCoolDown()){
            //TODO noni system for precheking with rays!
            //currentTarget = precheckForTargetAiming(); 
            
            dispatchQAbility(null);
		}
	}
	
	protected virtual bool qAbilityOffCoolDown(){
		if(qAbilityCooldownLeft <= 0){
			qAbilityCooldownLeft = qAbilityCooldown; //reset cooldown
			return true;
		}
		return false;
	}
	
	protected virtual void qAbilityTick(){
		if(qAbilityCooldownLeft <= 0){
			return;
		}
		qAbilityCooldownLeft -= Time.deltaTime; //update time has passed
		//TODO update q cool down visually?
	}
	
	/*
	 * This function is used for gameplay adjustment as requested by NONI
	 * when a player fire at a target in a straight line the shot should aim
	 * at that target automaticly
	 * How the fuck to implement it well?
	 */ 
	protected virtual GameObject precheckForTargetAiming(){
		RaycastHit hitInfo;
		Vector3 raySource = transform.position;
		raySource.y = GlobalConfig.plainHeight; //make sure colliders are always at height 0
		
		Debug.DrawRay(raySource, transform.TransformDirection(Vector3.forward) * qAbilityDistance, Color.red, 2);
		
        if (Physics.Raycast(raySource, transform.TransformDirection(Vector3.forward), out hitInfo, qAbilityDistance)){
            return hitInfo.collider.gameObject;
		}
        return null;
	}
	
	/**
	 * This should hold the logic for the q ability
	 * should probably be overwritten by each avatar for their own Q
	 * TODO should change to "onQhit" since the logic should be applied when
	 * we actually hit the Q not only fire ?!?!?!?!
	 */
    public virtual void dispatchQAbility(GameObject target) {
		
	}
	
	/**
	 * logic for sending the Q attack. should really be named differently...
	 */ 
	public virtual void dealDamage(GameObject currentTarget){
		DamageEventParams damageEventParams = new DamageEventParams();
		damageEventParams.damage = basePlayerDamage;
		damageEventParams.source = gameObject;
		damageEventParams.target = currentTarget;
		
		Messenger<DamageEventParams>.Broadcast("damage dealt", damageEventParams);
	}
}

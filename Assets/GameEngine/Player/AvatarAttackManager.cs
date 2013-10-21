//TODO add short global cooldown???

using UnityEngine;
using System.Collections;

public class AvatarAttackManager : MonoBehaviour, DamageDealer {
	public float basePlayerDamage;
	
	public float qAbilityCooldown;
	public float qAbilityDistance = 100f;
	public Transform qAbilityPrefab;
	
	protected float qAbilityCooldownLeft = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		qAbilityTick();
		//listen to input and fire
		if(Input.GetKeyDown ("q") && qAbilityOffCoolDown()){
			precheckForTargetAiming();
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
	
	protected virtual void precheckForTargetAiming(){
		RaycastHit hitInfo;
		Vector3 raySource = transform.position;
		raySource.y = GlobalConfig.plainHeight; //make sure colliders are always at height 0
		
		Debug.DrawRay(raySource, transform.TransformDirection(Vector3.forward) * qAbilityDistance, Color.red, 2);
		
        if (Physics.Raycast(raySource, transform.TransformDirection(Vector3.forward), out hitInfo, qAbilityDistance)){
			fireQAbility(hitInfo.collider.gameObject);
		}
	}
	
	public virtual void fireQAbility(GameObject target){
		if(target == null){ //if we didnt find a target
			//fire ahead
		} else {
			//fire to target
		}
	}
	
	public virtual void dealDamage(GameObject currentTarget){
		DamageEventParams damageEventParams = new DamageEventParams();
		damageEventParams.damage = basePlayerDamage;
		damageEventParams.source = gameObject;
		damageEventParams.target = currentTarget;
		
		Messenger<DamageEventParams>.Broadcast("damage dealt", damageEventParams);
	}
}

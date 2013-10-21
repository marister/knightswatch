using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour, DamageReciever {
	public float health = 100;
	public float armor;
	
	protected float currentHealth = 100;
	
	// Use this for initialization
	public virtual void Start () {
		initializeDamageTakenListener();
	}
	
	public virtual void initializeDamageTakenListener(){
		Messenger<DamageEventParams>.AddListener("damage dealt", onDamageTaken);
	}	
		
	public virtual void onDamageTaken(DamageEventParams damageEventParams){
		if(damageEventParams.target == null || damageEventParams.source == null){
			return;
		}
		
		if(damageEventParams.target == gameObject){
			this.currentHealth -= damageEventParams.damage;	
		}
    }	
	
	// Update is called once per frame
	void Update () {
	
	}
}

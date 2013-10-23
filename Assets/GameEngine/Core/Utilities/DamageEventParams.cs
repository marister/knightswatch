using UnityEngine;
using System.Collections;

//TODO add which type of avatar did the damage
//TODO add with which ability he did the damage
//TODO add custom scene modifiers to damage? if he is buffed or has something???

/**
 * The event params object to be sent with each hit in the game!
 * This way one class always has all the information of the hit and can manage
 * all the game health/death logic at a core location
 */ 
public class DamageEventParams {
	public float damage;
	public GameObject source;
	public GameObject target;
}

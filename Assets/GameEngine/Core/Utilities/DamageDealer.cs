using UnityEngine;
using System.Collections;


/**
 * Interface implemented by every game enemy that has the ability to
 * do any kind of damage! :)
 */ 
interface DamageDealer {
	void dealDamage(GameObject currentTarget);
}

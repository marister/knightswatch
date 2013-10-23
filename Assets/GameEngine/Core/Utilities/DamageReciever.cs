using UnityEngine;
using System.Collections;

/**
 * Basic interface that should be impleneted by any game entity 
 * that can take damage.
 */ 
interface DamageReciever {
	void initializeDamageTakenListener();
	void onDamageTaken(DamageEventParams damageEventParams);
}

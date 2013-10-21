using UnityEngine;
using System.Collections;

interface DamageReciever {
	void initializeDamageTakenListener();
	void onDamageTaken(DamageEventParams damageEventParams);
}

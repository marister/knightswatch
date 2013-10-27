using UnityEngine;
using System.Collections;

public class MageAttackManager : AvatarAttackManager {

    public Transform qAbilityPrefab;

    /**
     * The Q ability in the mage (fireball) is not instant so it must start
     * flying towards the target or up front
     */
    public override void dispatchQAbility(GameObject target) {
        Transform qAbilityClone;
        Vector3 sourcePosition = transform.position;
        qAbilityClone = Instantiate(qAbilityPrefab, sourcePosition, transform.rotation) as Transform;
    }

}

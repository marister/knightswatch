using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public float speed;
    public Transform explosionPrefab; //called to replace prefab on collision

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed);
	}

    
    void OnCollisionEnter(Collision collision) {
        Debug.Log("fireball hit.?");
        
        ContactPoint contact = collision.contacts[0];

        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (viableTarget(collision)) {
            Transform explosion = Instantiate(explosionPrefab, pos, rot) as Transform;
            Destroy(gameObject);
            Destroy(explosion.gameObject, 2);
            //DO DAMAGE!
        }
    }

    private bool viableTarget(Collision collision) {
        if (collision.gameObject.tag == "WizardAvatar") {
            return false;
        }
        if (collision.gameObject.tag == "Terrain") {
            return false;
        }
        return true;
    }
}

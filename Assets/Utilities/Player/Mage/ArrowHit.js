var explosionPrefab : Transform;
var explosionDamageRadius : float = 10;
var explosionDamage : float = 10;

function OnCollisionEnter(collision : Collision) {
	// Rotate the object so that the y-axis faces along the normal of the surface
	var contact = collision.contacts[0];
	var rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
	var pos = contact.point;
	Instantiate(explosionPrefab, pos, rot);
	
	ApplyExplosion(pos);
	
	// Destroy the projectile
	Destroy (gameObject);
}

function ApplyExplosion(center){
	var hitColliders = Physics.OverlapSphere(center, explosionDamageRadius);
	
	for (var i = 0; i < hitColliders.Length; i++) {
		Debug.Log(hitColliders[i].gameObject.tag);
		if(hitColliders[i].gameObject.tag == "Enemy"){
			hitColliders[i].gameObject.gameObject.GetComponent(EnemySpider).explodeAndDie();
		}
	}
}

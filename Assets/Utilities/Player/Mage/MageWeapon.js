var projectile : Rigidbody;
var speed = 20;
var globalCooldown : float = 0.5;

private var globalCooldownTimer : float;

function Start(){
	globalCooldownTimer = 0;
}

function Update () {
	globalCooldownTimer -= Time.deltaTime;

	if ( Input.GetButton ("Fire1") && coolDownPassed()) {
		fireStraightArrow();
		globalCooldownTimer = globalCooldown;
	}	
}

function coolDownPassed(){
	if (globalCooldownTimer <= 0){
		return true;
	}
	return false;
}

function fireStraightArrow(){
	var clone;
	clone = Instantiate(projectile, transform.position, transform.rotation);
	clone.AddForce(clone.transform.forward * speed);
	Destroy (clone.gameObject, 3);	
}
#pragma strict
var ememyMovementSpeed : float = 0.1;
var targetTowerPosition : Vector3;

private var mainTower : Transform;
private var _t : Transform;
private var _direction : Vector3;
private var _deathFlag : boolean;
private var startPos : Vector3;
private var endPos : Vector3;
private var i : float;



function Start(){
	animation.Play("walk");
	startPos = transform.position;
	var mainTower = GameObject.FindGameObjectWithTag("MainTower");
    transform.LookAt(mainTower.transform);
}

function Update(){
	if(!_deathFlag){
		i += Time.deltaTime * ememyMovementSpeed;
		transform.position = Vector3.Lerp(startPos, endPos, i); 
	}
}


function explodeAndDie(){
	_deathFlag = true;
	animation.Play("death1");
	Destroy(gameObject, 3);
}
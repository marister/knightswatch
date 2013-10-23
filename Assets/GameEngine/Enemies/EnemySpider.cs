using UnityEngine;
using System.Collections;

public class EnemySpider : Enemy{
	
	void start(){
		Debug.Log("spider start");
		//TODO fix this
		initializeEnemyMovement();
	}
	
	protected override void initializeEnemyMovement(){
		animation.Play("walk");
	}
	
	protected override void forceWalkAnimation(){
		if (!(animation["walk"].enabled == true)){
			animation.Play("walk");
		}
	}

	protected override void playAttackAnimation(){
		//right now randomizes between the 2 death animations...
		animation.Play("attack" + Random.Range(1,2));	
	}
	
	protected override void playDeathAnimation(){
		
		animation.Play("death" + Random.Range(1,2));
	}
}

using UnityEngine;
using System.Collections;

//TODO create custom wave logics that inherit from this?
//TODO probably rename this if the game is not going to be wave logic based
//TODO get all gameplay specs planned BEFORE continuing with this!

/**
 *	Used to spawn waves
 *  This class manages the spawning of enemies and waves logic
 */
public class WaveSpawner : MonoBehaviour {
	public float timeBetweenWaves; // rough base to the time between bosses
	public float baseEnemyAmount; // a base estimation as to how many enemies spawn
	public float baseEnemyDistance; //base value for distance from origin
	public float spawnHeight; //TODO make private no one needs this
	
	public Transform[] enemies; //Level enemies TODO break this to enemy types?
	
	
	private float timeToNextWave;
	private int currentWave = 1;
	
	
	void Start () {
		timeToNextWave = 0;
	}
	
	void Update () {
		timeToNextWave -= Time.deltaTime;
		
		if(needToSpawnWave()){
			for(int i = 0; i < (currentWave); i++){
				spawnEnemy();
			}
			
			timeToNextWave = timeBetweenWaves; //reset timer
			currentWave++;
		}
	}
	
	private bool needToSpawnWave(){
		if (timeToNextWave <= 0){
			return true;
		}
		return false;
	}
	
	private void spawnEnemy(){
		Vector3 randomPosition; //used to hold the position of where to place the enemy unit
		randomPosition = getPositionOnRandomCircle(new Vector3(0,0,0), baseEnemyDistance);
		Instantiate(getRandomEnemy(), randomPosition, transform.rotation);
	}
	
	private Vector3 getPositionOnRandomCircle(Vector3 center, float radius) {
	    // create random angle between 0 to 360 degrees
	    float ang = Random.value * 360;
	    Vector3 pos;
	    pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
	    pos.y = center.y + spawnHeight;
	    pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
	    return pos;
	}
	
	private Transform getRandomEnemy(){
		return enemies[0]; //TODO randomize enemy to spawn
	}
}

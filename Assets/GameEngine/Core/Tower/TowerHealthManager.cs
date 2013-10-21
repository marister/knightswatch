using UnityEngine;
using System.Collections;

public class TowerHealthManager : HealthManager {
	
	public override void Start(){
		base.Start();
	}		
	
	void OnGUI(){
		float healthBarWidth = Screen.width / 2;
		GUI.Box(new Rect(10, 10, healthBarWidth / (health/currentHealth), 20), currentHealth + "/" + health);
	}
}

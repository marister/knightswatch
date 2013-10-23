using UnityEngine;
using System.Collections;

/**
 * Inherits from all objects that has health.
 * The tower logic for what should be shown
 * when it looses/gains health...
 */ 
public class AvatarHealthManager : HealthManager {
	
	public override void Start(){
		
	}
	
	void OnGUI(){
		float healthBarWidth = Screen.width / 2;
		GUI.Box(new Rect(10, 40, healthBarWidth / (health/currentHealth), 20), currentHealth + "/" + health);
	}
}

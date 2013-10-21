using UnityEngine;
using System.Collections;

public class AvatarHealthManager : HealthManager {
	
	public override void Start(){
		
	}
	
	void OnGUI(){
		float healthBarWidth = Screen.width / 2;
		GUI.Box(new Rect(10, 40, healthBarWidth / (health/currentHealth), 20), currentHealth + "/" + health);
	}
}

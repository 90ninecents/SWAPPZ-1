using UnityEngine;
using System.Collections;


public class HUDManager : MonoBehaviour
{
	
	public UIToolkit manager;
	private float scaleFactor = 1.5f;
	UIAbsoluteLayout hBox;
	
	UISprite[] healthNotches;
	
	public Transform pauseMenu;
	public GameObject swapCarousel;
	
	UIButton portrait;
	
	void Start()
	{
		healthNotches = new UISprite[15];
		//Time.timeScale = 0;
		
		
		UIButton b = UIButton.create(manager, "hud_pause.png", "hud_pause.png", Screen.width-120, 20);
		b.onTouchUp += Pause;
		
		
		
		UISprite portraitBG = manager.addSprite("hud_02.png", 0,0,0);
		portraitBG.localScale /= scaleFactor;
		portraitBG.localPosition = new Vector3(0,-20,10);
		
		portrait = null;
		switch (Game.Player.playerName) {
			case "Donatello":
				portrait = UIButton.create(manager,"head_don.png","head_don.png", 0, 0, 0);
				break;
			case "Michelangelo":
				portrait = UIButton.create(manager,"head_mike.png","head_mike.png", 0, 0, 0);
				break;
			case "Leonardo":
				portrait = UIButton.create(manager,"head_leo.png","head_leo.png", 0, 0, 0);
				break;
			case "Raphael":
				portrait = UIButton.create(manager,"head_raph.png","head_raph.png", 0, 0, 0);
				break;
		}
		
		portrait.onTouchUpInside += Swap;
		portrait.localScale /= scaleFactor;
		portrait.localPosition = new Vector3(-17,0,1);
		
		int healthPosStartX = Mathf.FloorToInt(portrait.width/2)+15;
		int healthPosStartY = -35;
		
		var healthBase = manager.addSprite("hud_health_base.png", 0, 0, 1);
		healthBase.localScale /= scaleFactor;
		healthBase.localPosition = new Vector3(healthPosStartX+1, healthPosStartY, 3);
		
		string number;
		int offset = -14;//-12;
		
		for (int i = 0; i < healthNotches.Length; i++) {
			number = "";
				
			if (healthNotches.Length-i < 10) number = "0";
			number += ""+(healthNotches.Length-i);
			
			int pos = 0;
			if (i > 0) {
				pos = Mathf.RoundToInt(healthNotches[i-1].position.x + healthNotches[i-1].width + offset);
			}
			else pos = healthPosStartX;
			
			healthNotches[i] = manager.addSprite("hud_health_"+number+".png", pos, 0, 0);
			healthNotches[i].localScale /= scaleFactor;
			healthNotches[i].localPosition = new Vector3(pos+6, healthPosStartY-23, 2);
		}
		
		//UIButton pauseButton = UIButton.create("hud_pause.png", "hud_pause.png", Screen.width/2, 50);
		
		
//		hBox = new UIAbsoluteLayout();
//		hBox.addChild( portrait, healthHolder );
//		hBox.matchSizeToContentSize();
		//hBox.positionCenter();
	}
	
	void Pause(UIButton button) {
		Time.timeScale = 0;
		
		if (pauseMenu != null) pauseMenu.gameObject.SetActiveRecursively(true);
	}
	
	public void Swap(UIButton button) {
		if (swapCarousel != null && (Game.WaveManager.waveNumber > 0 || Game.EnemyGroup.childCount > 0)) {
			
			swapCarousel.SetActiveRecursively(!swapCarousel.active);
			
			if (!swapCarousel.active) {
				switch (Game.Player.playerName) {
				case "Donatello":
					portrait.setSpriteImage("head_don.png", "head_don.png");
					break;
				case "Michelangelo":
					portrait.setSpriteImage("head_mike.png", "head_mike.png");
					break;
				case "Leonardo":
					portrait.setSpriteImage("head_leo.png", "head_leo.png");
					break;
				case "Raphael":
					portrait.setSpriteImage("head_raph.png", "head_raph.png");
					break;
				}
			}
		}
	}
	
	void Update() {
		int health = Mathf.FloorToInt(Game.Player.HealthPercentage * healthNotches.Length);
		
		for (int i = 1; i < healthNotches.Length+1; i++) {
			healthNotches[i-1].hidden = !(i <= health);
		}
	}
}

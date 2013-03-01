using UnityEngine;
using System.Collections;


public class HUDManager : MonoBehaviour
{
	
	public UIToolkit manager;
	private int scaleFactor;
	UIAbsoluteLayout hBox;
	
	UISprite[] healthNotches;
	
	public Transform pauseMenu;
	public GameObject swapCarousel;
	
	UIButton portrait;
	
	void Start()
	{
		healthNotches = new UISprite[15];
		
		scaleFactor = UI.scaleFactor;
		//Time.timeScale = 0;
		
		
		UIButton b = UIButton.create(manager, "hud_pause.png", "hud_pause.png", Screen.width/2-60, 20);
		b.onTouchUp += Pause;
		
		
		
		UISprite portraitBG = manager.addSprite("hud_02.png", 0,0,2);
		portraitBG.localScale /= 2;
		portraitBG.localPosition = new Vector3(0,-20,1);
		
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
		portrait.localScale /= 2;
		portrait.localPosition = new Vector3(-17,0,0);
		
		int healthPosStartX = 75;
		int healthPosStartY = -35;
		
		var healthBase = manager.addSprite("hud_health_base.png", 0, 0, 2);
		healthBase.localScale /= 2;
		healthBase.localPosition = new Vector3(healthPosStartX, healthPosStartY, 3);
		//UIStateSprite healthHolder = UIStateSprite.create("hud_health_base.png", 0, 0);
		
		string number;
		int offset = -12;//11;
		
		for (int i = 0; i < healthNotches.Length; i++) {
			number = "";
				
			if (healthNotches.Length-i < 10) number = "0";
			number += ""+(healthNotches.Length-i);
			
			int pos = 0;
			if (i > 0) {
				pos = Mathf.RoundToInt(healthNotches[i-1].position.x + healthNotches[i-1].width + offset);
			}
			else pos = healthPosStartX-1;
			
			healthNotches[i] = manager.addSprite("hud_health_"+number+".png", pos, 0, 0);
			healthNotches[i].localScale /= 2;
			healthNotches[i].localPosition = new Vector3(pos+6, healthPosStartY-17, 2);
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
	
	void Swap(UIButton button) {
		if (swapCarousel != null && (Game.WaveManager.waveNumber > 0 || Game.EnemyGroup.childCount > 0)) {
			//if (swapCarousel.active) Game.UnfreezeAll();
			//else Game.FreezeAll();
			
			swapCarousel.SetActiveRecursively(!swapCarousel.active);
			
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
	
	void Update() {
		int health = Mathf.FloorToInt(Game.Player.HealthPercentage * healthNotches.Length);
		
		for (int i = 0; i < healthNotches.Length; i++) {
			healthNotches[i].hidden = !(i <= health);
		}
	}
}

using UnityEngine;
using System.Collections;

public class AppSetup : MonoBehaviour {
	
	static bool qualityChanged = false;
	
	// Use this for initialization
	void Awake () {
		// if the save profile for this user is empty, populate it with a default character
		if (SavedData.Characters == "") {
			SavedData.Characters = "CharacterLeonardo|CharacterDonatello|CharacterMichelangelo|CharacterRaphael";
			SavedData.UnlockedCharacters = "";
			SavedData.Inventory = "ItemSizeUp|ItemPizzaFull";
		}
		
		// debugging
		//SavedData.Characters = "CharacterLeonardo|CharacterDonatello|CharacterMichelangelo|CharacterRaphael";
		//SavedData.UnlockedCharacters = "CharacterLeonardo|CharacterMichelangelo";
		SavedData.Inventory = "ItemInvincibility|ItemCombo|ItemXP|ItemSizeUp|ItemSpeed|ItemPizzaFull|ItemNuke";
		// ---------
		
		if (!qualityChanged) {
			
			if (iPhone.generation == iPhoneGeneration.iPodTouch5Gen	||
				iPhone.generation == iPhoneGeneration.iPad2Gen		||
				iPhone.generation == iPhoneGeneration.iPad3Gen		||
				iPhone.generation == iPhoneGeneration.iPadUnknown	||
				iPhone.generation == iPhoneGeneration.iPhone4S		||
				iPhone.generation == iPhoneGeneration.iPhone5		||
				iPhone.generation == iPhoneGeneration.iPhoneUnknown) {
				
				QualitySettings.SetQualityLevel(1);
			}
			else {
				QualitySettings.SetQualityLevel(0);
			}
			
			qualityChanged = true;
			
			AudioManager.ChangeChannelVolume("Background", 0.5f);
			AudioManager.PlayAudio("MenuBG", "Background", 0, true);
			
			// throttle framerate for editor testing
			Application.targetFrameRate = 30;
		}
		
		// Play menu music
		if (AudioManager.GetChannelClipName("Background") != "MenuBG") AudioManager.PlayAudio("MenuBG", "Background", 0, true);
		
		// debugging
		LanguageManager.Test();
	}
}

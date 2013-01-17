using UnityEngine;
using System.Collections;

public class AppSetup : MonoBehaviour {
	
	bool qualityChanged = false;
	
	// Use this for initialization
	void Awake () {
		// if the save profile for this user is empty, populate it with a default character
		if (SavedData.Characters == "") {
			SavedData.Characters = "CharacterLeonardo|CharacterDonatello|CharacterMichelangelo|CharacterRaphael";
			SavedData.UnlockedCharacters = "";
			SavedData.Inventory = "ItemSizeUp|ItemPizzaFull";
		}
		
		// debugging
		SavedData.Characters = "CharacterLeonardo|CharacterDonatello|CharacterMichelangelo|CharacterRaphael";
		SavedData.UnlockedCharacters = "CharacterLeonardo|CharacterMichelangelo";
		SavedData.Inventory = "ItemXP|ItemCake|ItemCombo|ItemFlashBomb|ItemInvincibility|ItemPizzaFull|ItemSizeUp|ItemSpeed|ItemCake|ItemCombo|ItemFlashBomb|ItemInvincibility|ItemPizzaFull|ItemSizeUp|ItemSpeed";
		// ---------
		
		if (!qualityChanged) {
			if (iPhone.generation == iPhoneGeneration.iPodTouch4Gen || 
				iPhone.generation == iPhoneGeneration.iPad1Gen 		|| 
				iPhone.generation == iPhoneGeneration.iPhone4 		|| 
				iPhone.generation == iPhoneGeneration.iPhone3G) {
				QualitySettings.SetQualityLevel(0);
			}
			else {
				QualitySettings.SetQualityLevel(1);
			}
			
			qualityChanged = true;
			
			AudioManager.ChangeChannelVolume("Background", 0.5f);
			AudioManager.PlayAudio("MenuBG", "Background", 0, true);
			
			// throttle framerate for editor testing
			Application.targetFrameRate = 30;
		}
		
		// Play menu music
		if (AudioManager.GetChannel("Background").clip.name != "MenuBG") AudioManager.PlayAudio("MenuBG", "Background", 0, true);
	}
}

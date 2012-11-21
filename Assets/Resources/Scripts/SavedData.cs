using UnityEngine;
using System.Collections;

public class SavedData {
	// Holds the key names of saved data in PlayerPrefs for better encapsulation
	
	private static string inventory = "Inventory";
	private static string itemLoadout = "ItemLoadout";
	private static string unlockedCharacters = "UnlockedCharacters";
	private static string characterLoadout = "CharacterLoadout";
	
	private static string separator = "|";
	
	private static string numAllowedItems = "NumAllowedItems";
	private static string numAllowedCompanions = "NumAllowedCompanions";
	
	private static string currentLevel = "CurLevel";
	
	public static string Inventory {
		get { return PlayerPrefs.GetString(SavedData.inventory); }
		set { PlayerPrefs.SetString(SavedData.inventory, value); }
	}
	
	public static string ItemLoadout {
		get { return PlayerPrefs.GetString(SavedData.itemLoadout); }
		set { PlayerPrefs.SetString(SavedData.itemLoadout, value); }
	}
	
	public static string CharacterLoadout {
		get { return PlayerPrefs.GetString(SavedData.characterLoadout); }
		set { PlayerPrefs.SetString(SavedData.characterLoadout, value); }
	}
	
	public static string UnlockedCharacters {
		get { return PlayerPrefs.GetString(SavedData.unlockedCharacters); }
		set { PlayerPrefs.SetString(SavedData.unlockedCharacters, value); }
	}
	
	public static string Separator { get { return separator; } }
	
	public static int NumAllowedItems {
		get { return PlayerPrefs.GetInt(SavedData.numAllowedItems); }
		set { PlayerPrefs.SetInt(SavedData.numAllowedItems, value); }
	}
	
	public static int NumAllowedCompanions {
		get { return PlayerPrefs.GetInt(SavedData.numAllowedCompanions); }
		set { PlayerPrefs.SetInt(SavedData.numAllowedCompanions, value); }
	}
	
	public static string CurrentLevel {
		get { return PlayerPrefs.GetString(SavedData.currentLevel); }
		set { PlayerPrefs.SetString(SavedData.currentLevel, value); }
	}
}

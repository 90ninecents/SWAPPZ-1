using UnityEngine;
using System.Collections;

public class SavedData {
	// Holds the key names of saved data in PlayerPrefs for better encapsulation
	
	private static string inventory = "Inventory";
	private static string itemLoadout = "ItemLoadout";
	private static string characters = "Characters";
	private static string unlockedCharacters = "UnlockedCharacters";
	private static string characterLoadout = "CharacterLoadout";
	
	private static string separator = "|";
	
	private static string numAllowedItems = "NumAllowedItems";
	private static string numAllowedCompanions = "NumAllowedCompanions";
	
	private static string currentLevel = "CurLevel";
	
	private static string levelCoins = "LevelCoins";
	private static string levelKills = "LevelKills";
	private static string levelTime = "LevelTime";
	
	private static string totalCoins = "TotalCoins";
	
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
	
	public static string Characters {
		get { return PlayerPrefs.GetString(SavedData.characters); }
		set { PlayerPrefs.SetString(SavedData.characters, value); }
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
	
	public static int Coins {
		get { return PlayerPrefs.GetInt(SavedData.totalCoins); }
		set { PlayerPrefs.SetInt(SavedData.totalCoins, value); }
	}
	
	public static int LevelCoins {
		get { return PlayerPrefs.GetInt(SavedData.levelCoins); }
		set { PlayerPrefs.SetInt(SavedData.levelCoins, value);
			  PlayerPrefs.SetInt(SavedData.totalCoins, SavedData.Coins+value); }
	}
	
	public static int LevelKills {
		get { return PlayerPrefs.GetInt(SavedData.levelKills); }
		set { PlayerPrefs.SetInt(SavedData.levelKills, value); }
	}
	
	public static int LevelTime {
		get { return PlayerPrefs.GetInt(SavedData.levelTime); }
		set { PlayerPrefs.SetInt(SavedData.levelTime, value); }
	}
}

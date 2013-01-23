using UnityEngine;
using System.Collections;

public class StatsManager : MonoBehaviour {
//	public GUIText coinCounter;
//	public GUIText killCounter;
	public GUIText timeCounter;
//	public GUIText scoreCounter;
	
	public RunningCounter coinCounter;
	public RunningCounter killCounter;
	public RunningCounter scoreCounter;

	void Awake () {
		timeCounter.text = FormatTime(Game.LevelTimeInSeconds);
		
		coinCounter.Run(Game.Coins);
		killCounter.Run(Game.EnemiesKilled);
		scoreCounter.Run(CalculateScore());
		
		if (Game.LastLevelWon) AudioManager.PlayAudio("Win", "Background");
		else AudioManager.PlayAudio("Lose", "Background");
	}
	
	string FormatTime(int seconds) {
		int minutes = 0;
		
		while (seconds > 60) {
			minutes += 1;
			seconds -= 60;
		}
		
		string result;
		result = minutes+":";
		if (seconds < 10) result += "0";
		result +=seconds;
		
		return result;
	}
	
	int CalculateScore() {
		// Score from enemies killed, coins collected, and time in seconds
		return Game.Score + (Game.Coins*10) - Game.LevelTimeInSeconds;
	}
}

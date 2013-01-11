using UnityEngine;
using System.Collections;

public class StatsManager : MonoBehaviour {
	public GUIText coinCounter;
	public GUIText killCounter;
	public GUIText timeCounter;
	public GUIText scoreCounter;

	void Awake () {
		coinCounter.text = ""+Game.Coins;
		killCounter.text = ""+Game.EnemiesKilled;
		timeCounter.text = FormatTime(Game.LevelTimeInSeconds);
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
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	public static AudioManager instance;
	
	public static string NewChannel { get { return "NEW_CHANNEL"; } }
	public static string UnusedChannel { get { return "1ST_UNUSED_CHANNEL"; } }
	
	public AudioClip[] audioFiles;
	public string[] audioNames;
	
	public string[] channelNames;
	
	Dictionary<string, AudioSource> channels;	
	
	bool muted = false;
	
	public static AudioClip[] AudioFiles { get { return instance.audioFiles; } }
	public static string[]    AudioNames { get { return instance.audioNames; } }
	public static bool 		  IsMuted 	 { get { return instance.muted; } }
	
	public static Dictionary<string, AudioSource> Channels { get { return instance.channels; } }
	
	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
			
			if (AudioFiles.Length != AudioNames.Length) {
				Debug.LogError("AudioManager: Number of AudioClips does not match number of audio names set in the Inspector.");
				return;
			}
			
			channels = new Dictionary<string, AudioSource>();
			
			foreach (string s in channelNames) {
				AudioSource source = gameObject.AddComponent<AudioSource>();
				source.playOnAwake = false;
				channels.Add(s, source);
			}
			
			DontDestroyOnLoad(transform);
		}
		else {
			Destroy(this);
		}
	}
	
	public static void PlayAudio(string name, string channelName = "", float delay = 0, bool looping = false) {
		int index = -1;
		
		foreach (string s in AudioNames) {
			index++;
			if (s == name) {
				if (channelName == NewChannel) {
					// Create new, one-use channel to play this audio
					CreateNewChannel(AudioFiles[index], delay, looping);
				}
				
				else if (channelName == UnusedChannel || channelName == "" || channelName == null) {
					// Find the first idle existing channel and use it to play this audio
					bool channelFound = false;
					
					foreach (AudioSource a in Channels.Values) {
						if (!a.isPlaying) {
							a.clip = AudioFiles[index];
							a.loop = looping;
							a.Play(System.Convert.ToUInt64(delay*a.clip.samples));
							
							channelFound = true;
							break;
						}
					}
					
					// If no pre-existing channel is unused, make a new, one-use channel to play this audio
					if (!channelFound) {
						CreateNewChannel(AudioFiles[index], delay, looping);
					}
				}
				
				else if (Channels.ContainsKey(channelName)) {
					// Find the specified channel and play the audio clip
					Channels[channelName].clip = AudioFiles[index];
					Channels[channelName].loop = looping;
					Channels[channelName].Play(System.Convert.ToUInt64(delay*Channels[channelName].clip.samples));
				}
				
				else {
					// Channel name does not existing in dictionary
					Debug.LogError("AudioManager: Channel name \""+channelName+"\" does not exist.");
				}
				
				break;
			}
		}
	}
	
	public static void StopChannel(string name) {
		Channels[name].Stop();
	}
	
	public static void ChangeChannelVolume(string name, float volume) {
		Channels[name].volume = volume;
	}
	
	public static AudioSource GetChannel(string name) {
		return Channels[name];
	}
	
	static void CreateNewChannel(AudioClip clip, float delay, bool looping) {
		// If game is muted, don't bother 
		if (!instance.muted) {
			AudioSource a = instance.gameObject.AddComponent<AudioSource>();
			a.playOnAwake = false;
			a.clip = clip;
			a.loop = looping;
			a.Play(System.Convert.ToUInt64(delay*clip.samples));
			
			Destroy(a, (delay*clip.samples)+clip.length+1);
		}
	}
	
	public static void ToggleMute() {
		instance.muted = !instance.muted;
		
		foreach (AudioSource s in instance.transform.GetComponents<AudioSource>()) {
			s.mute = instance.muted;
		}
	}
}

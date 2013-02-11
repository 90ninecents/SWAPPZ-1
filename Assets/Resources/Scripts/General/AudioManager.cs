using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	/*
	 * AudioManager.cs
	 * Stephanie Owen
	 * 
	 * This class provides a simple way to access and manage all AudioClips and AudioSources in the project.
	 * 
	 */
	
	
	public static AudioManager instance;
	
	public static string NewChannel { get { return "NEW_CHANNEL"; } }			// Constant string used to signal the creation of a new, one-use AudioSource
	public static string UnusedChannel { get { return "1ST_UNUSED_CHANNEL"; } } // Constant string used to signal the use of the first AudioSource in the manager that is not playing a clip
	
	// Inspector variables
	public AudioClip[] audioFiles;				// List of all AudioClips to be managed by this class
	public string[] audioNames;					// List of the desired names for the AudioClips assigned to this manager, in order
	
	public string[] channelNames;				// List of the desired names for the AudioSources (channels) managed by this object; the number of elements dictates the number of permanent AudioSource components
	
	Dictionary<string, AudioSource> channels;	// Dictionary of permanent AudioSources using names as keys
	
	bool muted = false;							// Flag that indicates whether all audio is muted
	
	// Getters
	public static AudioClip[] AudioFiles { get { return instance.audioFiles; } }
	public static string[]    AudioNames { get { return instance.audioNames; } }
	public static bool 		  IsMuted 	 { get { return instance.muted; } }
	
	public static Dictionary<string, AudioSource> Channels { get { return instance.channels; } }
	
	
	// Methods
	
	void Awake () {
		/*
		 * Check inspector-assigned values and initialize permanent AudioSource components
		 */ 
		
		if (instance == null) {
			instance = this;
			
			// If there is a mismatch between number of AudioClips and number of clip names, exit
			if (AudioFiles.Length != AudioNames.Length) {
				Debug.LogError("AudioManager: Number of AudioClips does not match number of audio names set in the Inspector.");
				return;
			}
			
			// Create dictionary
			channels = new Dictionary<string, AudioSource>();
			
			// Create permanent AudioSources
			foreach (string s in channelNames) {
				AudioSource source = gameObject.AddComponent<AudioSource>();
				source.playOnAwake = false;
				channels.Add(s, source);
			}
			
			// Prevent this object from being destroyed on scene load
			DontDestroyOnLoad(transform);
		}
		else {
			// If an instance already exists, destroy duplicate
			Destroy(this);
		}
	}
	
	public static void PlayAudio(string name, string channelName = "", float delay = 0, bool looping = false) {
		/*
		 * Takes the name of the desired clip and the name of the desired channel, and uses the specified channel to play the AudioClip after 'delay' seconds
		 */ 
		
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
		/*
		 * Takes the name of a channel and stops the AudioSource
		 */ 
		Channels[name].Stop();
	}
	
	public static void ChangeChannelVolume(string name, float volume) {
		/*
		 * Takes the name of a channel and changes the volume of the AudioSource
		 */ 
		Channels[name].volume = volume;
	}
	
	public static AudioSource GetChannel(string name) {
		/*
		 * Takes the name of a channel and returns the AudioSource
		 */ 
		return Channels[name];
	}
	
	static void CreateNewChannel(AudioClip clip, float delay, bool looping) {
		/*
		 * Takes an AudioClip and creates a new AudioSource component to play it, then destroys the AudioSource after completion
		 */ 
		
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
	
	public static string GetChannelClipName(string channelName) {
		/*
		 * Takes the name of a channel and returns the name of the clip it is currently playing
		 */ 
		int index = -1;
				
		foreach (AudioClip c in AudioFiles) {
			index++;
			if (c == Channels[channelName].clip) {
				return AudioNames[index];
			}
		}
		
		return "";
	}
	
	public static void ToggleMute() {
		/*
		 * Toggles the muted state of all AudioSources
		 */ 
		instance.muted = !instance.muted;
		
		foreach (AudioSource s in instance.transform.GetComponents<AudioSource>()) {
			s.mute = instance.muted;
		}
	}
}

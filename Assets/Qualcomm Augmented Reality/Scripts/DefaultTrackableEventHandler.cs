/*==============================================================================
            Copyright (c) 2012 QUALCOMM Austria Research Center GmbH.
            All Rights Reserved.
            Qualcomm Confidential and Proprietary
==============================================================================*/

using UnityEngine;

// A custom handler that implements the ITrackableEventHandler interface.

public class DefaultTrackableEventHandler : MonoBehaviour,
                                            ITrackableEventHandler
{
	public TrackableBehaviour mtrackableBehaviour;
	public Transform turtle;
	
	bool scanned = false;
	Animation turtleAnim;
	
	GUITexture reticle;
	GUIText text;
	public GameObject[] activeOnTrack;
	public GameObject[] activeOnLost;
	
    #region PRIVATE_MEMBER_VARIABLES
 
    private TrackableBehaviour mTrackableBehaviour;
    
    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNTIY_MONOBEHAVIOUR_METHODS
	    
    void Start()
    {
		// TimeScale starts at 0.2 - don't know why
		Time.timeScale = 0.75f;
		
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
		
		turtleAnim = turtle.GetChild(0).GetComponent<Animation>();
		turtleAnim.Play();
		
        OnTrackingLost();
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS



    #region PUBLIC_METHODS

    // Implementation of the ITrackableEventHandler function called when the
    // tracking state changes.
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
                                    	TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED)
        {
			OnTrackingFound();
        }
        else
        {
           OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS



    #region PRIVATE_METHODS


    private void OnTrackingFound()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
		
        // Enable rendering:
//        foreach (Renderer component in rendererComponents) {
//            component.enabled = true;
//        }
		
		foreach (GameObject go in activeOnTrack) {
			go.SetActiveRecursively(true);
		}
		foreach (GameObject go in activeOnLost) {
			go.SetActiveRecursively(false);
		}
		
		if (!scanned) {
			// Unlock scanned character
			string character = "Character"+turtle.name;
			bool unlock = true;
			
			foreach (string s in SavedData.UnlockedCharacters.Split(SavedData.Separator[0])) {
				if (s == character) unlock = false;
			}
			foreach (string s in SavedData.CharacterLoadout.Split(SavedData.Separator[0])) {
				if (s == character) unlock = false;
			}
			
			if (unlock) SavedData.UnlockedCharacters = SavedData.UnlockedCharacters+SavedData.Separator+character;
			scanned = true;
		}
    }

    private void OnTrackingLost() {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();

        // Disable rendering:
//        foreach (Renderer component in rendererComponents) {
//            component.enabled = false;
//        }
		
		foreach (GameObject go in activeOnTrack) {
			go.SetActiveRecursively(false);
		}
		foreach (GameObject go in activeOnLost) {
			go.SetActiveRecursively(true);
		}
    }
	
	void OnTrack() {
	}
	
	void Update() {
		if (!turtleAnim.isPlaying) {
			turtleAnim.Play();
		}
	}

    #endregion // PRIVATE_METHODS
}

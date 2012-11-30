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
	Animation Idle_test_ani;
	
	
     GameObject ninja;
    #region PRIVATE_MEMBER_VARIABLES
 
    private TrackableBehaviour mTrackableBehaviour;
    
    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNTIY_MONOBEHAVIOUR_METHODS
	


    
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

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
        foreach (Renderer component in rendererComponents) {
            component.enabled = true;
        }

//        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }


    private void OnTrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();

        // Disable rendering:
        foreach (Renderer component in rendererComponents) {
            component.enabled = false;
        }

       if (Idle_test_ani != null)
       Idle_test_ani.Stop();
	
    }
	void OnTrack()
	{
		  Idle_test_ani.Play();
	}

    #endregion // PRIVATE_METHODS
}

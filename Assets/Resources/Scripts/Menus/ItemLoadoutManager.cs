using UnityEngine;
using System.Collections;

public class ItemLoadoutManager : MonoBehaviour {
	private bool _movedContainer;
	public UIToolkit textManager;	
	
	
	void Start()
	{
		// add a scrollable with paging enabled
		var scrollable = new UIScrollableHorizontalLayout( 75 );
		
		// we wrap the addition of all the sprites with a begin updates so it only lays out once when complete
		scrollable.beginUpdates();
		
		var height = UI.scaleFactor * 250f;
		
		// if you plan on making the scrollable wider than the item width you need to set your edgeInsets so that the
		// left + right inset is equal to the extra width you set
		scrollable.edgeInsets = new UIEdgeInsets( 0, 75, 0, 75 );		
		var width = UI.scaleFactor * ( 250f + 150f ); // item width + 150 extra width
		scrollable.setSize( width, height );
		
		// paging will snap to the nearest page when scrolling
		scrollable.pagingEnabled = true;
		//scrollable.pageWidth = 55 * UI.scaleFactor;
		
		// center the scrollable horizontally
		scrollable.position = new Vector3( Screen.width/2 - width/2, -Screen.height/2 + height/4, 0 );
		
		foreach (string s in SavedData.Inventory.Split(SavedData.Separator[0])) {
			var button = UIButton.create(s+".png", s+".png", 0, 0 );
			scrollable.addChild( button );
			
			
		}
		
		scrollable.endUpdates();
		scrollable.endUpdates(); // this is a bug. it shouldnt need to be called twice
	}
}

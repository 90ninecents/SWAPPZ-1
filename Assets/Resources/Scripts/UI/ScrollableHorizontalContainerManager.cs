using UnityEngine;
using System.Collections;



public class ScrollableHorizontalContainerManager : MonoBehaviour
{
	private bool _movedContainer;
	public UIToolkit textManager;	
	
	
	void Start()
	{
		// add a scrollable with paging enabled
		var scrollable = new UIScrollableHorizontalLayout( 20 );
		
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
		scrollable.pageWidth = 250f * UI.scaleFactor;
		
		// center the scrollable horizontally
		scrollable.position = new Vector3( ( Screen.width - width ) / 2, ( Screen.height - height ) / 2, 0 );
		
		for( var i = 0; i < 5; i++ )
		{
			var button = UIButton.create( "marioPanel.png", "marioPanel.png", 0, 0 );
			scrollable.addChild( button );
		}
		
		scrollable.endUpdates();
		scrollable.endUpdates(); // this is a bug. it shouldnt need to be called twice
	}

}

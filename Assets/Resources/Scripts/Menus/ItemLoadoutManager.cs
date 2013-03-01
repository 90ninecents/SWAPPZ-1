using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemLoadoutManager : MonoBehaviour {
	private bool _movedContainer;
	public UIToolkit textManager;
	
	public GUIText itemName;
	public GUIText itemDesc;
	
	int itemSpacing = 330;
	int itemWidth = 220;
	int itemHeight = 232;
	int numVisibleItems = 3;
	
	int maxItems = 4;
	
	float scaleFactor = 1.15f;
	float loadoutScaleFactor = 1.5f;
	
	List<InventoryItem> items;
	List<InventoryItem> loadoutItems;
	string[] inventoryNames;
	
	UIScrollableHorizontalLayout scrollable;
	
	UIScrollableHorizontalLayout loadoutScrollable;
	
	int prevPage = -1;
	
	
	void Start() {
		
		inventoryNames = SavedData.Inventory.Split(SavedData.Separator[0]);
		items = new List<InventoryItem>();
		loadoutItems = new List<InventoryItem>();
		
		int index = 0;
		foreach (string s in inventoryNames) {
			items.Add((Resources.Load("Prefabs/Loadout Items/"+s) as GameObject).GetComponent<InventoryItem>());
			
			if (index == 0) {
				itemName.text = items[index].itemName;
				itemDesc.text = items[index].description;
			}
			
			index++;
		}
		
		
		itemWidth   /= UI.scaleFactor;
		itemSpacing /= (UI.scaleFactor*numVisibleItems);
		
		// add a scrollable with paging enabled
		scrollable = new UIScrollableHorizontalLayout(itemSpacing);
		
		// we wrap the addition of all the sprites with a begin updates so it only lays out once when complete
		scrollable.beginUpdates();
		
		var height = UI.scaleFactor * itemHeight;
		var width  = UI.scaleFactor * (itemWidth*numVisibleItems);
		
		// if you plan on making the scrollable wider than the item width you need to set your edgeInsets so that the
		// left + right inset is equal to the extra width you set
		scrollable.edgeInsets = new UIEdgeInsets( 0, width/4 - Mathf.RoundToInt((itemWidth * (scaleFactor-1))/2), 0, width/4 );
		
		scrollable.setSize( width, height );
		
		// paging will snap to the nearest page when scrolling
		scrollable.pagingEnabled = true;
		//scrollable.pageWidth = itemWidth/2+itemSpacing/2;
		scrollable.pageWidth = itemWidth*scaleFactor;
		
		// center the scrollable horizontally
		scrollable.position = new Vector3( Screen.width/2 - width/2, -Screen.height/2 + height/4, 2 );
		
		//var blank = UIButton.create(null, null, 0, 0 );
		//scrollable.addChild(blank);
		
		int count = 0;
		foreach (string s in inventoryNames) {
			var button = UIButton.create(s+".png", s+".png", 0, 0 );
			scrollable.addChild( button );
			
			button.onTouchDown += OnTouchUp;
			
			count++;
		}
		
		scrollable.Children[scrollable.PageNumber].localScale = new Vector3(scaleFactor,scaleFactor,1);
		
		scrollable.endUpdates();
		scrollable.endUpdates(); // this is a bug. it shouldnt need to be called twice
		
		
		loadoutScrollable = new UIScrollableHorizontalLayout(Mathf.RoundToInt(itemSpacing/(2*loadoutScaleFactor)));
		loadoutScrollable.setSize( 20 + width/(loadoutScaleFactor), height/(2*loadoutScaleFactor) );
		loadoutScrollable.position = new Vector3( 30 + Screen.width/2 - width/(2*loadoutScaleFactor), -(itemHeight/UI.scaleFactor)/2, 2 );
		
		loadoutScrollable.locked = true;
	}
	
	
	void OnDisable() {
		// Write loadout choices to saved data
		SavedData.ItemLoadout = "";
		foreach (InventoryItem item in loadoutItems) {
			if (SavedData.ItemLoadout != "") SavedData.ItemLoadout += "|";
			SavedData.ItemLoadout += item.name;
		}
	}
	
	
	void OnTouchUp(UIButton button) {
		if (loadoutScrollable.Children.Count < maxItems) {
			if (button.index == scrollable.PageNumber) {
				if (button.index == scrollable.Children.Count-1) {
					scrollable.scrollToPage(scrollable.PageNumber-1);
				}
				
				loadoutItems.Add(items[button.index]);
				items.RemoveAt(button.index);
				
				scrollable.removeChild(button, false);
				scrollable.RefreshLayout();
				
				
				button.localScale /= loadoutScaleFactor;
				
				loadoutScrollable.addChild(button);
				
				button.onTouchDown -= OnTouchUp;
				button.onTouchDown += OnTouchUpReverse;
				
				foreach (UISprite child in scrollable.Children) {
					if (child.index > button.index)	child.index--;
				}
				
				button.index = loadoutScrollable.Children.Count-1;
			}
		}
	}
	
	void OnTouchUpReverse(UIButton button) {
		loadoutScrollable.removeChild(button, false);
		
		button.localScale *= loadoutScaleFactor;
		
		button.onTouchDown -= OnTouchUpReverse;
		button.onTouchDown += OnTouchUp;
		scrollable.addChild(button);
		
		items.Add(loadoutItems[button.index]);
		loadoutItems.RemoveAt(button.index);
	}
	
	
	void Update() {
		if (scrollable.Children.Count == 0) {
			itemName.text = "";
			itemDesc.text = "";
		}
		else if (itemName.text != items[scrollable.PageNumber].itemName) {
			if (prevPage >= 0 && prevPage+1 < scrollable.Children.Count) scrollable.Children[prevPage].localScale = new Vector3(1,1,1);
			scrollable.Children[scrollable.PageNumber].localScale = new Vector3(scaleFactor,scaleFactor,1); 
			
			// NaN?
			//scrollable.Children[scrollable.PageNumber].scaleTo(0, new Vector3(scaleFactor,scaleFactor,1), Easing.Exponential.easeIn);
			
			itemName.text = items[scrollable.PageNumber].itemName;
			itemDesc.text = items[scrollable.PageNumber].description;
		}
		
		prevPage = scrollable.PageNumber;
	}
	
	private IEnumerator animateScale( UISprite sprite, Vector3 scaleStart, Vector3 scaleStop , float duration , float delay , System.Func<float, float> ease ) {
		yield return new WaitForSeconds( delay );
		sprite.hidden = false;
		var ani = sprite.scaleFromTo( duration, scaleStart , scaleStop , ease );
	}
}

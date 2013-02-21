using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollUI : MonoBehaviour {
	
	public int maxItemsInScroll = 3;
	public int maxSlotsInInventory = 4;
	
	public List<string> itemNames;
	public List<UIButton> itemsInScroll = new List<UIButton>();
	private List<UIButton> itemsInSlots = new List<UIButton>();
	public int selectedIndex;
	private float centerX;
	public bool animating = false;
	
	#region Monobehaviors
	void Start () {
		StaticButtons();
		DynamicButtons();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	#endregion
	
	#region Button Creation
	void StaticButtons() {
		UIButton leftButton = UIButton.create("LeftButton.png", "LeftButton.png", 0 , 0);
		UIButton rightButton = UIButton.create("RightButton.png", "RightButton.png", 0 , 0);
		
		leftButton.positionFromLeft(0, 0);
		rightButton.positionFromRight(0, 0);
		
		leftButton.onTouchUpInside += onTouchUpInside;
		rightButton.onTouchUpInside += onTouchUpInside;
		
		leftButton.client.name = "LeftButton";
		rightButton.client.name = "RightButton";
		
		var headline = new UIText("Molot", "Molot");
		
	}
	
	void DynamicButtons() {
		maxItemsInScroll = (itemNames.Count < maxItemsInScroll) ? itemNames.Count : maxItemsInScroll;
		
		for (int i = 0; i < itemNames.Count; i++) {
			UIButton itemButton = UIButton.create (itemNames[i] + ".png", itemNames[i] + ".png", 0, 0);
			itemButton.centerize();
			itemButton.pixelsFromCenter(0, i * 220);
			itemButton.client.name = itemNames[i];
			if (i == 0) {
				//itemButton.scale = new Vector2 (1.2f, 1.2f);
				selectedIndex = i;
				centerX = itemButton.position.y;
				
			}
				
			if (itemButton.position.x < 35 || itemButton.position.x > 900)
				itemButton.hidden = true;
			
			itemsInScroll.Add(itemButton);
		}
		itemsInScroll[selectedIndex].onTouchUpInside += onTouchUpInside;
	}
	#endregion
	
	#region Animations
	void AnimateRight() {
		
		animating = true;
		
		for (int i = 0; i < itemsInScroll.Count; i++) {
			//itemsInScroll[i].scale = Vector3.one;
			itemsInScroll[i].hidden = false;
			var animation = itemsInScroll[i].positionTo(0.05f, 	new Vector2(itemsInScroll[i].position.x - 220, itemsInScroll[i].position.y), Easing.Linear.easeInOut);
			animation.onComplete = () => HideOutOfScreenItems();
			
		}
		selectedIndex ++;
		itemsInScroll[selectedIndex].onTouchUpInside += onTouchUpInside;
		//itemsInScroll[selectedIndex].scale = new Vector2(1.2f, 1.2f);
	}
	
	void AnimateLeft() {
		animating = true;
		
		for (int i = 0; i < itemsInScroll.Count; i++) {
			//itemsInScroll[i].scale = Vector3.one;
			itemsInScroll[i].hidden = false;
			var animation = itemsInScroll[i].positionTo(0.05f, 	new Vector2(itemsInScroll[i].position.x + 220, itemsInScroll[i].position.y), Easing.Linear.easeInOut);
			animation.onComplete = () => HideOutOfScreenItems();
			
		}
		selectedIndex --;
		itemsInScroll[selectedIndex].onTouchUpInside += onTouchUpInside;
		//itemsInScroll[selectedIndex].scale = new Vector2(1.2f, 1.2f);
	}
	

	void HideOutOfScreenItems() {
		foreach (UIButton itemButton in itemsInScroll) {
			if (itemButton.position.x < 45 || itemButton.position.x > 900)
				itemButton.hidden = true;
			else
				itemButton.hidden = false;
		}
		animating = false;
	}
	#endregion
	
	
	#region Event Triggers
	void onTouchUpInside (UIButton uiButton) {
		switch (uiButton.client.name) {
		case "LeftButton":
			//Move to one step to the left
			itemsInScroll[selectedIndex].onTouchUpInside -= onTouchUpInside;
			if (!animating && selectedIndex > 0)
				AnimateLeft();
			
			Debug.Log("Selected Item:" + itemsInScroll[selectedIndex].client.name);
			break;
			
		case "RightButton":
			//Move to one step to the right
			itemsInScroll[selectedIndex].onTouchUpInside -= onTouchUpInside;
			if (!animating && selectedIndex < itemsInScroll.Count - 1)
				AnimateRight();
			
			Debug.Log("Selected Item:" + itemsInScroll[selectedIndex].client.name);
			break;
			
		default:
			Debug.Log(uiButton.client.name);
			itemsInScroll[selectedIndex].onTouchUpInside -= onTouchUpInside;
			RemoveItemFromInventory(uiButton);
			//AddItemToSlot(uiButton);
			
			break;
		}
	}
	
	void AddItemToSlot(UIButton uiButton) {
		if (itemsInSlots.Count == maxSlotsInInventory)
			return;
		
		itemsInSlots.Add(uiButton);
		AnimateSlots();
	}
	
	void RemoveItemFromInventory(UIButton uiButton) {
		
		AddItemToSlot(uiButton);
		
		for (int i = itemsInScroll.IndexOf(uiButton); i < itemsInScroll.Count - 1; i++) {
			itemsInScroll[i].hidden = false;
			var sort = itemsInScroll[i + 1].positionTo(0.1f, itemsInScroll[i].position, Easing.Quartic.easeOut);
			if (i == itemsInScroll.Count - 2)
				sort.onComplete = () => { 
				Debug.Log("Running after completion of animations");
				HideOutOfScreenItems();
				itemsInScroll.Remove(uiButton);
				
			};
			Debug.Log("Moving " + itemsInScroll[i + 1].client.name + " to " + itemsInScroll[i].client.name);
			//itemsInScroll[i].pixelsFromCenter(0, i * 220);
		}
		
	}
	
	void AnimateSlots() {
		for (int i = 0; i < itemsInSlots.Count; i++) {
			var move = itemsInSlots[i].positionTo(0.1f, new Vector2((i+1) * 100, -100), Easing.Quartic.easeOut);
			//var scale = itemsInSlots[i].scaleTo(0.1f, new Vector2(0.5f, 0.5f), Easing.Quartic.easeOut);
		}	
	}

	#endregion
	
	
}

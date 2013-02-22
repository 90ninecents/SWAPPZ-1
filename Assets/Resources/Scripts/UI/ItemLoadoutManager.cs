using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemLoadoutManager : MonoBehaviour {
	
	#region Variables
	public UIToolkit UITextManager;
	public int maxItemsInSlots = 4;
		
	public List<string> inventoryItemNames;
	public List<string> slotItemNames;

	private List<UIButton> itemsInInventory, itemsInSlots;
	private UIHorizontalLayout inventoryContainer, slotContainer;
	
	private UITextInstance headlineText, descriptionText;
	#endregion
	
	#region Initializations
	void Start() {
		
		inventoryItemNames = new List<string>();
		var itemsFromSavedData = SavedData.Inventory.Split(SavedData.Separator[0]);
		foreach (string itemName in itemsFromSavedData)
			inventoryItemNames.Add(itemName);
		
		StaticObjects();
		CreateInventory();
		CreateSlots();
	}
	
	bool dragging = false;
	Vector2 dragStartPos;
	
	void OnEnable() {
		Gesture.onDraggingE += OnDraggingE;
		Gesture.onDraggingEndE += OnDraggingEnd;
	}

	void OnDisable() {
		Gesture.onDraggingE -= OnDraggingE;
	}
	
	void OnDraggingE (DragInfo dragInfo) {
		if (!dragging) {
			dragging = true;
			dragStartPos = dragInfo.pos;
		}
	}
	
	void OnDraggingEnd (Vector2 pos) {
		dragging = false;
		Vector2 direction = (dragStartPos - pos);
		direction.Normalize();
		//Debug.Log(direction);
		if (Mathf.Abs(direction.y) < 0.2) {
			if (Mathf.Sign(direction.x) != 1)
				MoveLeft();
			else
				MoveRight();
		}
	}


	
	/// <summary>
	/// Statics all the buttons and static objects in the scene.
	/// </summary>
	void StaticObjects() {
		
		UIButton leftButton = UIButton.create("LeftButton.png", "LeftButton.png", 0 , 0);
		UIButton rightButton = UIButton.create("RightButton.png", "RightButton.png", 0 , 0);
		UIButton backButton = UIButton.create("BackButton_Up.png", "BackButton_Down.png", 0, 0);
		UIButton nextButton = UIButton.create("NextButton_Up.png", "NextButton_Down.png", 0, 0);
		
		leftButton.positionFromLeft(0, 0);
		rightButton.positionFromRight(0, 0);
		backButton.positionFromBottomLeft(0, 0);
		nextButton.positionFromBottomRight(0, 0);
		
		leftButton.onTouchUpInside += onTouchUpInside;
		rightButton.onTouchUpInside += onTouchUpInside;
		backButton.onTouchUpInside += onTouchUpInside;
		nextButton.onTouchUpInside += onTouchUpInside;
		
		leftButton.client.name = "LeftButton";
		rightButton.client.name = "RightButton";
		backButton.client.name = "BackButton";
		nextButton.client.name = "NextButton";
		
		backButton.scale = new Vector2(0.4f, 0.4f);
		nextButton.scale = new Vector2(0.4f, 0.4f);
		
		UIText textUI = new UIText(UITextManager, "prototype", "prototype.png");
		headlineText = textUI.addTextInstance("hello world", 0, 0);
		headlineText.alignMode = UITextAlignMode.Center;
		headlineText.positionCenter();
		headlineText.position = new Vector2(headlineText.position.x, headlineText.position.y - 150);
		
		descriptionText = textUI.addTextInstance("Neque porro quisquam est qui dolorem ipsum\nquia dolor sit amet, consectetur, adipisci velit..", 0, 0);
		descriptionText.alignMode = UITextAlignMode.Center;
		descriptionText.positionCenter();
		descriptionText.position = new Vector2(descriptionText.position.x, descriptionText.position.y - 200);
	}
	#endregion
	
	#region Inventory Functions
	/// <summary>
	/// Creates the initial items to the inventory, using ItemNames as reference.
	/// </summary>
	void CreateInventory() {
		itemsInInventory = new List<UIButton>();
		inventoryContainer = new UIHorizontalLayout(0);
		
		for (int i = 0; i < inventoryItemNames.Count; i++) {
			UIButton itemButton = UIButton.create (inventoryItemNames[i] + ".png", inventoryItemNames[i] + ".png", 0, 0);
			itemButton.client.name = inventoryItemNames[i];
			itemButton.centerize();
			itemsInInventory.Add(itemButton);
			inventoryContainer.addChild(itemButton);
		}
		
		inventoryContainer.positionCenter();
		
		inventoryContainer.position = new Vector2(inventoryContainer.position.x - 150, inventoryContainer.position.y + 150); 
		inventoryContainer.client.name = "InventoryContainer";
		
		HighlightedButton = itemsInInventory[0];
		HideItemsInInventory();
	}
	
	/// <summary>
	/// Hides the items that are off the maximum items to be listed in inventory.
	/// </summary>
	void HideItemsInInventory() {
		int highlightedIndex = itemsInInventory.IndexOf(HighlightedButton);

		for (int i = 0; i < itemsInInventory.Count; i++) {
			if (!(Util.InRange(i, highlightedIndex - 2, highlightedIndex + 2))) 
				itemsInInventory[i].hidden = true;
			else
				itemsInInventory[i].hidden = false;
		}
	}
	
	private UIButton highlightedButton;
	
	/// <summary>
	/// Gets or sets the highlighted button.
	/// </summary>
	/// <value>
	/// The highlighted button.
	/// </value>
	private UIButton HighlightedButton {
		get { return highlightedButton; }
		set { 
			if (highlightedButton != null)
				highlightedButton.onTouchUpInside -= onTouchUpInside;
			
			highlightedButton = value;
			
			if (highlightedButton != null) {
				highlightedButton.onTouchUpInside += onTouchUpInside;
				highlightedButton.centerize();
				highlightedButton.scaleTo(0.1f, new Vector2(1.2f, 1.2f), Easing.Bounce.easeOut);
				headlineText.text = highlightedButton.client.name;
				headlineText.positionCenter();
				headlineText.position = new Vector2(headlineText.position.x, headlineText.position.y - 150);
			}
			
		}
	}
	
	/// <summary>
	/// Clears all the item buttons from the Inventory and recreates them.
	/// There is no way to insert between buttons in UItoolkit's horzontial panel.
	/// </summary>
	void RefreshInventory() {
		
		for (int i = 0; i < itemsInInventory.Count; i++) {
			if (i != storedNewIndex)
				inventoryContainer.removeChild(itemsInInventory[i], false);
		}
		
		for (int i = 0; i < itemsInInventory.Count; i++) {
			inventoryContainer.addChild(itemsInInventory[i]);	
		}
		HighlightedButton = itemsInInventory[storedNewIndex];
		Debug.Log("New highlighted item:" + highlightedButton.client.name);
		HideItemsInInventory();
		MoveRight();
	}
	#endregion
	
	#region Slot Functions
	/// <summary>
	/// Creates the slot container to hold the items in slot.
	/// </summary>
	void CreateSlots() {
		itemsInSlots = new List<UIButton>();
		slotContainer = new UIHorizontalLayout(0);
		
		slotContainer.client.name = "SlotContainer";
		slotContainer.positionFromTop(0);
		slotContainer.position = new Vector2(slotContainer.position.x - 200, slotContainer.position.y);
	}
	#endregion
	
	#region Event Trigger Functions
	void onTouchUpInside (UIButton uiButton) {
		if (animating)
			return;
		
		switch (uiButton.client.name) {
		case "LeftButton":
			//Move to one step to the left
			MoveLeft();
			break;
			
		case "RightButton":
			//Move to one item from the right
			MoveRight();
			break;
			
		case "BackButton":
			BackButton();
			break;
			
		case "NextButton":
			NextButton();
			break;
			
		default:
			if (uiButton == HighlightedButton)
				MoveItemFromInventoryToSlot(uiButton);
			else if (uiButton != HighlightedButton && itemsInSlots.Contains(uiButton))
				MoveItemFromSlotToInventory(uiButton);
			break;
		}
	}
	#endregion
	
	#region Static Button Functions
	private bool animating;
	/// <summary>
	/// Moves the inventory slot to the left.
	/// </summary>
	void MoveLeft() {
		int indexOfPreviousItem = itemsInInventory.IndexOf(HighlightedButton) - 1;
		if (Util.InRange(indexOfPreviousItem, -1, itemsInInventory.Count)) {
			animating = true;
			highlightedButton.scaleTo(0.2f, Vector3.one, Easing.Bounce.easeOut);
			// Shows the 2nd item after the highlighted item.
			int indexOfPreviousVisibleItem = itemsInInventory.IndexOf(HighlightedButton) - 2;
			if (Util.InRange(indexOfPreviousVisibleItem, -1, itemsInInventory.Count))
				itemsInInventory[indexOfPreviousVisibleItem].hidden = false;
			
			var moveAnim = inventoryContainer.positionTo(0.05f, new Vector2(inventoryContainer.position.x + 200, inventoryContainer.position.y), Easing.Quartic.easeOut);
			moveAnim.onComplete = () => { 
				HighlightedButton = itemsInInventory[indexOfPreviousItem];
				HideItemsInInventory();
				animating = false;
				//Debug.Log("Highlighted item: " + HighlightedButton.client.name);
			};
		}
	}
	
	/// <summary>
	/// Moves the inventory slot to the right.
	/// </summary>
	void MoveRight() {
		int indexOfNextItem = itemsInInventory.IndexOf(HighlightedButton) + 1;

		if (Util.InRange(indexOfNextItem, 0, itemsInInventory.Count)) {
			animating = true;
			highlightedButton.scaleTo(0.2f, Vector3.one, Easing.Bounce.easeOut);

			// Shows the 2nd item after the highlighted item.
			int indexOfNextVisibleItem = itemsInInventory.IndexOf(HighlightedButton) + 2;
			if (Util.InRange(indexOfNextVisibleItem, 0, itemsInInventory.Count))
				itemsInInventory[indexOfNextVisibleItem].hidden = false;
			
			
			var moveAnim = inventoryContainer.positionTo(0.05f, new Vector2(inventoryContainer.position.x - 200, inventoryContainer.position.y), Easing.Quartic.easeOut);
			moveAnim.onComplete = () => { 
				HighlightedButton = itemsInInventory[indexOfNextItem];
				HideItemsInInventory();
				animating = false;
				//Debug.Log("Highlighted item: " + HighlightedButton.client.name);
			};
		}
	}
	
	/// <summary>
	/// Back button function, takes to the previous button.
	/// </summary>
	void BackButton() {
		
	}
	
	string itemsToLoadout;
	/// <summary>
	/// Saves the inventory list and also the inventory slot.
	/// </summary>
	void NextButton() {
		foreach (UIButton item in itemsInSlots) {
			itemsToLoadout += item.client.name;
			if (itemsInSlots.IndexOf(item) != itemsInSlots.Count - 1)
				itemsToLoadout += "|";
		}
		SavedData.ItemLoadout = itemsToLoadout;
	}
	#endregion
	
	#region Common Functions
	
	int storedNewIndex;
	
	/// <summary>
	/// Moves the item from slot to inventory.
	/// </summary>
	/// <param name='item'>
	/// Item.
	/// </param>
	void MoveItemFromSlotToInventory(UIButton item) {
		Debug.Log("Item to be moved back: " + item.client.name);
		highlightedButton.scale = Vector3.one;
		int indexOfHighlightedItem = itemsInInventory.IndexOf(HighlightedButton);
		
		itemsInInventory.Insert(indexOfHighlightedItem, item);
		inventoryItemNames.Insert(indexOfHighlightedItem, item.client.name);
		slotItemNames.Remove(item.client.name);
		
		storedNewIndex = itemsInInventory.IndexOf(item);
		
		item.onTouchUpInside -= onTouchUpInside;
		
		itemsInSlots.Remove(item);
		slotContainer.removeChild(item, false);
		RefreshInventory();
	}
	
	/// <summary>
	/// Moves the item from inventory to slot.
	/// </summary>
	/// <param name='item'>
	/// Item.
	/// </param>
	void MoveItemFromInventoryToSlot(UIButton item) {
		if (itemsInSlots.Count >= maxItemsInSlots)
			return;
		
		int indexOfItemNext = itemsInInventory.IndexOf(HighlightedButton) + 1;
		int indexOfItemPrevious = itemsInInventory.IndexOf(HighlightedButton) - 1;
		
		if (Util.InRange(indexOfItemNext , 0, itemsInInventory.Count))
			HighlightedButton = itemsInInventory[indexOfItemNext ];
		else if (Util.InRange(indexOfItemPrevious, -1, itemsInInventory.Count)) {
			HighlightedButton = itemsInInventory[indexOfItemPrevious];
			inventoryContainer.positionTo(0.05f, new Vector2(inventoryContainer.position.x + 200, inventoryContainer.position.y), Easing.Quartic.easeOut);
		}
			
		inventoryContainer.removeChild(item, false);
		itemsInInventory.Remove(item);
		
		inventoryItemNames.Remove(item.client.name);
		slotItemNames.Add(item.client.name);
		
		slotContainer.addChild(item);
		itemsInSlots.Add(item);
		item.scaleTo(0.1f, new Vector2(0.5f, 0.5f), Easing.Circular.easeInOut).onComplete = () => {
			item.onTouchUpInside += onTouchUpInside;	
		};
		HideItemsInInventory();
	}
	#endregion
}
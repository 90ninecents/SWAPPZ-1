using UnityEngine;
using System.Collections;

public class InventoryPanel : MonoBehaviour {
	 /*
	  * This class manages a list of transforms that are dragged onto it and arranges them
	  * in a series of rows and columns as specified in the inspector
	  */
	
	public GameObject lockedPopup;
	public GameObject unlockedPopup;
	
	public int rows = 2;
	public int columns = 2;
	
	public int maxSelectable = 4;
	
	public float sizeIncrease = 1.25f;
	
	Vector2[] slots;
	Transform[] items;
	Transform[] selectableItems;
	Transform[] selectedItems;
	
	Transform lastAdded;
	Transform lastRemoved;
	int itemCount = 0;
	
	public int capacity = -1;		// allows the final rows*columns-capacity slots to become "locked" (unable to accept items)
	
	public bool IsFull 				 { get { return (itemCount == capacity); } }
	public Transform[] Items 		 { get { return items; } }
	public Transform[] SelectedItems { get { return selectedItems; } }
	
	public Transform LastRemoved 	 { get { return lastRemoved; } }
	public Transform LastAdded 		 { get { return lastAdded; } }
	
	void Awake() {
		int numItems = rows*columns;
		
		if (capacity == -1) capacity = numItems;
		
		items = new Transform[numItems];
		selectedItems = new Transform[numItems];
		selectableItems = new Transform[numItems];
		slots = new Vector2[numItems];
		
		float xSpace = (transform.collider.bounds.extents.x*2)/columns;
		float ySpace = (transform.collider.bounds.extents.y*2)/rows;
		
		float xStart = transform.position.x-(transform.collider.bounds.extents.x)+(xSpace/2);
		float yStart = transform.position.y+(transform.collider.bounds.extents.y)-(ySpace/2);
		
		int count = 0;
		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < columns; c++) {
				slots[count] = new Vector2(xStart + c*xSpace, yStart - r*ySpace);
				count++;
			}
		}
	}
	
	void OnEnable () {
		Gesture.onTouchUpE += OnTouchUp;
		
		if (lockedPopup != null && lockedPopup.active == true) lockedPopup.SetActiveRecursively(false);
		if (unlockedPopup != null && unlockedPopup.active == true) unlockedPopup.SetActiveRecursively(false);
	}
	
	void OnDisable () {
		Gesture.onTouchUpE -= OnTouchUp;
	}
	
	void OnTouchUp(Vector2 touchPos) {
		Ray ray = Camera.main.ScreenPointToRay(touchPos);
		RaycastHit hit;
			
		if (Physics.Raycast(ray, out hit, 1500)) {
			bool go = false;
			// Check if hit object is selectable
			foreach (Transform t in selectableItems) {
				if (t == hit.transform) {
					go = true;
					break;
				}
			}
			
			if (go) {
				bool found = false;
				
				// If already selected, deselect
				for (int i = 0; i < selectedItems.Length; i++) {
					if (hit.transform == selectedItems[i]) {
						selectedItems[i].localScale /= sizeIncrease;
						selectedItems[i] = null;
						found = true;
						
						if (unlockedPopup != null) unlockedPopup.SetActiveRecursively(false);
						
						break;
					}
				}
				
				// If not already selected, select
				if (!found) {
					for (int i = 0; i < items.Length; i++) {
						if (hit.transform == items[i]) {
							items[i].localScale *= sizeIncrease;
							selectedItems[i] = items[i];
							
							AudioManager.PlayAudio("ButtonSelect", "Effects");
							
							break;
						}
					}
					
					// popup on selectable tap
					if (unlockedPopup != null) unlockedPopup.SetActiveRecursively(true);
				}
			}
			
			else {
				// popup on unselectable tap
				if (lockedPopup != null) lockedPopup.SetActiveRecursively(true);
			}			
		}
	}
	
	public int AddItem(Transform item, bool selectable = true) {
		// Returns the index of the newly added item
		int index = -1;
		
		if (item != null) {
			for (int i = 0; i < items.Length; i++) {
				if (items[i] == null) {
					index = i;
					break;
				}
			}
			
			// if empty slot found, add to slot
			if (index >= 0) {Vector3 pos = new Vector3(slots[index].x, slots[index].y, item.position.z);
				item.transform.position = pos;
				
				Vector3 diff = item.transform.collider.bounds.center - pos;
				
				item.transform.Translate(diff);
				
				items[index] = item;
				if (selectable) selectableItems[index] = item;
				
				itemCount++;
				
				lastAdded = item;
				//item.parent = transform;
			}
		}
		
		return index;
	}
	
	public void RemoveItem(Transform item) {
		// find item's slot position
		for (int i = 0; i < items.Length; i++) {
			if (items[i] == item) {
				// make slot available
				items[i] = null;
				
				itemCount--;
				lastRemoved = item;
				break;
			}
		}
	}
	
	void Update() {
		
	}
}

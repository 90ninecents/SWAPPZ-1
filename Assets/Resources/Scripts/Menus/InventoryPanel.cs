using UnityEngine;
using System.Collections;

public class InventoryPanel : MonoBehaviour {
	 /*
	  * This class manages a list of transforms that are dragged onto it and arranges them
	  * in a series of rows and columns as specified in the inspector
	  */
	
	//debugging
	public int rows = 2;
	public int columns = 2;
	
	public int maxSelectable = 4;
	
	public float sizeIncrease = 1.25f;
	
	Vector2[] slots;
	Transform[] items;
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
		slots = new Vector2[numItems];
		
		float xSpace = transform.localScale.x/columns;
		float ySpace = transform.localScale.y/rows;
		
		float xStart = transform.position.x-(transform.localScale.x/2)+(xSpace/2);
		float yStart = transform.position.y+(transform.localScale.y/2)-(ySpace/2);
		
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
	}
	
	void OnDisable () {
		Gesture.onTouchUpE -= OnTouchUp;
	}
	
	void OnTouchUp(Vector2 touchPos) {
		Ray ray = Camera.main.ScreenPointToRay(touchPos);
		RaycastHit hit;
			
		if (Physics.Raycast(ray, out hit, 1000)) {
			bool found = false;
			
			for (int i = 0; i < selectedItems.Length; i++) {
				if (hit.transform == selectedItems[i]) {
					selectedItems[i].localScale /= sizeIncrease;
					selectedItems[i] = null;
					found = true;
					break;
				}
			}
			
			
			if (!found) {
				for (int i = 0; i < items.Length; i++) {
					if (hit.transform == items[i]) {
						items[i].localScale *= sizeIncrease;
						selectedItems[i] = items[i];
						
						break;
					}
				}
			}
		}
		
//		foreach (Transform t in selectedItems) {
//			if (t!=null) print (t.name);
//			else print ("null");
//		}
	}
	
	public int AddItem(Transform item) {
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
			if (index >= 0) {				
				item.position = new Vector3(slots[index].x, slots[index].y, item.position.z);
				
				items[index] = item;
				itemCount++;
				
				lastAdded = item;
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
}

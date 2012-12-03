using UnityEngine;
using System.Collections;

public class InventoryPanel : MonoBehaviour {
	 /*
	  * This class manages a list of transforms that are dragged onto it and arranges them
	  * in a series of rows and columns as specified in the inspector
	  */
	
	// debugging
	int screenWidth = 960;
	int screenHeight = 640;
	
	public int rows = 2;
	public int columns = 2;
	
	Vector2[] slots;
	Transform[] items;
	
	public bool populateOnStartup = false;
	
	Transform lastAdded;
	Transform lastRemoved;
	int itemCount = 0;
	
	public int capacity = -1;		// allows the final rows*columns-capacity slots to become "locked" (unable to accept items)
	
	public bool IsFull { get { return (itemCount == capacity); } }
	public Transform[] Items { get { return items; } }
	
	public Transform LastRemoved { get { return lastRemoved; } }
	public Transform LastAdded { get { return lastAdded; } }
	
	public Transform[] connections;		// Other Inventory panels that can "communicate" with this one
	bool accepting = true;
	
	void Awake() {
		int numItems = rows*columns;
		
		if (capacity == -1) capacity = numItems;
		
		items = new Transform[numItems];
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
	
	void OnEnable() {
		Gesture.onTouchDownE += OnTouchDown;
		Gesture.onTouchUpE += OnTouchUp;
	}
	
	void OnDisable() {
		Gesture.onTouchDownE -= OnTouchDown;
		Gesture.onTouchUpE -= OnTouchUp;
	}
	
	void OnTouchDown(Vector2 point) {		
		// World coordinates
		//Vector2 pos = new Vector2(point.x - Screen.width/2, point.y - Screen.height/2);
		Vector2 pos = new Vector2(point.x - screenWidth/2, point.y - screenHeight/2);
		
		// if touch occured inside this panel:
		if (transform.renderer.bounds.Contains(new Vector3(pos.x, pos.y, transform.position.z))) {
			accepting = true;
			
													// Screen coordinates
			Ray ray = Camera.main.ScreenPointToRay(point);
			RaycastHit hit;
			
			// If touch occured over a collider other than this panel's:
			if (Physics.Raycast(ray, out hit, 1000)) {
				// Remove dragged item from panel
				if (hit.transform != transform) RemoveItem(hit.transform);
			}
		}
		else {
			accepting = false;
			
			foreach (Transform t in connections) {
				if (t.renderer.bounds.Contains(new Vector3(pos.x, pos.y, t.position.z))) {
					accepting = true;
				}
			}
		}
	}
	
	void OnTouchUp(Vector2 point) {
		if (Camera.main != null) {
			Vector2 pos = new Vector2(point.x - screenWidth/2, point.y - screenHeight/2);
			
			Ray ray = Camera.main.ScreenPointToRay(point);
			RaycastHit[] hits = Physics.RaycastAll(ray, 1000);
				
			
			// if release occured inside this panel:
			if (transform.renderer.bounds.Contains(new Vector3(pos.x, pos.y, transform.position.z))) {
				Transform addItem = null;
				int swapIndex = -1;
				
				foreach (RaycastHit hit in hits) {
					// If release occured over a collider other than this panel's:
					if (hit.transform != transform) {
						// Find hit collider that matches new item (handles overlap with existing items)
						bool go = true;
						
						
						// Don't add anything under the touch event that is already part of this inventory panel
						int count = 0;
						foreach (Transform item in items) {
							if (hit.transform == item) {
								go = false;
								swapIndex = count;
							}
							count++;
						}
						
						if (go && accepting) {
							// Add dragged item to panel
							addItem = hit.transform;
						}
					}
				}
				
				if (addItem) {
					int addIndex = AddItem(addItem);
					
					if (swapIndex > -1 && addItem == lastRemoved) {
						Transform swapItem = items[swapIndex];
						
						items[swapIndex] = addItem;
						items[addIndex]  = swapItem;
						
						addItem.position  = new Vector3(slots[swapIndex].x, slots[swapIndex].y, addItem.position.z);
						swapItem.position = new Vector3(slots[addIndex].x, slots[addIndex].y, swapItem.position.z);
					}
				}
				
				accepting = false;
			}
			
			// if release occured outside the panel and did not occur inside another (non-full) inventory panel, retrieve item
			else if (lastRemoved != null) {
				bool retrieve = true;
				
				foreach (RaycastHit hit in hits) {
					InventoryPanel otherPanel = hit.transform.GetComponent<InventoryPanel>();
					if (otherPanel != null) {
						if (!otherPanel.IsFull || otherPanel.lastAdded == lastRemoved) {
							foreach (Transform t in connections) {
								if (t == otherPanel.transform) {
									retrieve = false;
									break;	
								}
							}
						}
					}
				}
				
				if (retrieve) AddItem(lastRemoved);
				lastRemoved = null;
			}
		}
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
				print (item.name+" - "+items[i]);
				
				itemCount--;
				lastRemoved = item;
				break;
			}
		}
	}
}

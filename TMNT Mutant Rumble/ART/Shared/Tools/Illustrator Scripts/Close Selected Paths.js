//	Close Selected Paths.js
//

if (documents.length > 0 && activeDocument.pathItems.length > 0){


	myObj = activeDocument.pathItems;
	myNum = activeDocument.pathItems.length;

	for ( i = 0; i < myNum; i++ ){
	
		if( myObj[ i ].selected && ! myObj[ i ].closed ){

			myObj[ i ].closed = true;

		}
 	}
}














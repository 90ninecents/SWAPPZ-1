////////////////////////////////////////////////////////////
// ------------------------
// -=> Close All Paths <=-
// ------------------------
//
// A Javascript for Adobe Illustrator
//
// 21.6.2004
//
// This script closes all (or all selected) open paths. It does not
// connect touching paths. 
//

//$.bp();

// -------------------------------------------------------------------

var warning_limit = 400;

// -------------------------------------------------------------------

var X="Close All Paths\n\n"; {

  var MSG_asksel = X+"Close all selected open path-items?";
  var MSG_ask = X+"Close all open path-items in this document.";
  var MSG_allclosed = X+"There are no open path-items in this document.";
  var MSG_allselclosed = X+"The selection does not contain any open path-item.";
  var MSG_nopath = X+"You have not selected any path-item.";
  var MSG_nodocs = X+"You have no open document."
  var MSG_warning = X+"The document will be analyzed for open path-items ...";
  var MSG_status = X+"Closed path-items: ";

}

var error=0;
var locked = false;
var proccessedItems = 0;

if (documents.length<1) {
  error++;
  alert(MSG_nodocs);
} else {
  {
    var allPaths = activeDocument.pathItems;
    if (selection.length > 0) {
      var onlySelection = 1;
      var selcount = 0;
      for (var i = 0; i < selection.lenght; i++)
      {
        if (selection[i].typename == "PathItem") { selcount++; }
      }
      if (selcount == selection.lenght)
      {
        var allPaths = selection;
      }
    } else {
      var onlySelection = 0;
    }
    if (allPaths.length > warning_limit ) { alert (MSG_warning+ " ("+allPaths.length+")"); }
    var count=0;
    for (var i=0; i < allPaths.length; i++) {
      locked = false;
      isLocked(allPaths[i]);
      if ((allPaths[i].selected == true || onlySelection == 0) && !locked && !allPaths[i].layer.locked )
      {
        if( !allPaths[i].closed) { count++; }
      }
    }
    if (count == 0) {
      error++;
      if (selection.length > 0) { alert(MSG_allselclosed); } else { alert(MSG_allclosed); }
    }

    if (allPaths.length < 0) { error++; alert(MSG_nopath); }
    if (error < 1) {
      if (selection.length > 0) {
        var confirmed = confirm(MSG_asksel + " ("+count+")" );
      } else {
        var confirmed = confirm(MSG_ask + " ("+count+")");
      }
      if (confirmed) {
        close(allPaths);
        if (proccessedItems != count) {
          alert(MSG_status+proccessedItems+"/"+count);
        } else {
          alert(MSG_status+proccessedItems);
        }
      }
    }
  }
}


function close(thePaths) {
  for (var i = 0; i < thePaths.length; i++) {

    if ((thePaths[i].selected == true || onlySelection == 0) && !thePaths[i].closed  )
    {
      try
      {
        thePaths[i].closed=true;
        proccessedItems++;
      } catch (e) { }
    }
  }
}

function isLocked (test) {
  if (test.typename == "Layer")
  {
    if (!locked) { locked = test.locked; }
    if (!locked) { locked = test.hidden; }
  } else {
    if (test.typename != "Layer" && test.locked == true)
    {
      if (!locked) { locked = test.locked; }
      if (!locked) { locked = test.hidden; }
    } else {
      isLocked(test.parent);
    }
  }
}
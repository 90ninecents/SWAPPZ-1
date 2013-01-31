// Path Length

// finds out length of the each selected path
// and total length of the selected pathes
// then write out them on the artboard as text object

// This script uses JavaScript's "length" property of PathItem.
// if it is available (= CS3 or later).
// You can force calculate the length by "use_native_property"
// setting set to false. ( see below )

// NOTE:
// The return values of "PathItem.length" property and the function in this script
// are slightly different especially in complex pathes.
// It seems that the difference is 0.05 mm at most.


// JavaScript Script for Adobe Illustrator CS3
// Tested with Adobe Illustrator CS3 13.0.3, Windows XP SP2 (Japanese version).
// This script provided "as is" without warranty of any kind.
// Free to use and distribute.

// Copyright(c) 2009 SATO Hiroyuki
// http://park12.wakwak.com/~shp/lc/et/en_aics_script.html

// 2009-05-23

var ver10  = (version.indexOf('10') == 0);
var verCS1 = (version.indexOf('11') == 0);
var verCS2 = (version.indexOf('12') == 0);

//var tim = new Date();
main();
//alert(new Date() - tim);

function main(){
  // Settings =================================

  // use "PathItem.length" property if CS3 or later
  var use_native_property = true;

  var font_size  = 12;
  var font_name  = "TimesNewRomanPSMT";
  var digit    = 2;                   // number of digits after decimal point (round off last digit)
  var use_mm_4_unit = true;           // use millimeter as unit: true/false(use point)
  
  var div_num  = 1024;

  // ==========================================
  if(ver10 || verCS1 || verCS2){
    use_native_property = false;
  }
  
  if (documents.length<1){
    return;
  }
  var sel = activeDocument.selection;
  if (!(sel instanceof Array) || sel.length<1){
    return;
  }

  var selected_pathes = [];
  extractPathes(sel, 1, selected_pathes);
  if(selected_pathes.length<1){
    return;
  }
  
  activateEditableLayer(selected_pathes[0]);
  
  var path_length = 0;
  var all_pathes_length = 0;
  
  var unit = use_mm_4_unit ? "mm" : "";
  var position_to_write_result;
  var i, j, k;
  var path_points, segment_length;
  
  for(i = 0; i < selected_pathes.length; i++){
    if(use_native_property){
      path_length = selected_pathes[i].length;
    } else {
      path_points = selected_pathes[i].pathPoints;
      for(j = 0; j < path_points.length; j++){
        if(j == path_points.length - 1){
          if(selected_pathes[i].closed){
            k = 0;
          } else {
            break;
          }
        } else {
          k = j + 1;
        }
        segment_length = getLength([path_points[j].anchor,        path_points[j].rightDirection,
                                    path_points[k].leftDirection, path_points[k].anchor],
                                   div_num);
        
        path_length += segment_length;
      }
    }
    all_pathes_length += path_length;

    // write out the result
    if(use_mm_4_unit){
      path_length = pt2mm(path_length); // convert to millimeter
    }
    position_to_write_result = findCenter( selected_pathes[i] );
    writeResultAsText(fixedTo(path_length, digit) + unit,
                      font_name,
                      font_size,
                      position_to_write_result);
    path_length = 0;
  }

  // write out the total length
  if(selected_pathes.length > 1){
    if( use_mm_4_unit ){
      all_pathes_length = pt2mm( all_pathes_length ); // convert to millimeter
    }
    position_to_write_result[1] -= font_size;
    writeResultAsText("all: " + fixedTo(all_pathes_length, digit) + unit,
                      font_name,
                      font_size,
                      position_to_write_result);
  }
}

// ------------------------------------------------
// return the segment length
// segment = part of a path between 2 anchors
// q = [Q0[x,y],Q1,Q2,Q3], div_num = division number
// Simpson's method : with simplified coefficients to speed-up
function getLength(q, div_num){
  var div_unit = 1 / div_num; 
  var m = [q[3][0] - q[0][0] + 3 * (q[1][0] - q[2][0]),
           q[0][0] - 2 * q[1][0] + q[2][0],
           q[1][0] - q[0][0]];
  var n = [q[3][1] - q[0][1] + 3 * (q[1][1] - q[2][1]),
           q[0][1] - 2 * q[1][1] + q[2][1],
           q[1][1] - q[0][1]];
  var k = [m[0] * m[0] + n[0] * n[0],
           4 * (m[0] * m[1] + n[0] * n[1]),
           2 * ((m[0] * m[2] + n[0] * n[2]) + 2 * (m[1] * m[1] + n[1] * n[1])),
           4 * (m[1] * m[2] + n[1] * n[2]),
           m[2] * m[2] + n[2] * n[2]];
  var fc = function(t, k){
    return Math.sqrt(t * ( t * ( t * ( t * k[0] + k[1]) + k[2]) + k[3]) + k[4]) || 0;
  };
  var total = 0;
  var i;
  for(i = 1; i < div_num; i += 2){
    total += fc(i * div_unit, k);
  }
  total *= 2;
  for(i = 2; i < div_num; i += 2){
    total += fc(i * div_unit, k);
  }
  return (fc(0, k) + fc(1, k) + total * 2) * div_unit;
}

// ------------------------------------------------
// less simplified Simpson's method (for verify)
function getLength2(q, div_num){
  var div_unit = 1 / div_num;
  var m = [q[3][0] - q[0][0] + 3 * (q[1][0] - q[2][0]),
           3 * (q[0][0] - 2 * q[1][0] + q[2][0]),
           3 * (q[1][0] - q[0][0])];
  var n = [q[3][1] - q[0][1] + 3 * (q[1][1] - q[2][1]),
           3 * (q[0][1] - 2 * q[1][1] + q[2][1]),
           3 * (q[1][1] - q[0][1])];
  var fc = function(t, m, n){
    return Math.sqrt(Math.pow(3*t*t*m[0] + 2*t*m[1] + m[2], 2)
                     + Math.pow(3*t*t*n[0] + 2*t*n[1] + n[2], 2)) || 0;
  };
  var total = 0;
  var i;
  for(i = 1; i < div_num; i += 2){
    total += 4.0 * fc(i * div_unit, m, n);
  }
  for(i = 2; i< div_num; i+= 2){
    total += 2.0 * fc(i * div_unit, m, n);
  }
  return (fc(0, m, n) + fc(1, m, n) + total) * div_unit / 3;
}
// ------------------------------------------------
// convert PostScript point to millimeter
function pt2mm(n){
  return n * 0.35277778;
}

// ------------------------------------------------
// writes out "str" as a Text object.
// AI10 compatibility is experimental
function writeResultAsText(str, nam, siz, posi){
  if(ver10){
    var tx = activeDocument.activeLayer.textArtItems.add();
    with(tx){
      contents = str;
      with(textRange()){
        font = nam;
        size = siz;
      }
      position = [posi[0]-width/2, posi[1]+height/2];
    }
  } else {
    var tx = activeDocument.activeLayer.textFrames.add();
    with(tx){
      contents = str;
      with(textRange){
        with(characterAttributes){
          size = siz;
          textFont = textFonts.getByName(nam);
        }
        with(paragraphAttributes){
          justification = Justification.LEFT;
          autoLeadingAmount = 120;
        }
      }
      position = [posi[0]-width/2, posi[1]+height/2];
    }
  }
}

// ------------------------------------------------
// find out the center[x, y] of the PageItem
function findCenter(pi){
  var gb = pi.geometricBounds; // left, top, right, bottom
  return [(gb[0] + gb[2]) / 2, (gb[1] + gb[3]) / 2];
}

// --------------------------------------
function extractPathes(s, pp_length_limit, pathes){
  for(var i = 0; i < s.length; i++){
    if(s[i].typename == "PathItem"
       && !s[i].guides && !s[i].clipping){
      if(pp_length_limit
         && s[i].pathPoints.length <= pp_length_limit){
        continue;
      }
      pathes.push(s[i]);
    } else if(s[i].typename == "GroupItem"){
      extractPathes(s[i].pageItems, pp_length_limit, pathes);
    } else if(s[i].typename == "CompoundPathItem"){
      extractPathes(s[i].pathItems, pp_length_limit , pathes);
    }
  }
}

// ------------------------------------------------
// notify 1st character's font name in the selected text object
function getFontName(){
  if (documents.length<1){
    return;
  }
  var s = activeDocument.selection;
  var text_object = ver10 ? "TextArtItem" : "TextFrame";
  if (!(s instanceof Array)
      || s.length<1
      || s[0].typename != text_object
      || s[0].contents.length<1){
    alert("Usage: Select a text object, then run this script");
  } else if(ver10){
    alert(activeDocument.selection[0].textRange(0,0).font);
  } else {
    alert(activeDocument.selection[0].textRange.characters[0].textFont.name);
  }
}

// ----------------------------------------------
function activateEditableLayer(pi){
  var lay = activeDocument.activeLayer;
  if(lay.locked || !lay.visible){
    activeDocument.activeLayer = pi.layer;
  }
}

// ------------------------------------------------
// It seems that "toFixed" is not available in AI10
function fixedTo(n, k){
  var m = Math.pow(10 ,k);
  var s = (Math.round(n * m)) + "";
  if(k <= 0){
    return s;
  }
  while(s.length < k + 1){
    s = "0" + s;
  }
  var len = s.length - k;
  s = s.substr(0, len) + "." + s.substr(len, k);
  s = s.replace(/0+$/, "");
  s = s.replace(/\.$/, "");
  return s;
}

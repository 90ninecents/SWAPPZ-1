// uzumaki (mosquito coil type)

// draws an (approximate) archimedes' spiral

// JavaScript Script for Adobe Illustrator CS3
// Tested with Adobe Illustrator CS3 13.0.3, Windows XP SP2 (Japanese version).
// This script provided "as is" without warranty of any kind.
// Free to use and distribute.

// Copyright(c) 2009 SATO Hiroyuki
// http://park12.wakwak.com/~shp/lc/et/en_aics_script.html

// 2009-05-23

var mpi = Math.PI; // Math.PI
var hpi = mpi / 2;
// Settings  ============================

var radius_increment = 5; // radius-increment per round (point)

var uzu_angle = 15 /180 * mpi; // unit of rounding angle (radian)

var uzu_roundtime = 10; // rounding times

// =================================
var han = 4 / 3 * (1 - Math.cos( uzu_angle / 2 )) / Math.sin( uzu_angle / 2 );

uzumaki();
// ----------------------------------------------
function uzumaki(){
  if(documents.length < 1) return;
  var lay = activeDocument.activeLayer;
  
  if(lay.locked || !lay.visible){
    alert("Please select an unlocked and visible layer,\nthen run this script again.");
    return;
  }
  
  var angle_rate = Math.abs(uzu_angle / (2 * mpi));
  radius_increment *= angle_rate;
  uzu_roundtime = uzu_roundtime / angle_rate + 2;

  // makes an array for anchors
  var wi;
  var pnts = [[0,0]];
  var hans = [0];
  
  for(var i = 1; i < uzu_roundtime; i++){
    wi = radius_increment * i;
    pnts.push( pnt4angle(uzu_angle * i, wi) );
    hans.push( wi * han );
  }

  // creates a path
  var pi = lay.pathItems.add();
  with(pi){
    // properties of the spiral
    closed = false;
    filled = false;
    stroked = true;
    strokeWidth = 1;
    //    strokeColor ...
  }
  pi.setEntirePath(pnts);
  var p = pi.pathPoints;
  p[p.length-1].remove();
  uzu_roundtime -= 1;
  
  // specifies handles
  var ti, hPnt;
  
  for(i = 1; i < uzu_roundtime; i++){
    ti = getRad(pnts[i-1], pnts[i+1]);
    hPnt = pnt4angle(ti, hans[i]);
    with(p[i]){
      rightDirection = addPnt(pnts[i], hPnt);
      leftDirection  = subPnt(pnts[i], hPnt);
      pointType = PointType.SMOOTH;
    }
  }
  
  // translate
  pi.translate(activeDocument.width / 2, activeDocument.height / 2);
}
// ----------------------------------------------
function getRad(p1, p2) {
  return Math.atan2(p2[1] - p1[1], p2[0] - p1[0]);
}
// ----------------------------------------------
function pnt4angle(t, m){
  return [Math.cos(t) * m, Math.sin(t) * m];
}
// ----------------------------------------------
function addPnt(p1, p2){
  return [p1[0] + p2[0], p1[1] + p2[1]];
}
// ----------------------------------------------
function subPnt(p1, p2){
  return [p1[0] - p2[0], p1[1] - p2[1]];
}

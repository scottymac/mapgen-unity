using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// Factory class to build the 'inside' function that tells us whether
// a point should be on the island or in the water.

class IslandShape {
  // This class has factory functions for generating islands of
  // different shapes. The factory returns a function that takes a
  // normalized point (x and y are -1 to +1) and returns true if the
  // point should be on the island, and false if it should be water
  // (lake or ocean).

  
  // The radial island radius is based on overlapping sine waves 
  static public var ISLAND_FACTOR:Number = 1.07;  // 1.0 means no small islands; 2.0 leads to a lot
  static public function makeRadial(seed:int):Function {
    var islandRandom:PM_PRNG = new PM_PRNG();
    islandRandom.seed = seed;
    var bumps:int = islandRandom.nextIntRange(1, 6);
    var startAngle:Number = islandRandom.nextDoubleRange(0, 2*Math.PI);
    var dipAngle:Number = islandRandom.nextDoubleRange(0, 2*Math.PI);
    var dipWidth:Number = islandRandom.nextDoubleRange(0.2, 0.7);
    
    function inside(q:Point):Boolean {
      var angle:Number = Math.atan2(q.y, q.x);
      var length:Number = 0.5 * (Math.max(Math.abs(q.x), Math.abs(q.y)) + q.length);

      var r1:Number = 0.5 + 0.40*Math.sin(startAngle + bumps*angle + Math.cos((bumps+3)*angle));
      var r2:Number = 0.7 - 0.20*Math.sin(startAngle + bumps*angle - Math.sin((bumps+2)*angle));
      if (Math.abs(angle - dipAngle) < dipWidth
          || Math.abs(angle - dipAngle + 2*Math.PI) < dipWidth
          || Math.abs(angle - dipAngle - 2*Math.PI) < dipWidth) {
        r1 = r2 = 0.2;
      }
      return  (length < r1 || (length > r1*ISLAND_FACTOR && length < r2));
    }

    return inside;
  }


  // The Perlin-based island combines perlin noise with the radius
  static public function makePerlin(seed:int):Function {
    var perlin:BitmapData = new BitmapData(256, 256);
    perlin.perlinNoise(64, 64, 8, seed, false, true);
    
    return function (q:Point):Boolean {
      var c:Number = (perlin.getPixel(int((q.x+1)*128), int((q.y+1)*128)) & 0xff) / 255.0;
      return c > (0.3+0.3*q.length*q.length);
    };
  }


  // The square shape fills the entire space with land
  static public function makeSquare(seed:int):Function {
    return function (q:Point):Boolean {
      return true;
    };
  }


  // The blob island is shaped like Amit's blob logo
  static public function makeBlob(seed:int):Function {
    return function(q:Point):Boolean {
      var eye1:Boolean = new Point(q.x-0.2, q.y/2+0.2).length < 0.05;
      var eye2:Boolean = new Point(q.x+0.2, q.y/2+0.2).length < 0.05;
      var body:Boolean = q.length < 0.8 - 0.18*Math.sin(5*Math.atan2(q.y, q.x));
      return body && !eye1 && !eye2;
    };
  }


}
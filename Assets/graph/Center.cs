using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Center {

	public int index;
  
    public Vector2 point;  // location
    public bool water;  // lake or ocean
    public bool ocean;  // ocean
    public bool coast;  // land polygon touching an ocean
    public bool border;  // at the edge of the map
    public string biome;  // biome type (see article)
    public double elevation;  // 0.0-1.0
    public double moisture;  // 0.0-1.0

    public List<Center> neighbors;
    public List<Edge> borders;
    public List<Corner> corners;

}

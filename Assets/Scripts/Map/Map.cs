using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Map : MonoBehaviour {
    [System.NonSerialized] public int difficulty = 0;

    [HideInInspector]
    public List<MapElement> mapElements = new List<MapElement>();

    public void ResetMap() {
        for(int i = 0; i < mapElements.Count; i++) {
            if (mapElements[i] != null)
                mapElements[i].ResetElement();
        }
    }

}

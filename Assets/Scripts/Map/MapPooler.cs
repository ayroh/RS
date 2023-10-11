using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapPooler : MonoBehaviour
{
    [SerializeField] private MapSO mapSO;
    private List<Map> maps;

    private void Awake() {
        Create();
    }

    private void Create() {
        maps = new List<Map>();
        for (int i = 0;i < mapSO.maps.Count;i++) {
            maps.Add(Instantiate(mapSO.maps[i], transform));
            maps[i].gameObject.SetActive(false);
        }
    }

    public Map Get() {
        Map map;
        while ((map = maps[Random.Range(0, maps.Count)]).gameObject.activeSelf) ;
        map.gameObject.SetActive(true);
        map.ResetMap();
        return map;
    }

    public void Release(Map map) {
        map.gameObject.SetActive(false);
    }

}

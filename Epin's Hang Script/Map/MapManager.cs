using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    static private MapManager instance = null;
    static public MapManager GetInstance
    {
        get
        {
            return instance;
        }
    }

    public List<GameObject> maps;
    public float firstFloorYPos = 0;
    public float middleFloorYPos = 4.3686f;
    public float secondFloorYPos = 8.7716f;

    private void Awake()
    {
        if(instance)
        {
            DestroyImmediate(this);
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundGhost.GetInstance.Spawn();
        }
    }
}
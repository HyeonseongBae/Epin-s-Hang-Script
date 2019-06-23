using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColliderCheck : MonoBehaviour
{
    public List<int> nearMap;
    public List<GameObject> DoorList;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.S.currentMap = nearMap[0];
            PlayerController.S.nearMap = nearMap;
        }
    }
}
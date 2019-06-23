using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRay : MonoBehaviour {

    RaycastHit rayHit;
    float rayLen = 50.0f;
    int objectMask;

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update()
    {
        rayShoot();
    }

    void rayShoot()
    {
        objectMask = LayerMask.GetMask("Object");

        if (Physics.Raycast(this.transform.position, this.transform.forward, out rayHit, rayLen, objectMask))
        {
            if (Controller.GetHairTrigger())
            {
                rayHit.transform.GetComponent<ObjectControll>().isTouch = !rayHit.transform.GetComponent<ObjectControll>().isTouch;
            }
        }
    }
}

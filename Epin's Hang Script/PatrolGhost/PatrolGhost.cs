using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolGhost : Ghost
{
    static private PatrolGhost instance = null;
    static public PatrolGhost GetInstance
    {
        get
        {
            return instance;
        }
    }

    NavMeshAgent nav;
    Animation ani;

    public bool playerFinding  = false;

    int arriveObjectsIndex = 1;
    public List<GameObject> arriveObjects;
    public GameObject roomPointObject;
    public GameObject curGoingObject;
    public GameObject screamPoint;

    public override void Spawn()
    {
        return;
    }

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
            return;
        }

        this.gameObject.SetActive(false);
        instance        = this;
        nav             = this.gameObject.GetComponent<NavMeshAgent>();
        curGoingObject  = arriveObjects[arriveObjectsIndex];
        ani             = this.gameObject.GetComponent<Animation>();
        roomPointObject.SetActive(false);

    }

    private void Update()
    {
        if (PlayerController.S.isLive)
        {
            if (!playerFinding)
            {
                nav.SetDestination(curGoingObject.transform.position);
                RayCheckPlayer();
                return;
            }

            PlayerFollow();
        }
    }

    void RayCheckPlayer()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit))
        {
            if (raycastHit.transform.gameObject.tag == "Player")
            {
                playerFinding = true;
                ani.Play("Running");
            }
        }
    }

    void PlayerFollow()
    {
       nav.SetDestination(PlayerController.S.transform.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && PlayerController.S.isLive)
        {
            ani.Play("Idle");
            PlayerController.S.isLive = false;
            this.transform.position = PlayerController.S.facePoint.transform.position;
            this.transform.LookAt(PlayerController.S.transform);
            this.transform.LookAt(PlayerController.S.transform.GetChild(3).transform);
            nav.SetDestination(this.transform.position);
            GetComponent<PatrolGhostAudioRandPlay>().Die();
            Destroy(this.gameObject.transform.GetChild(1).gameObject);
            GameOver();
        }

        if (other.tag == "PatrolArrivePoint")
        {
            if (other.gameObject == arriveObjects[0])
            {
                arriveObjectsIndex = 1;
                curGoingObject = arriveObjects[arriveObjectsIndex];
            }
            else if (other.gameObject == arriveObjects[1])
            {
                arriveObjectsIndex = 2;
                curGoingObject = arriveObjects[arriveObjectsIndex];
            }
            else if (other.gameObject == arriveObjects[2])
            {
                arriveObjectsIndex = 0;
                curGoingObject = arriveObjects[arriveObjectsIndex];
            }
        }

        else if (other.tag == "Door")
        {
            if (other.gameObject.GetComponent<DoorController>().connectRoom != 20)
            {
                if (Random.Range(1, 10) < 4)
                {
                    other.gameObject.transform.GetComponent<DoorController>().isTouch = true;
                    roomPointObject.transform.position = MapManager.GetInstance.maps[
                        other.gameObject.GetComponent<DoorController>().connectRoom].transform.position;

                    roomPointObject.SetActive(true);
                    curGoingObject = roomPointObject;
                }
            }
            else
            {
                other.gameObject.transform.GetComponent<DoorController>().isTouch = true;
            }
        }

        else if (other.tag == "roomPoint")
        {
            roomPointObject.SetActive(false);
            curGoingObject = arriveObjects[arriveObjectsIndex];
            ani.Play("Idle");
        }

        else if (other.tag == "RetiringObject" &&
                 other.transform.GetComponent<Closet>().playerIsHideThis)
        {
            other.gameObject.GetComponent<Closet>().openAni = true;
        }
    }
}

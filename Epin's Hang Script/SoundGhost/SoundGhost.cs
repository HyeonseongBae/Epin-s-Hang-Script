using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoundGhost : Ghost {
    static private SoundGhost instance = null;
    static public SoundGhost GetInstance
    {
        get
        {
            return instance;
        }
    }

    NavMeshAgent nav;
    Animator ani;

    public GameObject screamPoint;
    float lifeTime = 0;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
            return;
        }

        instance = this;
        nav = this.transform.GetComponent<NavMeshAgent>();
        ani = this.transform.GetComponentInChildren<Animator>();
        this.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        Follow(); ;
    }

    void Follow()
    {
        if (PhoneController.S.isNoise &&
            !PlayerController.S.GetComponent<PlayerAction>().isHide)
        {
            this.transform.GetComponent<AudioSource>().volume = 1;
            ani.SetBool("run", true);
            ani.SetBool("idle", false);
            ani.SetBool("walk", false);
            nav.speed = 20f;
        }
        else if (!PlayerController.S.isWalk &&
                 !PlayerController.S.GetComponent<PlayerAction>().isHide)
        {
            this.transform.GetComponent<AudioSource>().volume = 1;
            ani.SetBool("walk", true);
            ani.SetBool("idle", false);
            ani.SetBool("run", false);
            nav.speed = 3;
        }
        else
        {
            lifeTime += Time.deltaTime;
            if (lifeTime > 10) Death();
            this.transform.GetComponent<AudioSource>().volume = 0;
            ani.SetBool("idle", true);
            ani.SetBool("walk", false);
            ani.SetBool("run", false);
            nav.SetDestination(this.transform.position);
            nav.speed = 0;
        }

        nav.SetDestination(PlayerController.S.transform.position);
    }

    public override void Spawn()
    {
        if (!isLife)
        {
            int rand = Random.Range(1, PlayerController.S.nearMap.Count);
            print(MapManager.GetInstance.maps[PlayerController.S.nearMap[rand]].name);
            this.transform.position = MapManager.GetInstance.maps[
                PlayerController.S.nearMap[rand]].transform.position;
            this.gameObject.SetActive(true);
        }
    }

    void Death()
    {
        lifeTime = 0;
        isLife = false;
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerSound.GetInstance.SoundSelect(0);
            PlayerController.S.isLive = false;
            this.transform.position = PlayerController.S.facePoint.transform.position;
            this.transform.LookAt(PlayerController.S.transform.GetChild(3).transform);
            nav.SetDestination(this.transform.position);
            ani.SetBool("find", true);
            GameOver();
        }
    }

}

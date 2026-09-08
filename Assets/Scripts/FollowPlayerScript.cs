using System;
using UnityEngine;


public class FollowPlayerScript : MonoBehaviour
{
    public float speed;
    public float speedExp;
    public Transform player;
    public Transform other;
    public float Deadzone;
    public float Expansion;
    public float Contraction;
    public float delay;
    public float MinSize;
    public float Middlemult;
    Vector2 MoveVec;
    float DelayTimer;
    float MoveDeltaX;
    float MoveDeltaY;
    Camera cam;
    GameObject Midpos;


    void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
        Midpos = GameObject.Find("Midpos");
    }



    void Update()
    {
    //oldcamFollow()
        //Cam follow New
        calcMid();
        CamFollow();

        if(DelayTimer > 0)
        DelayTimer -= Time.deltaTime;
        else
        {
            ZoomIn();
        }
        

    }

    void oldcamFollow()
    {
            MoveVec = Vector2.MoveTowards(transform.position, player.position, Mathf.Pow(speed*Vector2.Distance(transform.position, player.position), speedExp)*Time.timeScale);
        MoveDeltaX = transform.position.x - player.position.x;
        MoveDeltaY = transform.position.y - player.position.y;


        if(Mathf.Abs(MoveDeltaX) > Deadzone)
        transform.position = new Vector3(MoveVec.x,transform.position.y, -10);
        if(Mathf.Abs(MoveDeltaY) > Deadzone)
        transform.position = new Vector3(transform.position.x,MoveVec.y, -10);

    }
    void CamFollow()
    {
        Midpos.transform.position = Vector2.MoveTowards(Midpos.transform.position, Vector2.Lerp(player.position, other.position, 0.5f), Vector2.Distance(Midpos.transform.position, Vector2.Lerp(player.position, other.position, 0.5f)));
        transform.position = Vector2.MoveTowards(transform.position, Midpos.transform.position, Middlemult*(Vector2.Distance(player.position, other.position)*.1f)*Time.timeScale);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        Vector2 CamBounds = cam.WorldToViewportPoint(player.transform.position);
        if(CamBounds.x > 1 || CamBounds.x < 0 || CamBounds.y > 1  || CamBounds.y < 0)
        {
            cam.orthographicSize += Expansion*Time.deltaTime;
            DelayTimer = delay;
        }

        
    }
    void ZoomIn()
    {
        if(cam.orthographicSize > Vector2.Distance(player.position, other.position)*.5f + MinSize)
        cam.orthographicSize -= Contraction*Time.deltaTime;
    }
    void calcMid()
    {
        
    }
}

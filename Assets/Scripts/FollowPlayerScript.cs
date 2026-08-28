using UnityEngine;


public class FollowPlayerScript : MonoBehaviour
{
    public float speed;
    public float speedExp;
    public Transform player;
    public float Deadzone;
    Vector2 MoveVec;
    float MoveDeltaX;
    float MoveDeltaY;

    
    
    void Update()
    {
        MoveVec = Vector2.MoveTowards(transform.position, player.position, Mathf.Pow(speed*Vector2.Distance(transform.position, player.position), speedExp));
        MoveDeltaX = transform.position.x - player.position.x;
        MoveDeltaY = transform.position.y - player.position.y;


        if(Mathf.Abs(MoveDeltaX) > Deadzone)
        transform.position = new Vector3(MoveVec.x,transform.position.y, -10);
        if(Mathf.Abs(MoveDeltaY) > Deadzone)
        transform.position = new Vector3(transform.position.x,MoveVec.y, -10);
    }
}

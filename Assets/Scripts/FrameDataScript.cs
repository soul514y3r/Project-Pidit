using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



public class FrameDataScript : MonoBehaviour
{
    [Header("FrameData")]
    public FrameData data; 
    CustomCollider2D cust;
    SpriteRenderer rend;

    public int FrameIndx;
    

  PhysicsShapeGroup2D shapeGroup2D = new PhysicsShapeGroup2D();

void Awake()
    {
        cust = gameObject.GetComponent<CustomCollider2D>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        Load();
        
    }

    void FixedUpdate()
    {
        Load();
    }

    void ShowCol()
    {
        cust = gameObject.GetComponent<CustomCollider2D>();
        if(cust != null)
        {
           cust.SetCustomShapes(shapeGroup2D); 
        }
        else
        Warning.Error("Can't find customcollider2D, please attach one to the gameobject");
    }

    void Load()
    {
        cust = gameObject.GetComponent<CustomCollider2D>();
        if(cust != null)
        {
            shapeGroup2D.Clear();
            if(rend.flipX == true)
            {
            foreach( Shape sh in data.frames[FrameIndx].shapes)
            {
                shapeGroup2D.AddCapsule(new Vector2(-sh.startpos.x,sh.startpos.y),new Vector2(-sh.Endpos.x,sh.Endpos.y), sh.radius);
            }
            }

            else
            {
            foreach( Shape sh in data.frames[FrameIndx].shapes)
            {
                shapeGroup2D.AddCapsule(sh.startpos,sh.Endpos,sh.radius);
            } 
            }
            
            
           cust.SetCustomShapes(shapeGroup2D); 
        }
        else
        Warning.Error("Can't find customcollider2D, please attach one to the gameobject");
    }



    [ContextMenu("Show Collider")] void showcol() => ShowCol();
    [ContextMenu("Load Collider Data")] void load() => Load();


}

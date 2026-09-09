using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



public class FrameDataScript : MonoBehaviour
{
SpriteRenderer renderer;

    void Awake()
    {
        
    }

    void OnDrawGizmos()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
       
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateChild()
    {
        
    }

    [ContextMenu("Create Framedata")] void CreateFrameData() => CreateChild();
}

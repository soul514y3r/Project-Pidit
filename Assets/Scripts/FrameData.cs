using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class frame
{
    public Shape[] shapes;
}

[Serializable]
public struct Shape
{
    public Vector2 startpos;
    public Vector2 Endpos;
    public float radius;
    public Color color;
}

[CreateAssetMenu(fileName = "FrameDataObject", menuName = "Scriptable Objects/FrameData")]
public class FrameData : ScriptableObject
{
    public frame[] frames;

    public Shape createShape(Vector2 v1, Vector2 v2, float rad)
    {
        Shape shape = new Shape();
        shape.startpos = v1;
        shape.Endpos = v2;
        shape.radius = rad;
        shape.color = new Color(0,255,0,150);
        return shape;
    }

}

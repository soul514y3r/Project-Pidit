using UnityEngine;
using UnityEngine.InputSystem;

public class InvertclickScript : MonoBehaviour
{
    public float Timermax;
    public float timer;
    public float timemult;
    public float cutout;
    public float cutmult;
    public Vector2 Normalpos;
    public Material material;
    static readonly int ExtentID  = Shader.PropertyToID("_Extents");
    static readonly int RadID  = Shader.PropertyToID("_Radi");
    static readonly int constID  = Shader.PropertyToID("_Const");


    void Update()
    {
        if(Mouse.current.leftButton.isPressed)
        {
            Invert();
        }
        cutout = timer/cutmult;
        if(timer < Timermax)
        timer += Time.deltaTime*timemult;

        

        
        material.SetFloat(RadID, timer);
        material.SetFloat(constID, cutout);
        material.SetVector(ExtentID, Normalpos);

    }
    void Invert()
    {
        Normalpos = new Vector2(Mouse.current.position.ReadValue().x/Screen.width, Mouse.current.position.ReadValue().y/Screen.height);
        timer = 0;

    }
}

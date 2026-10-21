
using UnityEngine;
using UnityEngine.InputSystem;

public class InvertclickScript : MonoBehaviour
{
    [Header("Edits")]
      public Material material;
    public float Timermax;
    public float timemult;
    public float tTC;
    public float cutmult;
    [Header("Debug showing")]
    public float timer;
    public float cutout;
    public Vector2 Normalpos;
    static readonly int OrigID  = Shader.PropertyToID("_Orig");
    static readonly int RadID  = Shader.PropertyToID("_Radi");
    static readonly int constID  = Shader.PropertyToID("_Const");




    void Update()
    {
        if(Mouse.current.leftButton.isPressed)
        {
            Invert();
        }
        if(timer>=Timermax*tTC && cutout < timer)
        {
            cutout += Time.deltaTime*cutmult;
        }
        if(timer < Timermax)
        timer += Time.deltaTime*timemult;

        

        
        material.SetFloat(RadID, timer);
        material.SetFloat(constID, cutout);
        material.SetVector(OrigID, Normalpos);

    }
    void Invert()
    {
        Normalpos = new Vector2(Mouse.current.position.ReadValue().x/Screen.width, Mouse.current.position.ReadValue().y/Screen.height);
        timer = 0;
        cutout = 0;

    }
}

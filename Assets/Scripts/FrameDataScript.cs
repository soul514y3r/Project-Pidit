

using UnityEngine;



public class FrameDataScript : MonoBehaviour
{
    [Header("FrameData")]
    public FrameData data; 
    CustomCollider2D cust;
    SpriteRenderer rend;

    public int FrameIndx;

    [Header("Rendering")]
    public Material material;
    [SerializeField] float margin = 0.1f;   // covers the AA band and any outline
    [SerializeField] Color tempColor;

    static readonly int StartId  = Shader.PropertyToID("_Startpos");
    static readonly int EndId    = Shader.PropertyToID("_EndPos");
    static readonly int RadiusId = Shader.PropertyToID("_Radi");
    static readonly int ColorId  = Shader.PropertyToID("_BaseColor");

    Mesh Quad;
    MaterialPropertyBlock PropBlock;
    

  PhysicsShapeGroup2D shapeGroup2D = new PhysicsShapeGroup2D();

void Awake()
    {
        cust = gameObject.GetComponent<CustomCollider2D>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        Quad = BuildQuad();
        PropBlock = new MaterialPropertyBlock();
        Load();
    }

    void Update()
    {
        Draw();
    }

    void ShowCol()
    {
        cust = gameObject.GetComponent<CustomCollider2D>();
        if(cust != null)
        {
           cust.SetCustomShapes(shapeGroup2D); 
        }
        else
        Debug.LogError("Can't find customcollider2D, please attach one to the gameobject");
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
                
                DrawCapsule(new Vector2(-sh.startpos.x,sh.startpos.y),new Vector2(-sh.Endpos.x,sh.Endpos.y),sh.radius, sh.color);

            }
            }

            else
            {
            foreach( Shape sh in data.frames[FrameIndx].shapes)
            {
                shapeGroup2D.AddCapsule(sh.startpos,sh.Endpos,sh.radius);

                DrawCapsule(sh.startpos,sh.Endpos,sh.radius, sh.color);
            } 
            }
            
            
           cust.SetCustomShapes(shapeGroup2D); 
        }
        else
        Debug.LogError("Can't find customcollider2D, please attach one to the gameobject");
    }
    void Draw()
    {
        #if UNITY_EDITOR
        if(rend == null)
        rend = gameObject.GetComponent<SpriteRenderer>();
        if(rend == null) return;

        if(rend.flipX == true)
            {
            foreach( Shape sh in data.frames[FrameIndx].shapes)
            {
                //raycast
                DrawCapsule(new Vector2(-sh.startpos.x,sh.startpos.y),new Vector2(-sh.Endpos.x,sh.Endpos.y),sh.radius, sh.color);
            }
            }
            else
            {
            foreach( Shape sh in data.frames[FrameIndx].shapes)
            {
                //raycast
                DrawCapsule(sh.startpos,sh.Endpos,sh.radius, sh.color);
            } 
            }
            #endif
    }



    [ContextMenu("Show Collider")] void showcol() => ShowCol();
    [ContextMenu("Load Collider Data")] void load() => Load();

    //Rendering part
    public void DrawCapsule(Vector2 strt, Vector2 nd, float radi, Color color, float z = 0f)
    {
        strt = transform.TransformPoint(strt);
        nd = transform.TransformPoint(nd);
        Vector2 center = (strt + nd) * 0.5f;
        Vector2 size = new Vector2(Mathf.Abs(nd.x - strt.x), Mathf.Abs(nd.y - strt.y))+ Vector2.one * 2f * (radi + margin);

        PropBlock.Clear();
        PropBlock.SetVector(StartId, strt);
        PropBlock.SetVector(EndId, nd);
        PropBlock.SetFloat(RadiusId, radi);
        PropBlock.SetColor(ColorId, color);

        RenderParams rendPar = new RenderParams(material) { matProps = PropBlock };
        Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(center.x, center.y, z),Quaternion.identity,new Vector3(size.x, size.y, 1f));
        Graphics.RenderMesh(rendPar, Quad, 0, matrix);
    }

    static Mesh BuildQuad()
    {
        var mesh = new Mesh { name = "UnitQuad" };
        mesh.vertices = new[]
        {
        new Vector3(-0.5f, -0.5f, 0), new Vector3(-0.5f,  0.5f, 0),
        new Vector3( 0.5f,  0.5f, 0), new Vector3( 0.5f, -0.5f, 0),
        };
        mesh.triangles = new[] { 0, 1, 2, 0, 2, 3 };
        return mesh;
    }



}

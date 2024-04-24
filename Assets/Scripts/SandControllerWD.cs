using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SandControllerWD : MonoBehaviour
{
    public class Pts {

       public Vector3 p;
       public Color c;
    }

    public CustomRenderTexture Texture;
    public Material SandEffectMaterial;
    public Material groundMaterial;
    public Transform ground;
    
    private Texture2D texX;
    [FormerlySerializedAs("SandColor")] [SerializeField] private Gradient sandColor;
    
    private ColliderPoolWD cp;
    
    private readonly Color[] colors = new Color[10];

    public List<Pts> points = new List<Pts>();
    private List<Pts> towerPoints = new List<Pts>();
    
    [FormerlySerializedAs("lvlTex")] public Texture2D[] lvlTexX = new Texture2D[2];


    
    public void LevelInitialize()
    {
        ReInitializeE();
        StartDelayedD();
        StartCoroutine(StampGroundD());
       
    }

    public void ReInitializeE() {
        Texture.initializationTexture = lvlTexX[PlayerControllerWD.levelL-1];

        Texture.Initialize();
        SandEffectMaterial.SetVector("_DrawPosition", -Vector2.one);
    }
    
    private void StartDelayedD()
    {
        colors[0] = new Color(.49f, .785f, .976f, 1f);
        colors[1] = new Color(1f, .35f, 0.35f, 1f);
        colors[2] = new Color(.8f, 0.8f, 0.8f, 1f);
        colors[3] = new Color(.8f, 0.8f, 0.8f, 1f);
        colors[4] = new Color(.8f, 0.8f, 0.8f, 1f);
        colors[5] = new Color(.8f, 0.8f, 0.8f, 1f);
        colors[6] = new Color(.8f, 0.8f, 0.8f, 1f);
        colors[7] = new Color(.8f, 0.8f, 0.8f, 1f);
        colors[8] = new Color(.8f, 0.8f, 0.8f, 1f);
        colors[9] = new Color(.8f, 0.8f, 0.8f, 1f);
        

        texX = new Texture2D(64, 64);
        groundMaterial.SetColorArray("colors", colors);
        cp = GameObject.FindObjectOfType<ColliderPoolWD>();

        StartCoroutine(DrawTowerDelayedD());



    }
    public void DrawTowerR(Color c, Vector2 p,int count)
    {
        towerPoints.Add(new Pts() { c = c, p = p });
    }

    private IEnumerator DrawTowerDelayedD() {

        SandEffectMaterial.SetFloat("_StartScale", .9f);

        yield return new WaitForSeconds(.25f);
        while (towerPoints.Count == 0) {
            yield return null;
        
        }

        while (towerPoints.Count > 0)
        {

            if (PlayerControllerWD.gameActiveE)
            {
                Texture.Update();

                SandEffectMaterial.SetVector("_DrawPosition", new Vector4(towerPoints[0].p.x, towerPoints[0].p.y, 0f, 0f));//Camera.main.ScreenToViewportPoint(Input.mousePosition));

                SandEffectMaterial.SetVector("_DrawColor", towerPoints[0].c);
                
               // print(SandEffectMaterial.GetVector("_DrawPosition") + " " + towerPoints[0].p);
                
                towerPoints.RemoveAt(0);


                Texture.Update();
            }

            yield return null;
        }

        SandEffectMaterial.SetFloat("_StartScale", 0f);
    }
    
    public void DrawRemotelyY(Color c, Vector2 p) 
    {
        StartCoroutine(DrawPoint(c,p));
        StartCoroutine(UpdateTexX());
    }

    private IEnumerator DrawPoint(Color c, Vector2 p) {
        SandEffectMaterial.SetVector("_DrawPosition", new Vector4(p.x, p.y, 0f, 0f));//Camera.main.ScreenToViewportPoint(Input.mousePosition));
        Color color = colors[0];// Color.green;
        SandEffectMaterial.SetVector("_DrawColor", c);
        yield return null;
       
    
    }

    private IEnumerator StampGroundD() {
        yield return new WaitForSeconds(1f);
        while (true) {

            if (PlayerControllerWD.gameActiveE)
            {


                if (points.Count > 0)
                {
                    Texture.Update();
                    SandEffectMaterial.SetVector("_DrawPosition", new Vector4(points[0].p.x, points[0].p.y, 0f, 0f));//Camera.main.ScreenToViewportPoint(Input.mousePosition));

                    SandEffectMaterial.SetVector("_DrawColor", points[0].c);

                    points.RemoveAt(0);
                    if (points.Count == 0)
                    {
                        Texture.Update();
                    }
                }
            }
            
            yield return null;
        }
    }
    
    private IEnumerator UpdateTexX() {
        Texture.Update(1);
        yield return null;
    }

    private IEnumerator UpdateColL()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            RenderTexture rt = RenderTexture.active;
            RenderTexture.active = Texture;
            texX.ReadPixels(new Rect(0, 0, texX.width, texX.height), 0, 0);
            RenderTexture.active = rt;
            Color[] pixels = texX.GetPixels();
            int pixelCount = pixels.Length;
            Color[,] vals = new Color[texX.width, texX.height];
            int col = 0;

            int step = 32;
            int step_ = texX.width / step;
            bool[,] c2d = new bool[step, step];
            for (int y = 0; y < texX.height; y+= step_)
            {
                for (int x = 0; x < texX.width; x+= step_)
                {
                    int index = y * texX.width + x;
                    
                    if (pixels[index].r > .1f && c2d[x/ step_, y/ step_] ==false) {
                        if (ground.transform.childCount > col)
                        {
                            ground.transform.GetChild(col).localPosition = new Vector3(-.5f +  (float)x / 256f, 1f, -.5f +  (float)y / 256f);
                        }
                        else {
                            cp.GetColliderR(ground.transform).localPosition = new Vector3(-.5f +  (float)x / 256f, 1f, -.5f +  (float)y / 256f);
                        }
                        c2d[x / step_, y / step_] = true;
                        col++;
                    }

                }
            }

            if (this.transform.childCount>col+1)
            {
                cp.RemoveColliderR(ground.transform.GetChild(col + 1));
            }

            yield return new WaitForSeconds(.15f);
          
        }
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandController : MonoBehaviour
{

    public class Pts {

       public Vector3 p;
        public Color c;
    
    }

    public CustomRenderTexture Texture;
    public Material SandEffectMaterial;
    public Material groundMaterial;
    public Transform ground;
    Texture2D tex;
    public Gradient SandColor;
    ColliderPoolWD CP;
    Color[] colors = new Color[10];

    public List<Pts> points = new List<Pts>();
    public List<Pts> TowerPoints = new List<Pts>();


    public Texture2D[] lvlTex = new Texture2D[2];


    
    public void LevelInitialize()
    {


        ReInitialize();
        StartDelayed();
        StartCoroutine(StampGround());
       
    }

    public void ReInitialize() {
        Texture.initializationTexture = lvlTex[PlayerController.level-1];

        Texture.Initialize();
        SandEffectMaterial.SetVector("_DrawPosition", -Vector2.one);
    }
    private void StartDelayed()
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
        

        tex = new Texture2D(64, 64);
        groundMaterial.SetColorArray("colors", colors);
        CP = GameObject.FindObjectOfType<ColliderPoolWD>();

        //Texture = tex;
        //DrawRemotely(colors[0], new Vector2(.3f, .2f));
        // StartCoroutine(UpdateCol());

        //  StartCoroutine(DrawPoint(colors[0], new Vector2(.5f,.2f)));
        // StartCoroutine(UpdateTex());
        //Texture.Update(1);
        // DrawRemotely(Color.blue, new Vector2(0f,0f));

        StartCoroutine(DrawTowerDelayed());



    }
    public void DrawTower(Color c, Vector2 p,int count)
    {

        //
        TowerPoints.Add(new Pts() { c = c, p = p });
       

    }

    IEnumerator DrawTowerDelayed() {

        SandEffectMaterial.SetFloat("_StartScale", .9f);

        yield return new WaitForSeconds(.25f);
        while (TowerPoints.Count == 0) {
            yield return null;
        
        }

        while (TowerPoints.Count > 0)
        {

            if (PlayerController.gameActive)
            {
                Texture.Update();

                SandEffectMaterial.SetVector("_DrawPosition", new Vector4(TowerPoints[0].p.x, TowerPoints[0].p.y, 0f, 0f));//Camera.main.ScreenToViewportPoint(Input.mousePosition));

                SandEffectMaterial.SetVector("_DrawColor", TowerPoints[0].c);

                //Texture.Update();
                print(SandEffectMaterial.GetVector("_DrawPosition") + " " + TowerPoints[0].p);

                // StartCoroutine(DrawPoint(points[0].c, points[0].p));
                //StartCoroutine(UpdateTex());
                TowerPoints.RemoveAt(0);


                Texture.Update();
            }

            yield return null;
        }

        SandEffectMaterial.SetFloat("_StartScale", 0f);



        /*

        yield return null;
        yield return new WaitForSeconds(.1f*(float)count);
        Texture.Update();
        print(count + " " + c);
        SandEffectMaterial.SetFloat("_StartScale", .9f);
        SandEffectMaterial.SetVector("_DrawPosition", new Vector4(p.x, p.y, 0f, 0f));//Camera.main.ScreenToViewportPoint(Input.mousePosition));
        Color color = colors[0];// Color.green;
        SandEffectMaterial.SetVector("_DrawColor", c);
       
        Texture.Update();
 yield return null;
        SandEffectMaterial.SetVector("_DrawPosition", -Vector2.one);
        SandEffectMaterial.SetFloat("_StartScale", 0f);
        */
    }
    public void DrawRemotely(Color c, Vector2 p) {

        //
        StartCoroutine(DrawPoint(c,p));
        StartCoroutine(UpdateTex());



    }

    /*
    IEnumerator DrawPoint(Color c, Vector2 p)
    {
        Texture.Update(1);
        SandEffectMaterial.SetVector("_DrawPosition", new Vector4(p.x, p.y, 0f, 0f));//Camera.main.ScreenToViewportPoint(Input.mousePosition));
            Color color = colors[0];// Color.green;
            SandEffectMaterial.SetVector("_DrawColor", c);
yield return null;

        Texture.Update(1);
        // new WaitForSeconds(.05f);
        SandEffectMaterial.SetVector("_DrawPosition", -Vector2.one);
       // yield return new WaitForSeconds(1f);
     

    }
    */
    IEnumerator DrawPoint(Color c, Vector2 p) {
        /*
        CustomRenderTextureUpdateZone Zone = new CustomRenderTextureUpdateZone();
        Zone.updateZoneSize = new Vector3(64f, 64f , 1.0f);
        Zone.updateZoneCenter = new Vector3(p.x*2048f,(1f-p.y)*2048f, 1f);
        Zone.needSwap = true;
        CustomRenderTextureUpdateZone[] updateZones = new CustomRenderTextureUpdateZone[1] { Zone };

        Texture.SetUpdateZones(updateZones);
        */
        // while (true) {
        // print("1");
        SandEffectMaterial.SetVector("_DrawPosition", new Vector4(p.x, p.y, 0f, 0f));//Camera.main.ScreenToViewportPoint(Input.mousePosition));
            Color color = colors[0];// Color.green;
            SandEffectMaterial.SetVector("_DrawColor", c);
        yield return null;// new WaitForSeconds(.1f);
       // }
    
    }
    
    private void Update()
    {


     
    }
    IEnumerator StampGround() {
        yield return new WaitForSeconds(1f);
        while (true) {

            if (PlayerController.gameActive)
            {


                if (points.Count > 0)
                {
                    Texture.Update();
                    SandEffectMaterial.SetVector("_DrawPosition", new Vector4(points[0].p.x, points[0].p.y, 0f, 0f));//Camera.main.ScreenToViewportPoint(Input.mousePosition));

                    SandEffectMaterial.SetVector("_DrawColor", points[0].c);

                    //Texture.Update();
                    //  print(SandEffectMaterial.GetVector("_DrawPosition") +" "+ points[0].p);

                    // StartCoroutine(DrawPoint(points[0].c, points[0].p));
                    //StartCoroutine(UpdateTex());
                    points.RemoveAt(0);
                    if (points.Count == 0)
                    {
                        Texture.Update();
                    }
                    //  print(points.Count);
                }
            }
            //else {
           //     SandEffectMaterial.SetVector("_DrawPosition", -Vector2.one);
           // }
            yield return null;
           // yield return null;
            //yield return new WaitForSecondsRealtime(.1f);
        }
    
    }
    IEnumerator UpdateTex() {
       // while (true)
       // {
          //  print("2");
            Texture.Update(1);
       // yield return null;// new WaitForSeconds(10f);// null;// new WaitForSeconds(.05f);
       // Texture.Update(1);

        //if(points.Count<=0)
       //     SandEffectMaterial.SetVector("_DrawPosition", -Vector2.one);
       // }
        yield return null;
    }

    IEnumerator UpdateCol()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            RenderTexture rt = RenderTexture.active;
            RenderTexture.active = Texture;
            tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
            RenderTexture.active = rt;
            Color[] pixels = tex.GetPixels();
            int pixelCount = pixels.Length;
            Color[,] vals = new Color[tex.width, tex.height];
            int col = 0;

            int step = 32;
            int step_ = tex.width / step;
            bool[,] c2d = new bool[step, step];
            for (int y = 0; y < tex.height; y+= step_)
            {
                for (int x = 0; x < tex.width; x+= step_)
                {
                    int index = y * tex.width + x;

                    //print(pixels[index]);
                    //vals[x, y] = pixels[index];
                    if (pixels[index].r > .1f && c2d[x/ step_, y/ step_] ==false) {
                        if (ground.transform.childCount > col)
                        {
                            ground.transform.GetChild(col).localPosition = new Vector3(-.5f +  (float)x / 256f, 1f, -.5f +  (float)y / 256f);
                        }
                        else {
                            //print(new Vector3(-.5f + .0625f * x / 16, 1f, -.5f + y / 16));
                            //print(new Vector3(-.5f + .0625f * (float)x / 256f, 1f, -.5f + (float)y / 256f));
                            CP.GetColliderR(ground.transform).localPosition = new Vector3(-.5f +  (float)x / 256f, 1f, -.5f +  (float)y / 256f);
                        }
                        c2d[x / step_, y / step_] = true;
                        col++;
                    }

                }
            }

            if (this.transform.childCount>col+1)
            {
                CP.RemoveColliderR(ground.transform.GetChild(col + 1));
            }

            yield return new WaitForSeconds(.15f);
          
        }
        yield return null;
    }
}

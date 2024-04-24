using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownRender : MonoBehaviour
{
    public static int size = 128;
    public static Color[,] map = new Color[128, 128];
    public static float scale = 10f;

    public static Vector3 pos;
    public RenderTexture rt;

    public Color[] c = new Color[3];

    private int[] teamC = new int[3];
    int totalCells = 0;//16384;
    public RectTransform[] bars = new RectTransform[3];

    // Start is called before the first frame update
    void Awake()
    {
        scale= this.transform.GetComponent<Camera>().orthographicSize = this.transform.parent.lossyScale.x/2f;
        print(scale);
        pos = this.transform.position;
        rt.Release();
        map = new Color[128, 128];

    }
    private void Start()
    {
        teamC = new int[c.Length];
        bars = new RectTransform[teamC.Length-1];
        for (int i = 0; i < teamC.Length-1; i++)
        {

            bars[i] = GameObject.Find("Enemy" + (i + 1).ToString()).GetComponent<RectTransform>();

            bars[i].gameObject.SetActive(true);
            bars[i].GetComponent<CanvasGroup>().alpha = 1f;



        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        
        if (!SceneController.levelActive)//PlayerController.gameActive)
            return;
        RenderTexture.active = rt;

        // Create a new Texture2D and read the RenderTexture image into it
        Texture2D tex = new Texture2D(rt.width, rt.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        RenderTexture.active = null;

        Color[] m = tex.GetPixels();
        for (int i = 0; i < teamC.Length; i++)
        {
            teamC[i] = 0;
        }
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                map[x, y] = m[x + y * tex.width];

                if (Vector3.Distance(new Vector3(map[x, y].r, map[x, y].g, map[x, y].b), new Vector3(c[0].r, c[0].g, c[0].b)) < .03f) {
                    teamC[0]++;


                }
                else if (Vector3.Distance(new Vector3(map[x, y].r, map[x, y].g, map[x, y].b), new Vector3(c[1].r, c[1].g, c[1].b)) < .03f)
                {
                    teamC[1]++;


                }else if (Vector3.Distance(new Vector3(map[x, y].r, map[x, y].g, map[x, y].b), new Vector3(c[2].r, c[2].g, c[2].b)) < .03f)
                {
                    teamC[2]++;


                }
                else if (Vector3.Distance(new Vector3(map[x, y].r, map[x, y].g, map[x, y].b), new Vector3(c[3].r, c[3].g, c[3].b)) < .03f)
                {
                    teamC[3]++;


                }

            }
           // print(map[x, 16]);
        }
      
        if (totalCells == 0 && teamC[0]!=0) {
            totalCells = teamC[0];
            print(totalCells);
        }
        UpdateBars();
      //  print(map[38, 19]);
      // print(map[64-38, 19]);
      // print(map[64 - 38, 64-19]);
      //  print(map[ 38, 64 - 19]);
    }


    public void UpdateBars()
    {


        int totalDiscs = totalCells;//plDiscs + enDiscs[0] + enDiscs[1] + enDiscs[2];
        if (totalDiscs == 0)
            return;
        float plPercent = (float)(teamC[1]) / (float)totalDiscs;
        float[] enPercent = new float[teamC.Length-2];

        for (int i = 0; i < enPercent.Length; i++)
        {
            enPercent[i] = (float)(teamC[i + 2]) / (float)totalDiscs;

        }

        // bars[0].anchoredPosition3D = Vector3.Lerp(bars[0].anchoredPosition3D, new Vector3(plPercent * 600f, 6f, 0f), Time.deltaTime * 4f);


        float w = plPercent * 600f;  // ;
        bars[0].sizeDelta = Vector2.Lerp(bars[0].sizeDelta, new Vector2(w, bars[0].sizeDelta.y), Time.deltaTime * 12f);
        float shift = w;// + enPercent[0] * 600f;
        //   bars[0].sizeDelta = new Vector2(w, bars[0].sizeDelta.y);


        for (int i = 1; i < enPercent.Length + 1; i++)
        {
            bars[i].anchoredPosition3D = Vector3.Lerp(bars[i].anchoredPosition3D, new Vector3(shift, 6f, 0f), Time.deltaTime * 12f);

            bars[i].sizeDelta = Vector2.Lerp(bars[i].sizeDelta, new Vector2(enPercent[i - 1] * 600f, bars[i].sizeDelta.y), Time.deltaTime * 12f);
            if (i < enPercent.Length)
                w = enPercent[i - 1] * 600f;
            shift += w;
            //  bars[i].sizeDelta = new Vector2(w, bars[i].sizeDelta.y);

        }



    }
}

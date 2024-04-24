using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TopDownRenderWD : MonoBehaviour
{
    public static readonly int sizeE = 128;
    public static Color[,] mapP = new Color[128, 128];
    public static float scaleE = 10f;

    public static Vector3 posS;
    [FormerlySerializedAs("rt")] public RenderTexture rtT;

    public Color[] c = new Color[3];

    private int[] teamCc = new int[3];
    private int totalCellsS = 0;//16384;
    [FormerlySerializedAs("bars")] public RectTransform[] barsS = new RectTransform[3];
    
    private void Awake()
    {
        scaleE= this.transform.GetComponent<Camera>().orthographicSize = this.transform.parent.lossyScale.x/2f;
        print(scaleE);
        posS = this.transform.position;
        rtT.Release();
        mapP = new Color[128, 128];

    }
    private void Start()
    {
        teamCc = new int[c.Length];
        barsS = new RectTransform[teamCc.Length-1];
        for (int i = 0; i < teamCc.Length-1; i++)
        {

            barsS[i] = GameObject.Find("Enemy" + (i + 1).ToString()).GetComponent<RectTransform>();

            barsS[i].gameObject.SetActive(true);
            barsS[i].GetComponent<CanvasGroup>().alpha = 1f;



        }
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        if (!SceneControllerWD.levelActive)
            return;
        RenderTexture.active = rtT;

        // Create a new Texture2D and read the RenderTexture image into it
        Texture2D tex = new Texture2D(rtT.width, rtT.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        RenderTexture.active = null;

        Color[] m = tex.GetPixels();
        for (int i = 0; i < teamCc.Length; i++)
        {
            teamCc[i] = 0;
        }
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                mapP[x, y] = m[x + y * tex.width];

                if (Vector3.Distance(new Vector3(mapP[x, y].r, mapP[x, y].g, mapP[x, y].b), new Vector3(c[0].r, c[0].g, c[0].b)) < .03f) {
                    teamCc[0]++;


                }
                else if (Vector3.Distance(new Vector3(mapP[x, y].r, mapP[x, y].g, mapP[x, y].b), new Vector3(c[1].r, c[1].g, c[1].b)) < .03f)
                {
                    teamCc[1]++;


                }else if (Vector3.Distance(new Vector3(mapP[x, y].r, mapP[x, y].g, mapP[x, y].b), new Vector3(c[2].r, c[2].g, c[2].b)) < .03f)
                {
                    teamCc[2]++;


                }
                else if (Vector3.Distance(new Vector3(mapP[x, y].r, mapP[x, y].g, mapP[x, y].b), new Vector3(c[3].r, c[3].g, c[3].b)) < .03f)
                {
                    teamCc[3]++;


                }

            }
        }
      
        if (totalCellsS == 0 && teamCc[0]!=0) {
            totalCellsS = teamCc[0];
            print(totalCellsS);
        }
        
        UpdateBarsS();
    }


    public void UpdateBarsS()
    {
        int totalDiscs = totalCellsS;
        if (totalDiscs == 0)
            return;
        float plPercent = (float)(teamCc[1]) / (float)totalDiscs;
        float[] enPercent = new float[teamCc.Length-2];

        for (int i = 0; i < enPercent.Length; i++)
        {
            enPercent[i] = (float)(teamCc[i + 2]) / (float)totalDiscs;

        }

        float w = plPercent * 600f;  // ;
        barsS[0].sizeDelta = Vector2.Lerp(barsS[0].sizeDelta, new Vector2(w, barsS[0].sizeDelta.y), Time.deltaTime * 12f);
        float shift = w;

        for (int i = 1; i < enPercent.Length + 1; i++)
        {
            barsS[i].anchoredPosition3D = Vector3.Lerp(barsS[i].anchoredPosition3D, new Vector3(shift, 6f, 0f), Time.deltaTime * 12f);

            barsS[i].sizeDelta = Vector2.Lerp(barsS[i].sizeDelta, new Vector2(enPercent[i - 1] * 600f, barsS[i].sizeDelta.y), Time.deltaTime * 12f);
            if (i < enPercent.Length)
                w = enPercent[i - 1] * 600f;
            shift += w;
        }
    }
}

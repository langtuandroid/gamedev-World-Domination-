using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerWD : MonoBehaviour
{
    [FormerlySerializedAs("mats")] public Material[] matsS = new Material[3];
    public Color[] c = new Color[3];
    
    private SandControllerWD sandC;
    
    [FormerlySerializedAs("stickman")] public Transform[] stickMan = new Transform[2];
    public int team = 0;

    public static int count = 0;
    
    private SceneControllerWD sCont;
    private PlayerControllerWD pc;
    
    private int maxPopP=21;
    private float spawnRateE = 2f;

    private float spawnRateBotT = 2f;
    private int maxPopBotT = 21;
   
    private void Start()
    {
        sCont = FindObjectOfType<SceneControllerWD>();
        pc = FindObjectOfType<PlayerControllerWD>();
        sandC = FindObjectOfType<SandControllerWD>();
        print(team);
        maxPopBotT = 21 + Random.Range(0, (SceneControllerWD.level)/2);
        spawnRateBotT = 2f + Random.Range(-.7f + (((float)SceneControllerWD.level) / 18f)  , ((float)SceneControllerWD.level) / 18f);
        //if (team == 1)
        //{
            spawnRateE = 2f + (float)sCont.upgradeLevels[1] / 10f;
            maxPopP = 21 + sCont.upgradeLevels[0];
        //}
        this.transform.GetChild(0).GetComponent<MeshRenderer>().material = matsS[team];
        if (team != 0)
        {
            int pY = Mathf.RoundToInt((this.transform.position.z + TopDownRenderWD.scaleE - TopDownRenderWD.posS.z) * (float)(TopDownRenderWD.sizeE - 1) / (TopDownRenderWD.scaleE * 2f));
            int pX = Mathf.RoundToInt((this.transform.position.x + TopDownRenderWD.scaleE - TopDownRenderWD.posS.x) * (float)(TopDownRenderWD.sizeE - 1) / (2f * TopDownRenderWD.scaleE));
            pX = Mathf.Clamp(pX, 0, TopDownRenderWD.sizeE - 1);
            pY = Mathf.Clamp(pY, 0, TopDownRenderWD.sizeE - 1);

            float pY_ = (this.transform.position.z + TopDownRenderWD.scaleE - TopDownRenderWD.posS.z) * (float)(TopDownRenderWD.sizeE - 1) / (TopDownRenderWD.scaleE * 2f);
            float pX_ = (this.transform.position.x + TopDownRenderWD.scaleE - TopDownRenderWD.posS.x) * (float)(TopDownRenderWD.sizeE - 1) / (2f * TopDownRenderWD.scaleE);
            sandC.DrawTowerR(c[team], new Vector2((float)pX_ / (float)TopDownRenderWD.sizeE, (float)pY_ / (float)TopDownRenderWD.sizeE), count);
            count++;
            if (team == 1)
                pc.playerTowers.Add(this);
            if (team == 2)
                pc.botTowers.Add(this);
            if (team == 3)
                pc.botTowers.Add(this);
        }
        else {
            pc.neutralTowers.Add(this);
        }
        StartCoroutine(SpawnN());

        StartCoroutine(CheckTeamM());
    }

    private IEnumerator CheckTeamM() {
        while (true)
        {

            int pY = Mathf.RoundToInt((this.transform.position.z + TopDownRenderWD.scaleE - TopDownRenderWD.posS.z) * (float)(TopDownRenderWD.sizeE - 1) / (TopDownRenderWD.scaleE * 2f));
            int pX = Mathf.RoundToInt((this.transform.position.x + TopDownRenderWD.scaleE - TopDownRenderWD.posS.x) * (float)(TopDownRenderWD.sizeE - 1) / (2f * TopDownRenderWD.scaleE));
            pX = Mathf.Clamp(pX, 0, TopDownRenderWD.sizeE - 1);
            pY = Mathf.Clamp(pY, 0, TopDownRenderWD.sizeE - 1);



            if (Vector3.Distance(new Vector3(TopDownRenderWD.mapP[pX, pY].r, TopDownRenderWD.mapP[pX, pY].g, TopDownRenderWD.mapP[pX, pY].b), new Vector3(.65f, .63f, .65f)) < .03f)
            {

            }
            else


          if (team!=1 && Vector3.Distance(new Vector3(TopDownRenderWD.mapP[pX, pY].r, TopDownRenderWD.mapP[pX, pY].g, TopDownRenderWD.mapP[pX, pY].b), new Vector3(c[1].r, c[1].g, c[1].b)) < .03f)
            {

                team = 1;
                this.transform.GetChild(0).GetComponent<MeshRenderer>().material = matsS[team];
                pc.playerTowers.Add(this);

                if (pc.neutralTowers.Contains(this)) {
                    pc.neutralTowers.Remove(this);
                }
                if (pc.botTowers.Contains(this))
                {
                    pc.botTowers.Remove(this);
                }
                foreach (StickmanWD st in this.transform.GetComponentsInChildren<StickmanWD>())
                {
                    pc.SplatT(st.transform.position, st.cSplatT);
                    Destroy(st.transform.gameObject);
                }
            }
            if (team != 2 && Vector3.Distance(new Vector3(TopDownRenderWD.mapP[pX, pY].r, TopDownRenderWD.mapP[pX, pY].g, TopDownRenderWD.mapP[pX, pY].b), new Vector3(c[2].r, c[2].g, c[2].b)) < .03f)
            {

                team = 2;
                this.transform.GetChild(0).GetComponent<MeshRenderer>().material = matsS[team];
                pc.botTowers.Add(this);
                if (pc.neutralTowers.Contains(this))
                {
                    pc.neutralTowers.Remove(this);
                }
                if (pc.playerTowers.Contains(this))
                {
                    pc.playerTowers.Remove(this);
                }
                foreach (StickmanWD st in this.transform.GetComponentsInChildren<StickmanWD>())
                {
                    pc.SplatT(st.transform.position, st.cSplatT);
                    Destroy(st.transform.gameObject);
                }
            }
            if (team != 3 && Vector3.Distance(new Vector3(TopDownRenderWD.mapP[pX, pY].r, TopDownRenderWD.mapP[pX, pY].g, TopDownRenderWD.mapP[pX, pY].b), new Vector3(c[3].r, c[3].g, c[3].b)) < .03f)
            {
                
                team = 3;
                this.transform.GetChild(0).GetComponent<MeshRenderer>().material = matsS[team];

                pc.botTowers.Add(this);
                if (pc.neutralTowers.Contains(this))
                {
                    pc.neutralTowers.Remove(this);
                }
                if (pc.playerTowers.Contains(this))
                {
                    pc.playerTowers.Remove(this);
                }
                foreach (StickmanWD st in this.transform.GetComponentsInChildren<StickmanWD>())
                {
                    pc.SplatT(st.transform.position, st.cSplatT);
                    Destroy(st.transform.gameObject);
                }


            }
            yield return new WaitForSeconds(.25f);
        }

    }

    private IEnumerator SpawnN() {

        yield return new WaitForSeconds(1f);
        while (true)
        {

            if (team != 0 &&((team==1 && this.transform.childCount < maxPopP ) || (team > 1 && this.transform.childCount < maxPopBotT)) && SceneControllerWD.levelActive)
            {


                Vector3 r = Random.insideUnitSphere;
                r.y = 0f;
                if (r.magnitude < .5f)
                    r = r.normalized * .5f;
                Vector3 pos = this.transform.position + r;
                pos.y = 0f;
                Transform st = Instantiate(stickMan[team - 1], pos, Quaternion.identity);
                st.parent = this.transform;
                st.GetComponent<StickmanWD>().team = team;
                if (team != 1)
                {
                    st.forward = -Vector3.forward;

                    if (this.transform.childCount >= maxPopBotT - 1)
                    {
                        pc.BotTowerTurnN(this);
                    }
                }




            }
            if (team != 0 && SceneControllerWD.levelActive)
            {
                if (team != 1)
                {
                    if (pc.neutralTowers.Contains(this))
                    {
                        pc.neutralTowers.Remove(this);
                    }
                    if (pc.playerTowers.Contains(this))
                    {
                        pc.playerTowers.Remove(this);
                    }

                }

                if (team == 1)
                {
                    if (pc.neutralTowers.Contains(this))
                    {
                        pc.neutralTowers.Remove(this);
                    }
                    if (pc.playerTowers.Contains(this))
                    {
                        pc.botTowers.Remove(this);
                    }

                }
            }
            yield return new WaitForSeconds(team==1?1f/spawnRateE:1f/spawnRateBotT);
        
        }
    
    }
}

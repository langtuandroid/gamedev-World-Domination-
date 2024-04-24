using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public Material[] mats = new Material[3];
    public Color[] c = new Color[3];
    //public Color[] c_ = new Color[3];
    SandController sandC;
    public Transform[] stickman = new Transform[2];
    public int team = 0;

    public static int count = 0;
    SceneController SCont;
    PlayerController PC;
    int maxPop=21;
    float spawnRate = 2f;

    float spawnRateBot = 2f;// + Random.Range(-.2f, .5f);
    int maxPopBot = 21;//+Random.Range(0,5);
    // Start is called before the first frame update
    void Start()
    {
        SCont = FindObjectOfType<SceneController>();
        PC = FindObjectOfType<PlayerController>();
        sandC = FindObjectOfType<SandController>();
        print(team);
        maxPopBot = 21 + Random.Range(0, (SceneController.level)/2);
        spawnRateBot = 2f + Random.Range(-.7f + (((float)SceneController.level) / 18f)  , ((float)SceneController.level) / 18f);
        //if (team == 1)
        //{
            spawnRate = 2f + (float)SCont.upgradeLevels[1] / 10f;
            maxPop = 21 + SCont.upgradeLevels[0];
        //}
        this.transform.GetChild(0).GetComponent<MeshRenderer>().material = mats[team];
        if (team != 0)
        {
            int pY = Mathf.RoundToInt((this.transform.position.z + TopDownRender.scale - TopDownRender.pos.z) * (float)(TopDownRender.size - 1) / (TopDownRender.scale * 2f));
            int pX = Mathf.RoundToInt((this.transform.position.x + TopDownRender.scale - TopDownRender.pos.x) * (float)(TopDownRender.size - 1) / (2f * TopDownRender.scale));
            pX = Mathf.Clamp(pX, 0, TopDownRender.size - 1);
            pY = Mathf.Clamp(pY, 0, TopDownRender.size - 1);

            float pY_ = (this.transform.position.z + TopDownRender.scale - TopDownRender.pos.z) * (float)(TopDownRender.size - 1) / (TopDownRender.scale * 2f);
            float pX_ = (this.transform.position.x + TopDownRender.scale - TopDownRender.pos.x) * (float)(TopDownRender.size - 1) / (2f * TopDownRender.scale);
            sandC.DrawTower(c[team], new Vector2((float)pX_ / (float)TopDownRender.size, (float)pY_ / (float)TopDownRender.size), count);
            count++;
            if (team == 1)
                PC.playerTwrs.Add(this);
            if (team == 2)
                PC.botTwrs.Add(this);
            if (team == 3)
                PC.botTwrs.Add(this);
        }
        else {
            PC.neutralTwrs.Add(this);
        }
        StartCoroutine(Spawn());

        StartCoroutine(CheckTeam());
    }

    // Update is called once per frame
    void Update()
    {


    }

    IEnumerator CheckTeam() {
        while (true)
        {

            int pY = Mathf.RoundToInt((this.transform.position.z + TopDownRender.scale - TopDownRender.pos.z) * (float)(TopDownRender.size - 1) / (TopDownRender.scale * 2f));
            int pX = Mathf.RoundToInt((this.transform.position.x + TopDownRender.scale - TopDownRender.pos.x) * (float)(TopDownRender.size - 1) / (2f * TopDownRender.scale));
            pX = Mathf.Clamp(pX, 0, TopDownRender.size - 1);
            pY = Mathf.Clamp(pY, 0, TopDownRender.size - 1);



            if (Vector3.Distance(new Vector3(TopDownRender.map[pX, pY].r, TopDownRender.map[pX, pY].g, TopDownRender.map[pX, pY].b), new Vector3(.65f, .63f, .65f)) < .03f)
            {



             //   print("out");
            }
            else


          if (team!=1 && Vector3.Distance(new Vector3(TopDownRender.map[pX, pY].r, TopDownRender.map[pX, pY].g, TopDownRender.map[pX, pY].b), new Vector3(c[1].r, c[1].g, c[1].b)) < .03f)
            {

                team = 1;
                this.transform.GetChild(0).GetComponent<MeshRenderer>().material = mats[team];
                PC.playerTwrs.Add(this);

                if (PC.neutralTwrs.Contains(this)) {
                    PC.neutralTwrs.Remove(this);
                }
                if (PC.botTwrs.Contains(this))
                {
                    PC.botTwrs.Remove(this);
                }
                foreach (Stickman st in this.transform.GetComponentsInChildren<Stickman>())
                {
                    PC.Splat(st.transform.position, st.cSplat);
                    Destroy(st.transform.gameObject);
                }
                //print(pX+" "+pY+" "+TopDownRender.map[pX, pY] + " " + c);
                //  print("STAMP" + (float)pX_ / (float)size+" "+ (float)pY_ / (float)size);
            }
            if (team != 2 && Vector3.Distance(new Vector3(TopDownRender.map[pX, pY].r, TopDownRender.map[pX, pY].g, TopDownRender.map[pX, pY].b), new Vector3(c[2].r, c[2].g, c[2].b)) < .03f)
            {

                team = 2;
                this.transform.GetChild(0).GetComponent<MeshRenderer>().material = mats[team];
                PC.botTwrs.Add(this);
                if (PC.neutralTwrs.Contains(this))
                {
                    PC.neutralTwrs.Remove(this);
                }
                if (PC.playerTwrs.Contains(this))
                {
                    PC.playerTwrs.Remove(this);
                }
                foreach (Stickman st in this.transform.GetComponentsInChildren<Stickman>())
                {
                    PC.Splat(st.transform.position, st.cSplat);
                    Destroy(st.transform.gameObject);
                }
                //print(pX+" "+pY+" "+TopDownRender.map[pX, pY] + " " + c);
                //  print("STAMP" + (float)pX_ / (float)size+" "+ (float)pY_ / (float)size);
            }
            if (team != 3 && Vector3.Distance(new Vector3(TopDownRender.map[pX, pY].r, TopDownRender.map[pX, pY].g, TopDownRender.map[pX, pY].b), new Vector3(c[3].r, c[3].g, c[3].b)) < .03f)
            {
                
                team = 3;
                this.transform.GetChild(0).GetComponent<MeshRenderer>().material = mats[team];

                PC.botTwrs.Add(this);
                if (PC.neutralTwrs.Contains(this))
                {
                    PC.neutralTwrs.Remove(this);
                }
                if (PC.playerTwrs.Contains(this))
                {
                    PC.playerTwrs.Remove(this);
                }
                foreach (Stickman st in this.transform.GetComponentsInChildren<Stickman>())
                {
                    PC.Splat(st.transform.position, st.cSplat);
                    Destroy(st.transform.gameObject);
                }


            }
            yield return new WaitForSeconds(.25f);
        }

    }

    IEnumerator Spawn() {

        yield return new WaitForSeconds(1f);
        while (true)
        {

            if (team != 0 &&((team==1 && this.transform.childCount < maxPop ) || (team > 1 && this.transform.childCount < maxPopBot)) && SceneController.levelActive)
            {


                Vector3 r = Random.insideUnitSphere;
                r.y = 0f;
                if (r.magnitude < .5f)
                    r = r.normalized * .5f;
                Vector3 pos = this.transform.position + r;
                pos.y = 0f;
                Transform st = Instantiate(stickman[team - 1], pos, Quaternion.identity);
                st.parent = this.transform;
                st.GetComponent<Stickman>().team = team;
                if (team != 1)
                {
                    st.forward = -Vector3.forward;

                    if (this.transform.childCount >= maxPopBot - 1)
                    {
                        PC.BotTowerTurn(this);
                    }
                }




            }
            if (team != 0 && SceneController.levelActive)
            {
                if (team != 1)
                {
                    if (PC.neutralTwrs.Contains(this))
                    {
                        PC.neutralTwrs.Remove(this);
                    }
                    if (PC.playerTwrs.Contains(this))
                    {
                        PC.playerTwrs.Remove(this);
                    }

                }

                if (team == 1)
                {
                    if (PC.neutralTwrs.Contains(this))
                    {
                        PC.neutralTwrs.Remove(this);
                    }
                    if (PC.playerTwrs.Contains(this))
                    {
                        PC.botTwrs.Remove(this);
                    }

                }
            }
            yield return new WaitForSeconds(team==1?1f/spawnRate:1f/spawnRateBot);
        
        }
    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerControllerWD : MonoBehaviour
{
    private bool isSelectedD = false;
    private Transform selectedT;
    private Vector3 dirR;

    [SerializeField] private Transform[] lvl = new Transform[2];

    private float camSpeedD = 0f;

    [SerializeField] private ParticleSystem splatter;

    private float delayY = 1f;

    public static bool gameActiveE =false;

    [FormerlySerializedAs("botTwrs")] public List<TowerWD> botTowers = new List<TowerWD>();
    [FormerlySerializedAs("playerTwrs")] public List<TowerWD> playerTowers = new List<TowerWD>();
    [FormerlySerializedAs("neutralTwrs")] public List<TowerWD> neutralTowers = new List<TowerWD>();
    private SandControllerWD sc;
    private SceneControllerWD sceneControllerWdR;
    public static int levelL = 2;// 0 - never, 1,2,3...
    // Start is called before the first frame update
    
    private void Start()
    {
       // Debug.Break();
        sc = FindObjectOfType<SandControllerWD>();
        sceneControllerWdR = FindObjectOfType<SceneControllerWD>();
        Application.targetFrameRate = 30;
        gameActiveE = true;
        print(10f * (1080f / 1920f) /( (float)Screen.width / (float)Screen.height));
        this.transform.GetComponent<Camera>().fieldOfView = 56f + 6f*(1080f / 1920f) / ((float)Screen.width / (float)Screen.height);

        //PickLevel();
        StartCoroutine(BotMoveE());
    }
    
    public void PickLevelL() {

        levelL = SceneControllerWD.level;
        for (int i = 0; i < lvl.Length; i++) {

            if (i != levelL - 1)
            {
                List<Transform> st = new List<Transform>();
                for (int j = 0; j < lvl[i].childCount; j++)
                {
                    if (lvl[i].GetChild(j).tag == "State")
                    {
                        st.Add(lvl[i].GetChild(j));
                    }

                }
                for (int j = 0; j < st.Count; j++)
                {
                    st[j].parent = lvl[i].parent;

                }
                lvl[i].gameObject.SetActive(false);
            }
            else {
                for (int j = 0; j < lvl[i].childCount; j++)
                {
                    if (lvl[i].GetChild(j).tag == "State")
                    {
                        lvl[i].GetChild(j).gameObject.SetActive(false);
                        lvl[i].GetComponentInChildren<Camera>(true).gameObject.SetActive( true);
                    }
                    if (lvl[i].GetChild(j).GetComponent<TowerWD>()!=null)
                    {
                        //print("tttt");
                        lvl[i].GetChild(j).GetComponent<TowerWD>().enabled = true;
                    }
                }

            }
        }
        sc.LevelInitialize();
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

            if (Physics.Raycast(ray, out hit, 100f, 1 << 10))
            {

                if (hit.transform.GetComponentInParent<TowerWD>().team == 1)
                {
                    selectedT = hit.transform;
                    isSelectedD = true;
                    dirR = Vector3.zero;
                }
            }

        }
        if (Input.GetMouseButton(0) && isSelectedD)
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

            if (Physics.Raycast(ray, out hit, 100f, 1<<13))
            {

                Vector3 hPoint = hit.point;
                hPoint.y = 0f;

                Vector3 sPoint = selectedT.position;
                sPoint.y = 0f;
                dirR = Vector3.Lerp(dirR, (hPoint - sPoint),Time.deltaTime*16f);

                dirR.y = 0f;


                selectedT.GetChild(0).localScale = Vector3.one * Mathf.Min( dirR.magnitude*5f,10f);
                selectedT.GetChild(0).rotation  =  Quaternion.LookRotation(-Vector3.up, dirR.normalized);

            }
        }

        if (Input.GetMouseButtonUp(0) && isSelectedD)
        {
            foreach (StickmanWD st in selectedT.parent.GetComponentsInChildren<StickmanWD>())
            {
                st.StartMoving(dirR);
                st.transform.parent = null;
            }
            print(selectedT.name);
            print(selectedT.GetChild(0).name);

            selectedT.GetChild(0).localScale = Vector3.zero;
            isSelectedD = false;
            selectedT = null;


            FingerMovementWD.disableTutT = true;
        }

        if (SceneControllerWD.levelActive)//delay <= 0f)
        {
            if (camSpeedD < 1f)
            {
                camSpeedD += Time.deltaTime;
            }

            this.transform.position = Vector3.Lerp(this.transform.position, lvl[levelL - 1].GetChild(0).position + new Vector3(.5f, 24f, -13f), Time.deltaTime * 4f * camSpeedD);
            // this.transform.position += (lvl1.position + new Vector3(0f, 20f, -12f) - this.transform.position).normalized*Time.deltaTime*2f* camSpeed;
        }
      //  else {
      //      delay -= Time.deltaTime;
      //  }
    }


    private IEnumerator BotMoveE() {

        while (botTowers.Count == 0 || playerTowers.Count == 0)
        {

            yield return null;
        }

        while (botTowers.Count > 0 && playerTowers.Count>0) {
            TowerWD bT = botTowers[Random.Range(0, botTowers.Count)];
          

            BotTowerTurnN(bT);
            yield return new WaitForSeconds(Random.Range(1f, 4f));
        }
        yield return new WaitForSeconds(3f);
       
        gameActiveE = false;

        if (playerTowers.Count > 0)
            sceneControllerWdR.EndLevelVictoryY();
        else
            sceneControllerWdR.EndLevelLossS();

        yield return new WaitForSeconds(2f);
        sc.ReInitializeE();
        //SceneManager.LoadScene(0);


    }


    public void BotTowerTurnN(TowerWD bT) {
        Vector3 bDir = Quaternion.AngleAxis(Random.Range(-30f, 30f), Vector3.up) * (playerTowers[Random.Range(0, playerTowers.Count)].transform.position - bT.transform.position).normalized;
        TowerWD botTarget = botTowers[Random.Range(0, botTowers.Count)];
        if (botTarget.team != bT.team && Random.value>.5f) {
            bDir = Quaternion.AngleAxis(Random.Range(-20f, 20f), Vector3.up) * (botTarget.transform.position - bT.transform.position).normalized;
        }


        if (neutralTowers.Count > 0 && Random.value > .5f)
        {
            print(neutralTowers.Count + " _________________");
            TowerWD targetT = neutralTowers[Random.Range(0, neutralTowers.Count)];
            float minDist = Vector3.Distance(targetT.transform.position, bT.transform.position);
            for (int i = 0; i < 5; i++)
            {
                TowerWD targetT_ = neutralTowers[Random.Range(0, neutralTowers.Count)];
                float d = Vector3.Distance(targetT_.transform.position, bT.transform.position);
                if (d < minDist) {
                    minDist = d;
                    targetT = targetT_;
                }

            }


            bDir = Quaternion.AngleAxis(Random.Range(-20f, 20f), Vector3.up) * (targetT.transform.position - bT.transform.position).normalized;

        }
        foreach (StickmanWD st in bT.GetComponentsInChildren<StickmanWD>())
        {
            st.StartMoving(bDir);
            st.transform.parent = null;
        }

    }

    public void SplatT(Vector3 pos, Color c) {
        splatter.transform.position = pos;
        splatter.startColor = c;
        splatter.Emit(25);


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    bool isSelected = false;
    Transform selectedT;
    Vector3 dir;

    public Transform[] lvl = new Transform[2];

    float camSpeed = 0f;

    public ParticleSystem splatter;

    float delay = 1f;

    public static bool gameActive =false;

    public List<Tower> botTwrs = new List<Tower>();
    public List<Tower> playerTwrs = new List<Tower>();
    public List<Tower> neutralTwrs = new List<Tower>();
    SandController SC;
    SceneController sceneController;
    public static int level = 2;// 0 - never, 1,2,3...
    // Start is called before the first frame update
    void Start()
    {
       // Debug.Break();
        SC = FindObjectOfType<SandController>();
        sceneController = FindObjectOfType<SceneController>();
        Application.targetFrameRate = 30;
        gameActive = true;
        print(10f * (1080f / 1920f) /( (float)Screen.width / (float)Screen.height));
        this.transform.GetComponent<Camera>().fieldOfView = 56f + 6f*(1080f / 1920f) / ((float)Screen.width / (float)Screen.height);

        //PickLevel();
        StartCoroutine(BotMove());


    }



    public void PickLevel() {

        level = SceneController.level;
        for (int i = 0; i < lvl.Length; i++) {

            if (i != level - 1)
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
                    if (lvl[i].GetChild(j).GetComponent<Tower>()!=null)
                    {
                        //print("tttt");
                        lvl[i].GetChild(j).GetComponent<Tower>().enabled = true;
                    }
                }

            }
        }
        SC.LevelInitialize();
    }


    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

            if (Physics.Raycast(ray, out hit, 100f, 1 << 10))
            {

                if (hit.transform.GetComponentInParent<Tower>().team == 1)
                {
                    selectedT = hit.transform;
                    isSelected = true;
                    dir = Vector3.zero;
                }
            }

        }
        if (Input.GetMouseButton(0) && isSelected)
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
                dir = Vector3.Lerp(dir, (hPoint - sPoint),Time.deltaTime*16f);

                dir.y = 0f;


                selectedT.GetChild(0).localScale = Vector3.one * Mathf.Min( dir.magnitude*5f,10f);
                selectedT.GetChild(0).rotation  =  Quaternion.LookRotation(-Vector3.up, dir.normalized);

            }
        }

        if (Input.GetMouseButtonUp(0) && isSelected)
        {
            foreach (Stickman st in selectedT.parent.GetComponentsInChildren<Stickman>())
            {
                st.StartMoving(dir);
                st.transform.parent = null;
            }
            print(selectedT.name);
            print(selectedT.GetChild(0).name);

            selectedT.GetChild(0).localScale = Vector3.zero;
            isSelected = false;
            selectedT = null;


            FingerMovement.disableTut = true;
        }

        if (SceneController.levelActive)//delay <= 0f)
        {
            if (camSpeed < 1f)
            {
                camSpeed += Time.deltaTime;
            }

            this.transform.position = Vector3.Lerp(this.transform.position, lvl[level - 1].GetChild(0).position + new Vector3(.5f, 24f, -13f), Time.deltaTime * 4f * camSpeed);
            // this.transform.position += (lvl1.position + new Vector3(0f, 20f, -12f) - this.transform.position).normalized*Time.deltaTime*2f* camSpeed;
        }
      //  else {
      //      delay -= Time.deltaTime;
      //  }
    }


    IEnumerator BotMove() {

        while (botTwrs.Count == 0 || playerTwrs.Count == 0)
        {

            yield return null;
        }

        while (botTwrs.Count > 0 && playerTwrs.Count>0) {
            Tower bT = botTwrs[Random.Range(0, botTwrs.Count)];
          

            BotTowerTurn(bT);
            yield return new WaitForSeconds(Random.Range(1f, 4f));
        }
        yield return new WaitForSeconds(3f);
       
        gameActive = false;

        if (playerTwrs.Count > 0)
            sceneController.EndLevelVictory();
        else
            sceneController.EndLevelLoss();

        yield return new WaitForSeconds(2f);
        SC.ReInitialize();
        //SceneManager.LoadScene(0);


    }


    public void BotTowerTurn(Tower bT) {
        Vector3 bDir = Quaternion.AngleAxis(Random.Range(-30f, 30f), Vector3.up) * (playerTwrs[Random.Range(0, playerTwrs.Count)].transform.position - bT.transform.position).normalized;
        Tower botTarget = botTwrs[Random.Range(0, botTwrs.Count)];
        if (botTarget.team != bT.team && Random.value>.5f) {
            bDir = Quaternion.AngleAxis(Random.Range(-20f, 20f), Vector3.up) * (botTarget.transform.position - bT.transform.position).normalized;
        }


        if (neutralTwrs.Count > 0 && Random.value > .5f)
        {
            print(neutralTwrs.Count + " _________________");
            Tower targetT = neutralTwrs[Random.Range(0, neutralTwrs.Count)];
            float minDist = Vector3.Distance(targetT.transform.position, bT.transform.position);
            for (int i = 0; i < 5; i++)
            {
                Tower targetT_ = neutralTwrs[Random.Range(0, neutralTwrs.Count)];
                float d = Vector3.Distance(targetT_.transform.position, bT.transform.position);
                if (d < minDist) {
                    minDist = d;
                    targetT = targetT_;
                }

            }


            bDir = Quaternion.AngleAxis(Random.Range(-20f, 20f), Vector3.up) * (targetT.transform.position - bT.transform.position).normalized;

        }
        foreach (Stickman st in bT.GetComponentsInChildren<Stickman>())
        {
            st.StartMoving(bDir);
            st.transform.parent = null;
        }

    }

    public void Splat(Vector3 pos, Color c) {
        splatter.transform.position = pos;
        splatter.startColor = c;
        splatter.Emit(25);


    }
}

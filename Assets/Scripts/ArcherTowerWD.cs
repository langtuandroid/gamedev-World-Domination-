using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ArcherTowerWD : MonoBehaviour
{
    [FormerlySerializedAs("mats")] [SerializeField] private Material[] matsS = new Material[3];
    [FormerlySerializedAs("bulletMats")] [SerializeField] private Material[] bulletMatsS = new Material[3];
    [FormerlySerializedAs("c")] [SerializeField] private Color[] colors = new Color[3];
    private SandControllerWD sandCc;
    [FormerlySerializedAs("stickman")] [SerializeField] private Transform[] stickmanN = new Transform[2];
    [SerializeField] private int team = 0;

    public static int count = 0;

    private PlayerControllerWD pc;

    private List<Transform> listOfStickMan = new List<Transform>();

    [SerializeField] private Transform projectile;
    private readonly List<Transform> projectileS = new List<Transform>();
  
    private void Start()
    {
        pc = FindObjectOfType<PlayerControllerWD>();
        
        StartCoroutine(CheckTeamM());
        StartCoroutine(ShootT());
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



             //   print("out");
            }
            else


          if (team!=1 && Vector3.Distance(new Vector3(TopDownRenderWD.mapP[pX, pY].r, TopDownRenderWD.mapP[pX, pY].g, TopDownRenderWD.mapP[pX, pY].b), new Vector3(colors[1].r, colors[1].g, colors[1].b)) < .03f)
            {
                listOfStickMan = new List<Transform>();
                team = 1;
                this.transform.GetChild(0).GetComponent<MeshRenderer>().material = matsS[team];
             
              
            }
            if (team != 2 && Vector3.Distance(new Vector3(TopDownRenderWD.mapP[pX, pY].r, TopDownRenderWD.mapP[pX, pY].g, TopDownRenderWD.mapP[pX, pY].b), new Vector3(colors[2].r, colors[2].g, colors[2].b)) < .03f)
            {
                listOfStickMan = new List<Transform>();
                team = 2;
                this.transform.GetChild(0).GetComponent<MeshRenderer>().material = matsS[team];
              
           
            }
            if (team != 3 && Vector3.Distance(new Vector3(TopDownRenderWD.mapP[pX, pY].r, TopDownRenderWD.mapP[pX, pY].g, TopDownRenderWD.mapP[pX, pY].b), new Vector3(colors[3].r, colors[3].g, colors[3].b)) < .03f)
            {
                listOfStickMan = new List<Transform>();
                team = 3;
                this.transform.GetChild(0).GetComponent<MeshRenderer>().material = matsS[team];


            }
            yield return new WaitForSeconds(.25f);
        }

    }

    private IEnumerator ShootT() {

        yield return new WaitForSeconds(1f);
        while (true) {

            float minDist = 10000f;
            Transform closest = null;

            List<Transform> toRemove = new List<Transform>();
            for (int i = 0; i < listOfStickMan.Count; i++) {
                if (listOfStickMan[i] == null)
                {
                    toRemove.Add(listOfStickMan[i]);
                    
                    continue;
                }
                float d = Vector3.Distance(listOfStickMan[i].position, this.transform.position);
                if (d < minDist) {
                    if (listOfStickMan[i].transform.GetComponent<StickmanWD>().team != team)
                    {
                        minDist = d;
                        closest = listOfStickMan[i];
                    }
                    else {
                        toRemove.Add(listOfStickMan[i]);
                    }
                }
            
            }

            if (closest != null)
            {
                Transform pr = Instantiate(projectile, this.transform.position, Quaternion.identity);
                pr.GetComponent<MeshRenderer>().sharedMaterial = bulletMatsS[team];
                pr.GetComponent<BulletWD>().targetT = closest;
                projectileS.Add(pr);
            }
            
            while (toRemove.Count>0)
            {
               if (listOfStickMan.Contains(toRemove[0]))
                    listOfStickMan.Remove(toRemove[0]); 
               if(toRemove[0]!=null)
                    Destroy(toRemove[0].gameObject);
               toRemove.RemoveAt(0);
               
            }


            yield return new WaitForSeconds(.25f);
        
        }
    
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && other.transform.GetComponent<StickmanWD>().team!=team) {

            listOfStickMan.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8 &&listOfStickMan.Contains(other.transform))
        {

            listOfStickMan.Remove(other.transform);
        }
    }
}

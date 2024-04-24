using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StickmanWD : MonoBehaviour
{
    private readonly float speedD = 1.5f;

    [FormerlySerializedAs("c")] public Color cC;
    [FormerlySerializedAs("cSplat")] public Color cSplatT;
    
    private int pX = 0;
    private int pY = 0;

    private float screenK = 1f;

    private SandControllerWD sandC;
    private Vector3 dirR;

    private float kK = 1f;

    private bool isMovingG = false;

    private PlayerControllerWD pc;

    public int team = 0; 

    private void Start()
    {
        pc = FindObjectOfType<PlayerControllerWD>();
        sandC = FindObjectOfType<SandControllerWD>();
        
        StartCoroutine(UpdateColorR());
        dirR = new Vector3(-1f, 0f, 1f)+Random.onUnitSphere/3f;

        dirR.y = 0f;
        kK = 3.74f / 3.12f;
    }

  
    private void Update()
    {
        if(isMovingG)
            MoveE(dirR);
        
        pY =Mathf.RoundToInt ((this.transform.position.z + TopDownRenderWD.scaleE-TopDownRenderWD.posS.z) * (float)(TopDownRenderWD.sizeE -1) / (TopDownRenderWD.scaleE*2f)  );
        pX = Mathf.RoundToInt((this.transform.position.x + TopDownRenderWD.scaleE - TopDownRenderWD.posS.x) * (float)(TopDownRenderWD.sizeE - 1) / (2f* TopDownRenderWD.scaleE) );
        pX = Mathf.Clamp(pX, 0, TopDownRenderWD.sizeE -1);
        pY = Mathf.Clamp(pY, 0, TopDownRenderWD.sizeE - 1);
    }
    public void StartMoving(Vector3 dir_) {
        this.transform.GetComponent<Animator>().SetTrigger("Move");
        dirR = dir_;
        isMovingG = true;
    }

    private IEnumerator UpdateColorR() {
        yield return null;
        yield return null;
        yield return null;
        while (true) {

            if (Vector3.Distance(new Vector3(TopDownRenderWD.mapP[pX, pY].r, TopDownRenderWD.mapP[pX, pY].g, TopDownRenderWD.mapP[pX, pY].b), new Vector3(.61f, .61f, .61f)) < .03f)
            {

              

                print("out");
            }else


            if (Vector3.Distance(new Vector3(TopDownRenderWD.mapP[pX, pY].r, TopDownRenderWD.mapP[pX, pY].g, TopDownRenderWD.mapP[pX, pY].b), new Vector3(cC.r,cC.g,cC.b))>.03f)
            {

                float pY_ =(this.transform.position.z + TopDownRenderWD.scaleE - TopDownRenderWD.posS.z) * (float)(TopDownRenderWD.sizeE - 1) / (TopDownRenderWD.scaleE*2f) ;
                float pX_ =  (this.transform.position.x + TopDownRenderWD.scaleE - TopDownRenderWD.posS.x) * (float)(TopDownRenderWD.sizeE - 1) / (2f* TopDownRenderWD.scaleE) ;
                // sandC.DrawRemotely(c, new Vector2((float)pX_ / (float)TopDownRender.size, (float)pY_ / (float)TopDownRender.size));
                sandC.points.Add(new SandControllerWD.Pts {c= cC,p= new Vector2((float)pX_ / (float)TopDownRenderWD.sizeE, (float)pY_ / (float)TopDownRenderWD.sizeE) });
                pc.SplatT(this.transform.position, cSplatT);
                 Destroy(this.gameObject);
            }
            yield return null;
        }
    }

    public void HitT() {
        pc.SplatT(this.transform.position, cSplatT);
        Destroy(this.gameObject);
    }

    private void MoveE(Vector3 dir)
    {
        transform.GetComponent<Rigidbody>().MovePosition(this.transform.position + dir.normalized * Time.deltaTime * speedD);
        transform.forward = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14 && other.GetComponentInParent<StickmanWD>().team!=team) {
            pc.SplatT(this.transform.position, cSplatT);
            Destroy(other.GetComponentInParent<StickmanWD>().gameObject);
            Destroy(this.gameObject);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickman : MonoBehaviour
{
    float speed = 1.5f;

    public Color c;
    public Color cSplat;


    int pX = 0;
    int pY = 0;

    float screenK = 1f;

    SandController sandC;
    Vector3 dir;

    float k = 1f;

    bool isMoving = false;

    PlayerController PC;

    public int team = 0; 
    // Start is called before the first frame update
    void Start()
    {
        PC = FindObjectOfType<PlayerController>();
        sandC = FindObjectOfType<SandController>();
       // screenK = (float)Screen.width / (float)Screen.height;
        StartCoroutine(UpdateColor());
        dir = new Vector3(-1f, 0f, 1f)+Random.onUnitSphere/3f;

        dir.y = 0f;
        k = 3.74f / 3.12f;

        // screenK = 3.74f/ 3.12f;
    }

    // Update is called once per frame
    void Update()
    {

        if(isMoving)
            Move(dir);

     

        pY =Mathf.RoundToInt ((this.transform.position.z + TopDownRender.scale-TopDownRender.pos.z) * (float)(TopDownRender.size -1) / (TopDownRender.scale*2f)  );
        pX = Mathf.RoundToInt((this.transform.position.x + TopDownRender.scale - TopDownRender.pos.x) * (float)(TopDownRender.size - 1) / (2f* TopDownRender.scale) );
        pX = Mathf.Clamp(pX, 0, TopDownRender.size -1);
        pY = Mathf.Clamp(pY, 0, TopDownRender.size - 1);



       // 


           
      //  
    }
    public void StartMoving(Vector3 dir_) {
        this.transform.GetComponent<Animator>().SetTrigger("Move");
        dir = dir_;
        isMoving = true;
    }

    IEnumerator UpdateColor() {
        yield return null;
        yield return null;
        yield return null;
        while (true) {

            if (Vector3.Distance(new Vector3(TopDownRender.map[pX, pY].r, TopDownRender.map[pX, pY].g, TopDownRender.map[pX, pY].b), new Vector3(.61f, .61f, .61f)) < .03f)
            {

              

                print("out");
            }else


            if (Vector3.Distance(new Vector3(TopDownRender.map[pX, pY].r, TopDownRender.map[pX, pY].g, TopDownRender.map[pX, pY].b), new Vector3(c.r,c.g,c.b))>.03f)
            {

                float pY_ =(this.transform.position.z + TopDownRender.scale - TopDownRender.pos.z) * (float)(TopDownRender.size - 1) / (TopDownRender.scale*2f) ;
                float pX_ =  (this.transform.position.x + TopDownRender.scale - TopDownRender.pos.x) * (float)(TopDownRender.size - 1) / (2f* TopDownRender.scale) ;
                // sandC.DrawRemotely(c, new Vector2((float)pX_ / (float)TopDownRender.size, (float)pY_ / (float)TopDownRender.size));
                sandC.points.Add(new SandController.Pts {c= c,p= new Vector2((float)pX_ / (float)TopDownRender.size, (float)pY_ / (float)TopDownRender.size) });
                PC.Splat(this.transform.position, cSplat);
                 Destroy(this.gameObject);
                //print(pX+" "+pY+" "+TopDownRender.map[pX, pY] + " " + c);
              //  print("STAMP" + (float)pX_ / (float)size+" "+ (float)pY_ / (float)size);
            }
            yield return null;
        }
    
    }

    public void Hit() {
        PC.Splat(this.transform.position, cSplat);
        Destroy(this.gameObject);
    }

    private void Move(Vector3 dir)
    {
        this.transform.GetComponent<Rigidbody>().MovePosition(this.transform.position + dir.normalized * Time.deltaTime * speed);
        //this.transform.position += dir.normalized * Time.deltaTime * speed;
        this.transform.forward = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14 && other.GetComponentInParent<Stickman>().team!=team) {
            PC.Splat(this.transform.position, cSplat);
            Destroy(other.GetComponentInParent<Stickman>().gameObject);
            Destroy(this.gameObject);

        }
    }
}

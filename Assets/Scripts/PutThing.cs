using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutThing : MonoBehaviour {

    Ray putray;
    RaycastHit info;

    public GameObject block;

	// Use this for initialization
	void Start () {
        putray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
	}

    void checkRay() {
        putray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
    }

    int up(float v) {
        int vi = (int)v;
        if (v < vi + 0.5) return vi;
        else return vi + 1;
    }

    Vector3 upVector3(Vector3 tar) {
        return new Vector3((float)up(tar.x), (float)up(tar.y), (float)up(tar.z));
    }

	// Update is called once per frame
	void Update () {
        checkRay();
		if(Physics.Raycast(putray,out info,10f)){
            if (Input.GetMouseButtonDown(0))
            {
                //if(info.transform.gameObject.tag.Equals("BREAK")){
                    Destroy(info.transform.gameObject);
                //}
            }else if (Input.GetMouseButtonDown(1))
            {
                GameObject g = GameObject.Instantiate(block,info.normal.normalized+info.transform.position,Quaternion.identity);
                if (g.GetComponent<Collider>().bounds.Intersects(PlayerMove.pubTrans.GetComponent<Collider>().bounds) && PlayerMove.pubTrans.position.y < g.transform.position.y + 1)
                {
                    Destroy(g);
                }
                g.transform.parent = transform;
            }
        }
        //Debug.Log(Mathf.Sign(Vector3.Dot(Vector3.forward, Camera.main.transform.forward)));
	}
}

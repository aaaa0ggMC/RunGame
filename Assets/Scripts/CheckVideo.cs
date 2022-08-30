using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVideo : MonoBehaviour {

    Ray checkRay;
    RaycastHit info;

	// Use this for initialization
	void Start () {
		
	}

    void updateRay() {
        checkRay = new Ray(transform.position,-Vector3.up);
    }
	
	// Update is called once per frame
	void Update () {
        updateRay();
        if(Physics.Raycast(checkRay,out info,1.2f)){
            if(info.transform.gameObject.tag.Equals("<")){
                VideoPlayerP.next_index();
                PlayerMove.getSpawn = true;
            }
            else if (info.transform.gameObject.tag.Equals(">"))
            {
                VideoPlayerP.up_index();
                PlayerMove.getSpawn = true;
            }
            else if (info.transform.gameObject.tag.Equals("pause"))
            {
                VideoPlayerP.player.Pause();
                PlayerMove.getSpawn = true;
            }
            else if (info.transform.gameObject.tag.Equals("play"))
            {
                VideoPlayerP.player.Play();
                PlayerMove.getSpawn = true;
            }
        }
	}
}

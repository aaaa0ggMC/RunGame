using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public static float start_index = 0;

    static bool part = false;

    public static float move_index = 0.032f;
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(new Vector3(start_index, 0, 0));
        start_index += move_index;
        if (!part)
        {
            old = start_index;
        }
	}

    public static float old = start_index;

    public static void partSet(float start,float move){
        start_index = start;
        move_index = move;
        part = true;
    }

    public static void reSet() {
        part = false;
        start_index = old;
    }
}

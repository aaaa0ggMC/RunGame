using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    float oldx, oldy;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        oldx = oldy = 0f;
	}

    float c_x = 16;
    float c_y = 90;
    float c_z = 0;

    float t_x = 0;
    float t_y = 0;
    float t_z = 0;

    public float sp = 5f;
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Mouse X") * sp + oldx;
        float y = Input.GetAxis("Mouse Y") * sp + oldy;
        Camera.main.transform.localRotation = Quaternion.Euler(-y+c_x, 0+c_y, 0+c_z);
        transform.localRotation = Quaternion.Euler(t_x, x+t_y, 0+t_z);
        oldx = x;
        oldy = y;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    //原理：通过RAY来移动
    Ray xray, zray,yray;

    public float move_sp = 0.2f;
    public float Jump_heiht = 0.2f;

    bool isjump, isTowjump;

    int jump_frame = 8;
    int now = 0;

    public static Vector3 spawnpoint;

    bool isHide = true;

    RaycastHit info;


    bool ice_move = false;
    bool isCreate = false;

    public static bool getSpawn = false;

    public static Transform pubTrans;

    bool shifting = false;

    bool creative = false;

    int crFrame = 0,ccFrame = 0;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        spawnpoint = transform.position;
	}
	
    void fixedRay(){
        //初始化RAY，默认xray.direction = transform.right;
        xray = new Ray(transform.position, transform.right);
        zray = new Ray(transform.position, transform.forward);
        yray = new Ray(transform.position, -Vector3.up);
    }

    void FixedUpdate() {
        if (crFrame != 0) {
            ccFrame++;
            if (ccFrame > 1.5 * (1 / 0.02)) {//1.5s未收到反映
                ccFrame = 0;
                crFrame = 0;
            }
        }
    }

	// Update is called once per frame
	void Update () {
        fixedRay();
        checkSpawn();
        #region 检测KEY
        if(Input.GetKey(KeyCode.W)){
            //想前移 W,ray:+xray
            W_move();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            /*bool use = false;
            if (shifting)
            {
                if (Physics.Raycast(yray, out info, 1.2f))
                {
                    use = true;
                }
            }*/
            transform.position = zray.GetPoint(move_sp);
            /*if (shifting)
            {
                while (!Physics.Raycast(yray, 1.2f) && use)
                {
                    transform.position += info.normal.normalized / 10;
                }
            }*/
        }
        else if (Input.GetKey(KeyCode.S))
        {
            /*bool use = false;
            if (shifting)
            {
                if (Physics.Raycast(yray, out info, 1.2f))
                {
                    use = true;
                }
            }*/
            transform.position = xray.GetPoint(-move_sp);
            /*if (shifting)
            {
                while (!Physics.Raycast(yray, 1.2f) && use)
                {
                    transform.position += info.normal.normalized / 10;
                }
            }*/
        }
        else if (Input.GetKey(KeyCode.D))
        {
            /*bool use = false;
            if (shifting)
            {
                if (Physics.Raycast(yray, out info, 1.2f))
                {
                    use = true;
                }
            }*/
            transform.position = zray.GetPoint(-move_sp);
            /*if (shifting)
            {
                while (!Physics.Raycast(yray, 1.2f) && use)
                {
                    transform.position += info.normal.normalized / 10;
                }
            }*/
        }
        if(Input.GetKeyDown(KeyCode.M)){
            selectCursor();
        }else if(Input.GetKeyDown(KeyCode.Delete)){
            exit();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            spawn();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (creative) {
                transform.position = new Vector3(transform.position.x, transform.position.y + Jump_heiht, transform.position.z);
            }else if ((!isjump || !isTowjump))
            {
                now++;
                if(now >= jump_frame){
                    if (!isjump)
                    {
                        isjump = true;
                    }
                    else
                    {
                        isTowjump = true;
                    }
                    now = 0;
                }
                transform.position = new Vector3(transform.position.x, transform.position.y+Jump_heiht, transform.position.z);
            }
        }
        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            shifting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            shifting = false;
        }*/
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (creative) {
                transform.position = transform.position - new Vector3(0, Jump_heiht, 0);
            }
        }
       // if (Input.GetKeyUp(KeyCode.Space)) {
         //   Debug.Log("Up Space");
         //   crFrame++;
         //   if (crFrame >= 2) {
         //       crFrame = 0;
         //       creative = !creative;
         //       GetComponent<Rigidbody>().useGravity = !creative;
         //   }
       // }
        #endregion
        #region 检测RAY
        if (Physics.Raycast(yray,out info,1.2f)){
            if(info.transform.gameObject.tag.Equals("SPAWNPOINT")){
                spawnpoint = new Vector3(info.transform.gameObject.transform.position.x,
                    info.transform.gameObject.transform.position.y + 1,
                    info.transform.gameObject.transform.position.z);
            }
            else if (info.transform.gameObject.tag.Equals("DIE"))
            {
                spawn();
            }
            else if (info.transform.gameObject.tag.Equals("Finish") && !Map_Eiter.finished && isCreate)
            {
                Map_Eiter.finished = true;
                isCreate = false;
            }
            else if (info.transform.gameObject.tag.Equals("GAMER") && !isCreate)
            {
                Map_Eiter.init = true;
                isCreate = true;
            }
            else if (info.transform.gameObject.tag.Equals("ICE"))
            {
                W_move();
                ice_move = true;
            }
            else if (info.transform.gameObject.tag.Equals("Water"))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
            }
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            isjump = isTowjump = false;
        }
        #endregion
        if(getSpawn){
            getSpawn = false;
            spawn();
        }
        if (ice_move)
        {
            ice_move = false;
            transform.position = xray.GetPoint(1.4f);
        }
        pubTrans = transform;
	}

    void W_move() {
        /*bool use = false;
        if (shifting)
        {
            if (Physics.Raycast(yray, out info, 1.2f))
            {
                use = true;
            }
        }*/
        transform.position = xray.GetPoint(move_sp);
        /*if (shifting) {
            while (!Physics.Raycast(yray, 1.2f) && use) {
                transform.position += info.normal.normalized / 10;
            }
        }*/
    }

    void exit()
    {
        UnityEngine.Application.Quit();
    }

    void selectCursor() {
        if (!isHide)
        {
            UnityEngine.Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            isHide = true;
        }
        else
        {
            UnityEngine.Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isHide = false;
        }
    }

    void checkSpawn() { 
        if(transform.position.y < -200){
            spawn();
        }
    }

    public void spawn() {
        Rigidbody ri = GetComponent<Rigidbody>();
        ri.constraints = RigidbodyConstraints.FreezeAll;
        transform.position = spawnpoint;
        ri.constraints = RigidbodyConstraints.FreezeRotation;
    }

}

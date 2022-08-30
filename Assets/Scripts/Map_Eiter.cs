using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Map_Eiter : MonoBehaviour {

    public int Each_Map_Length = 20;

    public GameObject[] maps;

    public GameObject restPlace;

    public GameObject finish;

    System.Random rander = new System.Random();

    int now_l = 0;

    public static bool init = false;

	// Use this for initialization
	void Start () {
        st = transform.position;
	}

    Vector3 st;
    public static bool finished = false;
    public Vector3 startGameBtn;
	
	// Update is called once per frame
	void Update () {
		if(Map_Eiter.init){
            initMap(5);
            Map_Eiter.init = false;
        }
        if (finished)
        {
            try
            {
                for (int i = 0; i < realMap.Length; i++)
                    GameObject.Destroy(realMap[i]);
            }
            catch (Exception)
            {

            }
            finished = false;
            transform.position = st;
            PlayerMove.spawnpoint = st;
            start_pos = new Vector3(-33, 6, 41);
        }
	}

   Vector3 start_pos = new Vector3(-33, 6, 41);


    public GameObject[] realMap;

    void initMap(int How) {
        int maps_c = 0;
        realMap = new GameObject[How*2+1];
        for (; maps_c < How*2;maps_c+=2)
        {
            realMap[maps_c] = GameObject.Instantiate(randomMap(), start_pos, Quaternion.identity);
            now_l += Each_Map_Length;
            start_pos = new Vector3(start_pos.x-20, start_pos.y, start_pos.z+9);
            realMap[maps_c + 1] = GameObject.Instantiate(restPlace, start_pos, Quaternion.identity);
            start_pos = new Vector3(start_pos.x, start_pos.y, start_pos.z+2);
        }
        realMap[realMap.Length - 1] = GameObject.Instantiate(finish, start_pos, Quaternion.identity);
        
    }



    GameObject randomMap() { 
        return maps[rander.Next(maps.Length)];
    }
}

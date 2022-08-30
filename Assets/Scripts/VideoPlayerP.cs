using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;
using System;

public class VideoPlayerP: MonoBehaviour {

    public VideoClip[] videos;

    static int index = 0;

    static bool change = false;

    public static UnityEngine.Video.VideoPlayer player;

	// Use this for initialization
    void Start()
    {
        player = GetComponent<UnityEngine.Video.VideoPlayer>();
    }
	
	// Update is called once per frame
	void Update () {
        if(change){
            int l_index = index%videos.Length;
            change = false;
            player.clip = videos[l_index];
        }
	}

    public static void next_index() {
        change = true;
        index++;
        if(index > 32567){
            index = 0;
        }
    }

    public static void up_index() {
        change = true;
        index--;
        if (index < 0)
        {
            index = 32567;
        }
    }
}

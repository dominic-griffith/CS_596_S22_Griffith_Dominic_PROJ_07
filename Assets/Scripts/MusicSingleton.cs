using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingleton : MonoBehaviour
{

    public static MusicSingleton _instance;
    public AudioSource audio;
    private int songCount = 0;
    public AudioClip[] songs;



    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = songs[songCount];
        // audio2 = GetComponent<AudioSource>();
        // audio3 = GetComponent<AudioSource>();


       

        //singleton to ensure the song donest resart on certain scenes
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            _instance = this;
        }

    }
    public void changeVolume(Slider slider){
        audio.volume = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
         if (!audio.isPlaying )
        {
            audio.clip = songs[songCount];
            audio.Play();
            if(songCount == 2)
                songCount = 0;
            else 
                songCount++;
        }
        
            // if (songCount == 0 && !audio.isPlaying && !audio2.isPlaying && !audio3.isPlaying){
            //     audio.Play();
            //     songCount++;
            // }
            // if (songCount == 1 && !audio.isPlaying && !audio2.isPlaying && !audio3.isPlaying){
            //     audio2.Play();
            //     songCount++;
            // }
            // if (songCount == 2 && !audio.isPlaying && !audio2.isPlaying && !audio3.isPlaying){
            //     audio3.Play();
            //     songCount = 0;
            // }
        
    }
}

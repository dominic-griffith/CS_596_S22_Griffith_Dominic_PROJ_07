using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingleton : MonoBehaviour
{

    public static MusicSingleton _instance;
    public AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        if (!audio.isPlaying)
        {
            audio.Play();
        }

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

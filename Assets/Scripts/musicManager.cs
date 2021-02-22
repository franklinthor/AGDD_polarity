using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    public AudioSource bgSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (!bgSound.isPlaying)
        {
            bgSound.Play();
        }

    }
}

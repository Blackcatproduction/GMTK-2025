using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioClip[] songs;

    AudioSource audioSource;

    public static MusicController controller = null;

    private void Awake() {
        if (controller == null) {
            controller = this;

            audioSource = GetComponent<AudioSource>();

            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}

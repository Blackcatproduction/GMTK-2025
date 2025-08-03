using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public const int MENU_SONG = 0;
    public const int PAST_SONG = 1;
    public const int PRESENT_SONG = 2;
    public const int FUTURE_SONG = 3;

    [SerializeField]
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

    public void PlaySong(int songIndex) {
        AudioClip chosenSong = songs[songIndex];

        if (audioSource.clip != chosenSong) {
            audioSource.clip = chosenSong;
            audioSource.Play();
        }
    } 
}

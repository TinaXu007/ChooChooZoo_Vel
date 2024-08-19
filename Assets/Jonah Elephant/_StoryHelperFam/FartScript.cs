using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartScript : MonoBehaviour
{
    private AudioSource audioSource;
    public KeyCode secondaryButton = KeyCode.Space;
    public AudioClip sound;

    private float scale = 1;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Story Handeler").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(secondaryButton)) {
            scale = 1.25f;
            audioSource.PlayOneShot(sound, .1f);
        }

        if (scale > 1) {
            scale -= .05f;
        }

        this.transform.localScale = new Vector3(scale, scale, scale);

    }
}

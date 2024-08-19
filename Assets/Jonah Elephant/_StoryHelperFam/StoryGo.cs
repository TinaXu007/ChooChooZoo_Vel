using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System;

public class StoryGo : MonoBehaviour
{

    public AudioSource audioSource;
    public Camera mainCam;
    public GameObject back;
    public GameObject titleStuff;
    public GameObject Screenshot;
    public Canvas mainCanvas;

    private GameObject Player; //
    private Text TalkUi; //

    private bool canGo = true;

    public int textCounter = 0;
    private int storyAt = -1;

    public KeyCode mainButton;
    public Vector3[] cameraPosition;
    public Vector3[] cameraRotation;
    public GameObject[] storyPrefabs;
    public float[] TextSpeed;
    public string[] StoryText;
    public AudioClip[] audioClips;
    public AudioClip[] additionalClips;

    public Text speedText; //

    private Image spriteGo;

    private Sprite screenshot;


    public float extraDelay;
    public float percentSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        this.GetComponent<MeshRenderer>().enabled = false;
        TalkUi = GameObject.Find("Talk Ui").GetComponent<Text>();
        StartCoroutine(TakeScreenshot());
        audioSource.pitch = percentSpeed;
        mainCam.transform.position = new Vector3(-100,300,-100);
        mainCam.transform.eulerAngles = new Vector3(0,0,0);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha2) && canGo == true)
        {
            StartCoroutine(textSpeed(.1f));
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && canGo == true)
        {
            StartCoroutine(textSpeed(-.1f));
        }

        if (Input.GetKeyDown(mainButton) && canGo == true)
        {
            back.SetActive(true);
            titleStuff.SetActive(false);
            canGo = false;
            audioSource.PlayOneShot(audioClips[textCounter], 1f);
            StartCoroutine(talking());

            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }

            storyAdvance();
        }
    }

    public void storyAdvance()
    {
        storyAt++;
        mainCam.transform.position = cameraPosition[storyAt];
        mainCam.transform.eulerAngles = cameraRotation[storyAt];

        Instantiate(storyPrefabs[storyAt], this.transform);
        GameObject clone = Instantiate(Screenshot, mainCanvas.transform);
        clone.GetComponent<Image>().sprite = screenshot;
    }

    IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();
        screenshot = GetScreenShot();
        SaveScreenShot(screenshot, "transistionImage");
       
    }

    void SaveScreenShot(Sprite sprite, string outputfilename)
    {
        Texture2D itemBGTex = sprite.texture;
        byte[] itemBGBytes = itemBGTex.EncodeToPNG();
        File.WriteAllBytes($"{outputfilename}.png", itemBGBytes);
       
    }
    //https://www.grogansoft.com/2022/03/31/easy-screenshot-in-unity/

    Sprite GetScreenShot()
    {

        var image = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        image.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);

        image.Apply();
        Rect rec = new Rect(0, 0, image.width, image.height);

        var sprite = Sprite.Create(image, rec, new Vector2(0.5f, 0.5f), 100);
        return sprite;
    }

    IEnumerator textSpeed(float a) {
        percentSpeed += a;

        if (percentSpeed < .2f) {
            percentSpeed = .2f;
        }

        audioSource.pitch = percentSpeed;

        speedText.text = "Speed: " + Math.Round((decimal)(percentSpeed * 100), 2) + "%";

        yield return new WaitForSeconds(.1f);

        speedText.text = "";

    }

    IEnumerator talking()
    {
        TalkUi.text = "";

        foreach (char c in StoryText[textCounter])
        {

            if (c == '~')
            {
                TalkUi.text = TalkUi.text + '\n';
            }
            if (c == '&')
            {
                TalkUi.text = "";
            }
            else if (c != '@')
            {
                TalkUi.text = TalkUi.text + c;
            }
            else
            {
                audioSource.Stop();
                yield return new WaitForSeconds((3f + extraDelay));
                audioSource.PlayOneShot(additionalClips[textCounter], 1f);
            }

           
            yield return new WaitForSeconds(((((1f / TextSpeed[textCounter]) / 12f) / percentSpeed) * Time.deltaTime));
        }


        textCounter++;

        if (textCounter == StoryText.Length)
        {
            textCounter = 0;
            storyAt = -1;
        }

        StartCoroutine(TakeScreenshot());
       
        yield return new WaitForSeconds(1f + extraDelay);
        canGo = true;
    }
}

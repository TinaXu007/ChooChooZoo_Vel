using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Minigame_Manager : MonoBehaviour
{
    [System.Serializable]
    public class Minigame
    {
        public GameObject animal; 
        public int trigger_distance = 10;
        public int scene_number;
        public GameObject button_sound; //'paly with' + 'animal name' 
        public bool instruction_played = false; 
    }
    [SerializeField] private Minigame[] minigames;
    [SerializeField] private Button minigameButton; // Assign in inspector
    [SerializeField] private Button Instructor;
    [SerializeField] private GameObject train;

    //[SerializeField] private AudioSource Train_horn;
    [SerializeField] private GameObject train_SFX;
    [SerializeField] private GameObject Instruction_sound;

    private AudioSource train_audioSource;
    private AudioSource instruction_audioSource;
    private bool space_horn;

    // Start is called before the first frame update
    void Start()
    {
        //get audio clip
        //audioSource.clip = hornSound;
        train_audioSource = train_SFX.GetComponent<AudioSource>();
        instruction_audioSource = Instruction_sound.GetComponent<AudioSource>();

        Minigame closeMinigame = null;

        foreach (var minigame in minigames)
        {
            if (Vector3.Distance(train.transform.position, minigame.animal.transform.position) <= minigame.trigger_distance)
            {
                closeMinigame = minigame;
                break; // Found a close minigame, no need to check further
            }
            else
            {
                minigameButton.gameObject.SetActive(false);
                Instructor.gameObject.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Minigame closeMinigame = null;

        foreach (var minigame in minigames)
        {
            //Debug.Log(minigame.animal + " " + Vector3.Distance(train.transform.position, minigame.animal.transform.position));
            if (Vector3.Distance(train.transform.position, minigame.animal.transform.position) <= minigame.trigger_distance)
            {
                space_horn = false;
                //Debug.Log("close to " + minigame);
                closeMinigame = minigame;
                minigameButton.gameObject.SetActive(true);
                Instructor.gameObject.SetActive(true);
                if (minigame.instruction_played == false)
                {
                    Debug.Log("play instruction sound");
                    instruction_audioSource.Play();
                    minigame.instruction_played = true;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AudioSource minigame_sound = minigame.button_sound.GetComponent<AudioSource>(); ;
                    minigame_sound.Play();
                    float trainYaw = train.transform.rotation.eulerAngles.y;
                    float trainPitch = train.transform.rotation.eulerAngles.x;
                    float trainRoll = train.transform.rotation.eulerAngles.z;

                    // Create a new Quaternion based on the yaw, pitch, and roll
                    PlayerPrefs.SetString("EnterMiniGame", "true");
                    PlayerPrefs.SetFloat("train_x", train.transform.position.x);
                    PlayerPrefs.SetFloat("train_y", train.transform.position.y);
                    PlayerPrefs.SetFloat("train_z", train.transform.position.z);

                    PlayerPrefs.SetFloat("train_yaw", trainYaw);
                    PlayerPrefs.SetFloat("train_pitch", trainPitch);
                    PlayerPrefs.SetFloat("train_roll", trainRoll);

                    PlayerPrefs.Save();
                    Debug.Log("save train position");

                    StartCoroutine(LoadSceneAfterSound(minigame.scene_number, minigame_sound.clip.length));
                }
                break; // Found a close minigame, no need to check further
            }
            else
            {
                minigameButton.gameObject.SetActive(false);
                Instructor.gameObject.SetActive(false);
                space_horn = true;
            }
        }
        if (space_horn == true && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("play horn");
            train_audioSource.Play();
        }
    }
    private IEnumerator LoadSceneAfterSound(int sceneNumber, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the length of the sound clip
        SceneManager.LoadScene(sceneNumber); // Then load the scene
    }

}

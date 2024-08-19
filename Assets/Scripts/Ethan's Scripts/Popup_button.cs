using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Popup_button : MonoBehaviour
{
    public GameObject Button_Minigame;
    public GameObject Button_Instruction;
    public GameObject Animal;
    public GameObject Train;
    [SerializeField] private int Scene_number;
    private float Trainx, Trainy, Trainz;
    [SerializeField] int trigger_distance = 10;


    // Start is called before the first frame update
    void Start()
    {
        Button_Minigame.SetActive(false);
        Button_Instruction.SetActive(false);
        float distance = Vector3.Distance(Animal.transform.position, Train.transform.position);
        if (distance<= trigger_distance)
        {
            Button_Minigame.SetActive(true);
            Button_Instruction.SetActive(true);
            //Debug.Log("Button_Elephant set active");
        }
        else
        {
            Button_Minigame.SetActive(false);
            Button_Instruction.SetActive(false);
            //Debug.Log("Button_Elephant set false");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Animal.transform.position, Train.transform.position);
        //Debug.Log("Train animal" + Animal.name + " distance" + distance);
        if (distance <= trigger_distance)
        {
            //if (i <= 30)
            //{
            //    Train.transform.position = new Vector3(0, 20, float(-3.95));
            //}
            Button_Minigame.SetActive(true);
            Button_Instruction.SetActive(true);
            //Debug.Log("Button_Elephant set active");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Load minigame scene by its name
                float trainYaw = Train.transform.rotation.eulerAngles.y;
                float trainPitch = Train.transform.rotation.eulerAngles.x;
                float trainRoll = Train.transform.rotation.eulerAngles.z;

                // Create a new Quaternion based on the yaw, pitch, and roll
                PlayerPrefs.SetString("EnterMiniGame", "true");
                PlayerPrefs.SetFloat("train_x", Train.transform.position.x);
                PlayerPrefs.SetFloat("train_y", Train.transform.position.y);
                PlayerPrefs.SetFloat("train_z", Train.transform.position.z);

                PlayerPrefs.SetFloat("train_yaw", trainYaw);
                PlayerPrefs.SetFloat("train_pitch", trainPitch);
                PlayerPrefs.SetFloat("train_roll", trainRoll);

                PlayerPrefs.Save();
                Debug.Log("save train position");

                SceneManager.LoadScene(Scene_number);
                //Button_Minigame.GetComponent<Button>().onClick.Invoke();
            }
        }
        else
        {
            Button_Minigame.SetActive(false);
            Button_Instruction.SetActive(false);
            //Debug.Log("Button_Elephant set false");
        }


    }
    public void PlayScene(string sceneName)
    {
        //MenuController.Instance?.HideMenu();
        SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
        //PauseController.Instance.ResumeGame();
    }
    //public void start_elephant_minigame()
    //{
    //    PlayScene(Scene_number);
    //}
}

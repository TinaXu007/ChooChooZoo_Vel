using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Button : MonoBehaviour
{
    [SerializeField] private List<GameObject> Buttons;
    public GameObject Button_Loop1;
    public GameObject Button_Loop2;
    public GameObject Button_Loop3;

    [SerializeField] private List<GameObject> Detectors;
    public GameObject Detector_Loop1;
    public GameObject Detector_Loop2;
    public GameObject Detector_Loop3;

    public GameObject Button_left;
    public GameObject Button_right;
    public GameObject Button_straight;

    public GameObject Train;

    [SerializeField] int triggerdistance = 10;
    private float Trainx,Trainy,Trainz;
    // Start is called before the first frame update
    void Start()
    {
        float distance1 = Vector3.Distance(Detector_Loop1.transform.position, Train.transform.position);
        float distance2 = Vector3.Distance(Detector_Loop2.transform.position, Train.transform.position);
        float distance3 = Vector3.Distance(Detector_Loop3.transform.position, Train.transform.position);
        Button_Loop1.SetActive(false);
        Button_Loop2.SetActive(false);
        Button_Loop3.SetActive(false);

        if (distance1 <= triggerdistance)
        {
            Button_Loop1.SetActive(true);
            Button_Loop2.SetActive(false);
            Button_Loop3.SetActive(false);
            //Debug.Log("loop1 set active");
        }
        else
        {
            Button_Loop1.SetActive(false);
        }
        if (distance2 <= triggerdistance)
        {
            Button_Loop1.SetActive(false);
            Button_Loop2.SetActive(true);
            Button_Loop3.SetActive(false);
            //Debug.Log("loop2 set active");
        }
        else
        {
            Button_Loop2.SetActive(false);
        }
        if (distance3 <= triggerdistance)
        {
            Button_Loop1.SetActive(false);
            Button_Loop2.SetActive(false);
            Button_Loop3.SetActive(true);
            //Debug.Log("loop3 set active");
        }
        else
        {
            Button_Loop3.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance1 = Vector3.Distance(Detector_Loop1.transform.position, Train.transform.position);
        float distance2 = Vector3.Distance(Detector_Loop2.transform.position, Train.transform.position);
        float distance3 = Vector3.Distance(Detector_Loop3.transform.position, Train.transform.position);
        //Debug.Log("Train animal" + Detector_Loop1.name + " distance" + distance1);
        //Debug.Log("Train animal" + Detector_Loop2.name + " distance" + distance2);
        //Debug.Log("Train animal" + Detector_Loop3.name + " distance" + distance3);

        Trainx = Train.transform.position.x;
        Trainy = Train.transform.position.y;
        Trainz = Train.transform.position.z;
        if (distance1 <= triggerdistance)
        {
            Button_Loop1.SetActive(true);
            Button_Loop2.SetActive(false);
            Button_Loop3.SetActive(false);
            //Debug.Log("loop1 set active");
        }
        else
        {
            Button_Loop1.SetActive(false);
        }
        if (distance2 <= triggerdistance)
        {
            Button_Loop1.SetActive(false);
            Button_Loop2.SetActive(true);
            Button_Loop3.SetActive(false);
            //Debug.Log("loop2 set active");
        }
        else
        {
            Button_Loop2.SetActive(false);
        }
        if (distance3 <= triggerdistance)
        {
            Button_Loop1.SetActive(false);
            Button_Loop2.SetActive(false);
            Button_Loop3.SetActive(true);
            //Debug.Log("loop3 set active");
        }
        else
        {
            Button_Loop3.SetActive(false);
        }

        if (distance1 > triggerdistance & distance2 > triggerdistance & distance3> triggerdistance)
        {
            Button_left.SetActive(false);
            Button_right.SetActive(false);
            Button_straight.SetActive(false);
        }
    }
}

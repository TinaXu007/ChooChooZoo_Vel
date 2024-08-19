using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RailController : MonoBehaviour
{
    [System.Serializable]
    public class Intersection
    {
        public Collider[] directionColliders; // Colliders for each direction
        public GameObject[] directionButtons; // Buttons for each direction
        public GameObject[] directionSounds; // Sounds for each direction
        public int selectedDirection = 0;     // Currently selected direction
    }

    public Intersection[] intersections; // Array of intersections
    private int currentIntersection = -1; // Index of the current intersection

    [SerializeField] private GameObject train;
    private Vector3 resetPosition = new Vector3(0, 20, 15);
    private Quaternion resetRotation = Quaternion.Euler(0, 0, 0);

    public float movementThreshold = 1f;
    public float hideButtonDelay = 10.0f;

    private bool isTrainMoving = false;

    // Audio parts
    [SerializeField] private GameObject instructionsound;
    private AudioSource left_s;
    private AudioSource right_s;
    private AudioSource straight_s;
    private AudioSource instruction_s;

    [SerializeField] private GameObject wheelsfront;
    [SerializeField] private GameObject wheelsrear;

    void Start()
    {
        HideDirectionButtons(); // Initially hide buttons
    }

    void Update()
    {
        if (currentIntersection >= 0 && !isTrainMoving && Input.GetKeyDown(KeyCode.DownArrow))
        {
            CycleDirection();
        }
    }

    void CycleDirection()
    {
        Intersection intersection = intersections[currentIntersection];

        intersection.directionColliders[intersection.selectedDirection].enabled = false;

        intersection.selectedDirection = (intersection.selectedDirection + 1) % intersection.directionColliders.Length;

        intersection.directionColliders[intersection.selectedDirection].enabled = true;

        UpdateButtonVisibilityAndPlaySound(intersection);

        StartCoroutine(HideDirectionButtonsAfterDelay(hideButtonDelay));
    }

    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < intersections.Length; i++)
        {
            if (other.gameObject.name == "IntersectionStop" + (i + 1))
            {
                isTrainMoving = false;
                SetCurrentIntersection(i);
                break;
            }
        }
    }

    void SetCurrentIntersection(int intersectionIndex)
    {
        currentIntersection = intersectionIndex;

        for (int i = 0; i < intersections[intersectionIndex].directionColliders.Length; i++)
        {
            intersections[intersectionIndex].directionColliders[i].enabled = (i == 0);
        }

        ShowDirectionButtons(currentIntersection);
    }

    IEnumerator HideDirectionButtonsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isTrainMoving = true;
        HideDirectionButtons();
    }

    void ShowDirectionButtons(int intersectionIndex)
    {
        Intersection intersection = intersections[intersectionIndex];
        foreach (var button in intersection.directionButtons)
        {
            if (button != null) button.gameObject.SetActive(true);
        }
    }

    void HideDirectionButtons()
    {
        foreach (var intersection in intersections)
        {
            foreach (var button in intersection.directionButtons)
            {
                if (button != null) button.gameObject.SetActive(false);
            }
        }
    }

    void UpdateButtonVisibilityAndPlaySound(Intersection intersection)
    {
        int selectedDirection = intersection.selectedDirection;

        // Hide all buttons initially and then show the selected one
        for (int i = 0; i < intersection.directionButtons.Length; i++)
        {
            if (intersection.directionButtons[i] != null)
                intersection.directionButtons[i].gameObject.SetActive(i == selectedDirection);

            if (i == selectedDirection && intersection.directionSounds[i] != null)
            {
                AudioSource sound =  intersection.directionSounds[i].GetComponent<AudioSource>();
                sound.Play();
            }
        }
    }

    public void ResetTrainPosition()
    {
        train.transform.position = resetPosition;
        train.transform.rotation = resetRotation;

        if (wheelsfront != null) wheelsfront.transform.rotation = resetRotation;
        if (wheelsrear != null) wheelsrear.transform.rotation = resetRotation;
    }
}

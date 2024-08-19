using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RailController2 : MonoBehaviour
{
    [System.Serializable]
    public class Intersection
    {
        public Collider[] directionColliders; // Colliders for each direction
        public GameObject leftButton; // Can be null if no left direction
        public GameObject rightButton; // Can be null if no right direction
        public GameObject straightButton; // Can be null if no straight direction
        public int selectedDirection = 0; // Currently selected direction
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
    [SerializeField] private GameObject leftsound;
    [SerializeField] private GameObject rightsound;
    [SerializeField] private GameObject straightsound;
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
        left_s = leftsound.GetComponent<AudioSource>();
        right_s = rightsound.GetComponent<AudioSource>();
        straight_s = straightsound.GetComponent<AudioSource>();
        instruction_s = instructionsound.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (currentIntersection >= 0)
        {
            if (!isTrainMoving && Input.GetKeyDown(KeyCode.DownArrow))
            {
                CycleDirection();
            }
        }
    }

    void CycleDirection()
    {
        Intersection intersection = intersections[currentIntersection];

        intersection.directionColliders[intersection.selectedDirection].enabled = false;

        intersection.selectedDirection = (intersection.selectedDirection + 1) % intersection.directionColliders.Length;

        intersection.directionColliders[intersection.selectedDirection].enabled = true;

        // Update button visibility based on the selected direction
        UpdateButtonVisibility(intersection.selectedDirection);

        // Start the coroutine to hide the selected button after the specified delay
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
                instruction_s.Play();
                break;
            }
        }
    }

    void SetCurrentIntersection(int intersectionIndex)
    {
        currentIntersection = intersectionIndex;

        // Initialize the first direction's collider
        for (int i = 0; i < intersections[intersectionIndex].directionColliders.Length; i++)
        {
            intersections[intersectionIndex].directionColliders[i].enabled = (i == 0);
        }

        ShowDirectionButtons();
    }

    IEnumerator HideDirectionButtonsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isTrainMoving = true;
        HideDirectionButtons();
    }

    void ShowDirectionButtons()
    {
        Intersection intersection = intersections[currentIntersection];
        if (intersection.leftButton) intersection.leftButton.gameObject.SetActive(true);
        if (intersection.rightButton) intersection.rightButton.gameObject.SetActive(true);
        if (intersection.straightButton) intersection.straightButton.gameObject.SetActive(true);
    }

    void HideDirectionButtons()
    {
        foreach (var intersection in intersections)
        {
            if (intersection.leftButton) intersection.leftButton.gameObject.SetActive(false);
            if (intersection.rightButton) intersection.rightButton.gameObject.SetActive(false);
            if (intersection.straightButton) intersection.straightButton.gameObject.SetActive(false);
        }
    }

    void UpdateButtonVisibility(int selectedDirection)
    {
        Intersection intersection = intersections[currentIntersection];

        // Hide all buttons initially
        if (intersection.leftButton) intersection.leftButton.gameObject.SetActive(false);
        if (intersection.rightButton) intersection.rightButton.gameObject.SetActive(false);
        if (intersection.straightButton) intersection.straightButton.gameObject.SetActive(false);

        // Activate the button based on the selected direction
        if (intersection.directionColliders.Length > selectedDirection)
        {
            if (selectedDirection == 0)
            {
                intersection.leftButton.gameObject.SetActive(true);
                left_s.Play();
            }
            else if (selectedDirection == 1)
            {
                intersection.rightButton.gameObject.SetActive(true);
                right_s.Play();
            }
            else if (selectedDirection == 2)
            {
                intersection.straightButton.gameObject.SetActive(true);
                straight_s.Play();
            }
        }
    }

    // Reset the train position when the reset button is pressed
    public void ResetTrainPosition()
    {
        train.transform.position = resetPosition;
        train.transform.rotation = resetRotation;

        if (wheelsfront != null)
        {
            wheelsfront.transform.rotation = resetRotation;
        }
        if (wheelsrear != null)
        {
            wheelsrear.transform.rotation = resetRotation;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidInstructions : MonoBehaviour
{
    public KeyCode leftKey = KeyCode.Space;
    public KeyCode rightKey = KeyCode.DownArrow;

    private float timer = 30f;

    private bool listenForLeft = false;
    private bool listenForRight = false;

    private bool leftPressed = false;
    private bool rightPressed = false;

    private bool runInstructions = true;

    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    private Animator leftAnimator;
    private Animator rightAnimator;

    // Start is called before the first frame update
    void Start()
    {
        leftAnimator = leftButton.GetComponent<Animator>();
        rightAnimator = rightButton.GetComponent<Animator>();

        StartCoroutine("startInstructions");
    }

    IEnumerator WaitForSecondsUnlessCondition(float seconds) 
    {
        for (int i = 0; i < (int) seconds; i++) {
            if (!runInstructions) 
            {
                break;
            }

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    IEnumerator startInstructions()
    {
        yield return new WaitForSecondsRealtime(3f);
        
        leftAnimator.enabled = true;

        while(runInstructions)
        {
            Debug.Log("its happenin");
            AudioController.Instance.PlayPressLeft();
            yield return new WaitForSecondsRealtime(1f);
            listenForLeft = true;
            yield return WaitForSecondsUnlessCondition(8f);
        }
        leftAnimator.enabled = false;


        yield return new WaitForSecondsRealtime(1.5f);
        rightAnimator.enabled = true;
        runInstructions = true;

        while (runInstructions)
        {
            AudioController.Instance.PlayPressRight();
            yield return new WaitForSecondsRealtime(1f);
            listenForRight = true;
            yield return WaitForSecondsUnlessCondition(8f);
        }
        
        if(leftPressed && rightPressed)
        {
            yield return new WaitForSecondsRealtime(1f);
            AudioController.Instance.PlayCheering();
        }

        yield return new WaitForSecondsRealtime(3f);

        rightAnimator.enabled = false;

        Debug.Log("we are done");

        LoadingScene.Instance.LoadScene("ScreenTestQuestions");
        
        
    }

   
    void Update()
    {
        if (listenForLeft)
        {
            if (Input.GetKeyDown(leftKey) && timer > 0)
            {
                leftPressed = true;
                runInstructions = false;
                listenForLeft = false;
                AudioController.Instance.PlayGetStar();
            }

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                listenForLeft = false;
                runInstructions = false;
                timer = 30f;
            }
            
        }

        if (listenForRight)
        {
            if (Input.GetKeyDown(rightKey) && timer > 0)
            {
                rightPressed = true;
                runInstructions = false;
                listenForRight = false;
                AudioController.Instance.PlayGetStar();
            }

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                listenForRight = false;
                runInstructions = false;
                timer = 30f;
            }
        }
    }
}

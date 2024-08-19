using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableStar : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 50;
    [SerializeField] private Vector3 rotAxie = new Vector3(1, 0, 0);
    [SerializeField] private GameObject mesh;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateSelf();
    }

    void RotateSelf()
    {
        transform.Rotate(rotAxie * rotSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Train")
        {
            RewardSystem.Instance.rewards += 1;
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        mesh.SetActive(false);
        this.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(RewardSystem.Instance.fadeTime);

        mesh.SetActive(true);
        this.GetComponent<Collider>().enabled = true;
    }
}

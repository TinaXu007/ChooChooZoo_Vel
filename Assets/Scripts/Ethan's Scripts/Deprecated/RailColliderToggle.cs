using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class RailColliderToggle : MonoBehaviour
{
    //[SerializeField] public GameObject railloop;
    //public railclass railloop1;
    public GameObject left_railPrefab;
    public GameObject right_railPrefab;
    public GameObject straight_railPrefab;

    public GameObject conflict_rail1_left;
    public GameObject conflict_rail_right;
    public GameObject conflict_rail_straight;
    //public GameObject straight_railPrefab_2;

    public GameObject Button_left;
    public GameObject Button_right;
    public GameObject Button_straight;

    private GameObject activeRail;

    public Button toggleButton1;

    private bool railActive_L = false;
    private bool railActive_R = false;
    private bool railActive_S = false;

    private bool switched = false;

    //switch by down arrow key
    private GameObject[] directionButtons;
    private GameObject[] rails;
    private GameObject[] conflict_rails;
    private int activeButtonIndex = 0;
    private int activeRailIndex = 0;
    private int activeRailIndex_conlict = 0;
    public KeyCode switchKey = KeyCode.DownArrow;

    private bool automode = false;

    private void Start()
    {

        // Attach a listener to the button's onClick event
        //toggleButton1.onClick.AddListener(ToggleRailColliders);

        // Initially activate the first rail and deactivate the third rail
        left_railPrefab.SetActive(railActive_L);
        right_railPrefab.SetActive(railActive_R);
        straight_railPrefab.SetActive(railActive_S);
        //straight_railPrefab_2.SetActive(railActive_S);
        //Button_left.interactable = true;
        //Button_right.interactable = false;
        //Button_straight.interactable = false;
        Button_left.SetActive(false);
        Button_right.SetActive(false);
        Button_straight.SetActive(false);

        directionButtons = new GameObject[] { Button_left, Button_right, Button_straight };
        rails = new GameObject[] { left_railPrefab, right_railPrefab, straight_railPrefab };
        conflict_rails = new GameObject[] { conflict_rail1_left, conflict_rail_right, conflict_rail_straight };
        // Initialize the active button
        switch_by_one_key(activeButtonIndex);
        switch_by_one_key(activeRailIndex);

    }

    private void ToggleRailColliders()
    {
        // Toggle the colliders and active state
        //rail1Active = !rail1Active;
        //left_railPrefab_1.SetActive(rail1Active);
        //left_railPrefab_2.SetActive(rail1Active);
        //right_railPrefab_1.SetActive(!rail1Active);
        //right_railPrefab_2.SetActive(!rail1Active);
        if(switched == false && railActive_L == true && railActive_R == false && railActive_S == false)
        {
            railActive_L = false;
            railActive_R = true;
            railActive_S = false;
            Button_left.SetActive(false);
            Button_right.SetActive(true);
            Button_straight.SetActive(false);
            Debug.Log("railActive_R = true");
            switched = true;
        }
        else if (switched == false && railActive_L == false && railActive_R == true && railActive_S == false)
        {
            railActive_L = false;
            railActive_R = false;
            railActive_S = true;
            Button_left.SetActive(false);
            Button_right.SetActive(false);
            Button_straight.SetActive(true);
            Debug.Log("railActive_S = true");
            switched = true;
        }
        else if (switched == false && railActive_L == false && railActive_R == false && railActive_S == true)
        {
            railActive_L = true;
            railActive_R = false;
            railActive_S = false;
            Button_left.SetActive(true);
            Button_right.SetActive(false);
            Button_straight.SetActive(false);
            Debug.Log("railActive_L = true");
            switched = true;
        }
        left_railPrefab.SetActive(railActive_L);
        right_railPrefab.SetActive(railActive_R);
        straight_railPrefab.SetActive(railActive_S);
    }

    private void switch_by_three_key()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (straight_railPrefab.name != "Collider_1_fill_null")
            {
                railActive_L = false;
                railActive_R = false;
                railActive_S = true;
                Button_left.SetActive(false);
                Button_right.SetActive(false);
                Button_straight.SetActive(true);
            }

            //Debug.Log("railActive_S = true");
        }
        //else
        //{
        //    Button_straight.SetActive(false);
        //}
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (left_railPrefab.name != "Collider_1_fill_null")
            {
                railActive_L = true;
                railActive_R = false;
                railActive_S = false;
                Button_left.SetActive(true);
                Button_right.SetActive(false);
                Button_straight.SetActive(false);
            }

            //Debug.Log("railActive_L = true");
        }
        //else
        //{
        //    Button_left.SetActive(false);
        //}
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Button_right.name != "Collider_1_fill_null")
            {
                railActive_L = false;
                railActive_R = true;
                railActive_S = false;
                Button_left.SetActive(false);
                Button_right.SetActive(true);
                Button_straight.SetActive(false);
            }

            //Debug.Log("railActive_R = true");
        }
        else
        {
            Button_right.SetActive(false);
        }
        left_railPrefab.SetActive(railActive_L);
        right_railPrefab.SetActive(railActive_R);
        straight_railPrefab.SetActive(railActive_S);
    }

    private void switch_by_one_key(int index, bool isActive = true)
    {
        // Ensure the index is within bounds
        if (index >= 0 && index < directionButtons.Length)
        {
            directionButtons[index].SetActive(isActive);
            rails[index].SetActive(isActive);

        }
    }

    private void switch_by_one_key_conflict(int index, bool isActive = true)
    {
        if (index >= 0 && index < directionButtons.Length)
        {
            conflict_rails[index].SetActive(isActive);
            //Debug.Log("conflict rails");
        }
    }

    void Update()
    {
        //Debug.Log(left_railPrefab.name);
        //Debug.Log(right_railPrefab.name);
        //Debug.Log(straight_railPrefab.name);
        //switch_by_three_key();
        if (Input.GetKeyDown(switchKey))
        {
            // Deactivate the currently active button
            switch_by_one_key(activeButtonIndex, false);

            // Increment the active button index and wrap around if necessary
            activeButtonIndex = (activeButtonIndex + 1) % directionButtons.Length;
            // Activate the new active button
            switch_by_one_key(activeButtonIndex, true);

            
            switch_by_one_key_conflict(activeRailIndex_conlict, false);
            activeRailIndex_conlict = (activeRailIndex_conlict + 1) % directionButtons.Length;

            switch_by_one_key_conflict(activeRailIndex_conlict, true);
        }
    }
}

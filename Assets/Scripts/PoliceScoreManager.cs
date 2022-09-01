using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliceScoreManager : MonoBehaviour
{
    public static PoliceScoreManager instance;
    public Text checkedText;

    int passedPoints = 0;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        checkedText.text = "Locations checked: " + passedPoints.ToString();
    }

    // Update is called once per frame
    public void AddPassed()
    {
        passedPoints++;
        checkedText.text = "Locations checked: " + passedPoints.ToString();
    }
}

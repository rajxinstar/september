using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LetterGrade();
        LetterGrade(-2);

    }
    void LetterGrade()
    {
        Debug.Log("testing");
    }

    string LetterGrade(double grade)
    {
        if (grade >= 0 && grade < 0.6)
        {
            Debug.Log("F");
            return "F";
        }
        else if (grade >= 0.6 && grade < 0.7)
        {
            Debug.Log("D");
            return "D";
        }
        else if (grade >= 0.7 && grade < 0.8)
        {
            Debug.Log("C");
            return "C";
        }
        else if (grade >= 0.8 && grade < 0.9)
        {
            Debug.Log("B");
            return "B";
        }
        else if (grade >= 0.9 && grade <= 1)
        {
            Debug.Log("A");
            return "A";
        }
        else
        {
            Debug.Log("out of bounds");
            return "out of bounds";
        }

    }



    // Update is called once per frame
    void Update()
    {

    }
}

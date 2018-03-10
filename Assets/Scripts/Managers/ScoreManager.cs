using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    public SteamVR_TrackedObject head;

    TextMesh text;


    void Awake ()
    {
        text = GetComponent <TextMesh> ();
        score = 0;
    }


    void Update()
    {
        string[] lines = text.text.Split('\n');

        text.text = "Score: " + score + "\n" + lines[lines.Length - 1];

        Vector3 euler = head.transform.rotation.eulerAngles;
        //euler.y = head.transform.rotation.eulerAngles.y;


       // Debug.Log(head.transform.rotation);
        //text.transform.rotation.SetEulerRotation(euler);
        
    }
}

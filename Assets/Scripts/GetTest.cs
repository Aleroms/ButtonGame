using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetTest : MonoBehaviour
{
    //temp variable
    public Text debug;

    // Start is called before the first frame update
    void Start()
    {

        //test to see if connected to localhost database
        StartCoroutine(Test());
    }

    //Backend Test
    IEnumerator Test()
    {
        UnityWebRequest req = UnityWebRequest.Get("http://localhost/Unity/getTest.php");
        yield return req.SendWebRequest();
        debug.text = req.downloadHandler.text;
    }
}

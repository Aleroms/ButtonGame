using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

//this script registers and logs-in the user
//to the mySQL  database.
public class Login : MonoBehaviour
{
   
    /* Login Variables */
    [Header("Login Variables")]
    [SerializeField] private TextMeshProUGUI login_uname;
    [SerializeField] private TextMeshProUGUI login_passwrd;

    /* Register Variables */
    [Header("Register Variables")]
    [SerializeField] private TextMeshProUGUI reg_uname;
    [SerializeField] private TextMeshProUGUI reg_email;
    [SerializeField] private TextMeshProUGUI reg_passwrd;
    [SerializeField] private TextMeshProUGUI feedback;



    private void Start()
    {
        //clear the feedback @ start
        feedback.text = "";
        


    }
    private IEnumerator RegisterPost()
    {
        //function should post username,email,and password to database as a form
        WWWForm form = new WWWForm();
        
        form.AddField("username", reg_uname.text);
        form.AddField("email", reg_email.text);
        form.AddField("password", reg_passwrd.text);
        form.AddField("unity-api-key", "SantiagoGames");

        UnityWebRequest post = UnityWebRequest.Post("http://localhost/Unity/login.php",form);
        yield return post.SendWebRequest();
    }
    
    public void UserRegister()
    {
        Debug.Log("registering...");
        Debug.Log(reg_email.text);
        Debug.Log(reg_passwrd.text);
        Debug.Log(reg_uname.text);

        StartCoroutine(RegisterPost());
        

    }
    public void UserLogin()
    {
        Debug.Log("entered values for login:");
        Debug.Log(login_uname.text); 
        Debug.Log(login_passwrd.text);

       // StartCoroutine(LoginPost());
    }
    
}

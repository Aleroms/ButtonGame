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
   
    [Header("Login Variables")]
    [SerializeField] private TextMeshProUGUI login_uname;
    [SerializeField] private TextMeshProUGUI login_passwrd;


    [Header("Register Variables")]
    [SerializeField] private TextMeshProUGUI reg_uname;
    [SerializeField] private TextMeshProUGUI reg_email;
    [SerializeField] private TextMeshProUGUI reg_passwrd;
    [SerializeField] private TextMeshProUGUI feedback;

    private Color feedback_color;
    private Button register_button;
    private void Start()
    {
        register_button = GameObject.FindGameObjectWithTag("register-button").GetComponent<Button>();
        
        //clear the feedback @ start
        feedback.text = "";
        feedback_color = feedback.color;

    }

    private bool Validate(int marker, string id)
    {
        // code
        /* 1 username
         * 2 email
         * 3 password
         */

        switch (marker)
        {
            case 1:
                return id.Length > 5 ? true : false;
            case 2:
                return id.Length > 5 ? true : false;
            case 3:
                return id.Length > 8 ? true : false;
            default:
                Debug.LogWarning("incorrect validation code sent.");
                return false;
        }

    }
    private void ErrorMessage(string msg)
    {
        feedback.text = msg;
        feedback.color = Color.red;
    }
    private void FeedbackMessage(string msg)
    {
        feedback.color = feedback_color;
        feedback.text = msg;
    }
    private IEnumerator RegisterPost()
    {
        //function should post username,email,and password to database as a form
        WWWForm form = new WWWForm();

        form.AddField("username", reg_uname.text);
        form.AddField("email", reg_email.text);
        form.AddField("password", reg_passwrd.text);
        form.AddField("unity-api-key", "SantiagoGames");

        UnityWebRequest post = UnityWebRequest.Post("http://localhost/Unity/login.php", form);
        yield return post.SendWebRequest();
    }
    public void UserRegister()
    {

        //Debug.Log("registering...");
        //Debug.Log(reg_email.text);
        //Debug.Log(reg_passwrd.text);
        //Debug.Log(reg_uname.text);
        if (!Validate(1, reg_uname.text))
            ErrorMessage("username is too short.");
        else if (!Validate(2, reg_email.text))
            ErrorMessage("email is too short.");
        else if (!Validate(3, reg_passwrd.text))
            ErrorMessage("password is too short.");
        else
        {
            register_button.enabled = false;

            //StartCoroutine(RegisterPost());
            FeedbackMessage("processing request");
        }
        

    }
    public void UserLogin()
    {
        Debug.Log("entered values for login:");
        Debug.Log(login_uname.text); 
        Debug.Log(login_passwrd.text);

       // StartCoroutine(LoginPost());
    }
    
}

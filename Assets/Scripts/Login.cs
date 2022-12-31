using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;

//this script registers and logs-in the user
//to the mySQL  database.
public class Login : MonoBehaviour
{
   
    [Header("Login Variables")]
    [SerializeField] private TextMeshProUGUI login_uname;
    [SerializeField] private TextMeshProUGUI login_passwrd;
    [SerializeField] private TextMeshProUGUI login_feedback;


    [Header("Register Variables")]
    [SerializeField] private TMP_InputField reg_uname;
    [SerializeField] private TMP_InputField reg_email;
    [SerializeField] private TMP_InputField reg_passwrd;
    [SerializeField] private TextMeshProUGUI reg_feedback;

    private Color feedback_color;
    private Color success_green;
    private Button register_button;
    private void Start()
    {
        register_button = GameObject.FindGameObjectWithTag("register-button").GetComponent<Button>();

        //clear the feedback @ start
        login_feedback.text = "";
        reg_feedback.text = "";
        
        //both login and reg share same color; save it
        feedback_color = reg_feedback.color;
        success_green = new Color(25f, 135f, 84f);
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
                if (id.Length < 6) ErrorMessage("username is too short");
                //else if (Regex.IsMatch(reg_uname2.text, "^[a-zA-Z0-9_]$")) ErrorMessage("username is invalid");
                else if (Regex.IsMatch(reg_uname.text, "^[a-zA-Z0-9_]$")) ErrorMessage("username is invalid");
                else if (id.Length > 20) ErrorMessage("username is too long");
                else
                    return true;
                break;
            case 2:
                if (id.Length < 6) ErrorMessage("email is too short");
                else if (!reg_email.text.Contains('@')
                      || !reg_email.text.Contains('.')
                      || reg_email.text.IndexOf('@') < 1 
                      ) ErrorMessage("email is invalid");
                else if (id.Length > 30) ErrorMessage("email is too long");
                else
                    return true;
                break;
            case 3:
                if (id.Length < 8) ErrorMessage("password minimum must be 8 characters");
                else return true;
                break;
            default:
                Debug.LogError("incorrect validation code sent.");
                return false;
        }
        return false;

    }
    private void ErrorMessage(string msg)
    {
        reg_feedback.text = msg;
        reg_feedback.color = Color.red;
        login_feedback.text = msg;
        login_feedback.color = Color.red;
    }
    private void FeedbackMessage(string msg, Color c)
    {
        reg_feedback.color = c;
        reg_feedback.text = msg;
        login_feedback.color = c;
        login_feedback.text = msg;
    }
    private void DatabaseMessage(string downloadHandler)
    {
        //Error Codes
        // 1 - database connection error
        // 2 - usernamecheck query error
        // 3 - user already exists
        // 4 - emailcheck query error
        // 5 - email already exists
        // 6 - insert query error

        switch (downloadHandler)
        {
            case "0":
                //No Errors
                FeedbackMessage("Success!", Color.green);
                Debug.Log("no errors from database.");
                break;
            case "3":
                ErrorMessage("user already exists.");
                break;
            case "5":
                ErrorMessage("email already exists.");
                break;
            default:
                ErrorMessage("server-side error. Please try again.");
                break;
        }
    }
    private IEnumerator RegisterPost()
    {
        //function should post username,email,and password to database as a form
        WWWForm form = new WWWForm();

        form.AddField("username", reg_uname.text);
        form.AddField("email", reg_email.text);
        form.AddField("password", reg_passwrd.text);
        form.AddField("unity-api-key", "SantiagoGames");

        UnityWebRequest post = UnityWebRequest.Post("http://localhost/Unity/register.php", form);
        yield return post.SendWebRequest();

        //if no errors
        if(post.downloadHandler.error == "")
        {
            DatabaseMessage(post.downloadHandler.text);
            Debug.Log(post.downloadHandler.text);
        }
        else
        {
            FeedbackMessage("Error occured", Color.red);
            Debug.LogError(post.downloadHandler.error);
        }

        //release locked register button so player can resubmit
        register_button.enabled = true;
    }
    private IEnumerator LoginPost()
    {
        WWWForm form = new WWWForm();

        form.AddField("username", reg_uname.text);
        form.AddField("password", reg_passwrd.text);
        form.AddField("unity-api-key", "SantiagoGames");

        UnityWebRequest post = UnityWebRequest.Post("http://localhost/Udemy/login.php", form);
        yield return post.SendWebRequest();
    }
    /*====================== Public Methods Below ===========================*/
    public void UserRegister()
    {
        ResetFeedback();

        if (!Validate(1, reg_uname.text))
            Debug.Log("uname failed");
        else if (!Validate(2, reg_email.text))
            Debug.Log("email failed");
        else if (!Validate(3, reg_passwrd.text))
            Debug.Log("password failed");
        else
        {
            register_button.enabled = false;

            StartCoroutine(RegisterPost());
            FeedbackMessage("processing request", feedback_color);
        }
        

    }
    public void UserLogin()
    {

        if (!Validate(1, reg_uname.text))
            Debug.Log("uname failed");
        else if (!Validate(3, reg_passwrd.text))
            Debug.Log("password failed");
        else
        {
            StartCoroutine(LoginPost());
            FeedbackMessage("logging in...", feedback_color);
        }
    }

    //======Reset Below============
    public void ResetFeedback()
    {
        reg_feedback.text = "";
        login_feedback.text = "";
    }
    public void ResetForm()
    {
        Debug.Log("form reset...");
        reg_email.text = "";
        reg_passwrd.text = "";
        reg_uname.text = "";

        login_passwrd.text = "";
        login_uname.text = "";

    }
    
}

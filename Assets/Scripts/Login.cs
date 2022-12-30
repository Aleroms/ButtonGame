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


    [Header("Register Variables")]
    [SerializeField] private TMP_InputField reg_uname;
    [SerializeField] private TMP_InputField reg_email;
    [SerializeField] private TMP_InputField reg_passwrd;
    [SerializeField] private TextMeshProUGUI feedback;

    private Color feedback_color;
    private Color success_green;
    private Button register_button;
    private void Start()
    {
        register_button = GameObject.FindGameObjectWithTag("register-button").GetComponent<Button>();
        
        //clear the feedback @ start
        feedback.text = "";
        feedback_color = feedback.color;
        success_green = new Color(25f, 135f, 84f);
    }

    private bool Validate(int marker, string id)
    {
        // code
        /* 1 username
         * 2 email
         * 3 password
         */
        Debug.Log(Regex.IsMatch(reg_uname.text, "^[a-zA-Z0-9_]$"));
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
        feedback.text = msg;
        feedback.color = Color.red;
    }
    private void FeedbackMessage(string msg, Color c)
    {
        feedback.color = c;
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

        UnityWebRequest post = UnityWebRequest.Post("http://localhost/Unity/register.php", form);
        yield return post.SendWebRequest();

        //if no errors
        if(post.downloadHandler.error == null)
        {
            
            FeedbackMessage("Success!", Color.green);
            Debug.Log("no errors from database");
            Debug.Log(post.downloadHandler.text);
        }
        else
        {
            FeedbackMessage("Error occured", Color.red);
            Debug.LogError(post.downloadHandler.error);
        }
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
            //StartCoroutine(LoginPost());
            FeedbackMessage("logging in...", feedback_color);
        }
    }

    //======Reset Below============
    public void ResetFeedback()
    {
        feedback.text = "";
    }
    public void ResetForm()
    {
        Debug.Log("form reset...");
        reg_email.text = "";
        reg_passwrd.text = "";
        reg_uname.text = "";
        
    }
    
}

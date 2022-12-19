using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//this script registers and logs-in the user
//to the mySQL  database.
public class Login : MonoBehaviour
{
    /* Login Variables */
    [Header("Login Variables")]
    [SerializeField] private TextMeshProUGUI login_uname;
    [SerializeField] private TextMeshProUGUI login_email;
    [SerializeField] private TextMeshProUGUI login_passwrd;

    /* Register Variables */
    [Header("Register Variables")]
    [SerializeField] private TextMeshProUGUI reg_uname;
    [SerializeField] private TextMeshProUGUI reg_email;
    [SerializeField] private TextMeshProUGUI reg_passwrd;



    public void UserRegister()
    {
        Debug.Log("entered values for register:");
        Debug.Log(reg_uname.text);
        Debug.Log(reg_email.text);
        Debug.Log(reg_passwrd.text);
    }
    public void UserLogin()
    {
        Debug.Log("entered values for login:");
        Debug.Log(login_uname.text);
        Debug.Log(login_email.text);
        Debug.Log(login_passwrd.text);
    }
}

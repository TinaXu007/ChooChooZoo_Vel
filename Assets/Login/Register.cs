using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Register : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_Username;
    [SerializeField] private TMP_InputField m_Password;
    [SerializeField] private TMP_InputField m_Email;
    [SerializeField] private TMP_InputField m_Name;
    [SerializeField] private TMP_InputField m_ComfirmPassword;
    [SerializeField] private TMP_Text m_Status;

    [SerializeField] private LoadingScene m_LoadingScene;
    [SerializeField] private string nextSceneToLoad;

    public async void UserRegistration()
    {
        m_Status.text = "Loading";
        // Check the password is consistent
        if (m_Password.text != m_ComfirmPassword.text)
        {
            Debug.Log("Password is not consistent!");
            m_Status.text = "Password is not consistent!";
        }
        else
        {
            var isRegistered = await WebHandler.instance.RegisterUser(m_Name.text,m_Username.text,m_Email.text,m_Password.text);
            if (isRegistered.IsRegistered)
            {
                // Do something
                StartCoroutine(RegisterSuccessDelay(isRegistered.Message));
            }
            else
            {
                m_Status.text = isRegistered.Message;
            }
        }

    }

    IEnumerator RegisterSuccessDelay(string message)
    {
        m_Status.text = message;
        yield return new WaitForSeconds(1);
        m_LoadingScene.LoadScene(nextSceneToLoad);
    }
}

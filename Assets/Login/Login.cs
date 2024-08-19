using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class Login : MonoBehaviour
{
    [SerializeField] TMP_InputField m_Username;
    [SerializeField] TMP_InputField m_Password;
    [SerializeField] TMP_Text m_Status;

    [SerializeField] LoadingScene m_LoadingScene;
    [SerializeField] string NextSceneToLoad;

    public async void UserLogin()
    {
        m_Status.text = "Loading";
        var isLogin = await WebHandler.instance.LoginUser(m_Username.text, m_Password.text);
        if (isLogin.IsLogin)
        {
            // Do something
            StartCoroutine(LoginSuccessDelay(isLogin.Message));
        }
        else
        {
            m_Status.text = isLogin.Message;
        }
    }
    IEnumerator LoginSuccessDelay(string message)
    {
        m_Status.text = message;
        yield return new WaitForSeconds(1);
        m_LoadingScene.LoadScene(NextSceneToLoad);
    }
}

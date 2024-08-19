using System;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;
using TMPro;
// using UnityEditor.VersionControl;

public class WebHandler : MonoBehaviour
{
    public static WebHandler instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    // Check Internet and Database health
    public async Task<(string Message,bool IsHealthy)> CheckHealth()
    {
        Debug.Log("Checking Internet Connection...");
        
        string url = Database.url + "/health";

        UnityWebRequest www = UnityWebRequest.Get(url);

        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("x-api-key", Database.API_KEY);

        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await System.Threading.Tasks.Task.Yield();

        var jsonResponse = www.downloadHandler.text;

        string message = "";
        switch (www.result)
        {
            case UnityWebRequest.Result.Success:
                message = "Connected!";
                Debug.Log("Connected!");
                return (message,true);

            case UnityWebRequest.Result.ProtocolError:
                message = "Protocol Error!";
                Debug.Log("Protocol Error!");
                return (message,false);

            case UnityWebRequest.Result.ConnectionError:
                message = "Connection Failed!";
                Debug.Log("Connection Failed!");
                return (message,false);

            default:
                Debug.LogError(www.error);
                return (www.error,false);
        }
    }


    // Login function
    public async Task<(string Message,bool IsLogin)> LoginUser(string username, string password)
    {
        Debug.Log("Loading....");

        string url = Database.url + "/login";
        string message = "";

        if (username == "" || password == "")
        {
            message = "Some fields are empty!";
            Debug.Log("Some fields are empty!");
            return (message,false);
        }

        /*----------------Parse Input Data---------------------*/
        LoginData loginData = new LoginData();
        loginData.username = username;
        loginData.password = password;

        Debug.Log("username : " + loginData.username);
        Debug.Log("password : " + loginData.password);

        var body = JsonUtility.ToJson(loginData);
        Debug.Log(body);

        /*----------------Send Post Request---------------------*/
        UnityWebRequest www = UnityWebRequest.Put(url, body);
        www.method = "POST";

        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("x-api-key", Database.API_KEY);

        var operation = www.SendWebRequest();


        while (!operation.isDone)
            await System.Threading.Tasks.Task.Yield();

        /*----------------Response received----------------------*/

        var jsonResponse = www.downloadHandler.text;
        Debug.Log(jsonResponse);


        switch (www.result)
        {
            case UnityWebRequest.Result.Success:
                LoginResponse result = JsonUtility.FromJson<LoginResponse>(jsonResponse);
                message = "Welcome back! " + result.user.name;
                Debug.Log("Welcome back! " + result.user.name);
                // Update database username and token
                Database.username = result.user.username;
                Database.token = result.token;
                return (message,true);

            case UnityWebRequest.Result.ProtocolError:
                ExceptionResponse error_result = JsonUtility.FromJson<ExceptionResponse>(jsonResponse);
                message = error_result.message;
                Debug.Log(error_result.message);
                return (message,false);

            case UnityWebRequest.Result.ConnectionError:
                message = "Connection Failed:" + www.error;
                Debug.LogError($"Connection Failed:{www.error}");
                return (message,false);

            default:
                message = www.error;
                Debug.LogError(www.error);
                return (message,false);
        }
    }

    // Registration function
    public async Task<(string Message,bool IsRegistered)> RegisterUser(string name, string username, string email, string password)
    {
        Debug.Log("Loading....");


        string url = Database.url + "/register";
        string message = "";

        if (name == "" || email == "" || username == "" || password == "")
        {
            message = "Some fields are empty!";
            Debug.Log("Some fields are empty!");
            return (message,false);
        }

        /*----------------Parse Input Data---------------------*/
        RegisterData registerData = new RegisterData();
        registerData.name = name;
        registerData.email = email;
        registerData.username = username;
        registerData.password = password;

        /*----------------Send Post Request---------------------*/
        UnityWebRequest www = UnityWebRequest.Put(url, JsonUtility.ToJson(registerData));

        www.method = "POST";

        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("x-api-key", Database.API_KEY);

        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await System.Threading.Tasks.Task.Yield();


        /*----------------Response received----------------------*/
        var jsonResponse = www.downloadHandler.text;
        Debug.Log(jsonResponse);

        switch (www.result)
        {
            case UnityWebRequest.Result.Success:
                RegisterResponse result = JsonUtility.FromJson<RegisterResponse>(jsonResponse);
                message = "Hello! " + result.username + ". You have succefully registered.";
                Debug.Log(message);
                return (message,true) ;

            case UnityWebRequest.Result.ProtocolError:
                ExceptionResponse error_result = JsonUtility.FromJson<ExceptionResponse>(jsonResponse);
                message = error_result.message;
                Debug.Log(error_result.message);
                return (message,false);

            case UnityWebRequest.Result.ConnectionError:
                message = "Connection Failed:" + www.error;
                Debug.LogError($"Connection Failed:{www.error}");
                return (message,false);

            default:
                message = www.error;
                Debug.LogError(www.error);
                return (message,false);
        }
    }

    // Register User Information function
    public async Task<bool> RegisterUserInfo(string gender, string age)
    {
        Debug.Log("Loading Register User Information.");


        string url = Database.url + "/registerInfo";

        if (Database.username == "")
        {
            Debug.Log("You have to login first.");
            return false;
        }

        if (gender == "" || age == "")
        {
            Debug.Log("Some fields are empty!");
            return false;
        }

        /*----------------Parse Input Data---------------------*/
        RegisterInfoData registerInfoData = new RegisterInfoData();
        registerInfoData.username = Database.username;
        registerInfoData.gender = gender;
        registerInfoData.age = Int32.Parse(age);
        registerInfoData.disability = "";

        /*----------------Send Post Request---------------------*/
        UnityWebRequest www = UnityWebRequest.Put(url, JsonUtility.ToJson(registerInfoData));

        www.method = "POST";

        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("x-api-key", Database.API_KEY);

        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await System.Threading.Tasks.Task.Yield();


        /*----------------Response received----------------------*/
        var jsonResponse = www.downloadHandler.text;
        Debug.Log(jsonResponse);

        switch (www.result)
        {
            case UnityWebRequest.Result.Success:
                RegisterResponse result = JsonUtility.FromJson<RegisterResponse>(jsonResponse);
                Debug.Log(result.username+" has succeefully registered the information.");
                return true;

            case UnityWebRequest.Result.ProtocolError:
                ExceptionResponse error_result = JsonUtility.FromJson<ExceptionResponse>(jsonResponse);
                Debug.Log(error_result.message);
                return false;

            case UnityWebRequest.Result.ConnectionError:
                Debug.LogError($"Connection Failed:{www.error}");
                return false;

            default:
                Debug.LogError(www.error);
                return false;
        }
    }


    // Update Score
    public async Task<bool> UpdateScore(int score)
    {
        Debug.Log("Loading...");

        string url = Database.url + "/update";

        if (Database.username == "" || Database.token == "")
        {
            Debug.Log("Need to Login first!");
            return false;
        }

        /*----------------Parse Input Data---------------------*/
        UpdateInfo updateInfo = new UpdateInfo();
        updateInfo.username = Database.username;
        updateInfo.token = Database.token;
        updateInfo.score = score.ToString();

        /*----------------Send Post Request---------------------*/
        UnityWebRequest www = UnityWebRequest.Put(url, JsonUtility.ToJson(updateInfo));

        www.method = "PUT";

        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("x-api-key", Database.API_KEY);

        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await System.Threading.Tasks.Task.Yield();


        /*----------------Response received----------------------*/
        var jsonResponse = www.downloadHandler.text;
        Debug.Log(jsonResponse);

        switch (www.result)
        {
            case UnityWebRequest.Result.Success:
                UpdateResponse result = JsonUtility.FromJson<UpdateResponse>(jsonResponse);
                Debug.Log(result.message + " to " + result.username);
                return true;

            case UnityWebRequest.Result.ProtocolError:
                ExceptionResponse error_result = JsonUtility.FromJson<ExceptionResponse>(jsonResponse);
                Debug.Log(error_result.message);
                return false;

            case UnityWebRequest.Result.ConnectionError:
                Debug.LogError($"Connection Failed:{www.error}");
                return false;

            default:
                Debug.LogError(www.error);
                return false;
        }
    }

    // Get Score
    public async Task<int> GetScore()
    {
        Debug.Log("Loading...");

        string url = Database.url + "/score";

        if (Database.username == "" || Database.token == "")
        {
            Debug.Log("Need to Login first!");
            return -1;
        }

        /*----------------Parse Input Data---------------------*/
        ScoreInfo scoreInfo = new ScoreInfo();
        scoreInfo.username = Database.username;
        scoreInfo.token = Database.token;


        /*----------------Send Post Request---------------------*/
        UnityWebRequest www = UnityWebRequest.Put(url, JsonUtility.ToJson(scoreInfo));

        www.method = "POST";

        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("x-api-key", Database.API_KEY);

        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await System.Threading.Tasks.Task.Yield();


        /*----------------Response received----------------------*/
        var jsonResponse = www.downloadHandler.text;
        Debug.Log(jsonResponse);

        switch (www.result)
        {
            case UnityWebRequest.Result.Success:
                ScoreResponse result = JsonUtility.FromJson<ScoreResponse>(jsonResponse);
                Debug.Log(result.message + " " + result.score);
                return result.score;

            case UnityWebRequest.Result.ProtocolError:
                ExceptionResponse error_result = JsonUtility.FromJson<ExceptionResponse>(jsonResponse);
                Debug.Log(error_result.message);
                return -1;

            case UnityWebRequest.Result.ConnectionError:
                Debug.LogError($"Connection Failed:{www.error}");
                return -1;

            default:
                Debug.LogError(www.error);
                return -1;
        }
    }
}
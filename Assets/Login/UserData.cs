using System;
using JetBrains.Annotations;

[Serializable]
public class UserData
{
    public static string username;
    public static string name;
    public static string token;
}


[Serializable]
public class LoginData
{
    public string username;
    public string password;
}


[Serializable]
public class RegisterData
{
    public string name;
    public string email;
    public string username;
    public string password;
}

[Serializable]
public class RegisterInfoData
{
    public string username;
    public string gender;
    public int age;
    public string disability;
}


[Serializable]
public class UserInfo
{
    public string username;
    public string name;
}

[Serializable]
public class UpdateInfo
{
    public string username;
    public string token;
    public string score;
}

[Serializable]
public class ScoreInfo
{
    public string username;
    public string token;
}

[Serializable]
public class UpdateResponse
{
    public string message;
    public string username;
}


[Serializable]
public class LoginResponse
{
    public UserInfo user;
    public string token;
}


[Serializable]
public class RegisterResponse
{
    public string username; 
}

[Serializable]
public class ScoreResponse
{
    public string message;
    public int score;
}


[Serializable]
public class ExceptionResponse
{
    public string message;
}


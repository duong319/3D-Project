using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLogin : MonoBehaviour
{
    void Start()
    {
        Login();
    }

    void Login()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var request = new LoginWithAndroidDeviceIDRequest
        {
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginSuccess, OnLoginFailure);

#elif UNITY_IOS && !UNITY_EDITOR
        var request = new LoginWithIOSDeviceIDRequest
        {
            DeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithIOSDeviceID(request, OnLoginSuccess, OnLoginFailure);

#else
        
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
#endif
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login successful! PlayFab ID: " + result.PlayFabId);
    }

    void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Login failed: " + error.GenerateErrorReport());
    }
}

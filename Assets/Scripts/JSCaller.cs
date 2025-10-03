using System.Runtime.InteropServices;
using UnityEngine;

public class JSCaller : MonoBehaviour {
    [DllImport("__Internal")]
    private static extern void ShowAlert(string msg);

    [DllImport("__Internal")]
    private static extern void LogToConsole(string msg);

    void Start() {
#if UNITY_WEBGL && !UNITY_EDITOR
        ShowAlert("Hello from Unity!");
        LogToConsole("This message goes to the browser console!");
#else
        Debug.Log("EditorÇ≈ÇÕ jslib åƒÇ—èoÇµÇÕñ≥å¯Ç≈Ç∑");
#endif
    }
}

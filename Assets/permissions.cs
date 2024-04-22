using UnityEngine;
using UnityEngine.Android;

public class StoragePermissionHandler : MonoBehaviour
{
    private PermissionCallbacks callbacks;

    void Start()
    {
        // Set up the callbacks for handling permission responses
        callbacks = new PermissionCallbacks();
        callbacks.PermissionGranted += PermissionGrantedHandler;
        callbacks.PermissionDenied += PermissionDeniedHandler;
        callbacks.PermissionDeniedAndDontAskAgain += PermissionDeniedAndDontAskAgainHandler;

        // Check if the READ_EXTERNAL_STORAGE permission is already granted
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Debug.Log("External storage read permission already granted.");
            // Continue with operations that require read permission
        }
        else
        {
            // Request permission to read external storage
            Debug.Log("Requesting read external storage permission.");
            Permission.RequestUserPermission(Permission.ExternalStorageRead, callbacks);
        }
    }

    // Callback for when permission is granted
    private void PermissionGrantedHandler(string permissionName)
    {
        Debug.Log($"{permissionName} Permission Granted");
        // Perform any action when permission is granted (e.g., access external storage)
    }

    // Callback for when permission is denied
    private void PermissionDeniedHandler(string permissionName)
    {
        Debug.Log($"{permissionName} Permission Denied");
        // Handle permission denial (consider showing a message to the user)
    }

    // Callback for when permission is denied and user selected "Don't ask again"
    private void PermissionDeniedAndDontAskAgainHandler(string permissionName)
    {
        Debug.Log($"{permissionName} Permission Denied and Don't Ask Again");
        // Inform the user about the consequence of not having permission
    }

    
}

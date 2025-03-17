using Unity.Netcode;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 offset = new Vector3(0, 3, -5); // Adjust as needed

    void Start()
    {
        // Wait for the local player to be assigned before looking for the target
        if (NetworkManager.Singleton.IsClient)
        {
            AssignLocalPlayer();
        }
    }

    void AssignLocalPlayer()
    {
        if (NetworkManager.Singleton.LocalClient != null)
        {
            var localPlayerObject = NetworkManager.Singleton.LocalClient.PlayerObject;
            if (localPlayerObject != null)
            {
                // Directly assign the camera target to the local player
                target = localPlayerObject.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Follow the local player with an offset
            transform.position = target.position + offset;
            transform.LookAt(target.position + Vector3.up * 1.5f); // Adjust the look-at position
        }
    }
}
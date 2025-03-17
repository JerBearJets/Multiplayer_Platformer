using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public Camera playerCamera; // Reference to the camera on the player

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // Ensure the camera is active only for the local player (IsOwner)
        if (IsOwner)
        {
            playerCamera.gameObject.SetActive(true);  // Activate camera for local player
            Debug.Log("Camera activated for local player");
        }
        else
        {
            playerCamera.gameObject.SetActive(false); // Deactivate for remote players
            Debug.Log("Camera deactivated for remote player");
        }
    }
}
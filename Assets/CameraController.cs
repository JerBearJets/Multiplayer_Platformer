using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraController : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if(!IsOwner)
        {
            gameObject.SetActive(false);
        }
    }
    
    
}

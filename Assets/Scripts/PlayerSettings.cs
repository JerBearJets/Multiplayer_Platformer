using UnityEngine;
using Unity.Netcode;
using Unity.Collections;


public class PlayerSettings : NetworkBehaviour
{
    // Sprites will be called to change player skin
    [SerializeField]
    Sprite[] playerSkin;


    //Need to update the sprite, and need to update the box collider
    private SpriteRenderer spriteRend;
    private BoxCollider boxCollider;
    private CharacterController characterController;

    // Awaken the renderers when changing skins
    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        characterController = GetComponent<CharacterController>();
    }

    public override void OnNetworkSpawn()
    {
        // If the player connecting is the client, change sprite model, direction, size, and hitbox

        if(OwnerClientId == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = playerSkin[1];

            transform.localScale = new Vector2(0.22f, 0.22f);

            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

            //Vector2 spriteSize = spriteRend.bounds.size;
            //boxCollider.size = spriteSize;

            characterController.radius = 2f;
        }
        
            
            
        
    }
}

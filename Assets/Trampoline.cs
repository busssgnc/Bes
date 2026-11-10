using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Zıplama Gücü")]
    public float bounceForce = 15f; 

   
    private void OnTriggerEnter(Collider other)
    {
      
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player != null)
        {
            player.Bounce(bounceForce);
        }
    }
}
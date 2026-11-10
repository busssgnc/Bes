using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
               
                Debug.Log("Yanlış taşa bastın, düştün!");

               
                other.transform.position = new Vector3(0f, 1f, 0f); 
            }
        }
    }
}
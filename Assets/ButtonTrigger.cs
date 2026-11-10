using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [Header("Kapı Script'inin Olduğu Obje")]
    public SlidingDoor doorController; 

   
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Player"))
        {
            if (doorController != null)
            {
                doorController.ToggleDoor(); 
                Debug.Log("Butona basıldı, kapı tetiklendi!");
            }
        }
    }
}
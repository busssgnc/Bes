using UnityEngine;
using System.Collections;

public class SlidingDoor : MonoBehaviour
{
    public Transform leftDoor; 
    public Transform rightDoor; 

    public float openDistance = 2f;
    public float speed = 3f;        

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;
    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;

    private bool isOpen = false;

    void Start()
    {
        if (leftDoor != null && rightDoor != null)
        {
          
            leftClosedPos = leftDoor.localPosition;
            rightClosedPos = rightDoor.localPosition;

           
            leftOpenPos = leftClosedPos - new Vector3(openDistance, 0f, 0f);
            rightOpenPos = rightClosedPos + new Vector3(openDistance, 0f, 0f);
        }
    }

    public void ToggleDoor()
    {
        StopAllCoroutines();
        StartCoroutine(MoveDoor(!isOpen));
        isOpen = !isOpen;
    }

    IEnumerator MoveDoor(bool open)
    {
        float time = 0;
        Vector3 targetLeft = open ? leftOpenPos : leftClosedPos;
        Vector3 targetRight = open ? rightOpenPos : rightClosedPos;

        Vector3 startLeft = leftDoor.localPosition;
        Vector3 startRight = rightDoor.localPosition;

        while (time < 1f)
        {
            time += Time.deltaTime * speed;
            leftDoor.localPosition = Vector3.Lerp(startLeft, targetLeft, time);
            rightDoor.localPosition = Vector3.Lerp(startRight, targetRight, time);
            yield return null;
        }
    }
}
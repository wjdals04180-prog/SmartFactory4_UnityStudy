using UnityEngine;

public class Elevater : MonoBehaviour
{
    public Transform[] positions = new Transform[2];
    [Range(1f, 10f)]
    public float moveSpeed = 1f;
    private int _destinationIndex = 0;
    
    void FixedUpdate()
    {
        transform.localPosition = 
            Vector3.MoveTowards(
                transform.localPosition, 
                positions[_destinationIndex].localPosition, 
                moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("들어왔다");
        _destinationIndex = 1;
    }

    private void OnTriggerExit(Collider other)
    {
        _destinationIndex = 0;
    }
}

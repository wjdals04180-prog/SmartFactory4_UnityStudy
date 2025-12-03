using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    
    public Animator doorAnimator;  // 문 애니메이터 연결용

   

    void OnTriggerEnter(Collider other)
    {
        // 플레이어가 범위 안으로 들어왔다면
        doorAnimator.SetBool("isOpen", true);
    }

    void OnTriggerExit(Collider other)
    {
        // 플레이어가 범위 밖으로 나갔다면
        doorAnimator.SetBool("isOpen", false);
    }
}

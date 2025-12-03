using UnityEngine;

public class PistonPusher : MonoBehaviour
{
    //물리적인 힘을 가할 게임오브젝트 변수
    public GameObject target;

    public float pushPower =10f;
    public void Push()
    {
        if (target == null)
            return;
        //물리적인 힘을 가하려면 리지드바디가 필요함
        Rigidbody rb = target.GetComponent<Rigidbody>();
        //리지드마디에 z축 (-)방향으로 이동하라고 명령

        
        rb.AddForce(Vector3.left * pushPower, ForceMode.VelocityChange);
        target = null;
    }
  
}

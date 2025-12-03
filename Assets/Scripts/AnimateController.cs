using UnityEngine;

public class AnimateController : MonoBehaviour
{
    //트리거 파라메터 이름 변수
    public string triggerName = "Trigger";
    public Animator triggerAnim;
    //회전 속도 파라메터 이름 변수
    public string SpeedName = "Speed";
    //회전 속도 값
    public float direction = 1f;
    [Range(0,1000f)]
    public float speedValue = 0f;
    //회전 속도 변화값
    public float changeValue = 0.1f;
    public Animator speedAnim;
    public string triggerName2 = "Trigger";
    public Animator PistonAnim;


    private void Start()
    {
        //기어가 시작할 때 Speed변수에 있는 값을 기준으로 회전하도록 초기화한다.
        speedAnim.SetFloat(SpeedName,direction * speedValue);
    }

    public void OnPush()
    {
        PistonAnim.SetTrigger(triggerName2);
    }
    public void OnAttack()
    {
        //애니메이터에 해당 이름을 가진 트리거 파라메터를 설정한다.
        triggerAnim.SetTrigger(triggerName);
       
    }

    public void OnFaster()
    {
        speedValue += changeValue;
        if(speedValue > 1000f)
        {
            speedValue = 1000f;
        }

        //애니메이터에 해당 이름을 가진 float 파라메터의 값을 변경한다.
        speedAnim.SetFloat(SpeedName, direction * speedValue);
    }
    public void OnSlower()
    {
        speedValue -= changeValue;
        if(speedValue < 0f)
            speedValue = 0f;
        //애니메이터에 해당 이름을 가진 float 파라메터의 값을 변경한다.
        speedAnim.SetFloat(SpeedName, direction * speedValue);
    }

    public void OnInvert()
    {
        direction *= -1f;
        //애니메이터에 해당 이름을 가진 float 파라메터의 값을 변경한다.
        speedAnim.SetFloat(SpeedName, direction * speedValue);
    }


}

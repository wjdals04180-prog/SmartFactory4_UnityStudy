using UnityEngine;

public class EnamyCore : Enemy
{
    //회전 속도 
    public float rotateSpeed = 90f;
    
    [SerializeField]//숨겨놓은 변수를 인스펙터에서 확인하고 수정할 수 있게 해주는 속성
    private Transform _target;

    protected override void Die()
    {
        //물리적인 무적상태 해제
        _rigidbody.isKinematic = false;
        base.Die();

    }

    private void Update()
    {
        if (_target == null)
            return;

        //타켓을 향해 회전속도만큼 쳐다보려고 노력함
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation((_target.position - transform.position).normalized, Vector3.up)
            , rotateSpeed * Time.deltaTime);
    }


}

using UnityEngine;

public class EnamyArmor : Enemy
{
    //활성화되면 물리 무적상태로 변경
    private void OnEnable()
    {
        _rigidbody.isKinematic = true;

    }

    protected override void Die()
    {
        //죽으면 무적상태 해제
        _rigidbody.isKinematic = false;
        base.Die();
    }


}

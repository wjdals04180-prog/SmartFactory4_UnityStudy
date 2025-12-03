using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    //반동 적용할 트랜스폼
    public Transform weaponPosition;

    //내부 계산용 변수
    private Vector3 _currentRotation;
    private Vector3 _targetRotation;
    private float _returnSpeed;
    private float _snappiness;

    void Update()
    {
        //목표 회전값은 서서히 원위치로 돌아오려고 하게 만들고
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
        //현재 회전값은 목표 회전값을 부드럽게 따라가게 만듬.
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappiness * Time.deltaTime);
        //카메라에 적용하기
        transform.localRotation = Quaternion.Euler(_currentRotation);
    }

    public void FeelRecoil(Vector3 recoil, float snappines, float returnSpeed)
    {
        //반동 회복속도
        _returnSpeed = returnSpeed;
        //반동 적용속도 
        _snappiness = snappines;
        //목표 회전값에 반동을 누적시킴
        _targetRotation += new Vector3(recoil.x, Random.Range(-recoil.y, recoil.y), 0f);
    }
}

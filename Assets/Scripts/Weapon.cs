using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("발사 관련")]
    //발사할 총알 
    public Projectile projectile;
    //총알이 발사되는 위치
    public Transform firePosition;
    //총알이 발사될 때 보여야 하는 이펙트
    public GameObject fireEffect;
    //자동 발사 기능 여부
    public bool automaticFire = false;
    //연사 속도
    [Range(0.2f, 1f)]
    public float rapidFireSpeed = 0.2f;

    [Space(10f), Header("조준 관련")]
    //에임 전환 속도
    public float aimSpeed = 0.15f;
    //에임 안하고 있을 때 총기 위치
    public Vector3 aimOffset = new Vector3(0.15f, -0.15f, 0.15f);
    //에임시 카메라 화각
    public float aimFieldOfView = 40f;

    //현재 방아쇠를 당기고 있는 중이냐
    private bool _isTrigger = false;
    //다음 총알을 발사할 수 있는 시간
    private float _waitFireTime = 0f;

    [Space(10f), Header("반동 관련")]
    public CameraRecoil recoilCamera;
    //반동시 카메라가 위로 튀거나 좌우로 흔들리는 강도.
    public Vector3 recoilValue = new Vector3(-2.0f, 0.5f, -0.03f);
    //반동이 얼마나 빠르게 튀는지 정도
    public float snappiness = 6.0f;
    //제자리로 돌아오는 속도
    public float returnSpeed = 2.0f;

    //반동 제어시 현재 위치값을 여기에 보관함.
    private Vector3 _recoilPosition;
    private Vector3 _currentPosition;

    //방아쇠를 당겼을 때 호출되는 함수
    public void Fire(bool isTrigger)
    {
        //방아쇠 당겼는지 여부 갱신
        _isTrigger = isTrigger;

        //당기지 않은 상태이면 return
        if (!isTrigger)
            return;

        //총을 발사할 수 없는 시간이면 return
        if (_waitFireTime > Time.time)
            return;

        //발사
        Shoot();
    }

    private void Shoot()
    {
        //총알 참조가 비어있으면 총알 생성없이 return
        if (projectile == null)
            return;

        //총알 생성 & 발사
        Projectile p = Instantiate(projectile, firePosition.position, firePosition.rotation);
        p.Launch();
        //발사 이펙트 켜기
        fireEffect.SetActive(true);

        //다음 연사 가능 시간 갱신
        _waitFireTime = Time.time + rapidFireSpeed;

        //반동 추가
        _recoilPosition = new Vector3(0, 0, recoilValue.z);
        recoilCamera.FeelRecoil(recoilValue, snappiness, returnSpeed);
    }

    private void OnEnable()
    {
        //활성화시 무조건 발사할 수 있는 상태로 만들기
        _waitFireTime = 0f;
    }

    private void Update()
    {
        //반동 제어를 Interpolation을 이용해 자연스럽게 만든 값을 구하기
        _recoilPosition = Vector3.Lerp(_recoilPosition, Vector3.zero, returnSpeed * Time.deltaTime);
        _currentPosition = Vector3.Lerp(_currentPosition, _recoilPosition, snappiness * Time.deltaTime);

        //현재 반동 적용 위치를 실제로 적용하기.
        transform.localPosition = _currentPosition;

        //방아쇠를 당기지 않았거나 오토 연사 기능이 해제되어 있으면 Return
        if (!_isTrigger || !automaticFire)
            return;

        //발사 할 수 없는 상태면 return
        if (_waitFireTime > Time.time)
            return;

        //발사
        Shoot();
    }
}

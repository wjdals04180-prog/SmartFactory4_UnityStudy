using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponTrigger : MonoBehaviour
{
    //현재 장비중인 무기
    public Weapon currentWeapon;
    //조준에 사용되는 카메라
    public Camera aimCamera;
    //무기를 드는 손의 위치
    public Transform weaponPosition;
    //카메라 반동 제어 참조
    public CameraRecoil recoilCamera;

    //현재 조준중이냐
    private bool _isAiming = false;
    //현재 조준 전환 시간 
    private float _aimTime = 0f;

    //발사키를 눌렀을 때 호출되는 함수
    public void OnShoot(InputValue value)
    {
        currentWeapon.Fire(value.isPressed);
    }

    //조준키를 눌렀을 때 호출되는 함수
    public void OnAim(InputValue value)
    {
        if (currentWeapon == null)
            return;

        if (_isAiming != value.isPressed)
            _aimTime = 0f;

        _isAiming = value.isPressed;
    }

    //아이템을 먹었을 때 호출해야 하는 함수
    public void PickWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        currentWeapon.transform.SetParent(weaponPosition);
        currentWeapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        weaponPosition.localPosition = weapon.aimOffset;
        currentWeapon.recoilCamera = recoilCamera;
    }

    private void Update()
    {
        _aimTime += Time.deltaTime;

        Vector3 offset = currentWeapon.aimOffset; offset.x = Mathf.Clamp(offset.x, 0f, 1f);
        offset.x = 0f;

        //조준키를 눌렀을 경우
        if (_isAiming)
        {
            //총의 위치를 조준시 위치로 자연스럽게 이동시키기
            weaponPosition.localPosition =
                Vector3.Lerp(
                    currentWeapon.aimOffset,
                    offset,
                    _aimTime / currentWeapon.aimSpeed
                    );

            //카메라의 화각을 자연스럽게 변경하기
            aimCamera.fieldOfView =
                Mathf.Lerp(
                    60f,
                    currentWeapon.aimFieldOfView,
                    _aimTime / currentWeapon.aimSpeed
                    );
        }
        else
        {
            //반대로 적용하기
            weaponPosition.localPosition =
                Vector3.Lerp(
                    offset,
                    currentWeapon.aimOffset,
                     _aimTime / currentWeapon.aimSpeed
                    );

            aimCamera.fieldOfView =
                Mathf.Lerp(
                    currentWeapon.aimFieldOfView,
                    60f,
                     _aimTime / currentWeapon.aimSpeed
                    );
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponTrigger : MonoBehaviour
{
    public Weapon currentWeapon;

    public void OnShoot(InputValue value)
    {
        currentWeapon.Fire(value.isPressed);
    }
}

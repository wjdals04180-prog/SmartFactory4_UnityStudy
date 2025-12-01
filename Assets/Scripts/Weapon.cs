using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Projectile projectile;
    public Transform firePosition;
    public GameObject fireEffect;

    public bool automaticFire = false;
    [Range(0.2f, 1f)]
    public float rapidRate = 1f;

    private bool _isTrigger = false;
    private float _waitFireTime = 0f;

    public void Fire(bool isTrigger)
    {
        _isTrigger = isTrigger;
        if (automaticFire)
            return;

        if (!isTrigger)
            return;

        if(projectile != null)
        {
            Projectile p = Instantiate(projectile, firePosition.position, firePosition.rotation);
            p.Launch(firePosition.forward);
            //Instantiate(fireEffect, firePosition.position, firePosition.rotation);
        }
    }

    private void Update()
    {
        if (!automaticFire || !_isTrigger)
        {
            fireEffect.SetActive(false);
            return;
        }

        if (_waitFireTime > Time.time)
            return;

        _waitFireTime += rapidRate;
        if (projectile != null)
        {
            fireEffect.SetActive(true);
            Projectile p = Instantiate(projectile, firePosition.position, firePosition.rotation);
            p.Launch(firePosition.forward);
            //Instantiate(fireEffect, firePosition.position, firePosition.rotation);
        }
    }
}

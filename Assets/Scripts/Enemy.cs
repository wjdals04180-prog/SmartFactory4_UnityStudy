using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //최대 체력
    public int maxHeath = 10;
    //현재 체력
    protected int _currentHp = 0;
    //현재 살아있냐?
    protected Rigidbody _rigidbody;


    private bool _isAlve = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentHp = maxHeath;
        _isAlve = true;
    }




    public bool Hit(int damage)
    {
        if (!_isAlve)
            return true;

        _currentHp = Mathf.Clamp(_currentHp - damage, 0, maxHeath);
        if( _currentHp== 0)
        {
            Die();
            return true;
        }
        return false;   
    }

    protected virtual void Die()
    {
        _isAlve = false;
        StartCoroutine(Duing());
    }

    protected IEnumerator Duing()
    {
        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }


    
}

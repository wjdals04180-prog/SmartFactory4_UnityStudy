using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject hitEffect;
    public float launchPower = 300f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //Launch(transform.forward);
    }

    public void Launch(Vector3 direction)
    {
        _rigidbody.AddForce(direction * launchPower);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.contacts[0].normal);
        Instantiate(hitEffect, 
            collision.contacts[0].point + collision.contacts[0].normal * 0.01f, 
            Quaternion.LookRotation(collision.contacts[0].normal));
        Destroy(gameObject);
    }
}

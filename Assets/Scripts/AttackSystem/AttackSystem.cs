using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public KeyCode keyShoot = KeyCode.Q;
    public float fireDelay = 0.5f;
    float fireElapsedTime;

    [SerializeField] GameObject projectile;
    [SerializeField] float velocity = 1.0f;
    [SerializeField] float damage = 30.0f;
    [SerializeField] GameObject shooter;


    // Start is called before the first frame update
    void Start()
    {
        fireElapsedTime = fireDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keyShoot))
        {
            if(fireElapsedTime >= fireDelay)
            {
                fireElapsedTime = 0.0f;
                GameObject projectile_shooted = Instantiate(projectile);
                projectile_shooted.transform.position = shooter.transform.TransformPoint(Vector3.zero);
                projectile_shooted.transform.rotation = Quaternion.Euler(projectile_shooted.transform.eulerAngles.x + shooter.transform.rotation.eulerAngles.x, shooter.transform.rotation.eulerAngles.y, shooter.transform.rotation.eulerAngles.z);
                projectile_shooted.GetComponent<Shot>().damage = damage;
                projectile_shooted.GetComponent<Rigidbody>().AddForce(velocity * 1000 * shooter.transform.forward, ForceMode.Force);
            }
        }
        fireElapsedTime += Time.deltaTime;
    }
}

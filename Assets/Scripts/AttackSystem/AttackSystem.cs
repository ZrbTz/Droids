using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    //public KeyCode keyShoot = KeyCode.Q;
    public float fireDelay = 0.01f;
    public float shotgunFireDelay = 1f;
    float fireElapsedTime;
    float shotgunFireElapsedTime;
    private GameObject projectile;

    [SerializeField] GameObject standardProjectile;
    [SerializeField] GameObject berserkProjectile;
    [SerializeField] float velocity = 1.0f;
    [SerializeField] float damage = 30.0f;
    [SerializeField] float berserkTime = 15.0f;
    [SerializeField] float shotgunTotalDamage = 5f;
    private float elapsedBerserkTime = 0.0f;
    [SerializeField] GameObject shooter;
    private bool weaponSelector = true;
    private List<Shot> shotPool;

    private GameUI gameUI;
    private InputManager inputManager;

    void Awake() {
        gameUI = FindObjectOfType<GameUI>();
        inputManager = FindObjectOfType<InputManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        fireElapsedTime = fireDelay;
        shotgunFireElapsedTime = shotgunFireDelay;
        projectile = standardProjectile;
    }

    // Update is called once per frame
    void Update() {
        if (projectile == berserkProjectile) {
            elapsedBerserkTime += Time.deltaTime;
            if (elapsedBerserkTime >= berserkTime) {
                switchMode();
                elapsedBerserkTime = 0.0f;
            }
        }

        if (inputManager.WeaponSwitched) {
            weaponSelector = !weaponSelector;
            gameUI.UpdateWeaponType(weaponSelector);
        }

        fireElapsedTime += Time.deltaTime;
        shotgunFireElapsedTime += Time.deltaTime;
        if (weaponSelector) {
            if (inputManager.WeaponFire) {
                if (fireElapsedTime >= fireDelay) {
                    fireElapsedTime = 0.0f;
                    this.GetComponent<PlayerAnimationSounds>().Rifle();
                    GameObject projectile_shooted = Instantiate(projectile);
                    projectile_shooted.transform.position = shooter.transform.TransformPoint(Vector3.zero);
                    projectile_shooted.transform.rotation = Quaternion.Euler(projectile_shooted.transform.eulerAngles.x + shooter.transform.rotation.eulerAngles.x, shooter.transform.rotation.eulerAngles.y, shooter.transform.rotation.eulerAngles.z);
                    projectile_shooted.GetComponent<Shot>().damage = damage;
                    projectile_shooted.GetComponent<Rigidbody>().AddForce(velocity * 1000 * shooter.transform.forward, ForceMode.Force);
                }
            }
        }
        else if (!weaponSelector)
            if (shotgunFireElapsedTime >= shotgunFireDelay) {
                if (inputManager.WeaponFireDown) {
                    shotgunFireElapsedTime = 0.0f;
                    this.GetComponent<PlayerAnimationSounds>().Shotgun();
                    for (int i = 0; i < 10; i++) {
                        GameObject projectile_shooted = Instantiate(projectile);
                        projectile_shooted.GetComponent<Shot>().timeToLive = projectile_shooted.GetComponent<Shot>().shotgunTimeToLive;
                        projectile_shooted.transform.position = shooter.transform.TransformPoint(Vector3.zero);
                        projectile_shooted.transform.rotation = Quaternion.Euler(projectile_shooted.transform.eulerAngles.x + shooter.transform.rotation.eulerAngles.x, shooter.transform.rotation.eulerAngles.y, shooter.transform.rotation.eulerAngles.z);
                        projectile_shooted.GetComponent<Shot>().damage = shotgunTotalDamage/10f;
                        Vector3 forwardForce = shooter.transform.forward * 1000 * velocity;
                        Vector3 rightJitter = shooter.transform.right * Random.Range(-100f, 100f);
                        Vector3 upJitter = shooter.transform.up * Random.Range(-100f, 100f);
                        projectile_shooted.GetComponent<Rigidbody>().AddForce(forwardForce + rightJitter + upJitter, ForceMode.Force);
                    }
                }
            }
    }


    public void switchMode() {
        if (projectile == standardProjectile) {
            projectile = berserkProjectile;
            velocity = velocity*1.5f;
            fireDelay = fireDelay/1.5f;
            damage = damage * 10;
        }
        else {
            projectile = standardProjectile;
            velocity = velocity/1.5f;
            fireDelay = fireDelay*1.5f;
            damage = damage / 10;
        }
    }
}

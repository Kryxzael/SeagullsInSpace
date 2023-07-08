using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileLauncher : MonoBehaviour
{
    private const string FIRE_PROJECTILE_BUTTON_NAME = "Fire";

    public Projectile Projectile;
    public float      FireForce;
    public float      Cooldown  = 2f;

    private Rigidbody2D _rigid;

    private float _cooldownTimer;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(FIRE_PROJECTILE_BUTTON_NAME) && _cooldownTimer <= 0)
        {
            Projectile  projectile   = Instantiate(Projectile, transform.position, transform.rotation);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            projectileRb.velocity = _rigid.velocity + FireForce * -(Vector2)projectile.transform.up;
            _cooldownTimer = Cooldown;
        }

        if (_cooldownTimer > 0)
            _cooldownTimer -= Time.deltaTime;
    }
}
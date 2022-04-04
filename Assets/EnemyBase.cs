using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D)), RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBase : MonoBehaviour
{
    public static event DealDamage DamageToPlayer;

    public UnitType Type { get { return _type; } }
    private UnitType _type = UnitType.Enemy;

    [SerializeField]
    public int damageAmount = 1;

    [Range(1f, 50f)][SerializeField]
    protected float speedMultiplier = 1f;

    public float speed = 1;

    public Vector3 Direction { get { return direction; } set { direction = value; } }
    protected Vector3 direction;

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        transform.Translate(direction * speedMultiplier * Time.deltaTime * speed);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
    }

    protected void DealDamageToPlayer(int damage, Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player)) // would be wacky when there is more than one player
        {
            DamageToPlayer?.Invoke(damage);
            Destroy(gameObject);
        }
    }
}

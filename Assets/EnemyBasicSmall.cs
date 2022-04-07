using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicSmall : EnemyBase
{
    protected Vector2 randomMovement;

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        //rigidbody2d.AddForce(randomMovement * speedMultiplier * direction); // only used for TestRandomMovement(), for now
    }

    protected override void Update()
    {
        //base.Update();
        TestWavyMovement();
        //TestRandomMovement();
    }

    protected void TestRandomMovement() // TODO: make it head towards the player
    {
        randomMovement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    protected void TestWavyMovement() // TODO: make it do wavy movement perpedicular to direction
    {
        var distanceOffset = 2;
        var frequencyOffset = 4;
        var _newPosition = transform.position;
        _newPosition.x += Mathf.Sin(Time.time * frequencyOffset) * Time.deltaTime * distanceOffset;
        _newPosition.y += Mathf.Cos(Time.time * frequencyOffset) * Time.deltaTime * distanceOffset;
        transform.position = _newPosition + ((Vector3)direction * speedMultiplier * Time.deltaTime);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        DealDamageToPlayer(damageAmount, collision);
    }
}

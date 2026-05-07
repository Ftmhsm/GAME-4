using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Patrol, Chase }

    [Header("Referensi")]
    public Transform player;

    [Header("Speed Setting")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    [Header("Detection/Deteksi")]
    public float chaseRange = 6f;
    public float patrolchange = 4f;

    [Header("Rotation/Rotasi")]
    public float rotationSpeed = 250f;

    private Rigidbody2D rb;
    private Vector2 move;
    private float patrolTime;
    private EnemyState currentstate;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentstate = EnemyState.Patrol;
        ChooseRandom();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        switch (currentstate)
        {
            case EnemyState.Patrol:
                Patrol(distance);
                break;

            case EnemyState.Chase:
                Chase(distance);
                break;
        }

        RotateMovement();
    }

    private void FixedUpdate()
    {
        float speed = currentstate == EnemyState.Patrol ? patrolSpeed : chaseSpeed;

        rb.velocity = move * speed;
    }

    void ChooseRandom()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        move = new Vector2(x, y).normalized;
    }

    void Patrol(float jarak)
    {
        patrolTime += Time.deltaTime;

        if (patrolTime >= patrolchange)
        {
            ChooseRandom();
            patrolTime = 0;
        }

        if (jarak <= chaseRange)
        {
            currentstate = EnemyState.Chase;
        }
    }

    void Chase(float jarak)
    {
        move = (player.position - transform.position).normalized;

        if (jarak >= chaseRange)
        {
            currentstate = EnemyState.Patrol;
            ChooseRandom();
        }
    }

    void RotateMovement()
    {
        if (move != Vector2.zero)
        {
            float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;

            angle += 90;

            Quaternion target = Quaternion.Euler(0, 0, angle);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                target,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentstate == EnemyState.Patrol)
        {
            ChooseRandom();
        }
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFight : MonoBehaviour
{
    [Header("Misc References")]
    public Animator animator;
    public LayerMask enemyLayer;

    [Header("Melee")]
    public Transform meleePoint;
    public float meleeRange;
    public int meleeDamage;
    public float meleeKnockback;
    public float bossKnockback;
    public float meleeRate;

    private float nextAttackTime;
    private int direction = 1;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        direction = (int) (gameObject.transform.lossyScale.x / Mathf.Abs(gameObject.transform.lossyScale.x));
        
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                melee();
                nextAttackTime = Time.time + meleeRate;
            }
        }
    }

    public void damage(int amount, Vector3 point, float range, float knockback) {
        if (Physics2D.OverlapCircle(point, range, enemyLayer))
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(point, range, enemyLayer);

            foreach (Collider2D enemyCol in enemies)
            {
                Enemy enemy = enemyCol.GetComponent<Enemy>();
                enemy.takeDamage(amount);
                gameObject.GetComponent<Player>().thirst += enemy.thirstGift;
                if (enemy.gameObject.tag == "Boss") {
                    gameObject.GetComponent<Player>().thirst += enemy.thirstGift;
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector3(bossKnockback * 
                        (transform.lossyScale.x / Mathf.Abs(transform.lossyScale.x)) * 0.6f, bossKnockback/5, 0), ForceMode2D.Impulse);
                }
                else enemy.GetComponent<Rigidbody2D>().AddForce(new Vector3(knockback * 
                    (transform.lossyScale.x / Mathf.Abs(transform.lossyScale.x)) * 0.6f, knockback, 0), ForceMode2D.Impulse);
            }
        }
    }

    private void melee() {
        damage(meleeDamage, meleePoint.position, meleeRange, meleeKnockback);
    }


    // In editor:
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
    }
}

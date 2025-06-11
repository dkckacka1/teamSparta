using System;
using System.Collections;
using System.Collections.Generic;
using RocketdanGamesProject.Battle;
using RocketdanGamesProject.Core;
using RocketdanGamesProject.UI;
using UnityEngine;
using UnityEngine.Serialization;


namespace RocketdanGamesProject.Enemy
{
    // 몬스터 기본 클래스
    public abstract class Monster : MonoBehaviour, ITakeDamageable, IHitDamageable
    {
        protected Rigidbody Rb;
        protected Collider col;
        protected Animator animator;

        public float maxHp;
        public float currentHp;
        
        public float movementSpeed;
        public float climbForce;
        public Vector3 climbDir;
        public float maxClimbSpeed;

        public float hitDamage = 10f;
        public bool isAttacking = false;

        public Func<ITakeDamageable> GetTarget;
        
        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            animator = GetComponent<Animator>();
        }
        
        protected virtual void FixedUpdate()
        {
            Move();
            
            var v = Rb.velocity;
            v.y = Mathf.Clamp(v.y, -Mathf.Infinity, maxClimbSpeed);
            Rb.velocity = v;
        }

        protected virtual void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag(BattleManager.HeroTag))
            {
                animator.SetBool("IsAttacking", true);
                isAttacking = true;

                ITakeDamageable takeDamage;
                if ((takeDamage = other.gameObject.GetComponent<ITakeDamageable>()) != null)
                {
                    GetTarget = () => takeDamage;
                }
            }
        }

        protected virtual void OnCollisionStay(Collision other)
        {
            if (other.transform.CompareTag(BattleManager.MonsterTag) && !isAttacking)
            {
                if (other.contacts[0].normal.x > 0.5f)
                    // 좌측 충돌만 계산
                {
                    Rb.AddForce(climbDir * climbForce);
                }
            }
        }

        protected virtual void OnCollisionExit(Collision other)
        {
            if (other.transform.CompareTag(BattleManager.HeroTag))
            {
                animator.SetBool("IsAttacking", false);
                isAttacking = false;
            }
        }
        
        public void TakeDamage(float damage)
        {
            currentHp -= damage;
            UIManager.Instance.ShowBattleText(damage.ToString(), transform.position);

            if (currentHp <= 0)
            {
                Dead();
            }
        }

        public void Dead()
        {
            animator.SetBool("IsDead", true);
            BattleManager.Instance.RemoveMonster(this);
            Release();
        }

        public void Release()
        {
            Destroy(gameObject);
        }

        public abstract void Move();

        public abstract void Hit();
    }
}

using System;
using RocketdanGamesProject.Battle;
using RocketdanGamesProject.Core;
using RocketdanGamesProject.Core.ObjectPool;
using RocketdanGamesProject.UI;
using UnityEngine;

namespace RocketdanGamesProject.Enemy
{
    // 몬스터 기본 클래스
    public abstract class Monster : MonoBehaviour, ITakeDamageable, IPoolable
    {
        private static readonly int IsAttackAnimHash = Animator.StringToHash("IsAttacking");
        private static readonly int IsDeadAnimHash = Animator.StringToHash("IsDead");

        private Rigidbody _rb;
        private Animator _animator;

        public float maxHp;
        public float currentHp;

        public float movementSpeed;
        public float hitDamage = 10f;

        private bool _isAttacking;
        protected Func<ITakeDamageable> GetTarget;
        
        [SerializeField] private Transform battleTextOffsetTransform;
        
        [SerializeField] private float climbForce;
        [SerializeField] private Vector3 climbDir;
        [SerializeField] private float maxClimbSpeed;
        [SerializeField] private float pressureForce;

        protected virtual void FixedUpdate()
        {
            Move();

            var v = _rb.velocity;
            v.y = Mathf.Clamp(v.y, -Mathf.Infinity, maxClimbSpeed);
            _rb.velocity = v;
        }

        protected void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag(BattleManager.HeroTag))
            {
                _animator.SetBool(IsAttackAnimHash, true);
                _isAttacking = true;

                ITakeDamageable takeDamage;
                if ((takeDamage = other.gameObject.GetComponent<ITakeDamageable>()) != null)
                {
                    GetTarget = () => takeDamage;
                }
            }
        }

        protected void OnCollisionStay(Collision other)
        {
            if (other.transform.CompareTag(BattleManager.MonsterTag))
            {
                if (!_isAttacking)
                {
                    bool isCollisionMonsterClimbing = other.rigidbody.velocity.y > 0;
                    
                    if (other.contacts[0].normal.x > 0.5f && isCollisionMonsterClimbing is false)
                        // 좌측 충돌만 계산 && 대상 몬스터가 오르고 있지 않다면
                    {
                        // 공격중이 아닐때 좌측에 몬스터 충돌 시 오르기
                        _rb.AddForce(climbDir * climbForce);
                    }
                }
                
                if (other.contacts[0].normal.y < -0.9f)
                    // 위쪽 충돌 계산
                {
                    // 공격중이 아닐때 좌측에 몬스터 충돌 시 오르기
                    _rb.AddForce(Vector3.right * pressureForce);
                }
            }
        }

        protected void OnCollisionExit(Collision other)
        {
            if (other.transform.CompareTag(BattleManager.HeroTag))
            {
                _animator.SetBool(IsAttackAnimHash, false);
                _isAttacking = false;
            }
        }

        public void TakeDamage(float damage)
        {
            currentHp -= damage;
            UIManager.Instance.ShowBattleText(damage.ToString(), battleTextOffsetTransform.position);

            if (currentHp <= 0)
            {
                Dead();
            }
        }

        public void Dead()
        {
            _animator.SetBool(IsDeadAnimHash, true);
            BattleManager.Instance.RemoveMonster(this);

            var poolObject = GetComponent<PoolObject>();
            ObjectPoolManager.Instance.Release(poolObject.poolName, poolObject);
        }

        public void OnCreate()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        public void OnGet()
        {
            currentHp = maxHp;
        }

        public void OnRelease()
        {
            _animator.SetBool(IsAttackAnimHash, false);
            _animator.SetBool(IsDeadAnimHash, false);
        }
        
        public void SetSortingLayer(string getSortingLayerName)
        {
            foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.sortingLayerName = getSortingLayerName;
            }
        }

        protected virtual void Move()
        {
            var movementDir = Vector3.left;
            _rb.MovePosition(transform.position + movementDir * (movementSpeed * Time.deltaTime));
        }

        public abstract void Hit();


    }
}
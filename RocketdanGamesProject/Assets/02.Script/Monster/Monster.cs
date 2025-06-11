using System;
using System.Collections;
using System.Collections.Generic;
using RocketdanGamesProject.Core;
using UnityEngine;
using UnityEngine.Serialization;


namespace RocketdanGamesProject.Monster
{
    // 몬스터 기본 클래스
    public abstract class Monster : MonoBehaviour
    {
        protected Rigidbody Rb;
        protected Collider col;
        
        public float movementSpeed;
        public float climbForce;
        public Vector3 climbDir;
        public float maxClimbSpeed;

        public bool isAttacking = false;

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
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
            if (other.transform.CompareTag(GameManager.HeroTag))
            {
                isAttacking = true;
            }
        }

        protected virtual void OnCollisionStay(Collision other)
        {
            if (other.transform.CompareTag(GameManager.MonsterTag) && !isAttacking)
            {
                if (other.contacts[0].normal.x > 0.5f)
                {
                    Rb.AddForce(climbDir * climbForce);
                }
            }
        }

        protected virtual void OnCollisionExit(Collision other)
        {
            if (other.transform.CompareTag(GameManager.HeroTag))
            {
                isAttacking = false;
            }
        }

        public abstract void Move();
    }
}

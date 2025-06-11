using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RocketdanGamesProject.Monster
{
    // 몬스터 기본 
    public abstract class Monster : MonoBehaviour
    {
        public float movementSpeed;

        public abstract void Move();
    }
}

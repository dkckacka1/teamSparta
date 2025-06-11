using System.Collections;
using System.Collections.Generic;
using RocketdanGamesProject.Battle;
using UnityEngine;

namespace RocketdanGamesProject.Player
{
    // 플레이어 박스 클래스
    public class Box : MonoBehaviour, ITakeDamageable
    {
        public void TakeDamage(float damage)
        {
            Debug.Log($"{damage} Hit");
        }

        public void Dead()
        {
            
        }
    }
}

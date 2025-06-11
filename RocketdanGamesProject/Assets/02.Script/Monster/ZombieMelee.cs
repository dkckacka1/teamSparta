using System;
using System.Collections;
using System.Collections.Generic;
using RocketdanGamesProject.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace RocketdanGamesProject.Monster
{
    // 근접 좀비 몬스터 클래스
    public class ZombieMelee : Monster
    {
        public override void Move()
        {
            var movementVector = Vector2.left;
            transform.Translate(movementVector * (movementSpeed * Time.deltaTime));
        }


    }
}
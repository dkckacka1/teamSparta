using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketdanGamesProject.Player.Truck
{
    public class Truck : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(this.transform.position + Vector3.right * (movementSpeed * Time.deltaTime));
        }
    }
}


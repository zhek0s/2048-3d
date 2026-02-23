using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Gameplay
{
    public class Cube : MonoBehaviour
    {
        public int Value { get; private set; }
        public bool IsLaunched { get; private set; }
        private Rigidbody rb;
        public event Action<Cube, Collision> OnCollisionEntered;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        void OnTriggerExit(Collider other)
        {
            IsLaunched = true;
        }

        public void Init(int value, Vector3 position, bool launched = false)
        {
            Value = value;
            IsLaunched = launched;

            transform.position = position;
            transform.rotation = Quaternion.identity;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            gameObject.SetActive(true);

            UpdateVisual();
        }

        public void Launch(float force)
        {
            IsLaunched = false;
            rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
        }

        public void Bounce(float upwardForce, Vector3 movingForce)
        {
            Vector3 randomDirection = new Vector3(Random.Range(-0.2f, 0.2f), 1f, Random.Range(-0.2f, 0.2f)).normalized;

            Vector3 force = randomDirection * upwardForce + movingForce;

            rb.AddForce(force, ForceMode.Impulse);
        }

        public float GetRigidBodySpeed()
        {
            if (rb.isKinematic)
                return 0;
            else
                return rb.velocity.magnitude;
        }

        public void EnablePhysics() => SetPhysics(false);
        public void DisablePhysics() => SetPhysics(true);

        public void Deactivate()
        {
            OnCollisionEntered = null;
            gameObject.SetActive(false);
        }

        private void SetPhysics(bool state)
        {
            rb.isKinematic = state;
            GetComponent<BoxCollider>().enabled = !state;
        }

        private void UpdateVisual()
        {
            var cubeRenderer = GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", GetColorFor(Value));

            TextMeshPro[] texts = GetComponentsInChildren<TextMeshPro>();
            foreach (TextMeshPro textMeshPro in texts)
                textMeshPro.text = Value.ToString();
        }

        private Color GetColorFor(int number)
        {
            return number switch
            {
                2 => Color.red,
                4 => Color.green,
                8 => Color.blue,
                16 => Color.white,
                32 => Color.yellow,
                64 => Color.cyan,
                128 => Color.magenta,
                _ => Color.gray,
            };
        }
    }
}
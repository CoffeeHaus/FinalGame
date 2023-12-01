using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Enemy : MonoBehaviour
    {
        public float health;
        public float speed;
        public float attackPower;
        public Animator Animator;
        

        public void TakeDamage()
        {
            health -= 1;
            if (health <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            GameManager.Instance.addScore();
            Destroy(gameObject);
        }

    }

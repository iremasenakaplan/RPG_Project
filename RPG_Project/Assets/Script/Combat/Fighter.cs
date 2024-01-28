using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponRange;
        [SerializeField] float weaponDamage = 10f;

        Health targetObject;
        float timeSinceLastAttack;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (targetObject == null)
            {
                return;
            }

            if(targetObject.IsDead() == true)
            {
                GetComponent<Animator>().ResetTrigger("attack");
                Cancel();
                return;
            }

            if (GetIsInRange() == false)
            {
                GetComponent<Mover>().MoveTo(targetObject.transform.position);
            }
            else
            {
                AttackMethod();
                GetComponent<Mover>().Cancel();
            }
        }

        private void AttackMethod()
        {
            if(timeSinceLastAttack > timeBetweenAttacks )
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
               
            }

        }

        void Hit()
        {
            targetObject.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, targetObject.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            targetObject = target.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            targetObject = null;
        }
    }
}


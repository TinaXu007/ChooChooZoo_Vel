using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame
{
    public class AnimalWalk : MonoBehaviour
    {
        [SerializeField] private List<Transform> path = new List<Transform>();

        [SerializeField] private float walkingSpeed = 0.1f;

        [SerializeField] private float turningSpeed = 0.1f;


        private int pathIndex = 0;

        private bool isWalking = true;


        private void Update()
        {
            if (isWalking)
            {
                var walkingStep = walkingSpeed * Time.deltaTime;
                var turningStep = turningSpeed * Time.deltaTime;

                Vector3 facingDirection = path[pathIndex].position - transform.position;

                transform.position = Vector3.MoveTowards(transform.position, path[pathIndex].position, walkingStep);

                Quaternion rotation = Quaternion.LookRotation(facingDirection, Vector3.up);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turningSpeed);

                if (Vector3.Distance(transform.position, path[pathIndex].position) < 0.1)
                {
                    pathIndex = (pathIndex + 1) % path.Count;
                }
            }
        }

        public void SetWalking(bool walk)
        {
            isWalking = walk;
        }
    }
}



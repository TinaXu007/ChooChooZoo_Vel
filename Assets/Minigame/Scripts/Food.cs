using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* The trajectory of food from the current position to the animal */

namespace Minigame 
{
    public class Food : MonoBehaviour
    {
        public float moveSpeed = 0.7f;
        public float height = 2.8f;


        private Vector3 startPos;
        private Vector3 endPos;
        private float startTime;
        private float distance;
        private bool moving = false;

        void Start()
        {
            startPos = transform.position;
        }

        void Update()
        {
            if (moving)
            {
                this.MoveObject();
            }
        }


        void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.tag == "Animal")
            {
                Debug.Log("eat!");
                other.gameObject.GetComponent<AnimalParticle>().PlayParticle(1f);
                other.gameObject.GetComponent<AnimalAnimation>().SetState(AnimalAnimation.State.Eat);
                Destroy(this.gameObject);
            }
        }


        public void FeedToAnimal(GameObject target)
        {
            if (!moving)
            {
                endPos = target.transform.position;
                distance = Vector3.Distance(startPos,endPos);
                this.startTime = Time.time;
                moving = true;
            }
        }

        private void MoveObject()
        {
            float distCovered = (Time.time - startTime) * moveSpeed;

            transform.position = Parabola(startPos,endPos,height,distCovered);

        }



        private static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
        {
            Func<float,float> f = x => -4 * height * x * x + 4 * height * x;

            var mid = Vector3.Lerp(start,end,t);

            return new Vector3(mid.x,f(t) + Mathf.Lerp(start.y, end.y , t), mid.z);
        }
    }

}

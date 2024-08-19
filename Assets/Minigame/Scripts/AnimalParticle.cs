using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame
{
    public class AnimalParticle : MonoBehaviour
    {

        [SerializeField] private ParticleSystem ps;

        private float duration = 1f;


        public void PlayParticle(float time)
        {
            if (ps != null)
            {   
                if (ps.isEmitting)
                {
                    if (time > duration)
                    {
                        ps.Stop(false,ParticleSystemStopBehavior.StopEmittingAndClear);
                    }
                }

                if (time != duration)
                {
                    var main = ps.main;
                    main.duration = time;
                    duration = time;
                }

                ps.Play();
            }
        }

    }
}


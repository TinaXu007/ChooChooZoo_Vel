using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Minigame 
{
    public class FoodSpawner : MonoBehaviour
    {
        [SerializeField] private Transform CameraPosition;

        [SerializeField] private Vector2 SpawnVec;

        [SerializeField] float Spawn_distance = 6;

        [SerializeField] AudioClip clip;

        public List<GameObject> Foods;

        private Vector3 spawnPosition;

        private void Awake()
        {
            spawnPosition = new Vector3(CameraPosition.position.x + SpawnVec.x, CameraPosition.position.y + SpawnVec.y, CameraPosition.position.z + Spawn_distance) + CameraPosition.transform.forward;
        }


        public GameObject SpawnFood(string foodname, float throwingSpeed, float throwingHeight)
        {
            // scale volume
            float scaledVolume = AudioController.Instance.sfxVolumeSetter(0.3f);
            AudioManager.Instance.PlayAudioClip(clip,scaledVolume); // Lulu added scaled volume for global volume control

            // Instantiate food and initialie its parameter
            GameObject foodPrefab = Foods.Where(obj => obj.name == foodname).SingleOrDefault();
            GameObject food = Instantiate(foodPrefab,spawnPosition,foodPrefab.transform.rotation);
            food.name = foodname;
            food.tag = "Food";

            Food foodScript = food.AddComponent<Food>();
            foodScript.moveSpeed = throwingSpeed;
            foodScript.height = throwingHeight;
            food.GetComponent<Collider>().isTrigger = true;


            return food;
        }

    }

}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace Minigame {
//     public class TouchScreenInput : MonoBehaviour
//     {
//         public PlayerAction playerAction;
//         public ItemSelection itemSelector;
//         public GameObject[] fruits;
//         private Touch touch;
//         private Vector3[] fruitLocations;
//         private Vector2 pos; // Position of the touch
//         private Ray ray; // Raycast for the touch
//         private RaycastHit hit; // Raycast hit for the touch
//         private float width;
//         private float height;

//         // From PlayerAction
//         public GameObject TargetAnimal = null;

//         public FoodSpawner m_FoodSpawner = null;

//         public ItemSelection m_ItemSelection = null;

//         public HandAnimation m_HandAnimation = null;

//         [SerializeField] private float FoodThrowingSpeed = 0.7f;

//         [SerializeField] private float FoodThrowingHeight = 2.8f;

//         void Awake()
//         {
//             width = (float)Screen.width / 2.0f;
//             height = (float)Screen.height / 2.0f;
//         }

//         // Start is called before the first frame update
//         void Start()
//         {
//             fruitLocations = new Vector3[] {
//             fruits[0].transform.position, // Apple
//             fruits[1].transform.position, // Banana
//             fruits[2].transform.position, // Cherry
//             fruits[3].transform.position, // Lemon
//             fruits[4].transform.position, // WhiteHand
//             };
//         }

//         // Update is called once per frame
//         void Update()
//         {
//             if (Input.touchCount > 0)
//             {
//                 if (touch.phase == TouchPhase.Began)
//                 {
//                     ray = Camera.main.ScreenPointToRay(touch.position);
//                     if (Physics.Raycast(ray, out hit))
//                     {
//                         switch (hit.collider.gameObject.name)
//                         {
//                             case "Apple_1":
//                                 itemSelector.SetItem(0);
//                                 break;
//                             case "Banana":
//                                 itemSelector.SetItem(1);
//                                 break;
//                             case "Cherry":
//                                 itemSelector.SetItem(2);
//                                 break;
//                             case "Lemon":
//                                 itemSelector.SetItem(3);
//                                 break;
//                             case "WhiteHand":
//                                 itemSelector.SetItem(4);
//                                 break;
//                             case "Elephant2.0":
//                                 GameObject choosedItem = itemSelector.GetItem();
//                                 if (choosedItem != null)
//                                 {
//                                     if (choosedItem.tag == "Food")
//                                     {
//                                         GameObject ThrowedFood = m_FoodSpawner.SpawnFood(choosedItem.name, FoodThrowingSpeed, FoodThrowingHeight);
//                                         Transform target = TargetAnimal.transform.Find("FeedingZone");
//                                         if (target != null)
//                                         {
//                                             ThrowedFood.GetComponent<Food>().FeedToAnimal(target.gameObject);
//                                         }
//                                         else
//                                         {
//                                             ThrowedFood.GetComponent<Food>().FeedToAnimal(TargetAnimal);
//                                         }

//                                     }
//                                     else if (choosedItem.tag == "Hand")
//                                     {
//                                         if (m_HandAnimation.PlayAnimation(HandAnimation.State.Pet))
//                                         {
//                                             TargetAnimal.GetComponent<AnimalParticle>().PlayParticle(3f);
//                                             TargetAnimal.GetComponent<AnimalAnimation>().SetState(AnimalAnimation.State.Dance);
//                                         }
//                                     }
//                                 }
//                                 break;
//                             default:
//                                 break;
//                         }
//                     }
//                 }


//             }
//         }
//     }
// }

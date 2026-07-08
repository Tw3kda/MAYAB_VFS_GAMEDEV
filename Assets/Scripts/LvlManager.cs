using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class LvlManager : MonoBehaviour
{
    public GameObject gate1, gate2, gate3, gate4, gate5, spawner1, spawner2, spawner3, fx1, fx2, fx3, fx4, fx5;
    public static int roomsCleared = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
            switch (roomsCleared)
            {
                case 0:
                    gate1.SetActive(true);
                    gate2.SetActive(true);
                    fx1.SetActive(true);
                    fx2.SetActive(true);
                    spawner1.SetActive(true);
                    break;
                case 1:
                    gate2.SetActive(true);
                    gate3.SetActive(true);
                    fx2.SetActive (true);
                    fx3.SetActive(true);
                    spawner2.SetActive(true);
                    break;
                case 2:
                    gate4.SetActive(true);
                    gate5.SetActive(true);
                    fx4.SetActive(true);
                    fx5.SetActive(true);
                    spawner3.SetActive(true);
                    break;
            }
    }

    public void roomCleared()
    {
        roomsCleared++;
        gate1.SetActive(false);
        gate2.SetActive(false);
        gate3.SetActive(false);
        gate4.SetActive(false);
        gate5.SetActive(false);

        fx1.SetActive(false);
        fx2.SetActive(false);
        fx3.SetActive(false);
        fx4.SetActive(false);
        fx5.SetActive(false);
    }
}

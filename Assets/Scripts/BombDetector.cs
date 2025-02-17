using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDetector : MonoBehaviour
{
    [SerializeField] private AudioSource _detectorSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bomb")) {
            // Get distance
            Vector3 bombPosition = other.gameObject.transform.position;
            Vector3 playerPosition = gameObject.transform.position;

            float distance = Mathf.Sqrt(
                Mathf.Pow(bombPosition.x - playerPosition.x, 2f) +
                Mathf.Pow(bombPosition.y - playerPosition.y, 2f) +
                Mathf.Pow(bombPosition.z - playerPosition.z, 2f));

            // Debug.Log(distance);
            
            if (distance > 8f) {
                new WaitForSeconds(1f);
                Debug.Log("Play Sound");
            }
        }
    }
}

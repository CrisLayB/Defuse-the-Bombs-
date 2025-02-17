using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDetector : MonoBehaviour
{
    [SerializeField] private AudioClip _detectorSound;
    private AudioSource _detectorSource;
    private float nextPlayTime;
    // Start is called before the first frame update
    void Start()
    {
        _detectorSource = GetComponent<AudioSource>();
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

            float distance = Vector3.Distance(bombPosition, playerPosition);

            // Debug.Log(distance);
            
            if (distance > 8f) {
                if (Time.time >= nextPlayTime) {
                    Debug.Log("Play Sound");
                    _detectorSource.PlayOneShot(_detectorSound);
                    nextPlayTime += 0.25f;
                }
            }
        }
    }
}

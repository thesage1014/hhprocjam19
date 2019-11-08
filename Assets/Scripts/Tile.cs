using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    List<Transform> objects;
    List<Vector3> originalScales;
    List<Vector3> originalPoss;
    List<Quaternion> originalAngles;
    [SerializeField] AudioClip[] spawnAudio = null;

    // Start is called before the first frame update
    bool scaling = true;
    bool soundPlayed = false;
    public float scaleSpeed = .0001f;
    public float spawnSpeedMultiplier = 1f;
    public string tileType;
    public float rotRandomness = 360;
    public float posRandomness = .3f;
    public bool rot90Rand = true;
    public bool animatedSpawn = false;
    public bool respawnable = true;
    void Start() {
        playSound();
        if (animatedSpawn) {
            objects = new List<Transform>(GetComponentsInChildren<Transform>());
            originalScales = new List<Vector3>();
            originalPoss = new List<Vector3>();
            originalAngles = new List<Quaternion>();
            if (objects.Count != 0 && rot90Rand) {
                objects[0].transform.localEulerAngles += Vector3.up * (Random.Range(0, 3)) * 90;
            }
            for (int i = 0; i < objects.Count; i++) {
                Transform obj = objects[i];
                originalScales.Add(obj.transform.localScale);
                originalPoss.Add(obj.transform.localPosition);
                originalAngles.Add(obj.transform.localRotation);
                obj.transform.localScale = Vector3.zero;
                obj.transform.localPosition += new Vector3(Random.Range(-posRandomness, posRandomness), Random.Range(-posRandomness, posRandomness), Random.Range(-posRandomness, posRandomness));
                obj.transform.localEulerAngles += new Vector3(Random.Range(-rotRandomness, rotRandomness), Random.Range(-rotRandomness, rotRandomness), Random.Range(-rotRandomness, rotRandomness));
            }
        }
    }
    // Update is called once per frame
    void Update() {
        if (!animatedSpawn || objects[0].localScale == originalScales[0]) {
            scaling = false;
        } else {
            for (int i = 0; i < objects.Count; i++) {
                Transform obj = objects[i];
                obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, originalScales[i], scaleSpeed * spawnSpeedMultiplier * Time.deltaTime);
                obj.transform.localPosition = Vector3.Lerp(obj.transform.localPosition, originalPoss[i], scaleSpeed * spawnSpeedMultiplier * Time.deltaTime);
                obj.transform.localRotation = Quaternion.Lerp(obj.transform.localRotation, originalAngles[i], scaleSpeed * spawnSpeedMultiplier * Time.deltaTime);
                //obj.transform. originalAngles[i], scaleSpeed * Time.deltaTime);
            }
        }
    }
    void playSound() {
        if (!(spawnAudio is null) && spawnAudio.Length != 0) {
            var audioSrc = GetComponent<AudioSource>();
            audioSrc.clip = spawnAudio[Random.Range(0, spawnAudio.Length)];
            audioSrc.pitch += Random.Range(-.2f, .2f);
            audioSrc.PlayDelayed(Random.value * .5f);
            Destroy(audioSrc, audioSrc.clip.length * 1.3f);
            soundPlayed = true;
        }
    }
    public void beExplored(Vector2 agentOffset) {
        scaling = true;
        //print(agentOffset);
        float mag = agentOffset.magnitude - .27f;
        mag *= mag * mag;
        scaleSpeed = 1.5f / Mathf.Min(Mathf.Max(mag, .001f), 100); //1.5f
    }
}

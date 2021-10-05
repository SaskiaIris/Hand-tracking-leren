using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Gesture {
    public string name;
    public List<Vector3> fingerDatas;
    public UnityEvent onRecognized;
}

public class GestureDetection : MonoBehaviour
{
    public float threshold = 0.1f;
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    public bool debugMode = true;
    private List<OVRBone> fingerBones;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(DelayRoutine(2.5f, Initialize));
    }

    public IEnumerator DelayRoutine(float delay, Action actionToDo) {
        yield return new WaitForSeconds(delay);
        actionToDo.Invoke();
    }

    // Update is called once per frame
    void Update() {
        if(debugMode && Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("hoi");
            SaveGesture();
        }
    }

    void Initialize() {
        fingerBones = new List<OVRBone>(skeleton.Bones);
    }

    void SaveGesture() {
        Gesture newGesture = new Gesture();
        newGesture.name = "Gesture";
        List<Vector3> data = new List<Vector3>();
        foreach(var bone in fingerBones) {
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }

        newGesture.fingerDatas = data;
        gestures.Add(newGesture);
    }

    Gesture Recognize() {
        Gesture currentGesture = new Gesture();
        float currentMinDistance = Mathf.Infinity;

        foreach(var gesture in gestures) {
            float sumDistance = 0;
            bool isDiscarded = false;

            for(int i = 0; i < fingerBones.Count; i++) {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentData, gesture.fingerDatas[i]);
            }
        }
    }
}
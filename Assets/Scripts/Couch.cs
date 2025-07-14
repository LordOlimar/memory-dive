using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couch : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TriggerZone>().OnEnterEvent.AddListener(OnCouch);
    }

    public void OnCouch(GameObject go)
    {
        go.SetActive(false);
    }
}

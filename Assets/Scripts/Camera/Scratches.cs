using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Scratches : MonoBehaviour
{
    /// <summary>
    /// all the scratches that are possible to show.
    /// index 0 of the scratch is always on. 
    /// Meanwhile, the others have a chance of showing up for a frame, until disappearing.
    /// </summary>
    public MeshRenderer[] scratches;

    private float lastScratchUpdate;
    private float scratchUpdate = 0.1f; // 24 frames per second = every 0.41 seconds

    private void Start()
    {
        //Copy the materials
        Material[] mats = new Material[scratches.Length];

        for (int i = 0; i < mats.Length; i++)
        {
            mats[i] = new Material(scratches[i].material);
            scratches[i].material = mats[i];
        }
        
    }

    private void DisableAllTempScratches()
    {
        for (int i = 1; i < scratches.Length; i++)
        {
            scratches[i].gameObject.SetActive(false);
        }
    }
    

    private void Update()
    {
        if (!(lastScratchUpdate + scratchUpdate < Time.time)) return;
        
        DisableAllTempScratches();

        for (int i = 0; i < scratches.Length; i++)
        {
            var offset = scratches[i].material.mainTextureOffset;
            offset.x += 0.39f;
            scratches[i].material.mainTextureOffset = offset;
        }
            
        //33% chance of showing a random scratch
        if (Random.Range(0, 3) == 1)
        {
            // Choose a random scratch and enable it.
            scratches[Random.Range(1, scratches.Length)].gameObject.SetActive(true);// = true;
        }
            
        lastScratchUpdate = Time.time;
    }
}

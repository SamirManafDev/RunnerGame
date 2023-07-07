using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLine : MonoBehaviour
{
    [SerializeField] PlayableDirector playerTimeLine;

    public void PlayTimeLine()
    {
        playerTimeLine.Play();
    }

}

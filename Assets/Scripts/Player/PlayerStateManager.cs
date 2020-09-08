using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager
{
    private bool isCrouched;
    private bool diving;

    private float diveLength;
    private float time;

    public PlayerStateManager(){
        this.time = 0.0f;
        this.diveLength = 2;
    }

    public void isCrouching(){
        this.isCrouched = true;
    }

    public void isNotCrouching(){
        this.isCrouched = false;
    }

    public void isDiving(float time, float diveLength){
        this.time = time;
        this.diving = true;
        this.diveLength = 2;
    }

    public (bool, bool) activeSate(float currentTime){
        if(currentTime > this.time+this.diveLength){
            this.diving = false;
        }

        return (this.isCrouched, this.diving);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    private FMOD.Studio.EventInstance swing;
    private FMOD.Studio.EventInstance footstep;

    void PlayLightSwing()
    {
        swing = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/PlayerAttack");
        swing.setParameterByNameWithLabel("SwingType", "Quick");
        swing.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        swing.start();
        swing.release();
    }

    void PlayHeavySwing()
    {
        swing = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/PlayerAttack");
        swing.setParameterByNameWithLabel("SwingType", "Heavy");
        swing.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        swing.start();
        swing.release();
    }

    void PlayFootstep()
    {
        footstep = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Footsteps");
        //swing.setParameterByNameWithLabel("SwingType", "Heavy");
        //swing.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        footstep.start();
        footstep.release();
    }

    void PlayEnemyFootstep()
    {
        footstep = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/EnemyFootsteps");
        footstep.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        footstep.start();
        footstep.release();
    }
}

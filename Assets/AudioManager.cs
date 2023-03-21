using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    //Singleton
    public static AudioManager instance => m_instance;
    private static AudioManager m_instance;

    public UIReferences uiRefs;
    public ObjectReferences objectRefs;

    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;
    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PoolingObjectType{Money, Population, BergSmall, BergMed, BergLarge};


public class PoolManager : MonoBehaviour
{
    public static PoolManager instance => m_instance;
    private static PoolManager m_instance;
    
    public List<PoolInfo> listOfPool;

    private Vector3 defaultPos = new Vector3(-100, -100, -100);

    [SerializeField] GameObject prefab; //Population prefab
    [SerializeField] GameObject prefabGDP; //GDP perfab

    private Transform gdpInitialTransform;
    private Quaternion gdpInitialRotation;

    private Transform popInitialTransform;
    private Quaternion popInitialRotation;
    





    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;

        for (int i = 0; i < listOfPool.Count; i++)
        {
            FillPool(listOfPool[i]);
        }

        gdpInitialTransform = prefabGDP.transform;
        gdpInitialRotation = prefabGDP.transform.rotation;

        popInitialTransform = prefab.transform;
        popInitialRotation = prefab.transform.rotation;
    }

    void FillPool(PoolInfo info)
    {
        for(int i = 0; i < info.amount; i++)
        {
            GameObject objInstance = null;
            objInstance = Instantiate(info.prefab, info.container.transform);
            objInstance.gameObject.SetActive(false);
            objInstance.transform.position = defaultPos;
            info.pool.Add(objInstance);
        }
    }

    public GameObject GetPoolObject(PoolingObjectType type)
    {
        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;
        
        GameObject objInstance = null;
        if(pool.Count>0)
        {
            objInstance = pool[pool.Count-1];
            pool.Remove(objInstance);
        }
        else
        {
            objInstance = Instantiate(selected.prefab, selected.container.transform);
        }

        return objInstance;
    }

    public void CoolObject(GameObject obj, PoolingObjectType type)
    {
        obj.SetActive(false);
        obj.transform.position = popInitialTransform.position;
        obj.transform.rotation = popInitialRotation;

        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;

          if(obj.tag == "HumanModel" || obj.tag ==  "MoneyModel")
            pool.Add(obj);
            

          /*  else if(type == PoolingObjectType.Money && pool.Count < 500)
            {
               pool.Add(prefabGDP);
            }
    */


    }


    private PoolInfo GetPoolByType(PoolingObjectType type)
    {
        for(int i = 0; i < listOfPool.Count; i++)
        {
            if(type == listOfPool[i].type)
            {
                return listOfPool[i];
            }
        }

        return null;
    }
}

[System.Serializable]
public class PoolInfo
{
    public PoolingObjectType type; //Enum type
    public int amount; //Amount to pool
    public GameObject prefab;
    public GameObject container;
    public List<GameObject> pool = new List<GameObject>(); 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollingManager : MonoBehaviour
{
    public enum ePoolingObject
    {
        PlayerBullet,
        TestBullet,
        Laser,
        EnemyBullet,
        Meteor,
        MeteorObj,
        RotatingSphere,
        BigBullet,
    }

    [System.Serializable]
    public class cPoolingObject
    {
        public GameObject obj;
        public int count;
        [TextArea] public string description;
    }

    [SerializeField] private List<cPoolingObject> m_listPoolingObj;

    public static PollingManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
        initPoolingParents();
        initPoolingChild();
    }

    private void Start()
    {
        
    }

 

    private void initPoolingParents()
    {
        List<string> listParentName = new List<string>();

        int count = transform.childCount;
        for (int iNum = 0; iNum < count; ++iNum)
        {
            string name = transform.GetChild(iNum).name;
            listParentName.Add(name);
        }

        count = m_listPoolingObj.Count;
        for (int iNum = 0; iNum < count; ++iNum)
        {
            if (m_listPoolingObj[iNum].obj == null)
            {
                continue;
            }

            cPoolingObject data = m_listPoolingObj[iNum];

            string name = data.obj.name;
            bool exist = listParentName.Exists(x => x == name);
            if (exist == true)
            {
                listParentName.Remove(name);
            }
            else
            {
                GameObject objParent = new GameObject();
                objParent.transform.SetParent(transform);
                objParent.name = name;
            }
        }

        count = listParentName.Count;
        for (int iNum = count - 1; iNum > -1; --iNum)
        {
            GameObject obj = transform.Find(listParentName[iNum]).gameObject;
            Destroy(obj);
        }
    }

    private void initPoolingChild()
    {
        int count = m_listPoolingObj.Count;
        for (int iNum = 0; iNum < count; ++iNum)
        {
            if (m_listPoolingObj[iNum].obj == null)
            {
                continue;
            }

            cPoolingObject objPooing = m_listPoolingObj[iNum];
            GameObject obj = m_listPoolingObj[iNum].obj;
            string name = obj.name;
            Transform parent = transform.Find(name);

            int objCount = parent.childCount;
           
            for (int idNum = objCount - 1; idNum > -1; --idNum)
            {
                Destroy(transform.GetChild(idNum).gameObject);
            }

            if (objCount < objPooing.count)
            {
                int diffcount = objPooing.count - objCount;
                for (int icNum = 0; icNum < diffcount; ++icNum)
                {
                    GameObject cObj = createObject(name);
                    cObj.transform.SetParent(parent);
                }
            }
        }
    }

    private GameObject createObject(string _name)
    {
        GameObject obj = m_listPoolingObj.Find(x => x.obj.name == _name).obj;
        GameObject iobj = Instantiate(obj);
        iobj.SetActive(false);
        iobj.name = _name;
        return iobj;
    }

    public GameObject CreateObject(ePoolingObject _value, Transform _parent)
    {
        string findObjectName = _value.ToString().Replace("_", " ");
        return getPoolingObject(findObjectName, _parent);
    }

    public GameObject CreateObject(string _name, Transform _parent)
    {
        return getPoolingObject(_name, _parent);
    }

    private GameObject getPoolingObject(string _name, Transform _parent)
    {
        Transform parent = transform.Find(_name);

        if (parent == null)
        {
            Debug.LogError("프리팹에 오브젝트가 들어가 있지 않은것 같습니다.");
            return null;
        }

        GameObject returnValue = null;
        if (parent.childCount > 0)
        {
            returnValue = parent.GetChild(0).gameObject;
        }
        else
        {
            returnValue = createObject(_name);
        }
        returnValue.transform.SetParent(_parent);
        returnValue.SetActive(true);
        return returnValue;
    }

    public void RemovePoolingObject(GameObject _obj)
    {
        string name = _obj.name;
        Transform parent = transform.Find(name);
        
        cPoolingObject poolingObj = m_listPoolingObj.Find(x => x.obj.name == name);
        
        int poolingCount = poolingObj.count;

        if (parent.childCount < poolingCount)//부족했을때
        {
            _obj.transform.SetParent(parent);
            _obj.SetActive(false);
            _obj.transform.position = Vector3.zero;
        }
        else
        {
            Destroy(_obj);
        }
    }
}

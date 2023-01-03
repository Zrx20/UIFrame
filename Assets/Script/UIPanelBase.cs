using UnityEngine;
using System.Collections;

public delegate void GameObjectLoadedCallBack(GameObject obj);

/// <summary>
/// UI层级
/// </summary>
public enum MUILayerType
{
    Top,
    Upper,
    Normal,
    Hud
}

/// <summary>
/// 加载UI的方式
/// </summary>
public enum MUILoadType
{
    SyncLoad,
    AsyncLoad,
}

public abstract class MUIBase
{
    /// <summary>
    /// 是否加载完成的标志位
    /// </summary>
    protected bool m_isInited;

    /// <summary>
    /// UI名字
    /// </summary>
    protected string m_uiName;

    /// <summary>
    /// 在关闭的时候是否缓存UI 默认不缓存
    /// </summary>
    protected bool m_isCatchUI = false;

    /// <summary>
    /// UI的实例化GamObejct
    /// </summary>
    protected GameObject m_uiGameObject;

    /// <summary>
    /// 设置UI可见性状态
    /// </summary>
    protected bool m_active = false;

    /// <summary>
    /// 加载完成的回调
    /// </summary>
    protected GameObjectLoadedCallBack m_callBack;

    /// <summary>
    /// UI的资源全路径
    /// </summary>
    protected string m_uiFullPath = null;

    //UILayerType UI层类型
    protected MUILayerType m_uiLayerType;

    //UI的加载方式
    protected MUILoadType m_uiLoadType;

    public string UIName
    {
        get { return m_uiName; }
        set
        {
            m_uiName = value;
        }
    }

    public bool IsCatchUI
    {
        get { return m_isCatchUI; }
        set
        {
            m_isCatchUI = value;
        }
    }

    public GameObject UIGameObject
    {
        get { return m_uiGameObject; }
        set { m_uiGameObject = value; }
    }

    public bool Active
    {
        get { return m_active; }
        set
        {
            m_active = value;
            if (m_uiGameObject != null)
            {
                m_uiGameObject.SetActive(value);
                if (m_uiGameObject.activeSelf)
                {
                    OnActive();
                }
                else
                {
                    OnDeActive();
                }
            }
        }
    }

    public bool IsInited { get { return m_isInited; } }

    protected MUIBase(string uiName, MUILayerType layerType, MUILoadType loadType = MUILoadType.SyncLoad)
    {
        m_uiFullPath = uiName;
        m_uiLayerType = layerType;
        m_uiLoadType = loadType;
        Init();
    }

    public virtual void Init()
    {
        if (m_uiFullPath!=null)
        {
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>(m_uiFullPath));
            OnGameObjectLoaded(go);
        }
    }

    private void OnGameObjectLoaded(GameObject uiObj)
    {
        if (uiObj == null)
        {
            Debug.Log("UI加载失败了看看路径 ResPath: " + m_uiFullPath);
            return;
        }
        m_uiGameObject = uiObj;
        m_isInited = true;
        SetPanetByLayerType(m_uiLayerType);
        m_uiGameObject.transform.localPosition = Vector3.zero;
        m_uiGameObject.transform.localScale = Vector3.one;
    }

    public virtual void Uninit()
    {
        m_isInited = false;
        m_active = false;
    }

    protected abstract void OnActive();

    protected abstract void OnDeActive();

    public virtual void Update(float deltaTime)
    {

    }

    public virtual void LateUpdate(float deltaTime)
    {

    }

    public virtual void OnLogOut()
    {

    }

    protected void SetPanetByLayerType(MUILayerType layerType)
    {
        switch (m_uiLayerType)
        {
            case MUILayerType.Top:
                m_uiGameObject.transform.SetParent(UIManager.Ins.MTransTop);
                break;
            case MUILayerType.Upper:
                m_uiGameObject.transform.SetParent(UIManager.Ins.MTransUpper);
                break;
            case MUILayerType.Normal:
                m_uiGameObject.transform.SetParent(UIManager.Ins.MTransNormal);
                break;
            case MUILayerType.Hud:
                m_uiGameObject.transform.SetParent(UIManager.Ins.MTransHUD);
                break;
        }
    }
}
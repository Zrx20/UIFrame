using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogonController : MUIBase
{
    public GameObject BtnStart;
    public GameObject BtnExit;

    public LogonController() : base("LoginPanel", MUILayerType.Normal)
    {
        //Init();
    }

    public override void Init()
    {
        base.Init();
        //BtnStart = m_uiGameObject.transform.Find("BtnLogin").gameObject;
        //BtnExit = m_uiGameObject.transform.Find("BtnExit").gameObject;

        //BtnStart.GetComponent<Button>().onClick.AddListener(delegate
        //{
        //    Debug.Log("Start");
        //});

        //BtnExit.GetComponent<Button>().onClick.AddListener(delegate
        //{
        //    Debug.Log("Exit");
        //});
    }

    protected override void OnActive()
    {
        Debug.Log("Active");
    }

    protected override void OnDeActive()
    {
        Debug.Log("OnDeActive");
    }

}

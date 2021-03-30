using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour 
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        SystemController.GetInstance().Start();
    }

    /// <summary>
    /// 第一次添加组件调用一次
    /// </summary>
    private void Reset()
    {
        Debug.Log("Reset");
    }
    /// <summary>
    /// 在start方法调用前调用一次
    /// </summary>
    private void Awake()
    {
        Debug.Log("Awake");
        SystemController.GetInstance().Awake();
    }
    /// <summary>
    /// 每帧都回刷新
    /// </summary>
    private void FixedUpdate()
    {

        //Debug.Log("FixedUpdate");
        SystemController.GetInstance().FixedUpdate();
    }
    /// <summary>
    /// update执行之后，必定执行该方法
    /// </summary>
    private void LateUpdate()
    {

        //Debug.Log("LateUpdate");
        SystemController.GetInstance().LateUpdate();
    }
    /// <summary>
    /// 绘制时调用
    /// </summary>
    private void OnGUI()
    {

        //Debug.Log("OnGUI");
        SystemController.GetInstance().OnGUI();
    }
    /// <summary>
    /// 销毁调用
    /// </summary>
    private void OnDestroy()
    { 
        Debug.Log("OnDestroy");
        SystemController.GetInstance().Destory();
    }
    /// <summary>
    /// 启用时
    /// </summary>
    private void OnEnable()
    {

        Debug.Log("OnEnable");
        SystemController.GetInstance().OnEnable();
    }
    /// <summary>
    /// 禁用时
    /// </summary>
    private void OnDisable()
    {

        Debug.Log("OnDisable");
        SystemController.GetInstance().OnDisable();
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Reset");
        SystemController.GetInstance().Update();

    }

    
}

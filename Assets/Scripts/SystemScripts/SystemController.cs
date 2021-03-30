using UnityEngine;
using XLua;  
using System;
using UnityEngine.EventSystems;

/// <summary>
/// 系统控制器
/// </summary>
[LuaCallCSharp]
public class SystemController : UnityEngine.Object
{
    [System.Serializable]
    public class Injection
    {
        public string name;
        public GameObject value;
    }

    /// <summary>
    /// 单例对象
    /// </summary>
    private static SystemController m_instance;

    /// <summary>
    /// 锁对象
    /// </summary>
    private static readonly object m_locker = new object();

    /// <summary>
    /// 控制器状态
    /// </summary>
    private EnumConfig.SystemControllerStatusEnum mStatus = EnumConfig.SystemControllerStatusEnum.DEFAULT;

    /// <summary>
    /// lua内核对象
    /// </summary>
    XLua.LuaEnv mLuaenv;
    /// <summary>
    /// 
    /// </summary>
    public TextAsset mLuaScript;
    public Injection[] injections;
    internal float lastGCTime = 0;
    internal const float GCInterval = 1;//1 second


    private Action mLuaStart;
    private Action mLuaUpdate;
    private Action mLuaOnDestroy;
    private LuaTable mScriptEnv;








    ///////////////////////////////方法字段分割线


    /// <summary>
    /// 私用构造，防止外部实例话该类
    /// </summary>
    private SystemController()
    {

    }


    internal void FixedUpdate()
    {
    }


    internal void Awake()
    {
        SystemController.GetInstance().init();
    }


    internal void LateUpdate()
    {

    }

    internal void OnGUI()
    {

    }

    internal void OnEnable()
    {

    }

    internal void OnDisable()
    {

    }
    /// <summary>
    /// 帧刷新
    /// </summary>
    public void Update()
    {
        if (mLuaUpdate != null)
        {
            mLuaUpdate();
        }
        if (Time.time - lastGCTime > GCInterval)
        {
            mLuaenv.Tick();
            lastGCTime = Time.time;
        }
    }


    /// <summary>
    /// 静态函数 获取控制器实例
    /// </summary>
    /// <returns>返回控制器实例，如果实例对象未创建，自动创建实例对象</returns>
    public static SystemController GetInstance()
    {
        // lock确保单例唯一
        lock (m_locker)
        {
            if (m_instance == null)
            {
                m_instance = new SystemController();
            }
        }
        return m_instance;
    }

    /// <summary>
    /// 初始化控制器
    /// </summary>
    private void init() {
        mStatus = EnumConfig.SystemControllerStatusEnum.INIT;
        mLuaenv = new XLua.LuaEnv();
        mLuaenv.DoString("CS.UnityEngine.Debug.Log('Lua Env create successful.')");

        mScriptEnv = mLuaenv.NewTable();

        Utils.BindMetaTable(mLuaenv, mScriptEnv);

        mScriptEnv.Set("self", this);

        foreach(var injection in injections)
        {
            mScriptEnv.Set(injection.name, injection.value);
        }
        mLuaenv.DoString(mLuaScript.text, "LuaTestScript", mScriptEnv);

        Action awake = mScriptEnv.Get<Action>("awake");

        if (awake != null)
        {
            awake();
        }

        mScriptEnv.Get("onStart", out mLuaStart);
        mScriptEnv.Get("update", out mLuaUpdate);
        mScriptEnv.Get("onDestroy", out mLuaOnDestroy);
    }
    /// <summary>
    /// 启动控制器
    /// </summary>
    /// <returns>是否启动成功</returns>
    public bool Start()
    {
        mStatus = EnumConfig.SystemControllerStatusEnum.STARTING;
        if (mLuaStart != null)
        {
            mLuaStart();
        }
        return true;
    }
    /// <summary>
    /// 销毁控制器
    /// </summary>
    /// <returns>是否销毁成功</returns>
    public bool Destory() {
        if (mStatus == EnumConfig.SystemControllerStatusEnum.OVER) {
            Debug.LogError("system controller status is overed. destory faild.");
            return false;
        }
        mStatus = EnumConfig.SystemControllerStatusEnum.OVER;

        if (mLuaOnDestroy != null)
        {
            mLuaOnDestroy();
        }

        if (mLuaenv != null)
        {
            mLuaenv.Dispose();
        }
        mLuaStart = null;
        mLuaUpdate = null;
        mLuaOnDestroy = null;
        mLuaenv = null;
        mScriptEnv.Dispose();
        injections = null;
        return true;
    }
    /// <summary>
    /// 暂停游戏
    /// </summary>
    /// <returns>是否暂停成功</returns>
    public bool Pause()
    {
        mStatus = EnumConfig.SystemControllerStatusEnum.PAUSE;
        return true;
    }
    /// <summary>
    /// 获取控制器状态
    /// </summary>
    /// <returns></returns>
    public EnumConfig.SystemControllerStatusEnum GetStatus() {
        return mStatus;
    }

}

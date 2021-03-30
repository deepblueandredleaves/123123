using UnityEngine;
using System.Collections;
using XLua;

public static class Utils : object
{
    public static void BindMetaTable(XLua.LuaEnv env , XLua.LuaTable table) {
        if (env == null) {
            return;
        }
        if (table == null)
        {
            return;
        }
        LuaTable t = env.NewTable();
        t.Set("__index", env.Global);
        table.SetMetaTable(t);
        t.Dispose();
    }
}

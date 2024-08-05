using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NGameObjectExtensions
    {
        public static List<GameObject> GetChildren(this GameObject go) {
            var list = new List<GameObject>();
            return GetChildrenHelper (go, list);
        }

        private static List<GameObject> GetChildrenHelper(GameObject go, List<GameObject> list) {
            if (go == null || go.transform.childCount == 0) {
                return list;
            }
            foreach (Transform t in go.transform) {
                list.Add (t.gameObject);
                GetChildrenHelper (t.gameObject, list);
            }
            return list;
        }

        public static void DestroyFirst(this List<GameObject> objs)
        {
            if (objs.Count == 0) return;
            Object.Destroy(objs.First());
        }

        public static void DestroyLast(this List<GameObject> objs)
        {
            if (objs.Count == 0) return;
            Object.Destroy(objs.Last());
        }
        
        public static T OrNull<T> (this T obj) where T : Object => obj ? obj : null;
    }
}
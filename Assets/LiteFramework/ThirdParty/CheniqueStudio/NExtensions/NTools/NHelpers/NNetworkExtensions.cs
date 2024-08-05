using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NNetworkExtensions
    {
        public static IEnumerator CheckInternetConnection(Action<bool> action)
        {
            var request = new UnityWebRequest("https://google.com");
            yield return request.SendWebRequest();
            if (request.error != null)
            {
                Debug.Log("Error");
                action(false);
            }
            else
            {
                Debug.Log("Success");
                action(true);
            }
        }

        public static async Task<bool> CheckInternetConnectionAsync()
        {
            var request = new UnityWebRequest("https://google.com");
            request.SendWebRequest();
            while (!request.isDone) await Task.Yield();

            if (request.error != null)
            {
                Debug.Log("Error");
                return false;
            }

            Debug.Log("Success");
            return true;
        }
    }
}
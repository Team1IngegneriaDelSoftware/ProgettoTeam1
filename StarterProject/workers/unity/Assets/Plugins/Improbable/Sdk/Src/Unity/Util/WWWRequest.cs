// Copyright (c) Improbable Worlds Ltd, All Rights Reserved
// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Improbable.Unity.Util
{
    public class WWWRequest : IWWWRequest
    {
        /// <inheritdoc />
        public void SendGetRequest(MonoBehaviour coroutineHost, string url, Action<WWWResponse> callback)
        {
            coroutineHost.StartCoroutine(SendGetRequestCoroutine(url, callback));
        }

        private IEnumerator SendGetRequestCoroutine(string url, Action<WWWResponse> callback)
        {
            var www = new WWW(url);
            yield return www;
            var wwwResponse = WWWResponse.CreateFromWWW(www);
            callback(wwwResponse);
        }

        /// <inheritdoc />
        public void SendPostRequest(MonoBehaviour coroutineHost, string url, byte[] postData, Dictionary<string, string> headers, Action<WWWResponse> callback)
        {
            coroutineHost.StartCoroutine(SendPostRequestCoroutine(url, postData, headers, callback));
        }

        private IEnumerator SendPostRequestCoroutine(string url, byte[] postData, Dictionary<string, string> headers, Action<WWWResponse> callback)
        {
            var www = new WWW(url, postData, headers);
            yield return www;
            var wwwResponse = WWWResponse.CreateFromWWW(www);
            callback(wwwResponse);
        }
    }
}

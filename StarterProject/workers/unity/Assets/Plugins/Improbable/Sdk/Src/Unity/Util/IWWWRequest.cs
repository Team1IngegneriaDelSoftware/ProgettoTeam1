// Copyright (c) Improbable Worlds Ltd, All Rights Reserved
// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unit test wrapper for WWW requests.
/// </summary>
namespace Improbable.Unity.Util
{
    internal interface IWWWRequest
    {
        /// <summary>
        /// Send a GET request.
        /// </summary>
        void SendGetRequest(MonoBehaviour coroutineHost, string url, Action<WWWResponse> callback);

        /// <summary>
        /// Send a POST request.
        /// </summary>
        void SendPostRequest(MonoBehaviour coroutineHost, string url, byte[] postData, Dictionary<string, string> headers, Action<WWWResponse> callback);
    }

    /// <summary>
    /// Unit test wrapper for WWW response object.
    /// </summary>
    public class WWWResponse
    {
        public byte[] bytes { get; set; }
        public string error { get; set; }
        public int bytesDownloaded { get; set; }
        public string url { get; set; }
        public string text { get; set; }
        public AssetBundle assetBundle { get; set; }
        public Dictionary<string, string> responseHeaders { get; set; }

        public static WWWResponse CreateFromWWW(WWW www)
        {
            return new WWWResponse
            {
                bytes = www.bytes,
                error = www.error,
                bytesDownloaded = www.bytesDownloaded,
                url = www.url,
                text = www.text,
                assetBundle = www.assetBundle,
                responseHeaders = www.responseHeaders
            };
        }
    }
}

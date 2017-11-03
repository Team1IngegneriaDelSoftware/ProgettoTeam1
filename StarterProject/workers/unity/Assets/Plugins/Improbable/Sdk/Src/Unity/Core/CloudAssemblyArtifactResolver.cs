// Copyright (c) Improbable Worlds Ltd, All Rights Reserved
// ===========
// DO NOT EDIT - this file is automatically regenerated.
// =========== 
using Improbable.Unity.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Improbable.Unity.Core
{
    internal static class CloudAssemblyArtifactResolver
    {
        public static void ResolveAssetUrls(MonoBehaviour coroutineHost, IWWWRequest wwwRequest, string infraServiceUrl, string projectName, string assemblyName, Action<Dictionary<string, string>> onAssetsResolved, Action<Exception> onFailed)
        {
            var taskRunner = new TaskRunnerWithExponentialBackoff<WWWResponse>();
            Action runTask = () =>
            {
                var headers = new Dictionary<string, string>
                {
                    { "Accept", "application/json" }
                };
                var url = string.Format("{0}/assembly_content/v3/{1}/{2}/artifacts", infraServiceUrl, projectName, assemblyName);
                wwwRequest.SendPostRequest(coroutineHost, url, null, headers, taskRunner.ProcessResult);
            };
            Func<WWWResponse, TaskResult> evaluationFunc = response =>
            {
                return new TaskResult
                {
                    IsSuccess = string.IsNullOrEmpty(response.error),
                    ErrorMessage = response.error
                };
            };
            Action<WWWResponse> onSuccess = response =>
            {
                var assetUrls = new Dictionary<string, string>();
                var jsonResponse = JsonUtility.FromJson<AssemblyResponse>(response.text);
                if (jsonResponse.Artifacts.Count == 0)
                {
                    onFailed(new Exception(string.Format("No artifacts found at {0}", response.url)));
                }
                else
                {
                    for (var i = 0; i < jsonResponse.Artifacts.Count; i++)
                    {
                        var artifact = jsonResponse.Artifacts[i];
                        assetUrls[artifact.ArtifactId.Name] = artifact.Url;
                    }
                    onAssetsResolved(assetUrls);
                }
            };
            Action<string> onFailure = (string errorMessage) =>
            {
                onFailed(new Exception("Failed to retrieve assembly list: " + errorMessage));
            };
            taskRunner.RunTaskWithRetries("CloudAssemblyArtifactResolver::ResolveAssetUrls", coroutineHost, runTask, evaluationFunc, onSuccess, onFailure);
        }
    }

    [Serializable]
    public class AssemblyResponse
    {
        [SerializeField]
#pragma warning disable 0649
        private List<AssemblyArtifact> artifacts;
#pragma warning restore 0649
        public List<AssemblyArtifact> Artifacts
        {
            get { return artifacts; }
            set { artifacts = value; }
        }
    }

    [Serializable]
    public class ArtifactId
    {
        [SerializeField]
#pragma warning disable 0649
        private string name;
#pragma warning restore 0649
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    [Serializable]
    public class AssemblyArtifact
    {
        [SerializeField]
#pragma warning disable 0649
        // ReSharper disable once InconsistentNaming
        private ArtifactId artifact_id;
#pragma warning restore 0649
        public ArtifactId ArtifactId
        {
            get { return artifact_id; }
            set { artifact_id = value; }
        }

        [SerializeField]
#pragma warning disable 0649
        private string url;
#pragma warning restore 0649
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
    }
}
﻿using UnityEngine;

namespace Osiris.Utilities.Logging
{
    [CreateAssetMenu(fileName = AssetMenu.UnityConsoleLoggerFileName,
                     menuName = AssetMenu.UnityConsoleLoggerPath)]
    public class UnityConsoleLogger : ScriptableObject, ILogger
    {
        [SerializeField] private bool _DisplayLogging;

        public bool DisplayLogging { get => _DisplayLogging; set => _DisplayLogging = value; }

        public virtual void Log(string message, GameObject sender = null, LogLevel logLevel = LogLevel.Info)
        {
            Log(message, sender.name, logLevel);
        }

        public void Log(string message, string senderName = null, LogLevel logLevel = LogLevel.Info)
        {
#if UNITY_EDITOR
            if (!DisplayLogging & logLevel == LogLevel.Info)
            {
                return;
            }

            switch (logLevel)
            {
                case LogLevel.Error:
                    Debug.LogError(PrefixMessage(message, senderName));
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(PrefixMessage(message, senderName));
                    break;
                default:
                    Debug.Log(PrefixMessage(message, senderName));
                    break;
            }
#endif
        }

        protected string PrefixMessage(string message, string prefix = null)
        {
            if (prefix != null)
            {
                return $"[{prefix}] " + message;
            }
            return message;
        }
    }
}
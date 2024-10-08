using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LiteFramework.Runtime.Utils
{
    public static class TemplateHelper
    {
        public static string GetTemplate(string templateName, Dictionary<string, string> arguments)
        {
            var filePath = $"Templates/{templateName}";
            var textAsset = Resources.Load<TextAsset>(filePath);
            var template = textAsset.text;
            Resources.UnloadAsset(textAsset);
            return arguments.Aggregate(template, (current, argument) => current.Replace($"$[{argument.Key}]", argument.Value));
        }
    }
}
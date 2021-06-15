using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Device.Components.Core.Attributes;
using Device.Components.Core.Enums;
using Device.Components.Core.Models.Common;
using Orion.Client.Controls;
using Orion.Client.Controls.ViewModels;

namespace Orion.Client.Helpers
{
    public static class ComponentsHelper
    {
        private static readonly Dictionary<ComponentType, string> CompModelType2ImageUri
            = new Dictionary<ComponentType, string>()
        {
            { ComponentType.Cpu, "../Resources/CPU.png"},
            { ComponentType.Motherboard, "../Resources/Motherboard.png"},
            { ComponentType.Gpu, "../Resources/GPU.png"},
            { ComponentType.PowerSupply, "../Resources/Power Supply.png"},
            { ComponentType.Ram, "../Resources/RAM.png"},
            { ComponentType.Disk, "../Resources/HDD.png"},
        };

        public static ComponentControl InitializeUniversalComponentControl<TModel>(bool isVirtual = false, params TModel[] models)
            where TModel : BaseComponentModel
        {
            var firstModel = models.First();
           // firstModel.ComponentImagePath = NormalizeName(firstModel.ComponentName);
            var imgUrl = GetImagePath(firstModel);
            var compName = GetName(firstModel);
            var dicCollection = new List<Dictionary<string, string>>();

            foreach (var model in models)
            {
                if (model == null) continue;

                var dic = new Dictionary<string, string>();

                foreach (var propertyInfo in model.GetType().GetProperties())
                {
                    var attribute = propertyInfo.AttributeOrNull<CharNameAttribute>();
                    if (attribute == null) continue;

                    var name = attribute.Name;

                    if (string.IsNullOrWhiteSpace(name))
                        name = NormalizeName(propertyInfo.Name);

                    var value = propertyInfo.GetValue(model).ToString();
                    if (string.IsNullOrWhiteSpace(value))
                        value = "Unknown";

                    dic.Add(name, value);
                }

                foreach (var item in model.Key2Value)
                {
                    var name = item.Key;

                    if (string.IsNullOrWhiteSpace(name)) continue;

                    name = NormalizeName(name);

                    var value = item.Value;
                    if (string.IsNullOrWhiteSpace(value)) continue;

                    dic.Add(name, value);
                }
                dicCollection.Add(dic);
            }

            var viewModel = new ComponentControlViewModel()
            {
                ComponentName = compName,
                ImageUri = imgUrl,
                ComponentItemsInfo = dicCollection,
                BlurRadius = isVirtual ? 2 : 0,
            };

            var control = ViewHelper.InitializeControl<ComponentControl, ComponentControlViewModel>(viewModel);
            return control;
        }

        private static string GetImagePath<TModel>(TModel model)
            where TModel: BaseComponentModel
        {
            if (!string.IsNullOrWhiteSpace(model.ComponentImagePath))
                return model.ComponentImagePath;

            return CompModelType2ImageUri[model.ComponentType];
        }

        private static string GetName<TModel>(TModel model)
            where TModel : BaseComponentModel
        {
            if (!string.IsNullOrWhiteSpace(model.ComponentName))
                return model.ComponentName;

            return model.ComponentType.ToString();
        }

        private static string NormalizeName(string name)
        {
            return $"{name}:";
        }

        private static TAttribute AttributeOrNull<TAttribute>(this MemberInfo type)
            where TAttribute : Attribute
        {
            return type.GetCustomAttribute<TAttribute>();
        }

        private static TAttribute Attribute<TAttribute>(this MemberInfo type)
            where TAttribute : Attribute
        {
            var attr = type.GetCustomAttribute<TAttribute>();
            if (attr == null)
                throw new Exception($"Can't find attribute: {typeof(TAttribute)} in type: {type}");

            return attr;
        }
    }
}

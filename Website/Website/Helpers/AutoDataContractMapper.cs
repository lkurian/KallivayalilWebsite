using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kallivayalil.Client;

namespace Website.Helpers
{
    public class AutoDataContractMapper
    {
        private readonly List<Type> primitiveTypes = new List<Type> {typeof(int?), typeof(decimal?), typeof(decimal), typeof(string), typeof(DateTime?), typeof(DateTime) };

        public IEnumerable MapList(IEnumerable source, IEnumerable destination, Type destinationType)
        {
            foreach (var srcObj in source)
            {
                var objects = destination as IList;
                var instance = Activator.CreateInstance(destinationType);
                Map(srcObj, instance);
                if (objects != null) objects.Add(instance);
            }
            return destination;
        }

        public void Map<T>(object source, T destination)
        {
            if (source == null)
            {
                return;
            }
            var sourcePropInfos = source.GetType().GetProperties().ToList();
            var destinationProperties = destination.GetType().GetProperties().ToList();

            foreach (var destinationProperty in destinationProperties)
            {
                var destinationType = destinationProperty.PropertyType;
                var destPropName = destinationProperty.Name;
                var srcPropInfo = sourcePropInfos.Find(p => p.Name.Equals(destPropName, StringComparison.CurrentCultureIgnoreCase));
                if (srcPropInfo == null) continue;

                var srcValue = srcPropInfo.GetValue(source, null);
                if (srcValue == null)
                {
                    continue;
                }
                if (typeof(IEnumerable).IsAssignableFrom(destinationType) && ((destinationType.IsGenericType || destinationType.BaseType.IsGenericType) && destinationType.BaseType != typeof(List<string>)))
                {
                    Type actualDestinationType;
                    Type genericType;
                    if (destinationType.BaseType.IsGenericType)
                    {
                        actualDestinationType = destinationType.BaseType.GetGenericArguments()[0];
                        genericType = destinationType;
                    }
                    else
                    {
                        actualDestinationType = destinationType.GetGenericArguments()[0];
                        genericType = typeof(List<>).MakeGenericType(actualDestinationType);
                    }
                    var destList = Activator.CreateInstance(genericType);
                    MapList((IEnumerable)srcValue, (IEnumerable)destList, actualDestinationType);
                    destinationProperty.SetValue(destination, destList, null);
                    continue;
                }
                if (typeof(IList).IsAssignableFrom(destinationType))
                {
                    var destList = (IList)Activator.CreateInstance(destinationType);
                    var srcValues = (IEnumerable)srcValue;
                    foreach (var value in srcValues)
                    {
                        destList.Add(value);
                    }
                    destinationProperty.SetValue(destination, destList, null);
                    continue;
                }
                if (IsNonPrimitiveType(destinationType))
                {
                    var instance = Activator.CreateInstance(destinationType);
                    destinationProperty.SetValue(destination, instance, null);
                    Map(srcValue, instance);
                    destinationProperty.SetValue(destination, instance, null);
                    continue;
                }
                if(typeof(LinkData).IsAssignableFrom(destinationType))
                {
                    var instance = (LinkData)Activator.CreateInstance(destinationType);
                    var propertyInfos = srcValue.GetType().GetProperties().ToList();
                    var idPropertyInfos = propertyInfos.FindAll(info => info.Name.Equals("Id"));
                    var sourceId = idPropertyInfos[0];

                    if(idPropertyInfos.Count == 2)
                    {
                        if (srcPropInfo.PropertyType.IsAssignableFrom(typeof(ReferenceDataEntity)))
                        {
                            sourceId = idPropertyInfos.Find(info => info.PropertyType.Equals(typeof (int)));
                        }
                    }

                    instance.Id = Convert.ToInt32(sourceId.GetValue(srcValue, null));
                    destinationProperty.SetValue(destination,instance, null);
                    continue;
                }
                if (destinationProperty.PropertyType.Name.Equals("Int32") && srcValue.GetType().Name.Equals("Int64"))
                {
                    srcValue = Convert.ToInt32(srcValue);
                }
                destinationProperty.SetValue(destination, srcValue, null);
            }
        }

        private bool IsNonPrimitiveType(Type destinationType)
        {
            return !(destinationType.IsValueType || primitiveTypes.Contains(destinationType));
        }

    }
}
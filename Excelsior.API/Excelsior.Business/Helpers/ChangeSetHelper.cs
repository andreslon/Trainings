using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Excelsior.Business.Helpers
{
    public class EntityChangeInfo
    {
        public string EntityName { get; set; }

        public string Operation { get; set; }

        public List<PropertyChangeInfo> Changes { get; set; }
    }

    public class PropertyChangeInfo
    {
        public string PropertyName { get; set; }

        public string OriginalValue { get; set; }

        public string CurrentValue { get; set; }
    }

    public static class ChangeSetHelper
    {
        public static List<EntityChangeInfo> CreateEntityChangeList(string entityName, string operation, string propertyName, string originalValue, string currentValue)
        {
            var entitiesChanges = new List<EntityChangeInfo>();
            entitiesChanges.Add(new EntityChangeInfo()
            {
                EntityName = entityName,
                Operation = operation,
                Changes = new List<PropertyChangeInfo>()
            });
            entitiesChanges[0].Changes.Add(new PropertyChangeInfo()
            {
                PropertyName = propertyName,
                OriginalValue = originalValue,
                CurrentValue = currentValue
            });
            return entitiesChanges;
        }

        public static string ToXML(this object obj)
        {
            try
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                StringBuilder sb = new StringBuilder();
                using (XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = false, NamespaceHandling = NamespaceHandling.OmitDuplicates }))
                {
                    new XmlSerializer(obj.GetType()).Serialize(writer, obj, ns);
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Serialization failed: {0}", ex.Message);
            }
            return null;
        }

        public static List<EntityChangeInfo> CreateEntityChangeList(string entityName, string operation, List<PropertyChangeInfo> changes)
        {
            var result = new List<EntityChangeInfo>();
            result.Add(new EntityChangeInfo()
            {
                EntityName = entityName,
                Operation = operation,
                Changes = changes
            });

            return result;
        }

        public static List<PropertyChangeInfo> GetPropertiesChangeInfo(object x, object y, string fields = null)
        {
            var properties = x.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var propertyChanges = new List<PropertyChangeInfo>();
            IEnumerable<PropertyInfo> filteredProperties = properties;
            if (!string.IsNullOrEmpty(fields))
            {
                var fieldList = fields.ToLower().Split(',');
                filteredProperties = properties.Where(p => fieldList.Contains(p.Name.ToLower()));
            }

            foreach (PropertyInfo property in filteredProperties)
            {
                if (property.Name == "EntityState")
                    continue;

                if (property.GetCustomAttributes(typeof(XmlIgnoreAttribute), true).Any())
                    continue;

                object valueOfX = property.GetValue(x, null);
                if (y == null)
                {
                    if (valueOfX != null)
                    {
                        string value = valueOfX.ToString();
                        if (!String.IsNullOrEmpty(value))
                        {
                            propertyChanges.Add(new PropertyChangeInfo()
                            {
                                CurrentValue = value,
                                OriginalValue = null,
                                PropertyName = property.Name
                            });
                        }
                    }
                }
                else
                {
                    int compareValue = 0;
                    object valueOfY = property.GetValue(y, null);

                    if (valueOfX == null && valueOfY == null)
                    {
                    }
                    else if (valueOfX != null && valueOfX is IComparable)
                    {
                        compareValue = (valueOfX as IComparable).CompareTo(valueOfY);
                    }
                    else if (valueOfY != null && valueOfY is IComparable)
                    {
                        compareValue = (valueOfY as IComparable).CompareTo(valueOfX);
                    }

                    if (compareValue != 0)
                    {
                        propertyChanges.Add(new PropertyChangeInfo()
                        {
                            CurrentValue = valueOfX != null ? valueOfX.ToString() : null,
                            OriginalValue = valueOfY != null ? valueOfY.ToString() : null,
                            PropertyName = property.Name
                        });

                    }
                }
            }

            return propertyChanges;
        }
    }
}

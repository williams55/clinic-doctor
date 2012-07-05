using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Xml;
using AppointmentSystem.Data.SqlClient;
using AppointmentSystem.Entities;

namespace Arch
{
    public static class GlobalUtilities
    {
        public static TList<T> SelectDistinctFromList<T>(TList<T> originalList, Enum[] distinctProperties) where T : IEntity, new()
        {
            Type instanceType = originalList.GetType().GetGenericArguments()[0].BaseType;

            PropertyInfo[] propertyColumns = new PropertyInfo[distinctProperties.Length];
            for (int i = 0; i < distinctProperties.Length; i++)
            {
                propertyColumns[i] = instanceType.GetProperty(distinctProperties[i].ToString());
            }

            TList<T> resultList = new TList<T>();
            StringCollection keys = new StringCollection();
            foreach (T instance in originalList)
            {
                string key = string.Empty;
                foreach (PropertyInfo pi in propertyColumns)
                {
                    key += pi.GetValue(instance, null) + "|";
                }
                if (!keys.Contains(key))
                {
                    keys.Add(key);
                    resultList.Add(instance);
                }
            }
            return resultList;
        }
        public static SqlNetTiersProvider CreateProvider(string xmlPath, string connectionName)
        {
            FileStream reader = new FileStream(xmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //Set up the filestream (READER) //
            XmlDocument xmlDocument = new XmlDocument();// Set up the XmlDocument (CompSpecs) //
            xmlDocument.Load(reader); //Load the data from the file into the XmlDocument (CompSpecs) //


            XmlNodeList nodeList = xmlDocument.GetElementsByTagName("Server"); // Create a list of the nodes in the xml file //
            string con = "";
            foreach (XmlNode node in nodeList)
            {
                if (node.Attributes["Name"].Value.Equals(connectionName))
                {
                    con = node.Attributes["ConnectionString"].Value;
                    break;
                }
            }
            SqlNetTiersProvider provider = new SqlNetTiersProvider();
            NameValueCollection collection = new NameValueCollection();
            collection.Add("UseStoredProcedure", "false");
            collection.Add("EnableEntityTracking", "true");
            collection.Add("EntityCreationalFactoryType", "Northwind.Entities.EntityFactory");
            collection.Add("EnableMethodAuthorization", "false");
            collection.Add("ConnectionString", con);
            collection.Add("ConnectionStringName", "MyDynamicConnectionString");
            collection.Add("ProviderInvariantName", "System.Data.SqlClient");
            provider.Initialize("DynamicSqlNetTiersProvider", collection);
            return provider;
        }
    }
}

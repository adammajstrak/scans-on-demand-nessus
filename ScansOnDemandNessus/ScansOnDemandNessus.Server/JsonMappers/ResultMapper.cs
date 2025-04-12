﻿using ScansOnDemandNessus.Server.DTO;
using System.Xml.Serialization;

namespace ScansOnDemandNessus.Server.JsonMappers
{
    public static class ResultMapper
    {
        public static NessusClientData_v2 MapResultResponse(this string xmlResponse)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(NessusClientData_v2));

            using (TextReader reader = new StringReader(xmlResponse))
            {
                return (NessusClientData_v2)serializer.Deserialize(reader);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    public enum ErrorOperation { Create, Read, Update, Delete }
   
    [DataContract]
    public class EntityOperationFault
    {
        [DataMember]
        public Type ErrorInputType { get; set; }
        [DataMember]
        public ErrorOperation ErrorOperation { get; set; }
        [DataMember]
        public int ErrorInputId { get; set; }
    }
}
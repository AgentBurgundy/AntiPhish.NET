using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace AntiPhish.NET.Exceptions
{
    [Serializable]
    public class ScanNotCompletedException : Exception
    {
        public string ResourceReferenceProperty { get; set; }

        #region Constructors

        public ScanNotCompletedException() { }

        public ScanNotCompletedException(string message) : base(message) { }

        public ScanNotCompletedException(string message, Exception inner) : base(message, inner) { }

        protected ScanNotCompletedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }

        #endregion

        #region Exception Implementation

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("ResourceReferenceProperty", ResourceReferenceProperty);
            base.GetObjectData(info, context);
        }

        #endregion        
    }
}

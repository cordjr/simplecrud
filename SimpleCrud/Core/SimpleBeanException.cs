using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleCrud.Core {
    public class SimpleBeanException :Exception {
        public SimpleBeanException():base() {
        }

        public SimpleBeanException(string message) : base(message) {
        }

        public SimpleBeanException(string message, Exception innerException) : base(message, innerException) {
        }

        protected SimpleBeanException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}                                       

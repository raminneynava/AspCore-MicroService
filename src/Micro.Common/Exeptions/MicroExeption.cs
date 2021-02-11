using System;
using System.Collections.Generic;
using System.Text;

namespace Micro.Common.Exeptions
{
    public class MicroExeption : Exception
    {
        public string Code { get; set; }

        public MicroExeption()
        {

        }

        public MicroExeption(string code)
        {
            this.Code = code;
        }

        public MicroExeption(string message, params object[] args) : this(string.Empty, message, args)
        {

        }
        public MicroExeption(string code, string message, params object[] args) : this(null, code, message, args)
        {

        }
        public MicroExeption(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {

        }
        public MicroExeption(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }

    }
}

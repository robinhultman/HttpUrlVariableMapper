using System;
using System.Collections;
using System.Linq;
using BizTalkComponents.Utils;

namespace BizTalkComponents.HttpUrlVariableMapper
{
    public partial class HttpUrlVariableMapper
    {
        public string Name { get { return "HttpUrlVariableMapper"; } }
        public string Version { get { return "1.0"; } }
        public string Description { get { return "Parses incomming url and writes values to context properties."; } }

        public void GetClassID(out Guid classID)
        {
            classID = Guid.Parse("8D7B2C66-9435-4CCC-B480-A8E81014CE13");
        }

        public void InitNew()
        {
            
        }

        public IEnumerator Validate(object projectSystem)
        {
            return ValidationHelper.Validate(this, false).ToArray().GetEnumerator();
        }

        public bool Validate(out string errorMessage)
        {
            var errors = ValidationHelper.Validate(this, true).ToArray();

            if (errors.Any())
            {
                errorMessage = string.Join(",", errors);

                return false;
            }

            errorMessage = string.Empty;

            return true;
        }

        public IntPtr Icon { get { return IntPtr.Zero; } }
    }
}
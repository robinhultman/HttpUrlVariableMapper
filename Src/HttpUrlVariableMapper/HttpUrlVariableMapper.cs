using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using BizTalkComponents.Utils;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;

namespace BizTalkComponents.HttpUrlVariableMapper
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("3F94C0CB-681B-4FFA-B05F-8B8E69D1BA36")]
    [ComponentCategory(CategoryTypes.CATID_Receiver)]
    public partial class HttpUrlVariableMapper : IComponent, IBaseComponent,
                                        IPersistPropertyBag, IComponentUI
    {
        [RequiredRuntime]
        public string VariablePropertyMapping { get; set; }
        [RequiredRuntime]
        public string  UrlTemplate { get; set; }

        private const string VariablePropertyMappingSchema = @"<xs:schema attributeFormDefault='unqualified' elementFormDefault='qualified' xmlns:xs='http://www.w3.org/2001/XMLSchema'>
  <xs:element name='BtsVariablePropertyMapping'>
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs='1' maxOccurs='unbounded' name='Variable'>
          <xs:complexType>
            <xs:attribute name='Name' type='xs:string' use='required' />
            <xs:attribute name='PropertyName' type='xs:string' use='required' />
            <xs:attribute name='PropertyNamespace' type='xs:string' use='required' />
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        private const string VariablePropertyMappingPropertyName = "VariablePropertyMapping";
        private const string UrlTemplatePropertyName = "UrlTemplate";

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            string errorMessage;
            if(!Validate(out errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            var doc = ParseVariableMappingDocument();
            var urlPattern = ParseUrlTemplate();
            string inboundUrl;

            if (
                !pInMsg.Context.TryRead(
                    new ContextProperty("http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties#To"),
                    out inboundUrl))
            {
                throw new InvalidOperationException("Could not find the inbound URL");
            }

            var match = Regex.Match(inboundUrl, urlPattern, RegexOptions.IgnoreCase);

            foreach (var variable in doc.Descendants("Variable"))
            {
                var group = match.Groups[variable.Attribute("Name").ToString()];
                pInMsg.Context.Write(variable.Attribute("PropertyName").ToString(),variable.Attribute("PropertyNamespace").ToString(),group.Value);
            }

            return pInMsg;
        }

        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            VariablePropertyMapping = PropertyBagHelper.ToStringOrDefault(PropertyBagHelper.ReadPropertyBag(propertyBag, VariablePropertyMappingPropertyName), string.Empty);
            UrlTemplate = PropertyBagHelper.ToStringOrDefault(PropertyBagHelper.ReadPropertyBag(propertyBag, UrlTemplatePropertyName), string.Empty);
        }

        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            PropertyBagHelper.WritePropertyBag(propertyBag, VariablePropertyMappingPropertyName, VariablePropertyMapping);
            PropertyBagHelper.WritePropertyBag(propertyBag, UrlTemplatePropertyName, UrlTemplate);
        }

        private string ParseUrlTemplate()
        {
            return UrlTemplate.Replace("?", "\\?").Replace("{", "(?<").Replace("}", @">\w+)");
        }

        private XDocument ParseVariableMappingDocument()
        {
            XDocument variablePropertyMappingDocument;

            try
            {
                variablePropertyMappingDocument = XDocument.Parse(VariablePropertyMapping);
            }
            catch (XmlException)
            {
                throw new ArgumentException("The specified VariablePropertyMapping xml is not valid");
            }

            var schemas = new XmlSchemaSet();
            schemas.Add("", XmlReader.Create(new StringReader(VariablePropertyMappingSchema)));

            var sb = new StringBuilder();
            var errors = false;

            variablePropertyMappingDocument.Validate(schemas, (o, e) =>
            {
                sb.AppendLine(e.Message);
                errors = true;
            });

            if (errors)
            {
                throw new ArgumentException("The specified VariablePropertyMapping xml is not valid " + sb);
            }

            return variablePropertyMappingDocument;
        }


    }
}

namespace BizTalk_Server_Project1
{
    using System;
    using System.Collections.Generic;
    using Microsoft.BizTalk.PipelineOM;
    using Microsoft.BizTalk.Component;
    using Microsoft.BizTalk.Component.Interop;
    
    
    public sealed class ReceivePipeline1 : Microsoft.BizTalk.PipelineOM.ReceivePipeline
    {
        
        private const string _strPipeline = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instanc"+
"e\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" MajorVersion=\"1\" MinorVersion=\"0\">  <Description /> "+
" <CategoryId>f66b9f5e-43ff-4f5f-ba46-885348ae1b4e</CategoryId>  <FriendlyName>Receive</FriendlyName>"+
"  <Stages>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"1\" Name=\"Decode\" minOccurs=\""+
"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0e4103-4cce-4536-83fa-4a5040674ad6\" />      <Component"+
"s>        <Component>          <Name>BizTalkComponents.HttpUrlVariableMapper.HttpUrlVariableMapper,B"+
"izTalkComponents.HttpUrlVariableMapper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ba668d9852c"+
"d0fd9</Name>          <ComponentName>HttpUrlVariableMapper</ComponentName>          <Description>Par"+
"ses incomming url and writes values to context properties.</Description>          <Version>1.0</Vers"+
"ion>          <Properties>            <Property Name=\"VariablePropertyMapping\">              <Value "+
"xsi:type=\"xsd:string\" />            </Property>            <Property Name=\"UrlTemplate\">            "+
"  <Value xsi:type=\"xsd:string\" />            </Property>          </Properties>          <CachedDisp"+
"layName>HttpUrlVariableMapper</CachedDisplayName>          <CachedIsManaged>true</CachedIsManaged>  "+
"      </Component>      </Components>    </Stage>    <Stage>      <PolicyFileStage _locAttrData=\"Nam"+
"e\" _locID=\"2\" Name=\"Disassemble\" minOccurs=\"0\" maxOccurs=\"-1\" execMethod=\"FirstMatch\" stageId=\"9d0e4"+
"105-4cce-4536-83fa-4a5040674ad6\" />      <Components />    </Stage>    <Stage>      <PolicyFileStage"+
" _locAttrData=\"Name\" _locID=\"3\" Name=\"Validate\" minOccurs=\"0\" maxOccurs=\"-1\" execMethod=\"All\" stageI"+
"d=\"9d0e410d-4cce-4536-83fa-4a5040674ad6\" />      <Components />    </Stage>    <Stage>      <PolicyF"+
"ileStage _locAttrData=\"Name\" _locID=\"4\" Name=\"ResolveParty\" minOccurs=\"0\" maxOccurs=\"-1\" execMethod="+
"\"All\" stageId=\"9d0e410e-4cce-4536-83fa-4a5040674ad6\" />      <Components />    </Stage>  </Stages></"+
"Document>";
        
        private const string _versionDependentGuid = "52563b40-affd-46c1-befa-9c08e718f074";
        
        public ReceivePipeline1()
        {
            Microsoft.BizTalk.PipelineOM.Stage stage = this.AddStage(new System.Guid("9d0e4103-4cce-4536-83fa-4a5040674ad6"), Microsoft.BizTalk.PipelineOM.ExecutionMode.all);
            IBaseComponent comp0 = Microsoft.BizTalk.PipelineOM.PipelineManager.CreateComponent("BizTalkComponents.HttpUrlVariableMapper.HttpUrlVariableMapper,BizTalkComponents.HttpUrlVariableMapper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ba668d9852cd0fd9");;
            if (comp0 is IPersistPropertyBag)
            {
                string comp0XmlProperties = "<?xml version=\"1.0\" encoding=\"utf-16\"?><PropertyBag xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-inst"+
"ance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  <Properties>    <Property Name=\"VariablePropert"+
"yMapping\">      <Value xsi:type=\"xsd:string\" />    </Property>    <Property Name=\"UrlTemplate\">     "+
" <Value xsi:type=\"xsd:string\" />    </Property>  </Properties></PropertyBag>";
                PropertyBag pb = PropertyBag.DeserializeFromXml(comp0XmlProperties);;
                ((IPersistPropertyBag)(comp0)).Load(pb, 0);
            }
            this.AddComponent(stage, comp0);
        }
        
        public override string XmlContent
        {
            get
            {
                return _strPipeline;
            }
        }
        
        public override System.Guid VersionDependentGuid
        {
            get
            {
                return new System.Guid(_versionDependentGuid);
            }
        }
    }
}

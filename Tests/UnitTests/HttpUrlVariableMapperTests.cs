using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Winterdom.BizTalk.PipelineTesting;
using BizTalkComponents.Utils;
using ContextProperty = BizTalkComponents.Utils.ContextProperty;

namespace BizTalkComponents.HttpUrlVariableMapper.Tests.UnitTests
{
    [TestClass]
    public class HttpUrlVariableMapperTests
    {
        [TestMethod]
        public void TestMapUrlVariableTest()
        {
            var pipeline = PipelineFactory.CreateEmptyReceivePipeline();

            string config = @"<BtsVariablePropertyMapping>
<Variable Name='customerId' PropertyName='CustomerId' PropertyNamespace='http://mypropertyschema' />
<Variable Name='Test' PropertyName='Test' PropertyNamespace='http://mypropertyschema' />
</BtsVariablePropertyMapping>";

            var component = new HttpUrlVariableMapper
            {
                UrlTemplate = "http://tempuri.org/Customer/{customerId}?test={test}",
                VariablePropertyMapping = config
            };

            pipeline.AddComponent(component, PipelineStage.Decode);

            var message = MessageHelper.Create("<test></test>");
            message.Context.Promote(new ContextProperty("http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties#To"), "http://tempuri.org/Customer/10?test=1");


            var output = pipeline.Execute(message);

            Assert.AreEqual(1, output.Count);
            
            Assert.AreEqual("10",output[0].Context.Read("CustomerId", "http://mypropertyschema"));
        }
    }
}

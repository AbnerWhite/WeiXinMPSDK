﻿using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Test.MessageHandlers
{
    public partial class MessageHandlersTest
    {
        #region 微信认证事件推送

        /// <summary>
        /// 微信认证事件测试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private CustomerMessageHandlers VerifyEventTest<T>(string xml, Event eventType)
            where T : RequestMessageEventBase
        {
            var messageHandlers = new CustomerMessageHandlers(XDocument.Parse(xml));
            Assert.IsNotNull(messageHandlers.RequestDocument);
            messageHandlers.Execute();
            Assert.IsNotNull(messageHandlers.ResponseMessage);

            var requestMessage = messageHandlers.RequestMessage as T;

            Assert.IsNotNull(requestMessage);
            Assert.AreEqual(Event.qualification_verify_success, requestMessage.Event);

            return messageHandlers;
        }


        [TestMethod]
        public void QualificationVerifySuccessTest()
        {
            var xml = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1442401156</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[qualification_verify_success]]></Event>
<ExpiredTime>1442401156</ExpiredTime>
</xml> ";
            var messageHandler = VerifyEventTest<RequestMessageEvent_QualificationVerifySuccess>(xml,Event.qualification_verify_success);
            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_QualificationVerifySuccess;
            Assert.AreEqual("2015-09-16 18:59:16", requestMessage.ExpiredTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual("success", messageHandler.TextResponseMessage);


        }



        #endregion

    }
}

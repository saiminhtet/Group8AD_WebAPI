using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;

namespace Group8AD_WebAPI.Controllers
{
    public class NotificationController : ApiController
    {
        //addNewReqNotification
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Notification/{empId}/{currReq}/addNoti")]
        public HttpResponseMessage AddNewReqNotification(int empId, int currReq)
        {
            NotificationVM noti = BusinessLogic.NotificationBL.AddNewReqNotification(empId, currReq);

            if (noti == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, noti);
        }

        //List<NotificationVM> GetNotifications(int empId)
        //tested
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Notifications/{empId}")]
        public HttpResponseMessage GetNotifications(int empId)
        {
            List<NotificationVM> noti = BusinessLogic.NotificationBL.GetNotifications(empId);

            if (noti == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, noti);
        }

        //NotificationVM NotifyManager(Notification n)
        //tested
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Notification/StoreManager")]
        public HttpResponseMessage NotifyManager(NotificationVM n)
        {
            NotificationVM notification = BusinessLogic.NotificationBL.NotifyManager(n);

            if (notification == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, notification);
        }

        //NotificationVM NotifySupervisor(Notification n)
        //tested
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Notification/Supervisor")]
        public HttpResponseMessage NotifySupervisor(NotificationVM n)
        {
            NotificationVM notification = BusinessLogic.NotificationBL.NotifySupervisor(n);

            if (notification == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, notification);
        }
    }
}
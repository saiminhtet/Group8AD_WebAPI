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
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Notification/AddNewReqNotification")]
        public HttpResponseMessage AddNewReqNotification(NotificationVM notiVM)
        {
            try
            {
                BusinessLogic.NotificationBL.AddNewReqNotification(notiVM.EmpId, notiVM.Request);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //List<NotificationVM> GetNotifications(int empId)
        [AcceptVerbs("GET")]
        [HttpGet]
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

        //AddLowStkNotification(int empId, Item i)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Notification/AddLowStkNotification")]
        public HttpResponseMessage AddLowStkNotification(NotificationVM notiVM)
        {
            try
            {
                BusinessLogic.NotificationBL.AddLowStkNotification(notiVM.EmpId,notiVM.Item);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //AddFulfillNotification(int empId, int repId)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Notification/AddFulfillNotification")]
        public HttpResponseMessage AddFulfillNotification(NotificationVM notiVM)
        {
            try
            {
                BusinessLogic.NotificationBL.AddFulfillNotification(notiVM.EmpId,notiVM.RepId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //AddAcptNotification(int repId)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Notification/AddFulfillNotification")]
        public HttpResponseMessage AddAcptNotification(int repId)
        {
            try
            {
                BusinessLogic.NotificationBL.AddAcptNotification(repId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //NotificationVM NotifyManager(Notification n)
        [AcceptVerbs("GET")]
        [HttpGet]
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
        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Notification/Supervisor")]
        public HttpResponseMessage NotifySupervisor(NotificationVM n)
        {
            NotificationVM notification = BusinessLogic.NotificationBL.NotifySupervisor(n);

            if (notification == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, notification);
        }

        //AdjApprNotification(int fromEmpId , int toEmpId, Notification n)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Notification/AdjApprNotification")]
        public HttpResponseMessage AdjApprNotification(NotificationVM notiVM)
        {
            try
            {
                BusinessLogic.NotificationBL.AdjApprNotification(notiVM.FromEmp,notiVM.ToEmp,notiVM);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
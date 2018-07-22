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
        public HttpResponseMessage AddNewReqNotification(int empId, RequestVM currReq)
        {
            try
            {
                BusinessLogic.NotificationBL.AddNewReqNotification(empId, currReq);
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
        [Route("api/Notification/{empId}")]
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
        public HttpResponseMessage AddLowStkNotification(int empId, Item item)
        {
            try
            {
                BusinessLogic.NotificationBL.AddLowStkNotification(empId,item);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //AddLowStkNotification
        //dummy
        //[AcceptVerbs("POST")]
        //[HttpPost]
        //[Route("api/Notification/AddLowStkNotification")]
        //public HttpResponseMessage AddLowStkNotification(int empId, Item i)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        //AddFulfillNotification(int empId, int repId)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Notification/AddFulfillNotification")]
        public HttpResponseMessage AddFulfillNotification(int empId, int repId)
        {
            try
            {
                BusinessLogic.NotificationBL.AddFulfillNotification(empId,repId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //AddFulfillNotification
        //dummy
        //[AcceptVerbs("POST")]
        //[HttpPost]
        //[Route("api/Notification/AddFulfillNotification")]
        //public HttpResponseMessage AddFulfillNotification(int empId, int repId)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        //AddAcptNotification(int repId)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Notification/AddAcptNotification")]
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

        //AddAcptNotification
        //dummy
        //[AcceptVerbs("POST")]
        //[HttpPost]
        //[Route("api/Notification/AddAcptNotification")]
        //public HttpResponseMessage AddAcptNotification(int repId)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

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
        //[AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Notification/AdjApprNotification")]
        public HttpResponseMessage AdjApprNotification(int fromEmpId, int toEmpId, NotificationVM n)
        {
            try
            {
                BusinessLogic.NotificationBL.AdjApprNotification(fromEmpId,toEmpId,n);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        //AdjApprNotification
        //dummy
        //[AcceptVerbs("POST")]
        //[HttpPost]
        //[Route("api/Notification/AdjApprNotification")]
        //public HttpResponseMessage AdjApprNotification(int fromEmpId, int toEmpId, NotificationVM n)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}
    }
}
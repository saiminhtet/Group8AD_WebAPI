using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Group8AD_WebAPI.BusinessLogic;
using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;

namespace Group8AD_WebAPI.Controllers
{
    public class NotificationController : ApiController
    {
        // add new notification
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Notification/add")]
        public HttpResponseMessage AddNewNotification(int fromEmpId, int toEmpId, string type, string content)
        {
            try
            {
                NotificationBL.AddNewNotification(fromEmpId, toEmpId, type, content);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //addNewReqNotification
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Notification/AddNewReqNotification")]
        public HttpResponseMessage AddNewReqNotification(string empId, RequestVM currReq)//int empId
        {
            int EmpId = Convert.ToInt16(empId);
            try
            {
                BusinessLogic.NotificationBL.AddNewReqNotification(EmpId, currReq);
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
        public HttpResponseMessage GetNotifications(string empId)
        {
            int EmpId = Convert.ToInt16(empId);
            List<NotificationVM> noti = BusinessLogic.NotificationBL.GetNotifications(EmpId);

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
        public HttpResponseMessage AddLowStkNotification(string empId, Item item)
        {
            int EmpId = Convert.ToInt16(empId);
            try
            {
                BusinessLogic.NotificationBL.AddLowStkNotification(EmpId,item);
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
        public HttpResponseMessage AddFulfillNotification(string empId, string repId)
        {
            int EmpId = Convert.ToInt16(empId);
            int RepId = Convert.ToInt16(repId);
            try
            {
                BusinessLogic.NotificationBL.AddFulfillNotification(EmpId,RepId);
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
        public HttpResponseMessage AddAcptNotification(string repId)
        {
            int RepId = Convert.ToInt16(repId);
            try
            {
                BusinessLogic.NotificationBL.AddAcptNotification(RepId);
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
        public HttpResponseMessage AdjApprNotification(string fromEmpId, string toEmpId, NotificationVM n)
        {
            int FromEmpId = Convert.ToInt16(fromEmpId);
            int ToEmpId = Convert.ToInt16(toEmpId);
            try
            {
                BusinessLogic.NotificationBL.AdjApprNotification(FromEmpId,ToEmpId,n);
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
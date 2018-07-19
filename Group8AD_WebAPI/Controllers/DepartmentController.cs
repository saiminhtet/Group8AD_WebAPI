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
    public class DepartmentController : ApiController
    {
        //removeDelegate by deptCode
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Department/removeDelegate/{deptCode}")]
        public HttpResponseMessage removeDelegate(string deptCode)
        {
            try
            {
                BusinessLogic.DepartmentBL.removeDelegate(deptCode);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //set Delegate by DepartmentCode , fromDate , toDate and empId
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Department/setDelegate")]
        public HttpResponseMessage setDelegate(SetDelegateVM delegateVM)
        {
            try
            {
                BusinessLogic.DepartmentBL.setDelegate(delegateVM.DeptCode, delegateVM.DelegateFromDate, delegateVM.DelegateToDate, delegateVM.EmpId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }            
        }

        //set Rep by DepartmentCode , fromEmpId and toEmpId
        //tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Department/setRep")]
        public HttpResponseMessage setRep(string deptCode, int empId)
        {
            try
            {
                BusinessLogic.DepartmentBL.setRep(deptCode,empId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //setCollPt(string deptCode, int collPt)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Department/setCollPt")]
        public HttpResponseMessage setCollPt(string deptCode, int collPt)
        {
            try
            {
                BusinessLogic.DepartmentBL.setCollPt(deptCode, collPt);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //GetCollPtList
        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Department/GetCollectionPoint")]
        public HttpResponseMessage GetCollPtList()
        {
            List<CollectionPointVM> departments = BusinessLogic.DepartmentBL.GetCollPtList();

            if (departments == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, departments);
        }

        //GetCollPt
        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Department/GetCollPt/{deptCode}")]
        public HttpResponseMessage GetCollPt(string deptCode)
        {
            CollectionPointVM colPt = BusinessLogic.DepartmentBL.GetCollPt(deptCode);

            if (colPt == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, colPt);
        }

        //List<string> GetDeptCodes()
        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Department/GetDeptCodes")]
        public HttpResponseMessage GetDeptCodes()
        {
            List<string> departments = BusinessLogic.DepartmentBL.GetDeptCodes();

            if (departments == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, departments);
        }

        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Department/GetDept")]
        public HttpResponseMessage GetDept(int empId)
        {
            DepartmentVM dep = BusinessLogic.DepartmentBL.GetDept(empId);
            if (dep == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, dep);
        }
    }
}
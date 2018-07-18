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
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Department/setRep")]
        public HttpResponseMessage setRep(SetRepVM repVM)
        {
            try
            {
                BusinessLogic.DepartmentBL.setRep(repVM.DeptCode, repVM.FromEmpId, repVM.ToEmpId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //removeRep(string deptCode, int fromEmpId)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Department/removeRep/{deptCode}")]
        public HttpResponseMessage removeRep(SetRepVM setRepvm)
        {
            try
            {
                BusinessLogic.DepartmentBL.removeRep(setRepvm.DeptCode, setRepvm.FromEmpId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "error");
            }
        }

        //setCollPt(string deptCode, int collPt)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Department/setCollPt/{deptCode}/{collPt}")]
        public HttpResponseMessage setCollPt(string deptCode, int collPt)
        {
            DepartmentVM department = BusinessLogic.DepartmentBL.setCollPt(deptCode, collPt);

            if (department == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, department);
        }

        //GetCollPtList
        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Department/GetCollectionPoint")]
        public HttpResponseMessage GetCollPtList()
        {
            List<string> departments = BusinessLogic.DepartmentBL.GetCollPtList();

            if (departments == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, departments);
        }

        //GetCollPt
        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Department/{deptCode}")]
        public HttpResponseMessage GetCollPt(string deptCode)
        {
            string colPt = BusinessLogic.DepartmentBL.GetCollPt(deptCode);

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
    }
}
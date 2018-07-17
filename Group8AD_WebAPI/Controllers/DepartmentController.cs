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
        //removeDelegate
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Department/{deptCode}/removeDelegate")]
        public HttpResponseMessage removeDelegate(string deptCode)
        {
            DepartmentVM department = BusinessLogic.DepartmentBL.removeDelegate(deptCode);

            if (department == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, department);
        }

        //set Delegate by DepartmentCode , fromDate , toDate and empId
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Department/{deptCode}/setDelegate")]
        public HttpResponseMessage setDelegate(string deptCode, DateTime fromDate, DateTime toDate, int empId)
        {
            DepartmentVM department = BusinessLogic.DepartmentBL.setDelegate(deptCode,fromDate,toDate,empId);

            if (department == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, department);
        }

        //set Rep by DepartmentCode , fromEmpId and toEmpId
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Department/{deptCode}/setRep")]
        public HttpResponseMessage setRep(string deptCode, int fromEmpId, int toEmpId)
        {
            DepartmentVM department = BusinessLogic.DepartmentBL.setRep(deptCode, fromEmpId, toEmpId);

            if (department == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, department);
        }

        //removeRep(string deptCode, int fromEmpId)
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Department/{deptCode}/removeRep")]
        public HttpResponseMessage removeRep(string deptCode, int fromEmpId)
        {
            DepartmentVM department = BusinessLogic.DepartmentBL.removeRep(deptCode, fromEmpId);

            if (department == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, department);
        }

        //setCollPt(string deptCode, int collPt)
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Department/{deptCode}/removeRep")]
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
        //tested
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
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
        //tested
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
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
        //tested
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
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
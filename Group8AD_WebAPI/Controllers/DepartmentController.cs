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

                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        //set Delegate by DepartmentCode , fromDate , toDate and empId
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Department/setDelegate")]
        //[Route("api/Department/setDelegate/{deptCode}/{fromDate}/{toDate}/{empId}")]
        public HttpResponseMessage setDelegate(string deptCode, string fromDate, string toDate, string empId)
        {
            DateTime fromdate = Convert.ToDateTime(fromDate);
            DateTime todate = Convert.ToDateTime(toDate);
            int EmpId = Convert.ToInt16(empId);
            try
            {
                BusinessLogic.DepartmentBL.setDelegate(deptCode,fromdate,todate,EmpId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }            
        }

        //set Rep by DepartmentCode , fromEmpId and toEmpId
        //tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Department/setRep")]
        public HttpResponseMessage setRep(string deptCode, string empId)//int empId
        {
            try
            {
                int emp = Convert.ToInt16(empId);
                BusinessLogic.DepartmentBL.setRep(deptCode,emp);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        //setCollPt(string deptCode, int collPt)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Department/setCollPt")]
        //[Route("api/Department/setCollPt/{deptCode}/{collPt}")]
        public HttpResponseMessage setCollPt(string deptCode, string collPt)//int collPt 
        {
            try
            {
                int collPoint = Convert.ToInt16(collPt);
                BusinessLogic.DepartmentBL.setCollPt(deptCode, collPoint);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
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
                return Request.CreateResponse(HttpStatusCode.BadRequest);
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
                return Request.CreateResponse(HttpStatusCode.BadRequest);
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
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, departments);
        }

        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Department/GetDept")]
        //[Route("api/Department/GetDept/{empId}")]
        public HttpResponseMessage GetDept(string empId)//int empId
        {
            int EmpId = Convert.ToInt16(empId);
            DepartmentVM dep = BusinessLogic.DepartmentBL.GetDept(EmpId);
            if (dep == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, dep);
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Department")]
        public HttpResponseMessage GetAllDept()
        {
            List<Department> departments = BusinessLogic.DepartmentBL.GetAllDept();

            if (departments == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, departments);
        }
    }
}
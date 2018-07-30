using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Group8AD_WebAPI.Controllers
{
 
    public class EmployeeController : ApiController
    {
        //Get All Employeelist
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Employee/")]
        public HttpResponseMessage GetEmployeebyList()
        {
            List<EmployeeVM> employee = BusinessLogic.EmployeeBL.GetAllEmp();


            if (employee == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Employee/{id}")]
        public HttpResponseMessage GetEmployeebyID(int id)
        {
            EmployeeVM employee = BusinessLogic.EmployeeBL.GetEmp(id);


            if (employee == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }


        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Employee/{id}/DepartmentCode")]
        public HttpResponseMessage GetDepartmentCode(int id)
        {
          
            string DeptCode = BusinessLogic.EmployeeBL.GetDeptCode(id);

            if (DeptCode == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, DeptCode);
        }


        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Employee/{id}/DepartmentHead")]
        public HttpResponseMessage GetDepartmentHead(int id)
        {
            string DeptHead = BusinessLogic.EmployeeBL.GetHeadId(id);

            if (DeptHead == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, DeptHead);
        }


        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Employee/{id}/Role")]
        public HttpResponseMessage GetRole(int id)
        {
            string role = BusinessLogic.EmployeeBL.GetRole(id);

            if (role == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, role);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Employee/GetEmp")]
        public HttpResponseMessage GetEmp(string dCode, string name)
        {
             List<EmployeeVM> emplist = BusinessLogic.EmployeeBL.GetEmp(dCode, name);

            if (emplist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, emplist);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Employee/GetEmpReq")]
        public HttpResponseMessage GetEmpReq()
        {
            List<EmployeeVM> emplist = BusinessLogic.EmployeeBL.GetEmpReq();

            if (emplist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, emplist);
        }
    }
}

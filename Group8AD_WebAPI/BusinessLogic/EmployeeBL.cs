using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.BusinessLogic
{
    public static class EmployeeBL
    {

        //get employee by employeeid
        public static EmployeeVM GetEmp(int empId)
        {
            EmployeeVM employee = new EmployeeVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                
                employee = entities.Employees.Where(e => e.EmpId == empId).Select(e => new EmployeeVM()
                {
                    EmpId = e.EmpId,
                    EmpName = e.EmpName,
                    DeptCode = e.DeptCode,
                    EmpAddr = e.EmpAddr,
                    EmpCtcNo = e.EmpCtcNo,
                    EmpEmail = e.EmpEmail,
                    Role = e.Role
                }).First<EmployeeVM>();
            }
            return employee;
        }


        //get department code by employeeid 
        public static string GetDeptCode(int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string deptcode = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.DeptCode).First();
                return deptcode;
            }
        }

        //need to clearify return type and model
        //public static string GetHeadId(int empId)
        //{
        //    using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
        //    {
        //        var DeptH = from dept in entities.Employee.Where(e => e.EmpId == empId).Select(e => e.).ToString();
        //        return employee;
        //    }
        //}



        //get employee role by employeeid
        public static string GetRole(int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var role = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.Role).ToString();
                return role;
            }
        }

        //get all employee list
        public static List<EmployeeVM> GetAllEmp()
        {
            List<EmployeeVM> emplists = new List<EmployeeVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
              
                emplists = entities.Employees.Select(e => new EmployeeVM()
                {
                    EmpId = e.EmpId,
                    EmpName = e.EmpName,
                    DeptCode = e.DeptCode,
                    EmpAddr = e.EmpAddr,
                    EmpCtcNo = e.EmpCtcNo,
                    EmpEmail = e.EmpEmail,
                    Role = e.Role
                }).ToList<EmployeeVM>();
               
            }
            return emplists;
        }
    }
}
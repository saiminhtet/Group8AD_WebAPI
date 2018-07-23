﻿using Group8AD_WebAPI.Models;
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
        public static string GetHeadId(int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string deptCode = GetDeptCode(empId);
                string DeptHeadId = entities.Departments.Where(x => x.DeptCode == deptCode).Select(x => x.DeptHeadId).First().ToString();
                return DeptHeadId;
            }
        }



        //get employee role by employeeid
        public static string GetRole(int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var role = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.Role).First();
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

        //Get Employee 
        public static List<EmployeeVM> GetEmplistsbyDeptCode(string deptCode)
        {
            List<EmployeeVM> empvmList = new List<EmployeeVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Employee> empList = entities.Employees.Where(e => e.DeptCode.Equals(deptCode)).ToList();

                empvmList = Utility.EmployeeUtility.Convert_Employee_To_EmployeeVM(empList);
            }

            return empvmList;
        }


        public static List<EmployeeVM> GetEmp(string dCode, string name)
        {
            List<EmployeeVM> empvmList = new List<EmployeeVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                if (dCode != null && name == null)
                {
                     
                }
                else if (name != null && dCode == null)
                {

                }
                else if (name != null && dCode != null)
                {

                }
                else
                    return empvmList;
            }
            return empvmList;
        }
    }
}
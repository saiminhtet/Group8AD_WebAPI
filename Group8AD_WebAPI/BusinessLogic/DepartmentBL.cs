using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8AD_WebAPI.Models;

namespace Group8AD_WebAPI.BusinessLogic
{
    public static class DepartmentBL
    {
        //remove Delegate by DepartmentCode
        public static void removeDelegate(string deptCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                //var department = entities.Department.FirstOrDefault(d => d.DeptCode == deptCode);
                var department = entities.Departments.Where(d => d.DeptCode == deptCode).FirstOrDefault();
                if (department != null)
                {
                    entities.Departments.Remove(department);
                    entities.SaveChanges();
                }

            }
        }

        //set Delegate by DepartmentCode , fromDate , toDate and empId
        public static void setDelegate(string deptCode, DateTime fromDate, DateTime toDate, int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var department = entities.Departments.Where(d => d.DeptCode == deptCode && d.DelegateFromDate == fromDate && d.DelegateToDate == toDate && d.DeptRepId == empId).FirstOrDefault();
                if (department != null)
                {
                    department.DeptCode = deptCode;
                    department.DelegateFromDate = fromDate;
                    department.DelegateToDate = toDate;
                    department.Employee.EmpId = empId;
                    entities.SaveChanges();
                }

            }
        }

        //set Rep by DepartmentCode , fromEmpId and toEmpId
        public static void setRep(string deptCode, int fromEmpId, int toEmpId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var department = entities.Departments.Where(d => d.DeptCode == deptCode && d.DeptRepId == fromEmpId && d.DeptRepId == toEmpId).FirstOrDefault();
                if (department != null)
                {
                    department.DeptCode = deptCode;
                    department.Employee1.EmpId = fromEmpId;
                    department.Employee2.EmpId = toEmpId;
                    entities.SaveChanges();
                }
            }
        }

        //remove Rep by deptCode and fromEmpId 
        public static void removeRep(string deptCode, int fromEmpId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var department = entities.Departments.Where(d => d.DeptCode == deptCode && d.DeptRepId == fromEmpId).FirstOrDefault();
                if (department != null)
                {
                    entities.Departments.Remove(department);
                    entities.SaveChanges();
                }

            }
        }

        public static List<string> GetCollPtList()
        {
            List<string> collList = new List<string>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var collPtList = entities.Departments.Select(d => d.ColPtId);
                foreach (var v in collPtList)
                {
                    collList.Add(v.ToString());
                }
            }
            return collList;
        }


        //get CollPt by DepartmentCode 
        public static string GetCollPt(string deptCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var collPt = entities.Departments.Where(d => d.DeptCode == deptCode).Select(d => d.ColPtId).ToString();
                return collPt;
            }
        }

        //set CollPt by DepartmentCode , collPt
        public static void setCollPt(string deptCode, int collPt)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var collPoint = entities.Departments.Where(d => d.DeptCode == deptCode && d.ColPtId == collPt).FirstOrDefault();
                if (collPoint != null)
                {
                    collPoint.DeptCode = deptCode;
                    collPoint.ColPtId = collPt;
                    entities.SaveChanges();
                }
            }
        }

        //get department code 
        public static List<string> GetDeptCodes()
        {
            List<string> list = new List<string>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {

                var departmentList = entities.Departments.Select(d => d.DeptCode);
                foreach (var v in departmentList)
                {
                    list.Add(v.ToString());
                }
            }
            return list;
        }
    }
}

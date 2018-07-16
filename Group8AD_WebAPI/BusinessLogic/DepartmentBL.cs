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
                var department = entities.Department.Where(d => d.DeptCode == deptCode).First<Department>();
                if (department != null)
                {
                    entities.Department.Remove(department);
                    entities.SaveChanges();
                }

            }
        }

        //set Delegate by DepartmentCode , fromDate , toDate and empId
        public static void setDelegate(string deptCode, DateTime fromDate, DateTime toDate, int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var department = entities.Department.Where(d => d.DeptCode == deptCode && d.DelegateFromDate == fromDate && d.DelegateToDate == toDate && d.DeptRepId == empId).First<Department>();
                //var department = entities.Department.FirstOrDefault(d => d.DeptCode == deptCode && d.DelegateFromDate == fromDate && d.DelegateToDate == toDate && d.DeptRepId == empId);
                if (department != null)
                {
                    department.DeptCode = deptCode;
                    department.DelegateFromDate = fromDate;
                    department.DelegateToDate = toDate;
                    department.Employee.EmpId = empId;
                    entities.SaveChanges();
                    //entities.Department.Add(department);
                    //entities.SaveChanges();
                }

            }
        }

        //set Rep by DepartmentCode , fromEmpId and toEmpId
        public static void setRep(string deptCode, int fromEmpId, int toEmpId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var department = entities.Department.Where(d => d.DeptCode == deptCode && d.DeptRepId == fromEmpId && d.DeptRepId == toEmpId).First<Department>();
                //var department = entities.Department.FirstOrDefault(d => d.DeptCode == deptCode && d.DeptRepId == fromEmpId && d.DeptRepId == toEmpId);
                if (department != null)
                {
                    department.DeptCode = deptCode;
                    department.Employee1.EmpId = fromEmpId;
                    department.Employee2.EmpId = toEmpId;
                    entities.SaveChanges();
                    //entities.Department.Add(department);
                    //entities.SaveChanges();
                }
            }
        }

        //remove Rep by deptCode and fromEmpId 
        public static void removeRep(string deptCode, int fromEmpId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var department = entities.Department.Where(d => d.DeptCode == deptCode && d.DeptRepId == fromEmpId).First<Department>();
                //var department = entities.Department.FirstOrDefault(d => d.DeptCode == deptCode && d.DeptRepId == fromEmpId);
                if (department != null)
                {
                    entities.Department.Remove(department);
                    entities.SaveChanges();
                }

            }
        }

        //get collection point list 
        public static List<CollectionPoint> GetCollPtList()
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<CollectionPoint> collectionpoint = entities.CollectionPoint.ToList<CollectionPoint>();
                return collectionpoint;
            }
        }

        //get CollPt by DepartmentCode 
        public static string GetCollPt(string deptCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var department = entities.Department.Select(d => d.DeptCode == deptCode).ToString();
                return department;
            }
        }

        //set CollPt by DepartmentCode , collPt
        public static void setCollPt(string deptCode, int collPt)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                //var department = entities.Department.FirstOrDefault(d => d.DeptCode == deptCode && d.ColPtId == collPt);
                var department = entities.Department.Where(d => d.DeptCode == deptCode && d.ColPtId == collPt).First<Department>();               
                if (department != null)
                {
                    department.DeptCode = deptCode;
                    department.ColPtId = collPt;
                    //entities.Department.Add(department);
                    entities.SaveChanges();
                }
            }
        }

        //get department code 
        public static List<string> GetDeptCodes()
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<string> list = new List<string>();
                var departmentList = entities.Department.Select(d => d.DeptCode);
                foreach (var v in departmentList)
                {
                    list.Add(v.ToString());
                }
                return list;
            }
        }
    }
}
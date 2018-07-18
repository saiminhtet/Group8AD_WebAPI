using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;

namespace Group8AD_WebAPI.BusinessLogic
{
    public static class DepartmentBL
    {
        //remove Delegate by DepartmentCode
        public static void removeDelegate(string deptCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Department department = entities.Departments.Where(d => d.DeptCode == deptCode).First<Department>();
                {
                    department.DelegateApproverId = null;
                    department.DelegateFromDate = null;
                    department.DelegateToDate = null;
                    entities.SaveChanges();
                };
            }
        }

        //set Delegate by DepartmentCode , fromDate , toDate and empId
        public static void setDelegate(string deptCode, DateTime fromDate, DateTime toDate, int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Department department = entities.Departments.Where(d => d.DeptCode == deptCode).First<Department>();
                {
                    department.DelegateApproverId = empId;
                    department.DelegateFromDate = fromDate;
                    department.DelegateToDate = toDate;
                    entities.SaveChanges();                    
                };
            }            
        }

        //set Rep by DepartmentCode , fromEmpId and toEmpId
        public static void setRep(string deptCode, int fromEmpId, int toEmpId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Department department = entities.Departments.Where(d => d.DeptCode == deptCode).First<Department>();
                {
                    department.DeptRepId = fromEmpId;
                    entities.SaveChanges();
                };
            }
        }

        //remove Rep by deptCode and fromEmpId 
        public static void removeRep(string deptCode, int fromEmpId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {               
                Department department = entities.Departments.Where(d => d.DeptCode == deptCode).First<Department>();
                {
                    department.DeptRepId = null;
                    department.DelegateFromDate = null;
                    department.DelegateToDate = null;
                    entities.SaveChanges();
                };
            }
        }

        public static List<string> GetCollPtList()
        {
            List<string> list = new List<string>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var departmentList = entities.Departments.Select(d => d.ColPtId);
                foreach (var v in departmentList)
                {
                    list.Add(v.ToString());
                }
            }
            return list;
        }
        
        //get CollPt by DepartmentCode 
        public static string GetCollPt(string deptCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var collPt = entities.Departments.Where(d => d.DeptCode == deptCode).Select(d => d.ColPtId).First().ToString();
                return collPt;
            }
        }

        //set CollPt by DepartmentCode , collPt
        public static DepartmentVM setCollPt(string deptCode, int collPt)
        {
            DepartmentVM rep = new DepartmentVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                rep = entities.Departments.Where(d => d.DeptCode == deptCode && d.ColPtId == collPt).Select(d => new DepartmentVM()
                {
                    DeptCode = d.DeptCode,
                    DeptName = d.DeptName,
                    DeptCtcNo = d.DeptCtcNo,
                    DeptFaxNo = d.DeptFaxNo,
                    ColPtId = d.ColPtId,
                    DeptHeadId = d.DeptHeadId,
                    DeptRepId = d.DeptRepId,
                    DelegateApproverId = d.DelegateApproverId
                }).First<DepartmentVM>();
                entities.SaveChanges();
            }
            return rep;
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

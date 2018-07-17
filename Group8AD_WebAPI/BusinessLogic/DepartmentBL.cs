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
        //instead void
        public static DepartmentVM removeDelegate(string deptCode)
        {            
            DepartmentVM remDeleGate = new DepartmentVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                remDeleGate = entities.Departments.Where(d => d.DeptCode == deptCode).Select(d => new DepartmentVM()
                {
                    DeptCode = d.DeptCode,
                    DeptName = d.DeptName,
                    DeptCtcNo = d.DeptCtcNo,
                    DeptFaxNo = d.DeptFaxNo,
                    ColPtId = d.ColPtId,
                    DeptHeadId = d.DeptHeadId,
                    DeptRepId = d.DeptRepId,
                    DelegateApproverId = d.DelegateApproverId,
                    DelegateFromDate = d.DelegateFromDate,
                    DelegateToDate = d.DelegateToDate
                }).First<DepartmentVM>();
                entities.SaveChanges();
            }
            return remDeleGate;
        }

        //set Delegate by DepartmentCode , fromDate , toDate and empId
        //instead void
        public static DepartmentVM setDelegate(string deptCode, DateTime fromDate, DateTime toDate, int empId)
        {
            DepartmentVM deleGate = new DepartmentVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {                
                deleGate = entities.Departments.Where(d => d.DeptCode == deptCode && d.Employee.EmpId == empId).Select(d => new DepartmentVM()
                {
                    DeptCode = d.DeptCode,
                    DeptName = d.DeptName,
                    DeptCtcNo =d.DeptCtcNo,
                    DeptFaxNo =d.DeptFaxNo,
                    ColPtId = d.ColPtId,
                    DeptHeadId = d.DeptHeadId,
                    DeptRepId = d.DeptRepId,
                    DelegateApproverId = d.DelegateApproverId,
                    DelegateFromDate = d.DelegateFromDate,
                    DelegateToDate =d.DelegateToDate
                }).First<DepartmentVM>();
                entities.SaveChanges();
        }
            return deleGate;
        }

        //set Rep by DepartmentCode , fromEmpId and toEmpId
        public static DepartmentVM setRep(string deptCode, int fromEmpId, int toEmpId)
        {
            DepartmentVM rep = new DepartmentVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                rep = entities.Departments.Where(d => d.DeptCode == deptCode).Select(d => new DepartmentVM()
                {
                    DeptCode = d.DeptCode,
                    DeptName = d.DeptName,
                    DeptCtcNo = d.DeptCtcNo,
                    DeptFaxNo = d.DeptFaxNo,
                    ColPtId = d.ColPtId,
                    DeptHeadId = d.DeptHeadId,
                    DeptRepId = d.DeptRepId,
                    DelegateApproverId = d.DelegateApproverId,
                    DelegateFromDate = d.DelegateFromDate,
                    DelegateToDate = d.DelegateToDate
                }).First<DepartmentVM>();
                entities.SaveChanges();
            }
            return rep;
        }

        //remove Rep by deptCode and fromEmpId 
        public static DepartmentVM removeRep(string deptCode, int fromEmpId)
        {
            DepartmentVM remRep = new DepartmentVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                remRep = entities.Departments.Where(d => d.DeptCode == deptCode && d.Employee.EmpId == fromEmpId).Select(d => new DepartmentVM()
                {
                    DeptCode = d.DeptCode,
                    DeptName = d.DeptName,
                    DeptCtcNo = d.DeptCtcNo,
                    DeptFaxNo = d.DeptFaxNo,
                    ColPtId = d.ColPtId,
                    DeptHeadId = d.DeptHeadId,
                    DeptRepId = d.DeptRepId,
                    DelegateApproverId = d.DelegateApproverId,
                    DelegateFromDate = d.DelegateFromDate,
                    DelegateToDate = d.DelegateToDate
                }).First<DepartmentVM>();
                entities.SaveChanges();
            }
            return remRep;
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
                    DelegateApproverId = d.DelegateApproverId,
                    DelegateFromDate = d.DelegateFromDate,
                    DelegateToDate = d.DelegateToDate
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

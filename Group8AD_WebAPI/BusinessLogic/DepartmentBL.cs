﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;

namespace Group8AD_WebAPI.BusinessLogic
{
    /* 
     * Class Name       :       DepartmentBL
     * Created by       :       Noel Noel Han
     * Created date     :       12/Jul/2018
     * Student No.      :       A0180529B
     */

    public static class DepartmentBL
    {
        //remove Delegate by DepartmentCode
        public static void removeDelegate(string deptCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Department department = entities.Departments.Where(d => d.DeptCode.Equals(deptCode)).First<Department>();
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
                Department department = entities.Departments.Where(d => d.DeptCode.Equals(deptCode)).First();
                department.DeptCode = deptCode;
                department.DelegateFromDate = fromDate;
                department.DelegateToDate = toDate;
                department.DelegateApproverId = empId;
                entities.SaveChanges();

                string startDate = (department.DelegateFromDate ?? default(DateTime)).ToString("dd MMMM yyyy");
                string endDate = (department.DelegateToDate ?? default(DateTime)).ToString("dd MMMM yyyy");

                EmailBL.AddNewEmailToEmp(empId, "Assign Delegate", "You have been assigned as delegate from " + startDate + " to " + endDate);

            }
        }

        //set Rep by DepartmentCode , fromEmpId and toEmpId

        public static void setRep(string deptCode, int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Department department = entities.Departments.Where(d => d.DeptCode.Equals(deptCode)).First<Department>();
                {
                    department.DeptRepId = empId;
                    entities.SaveChanges();
                    EmailBL.AddNewEmailToEmp(empId, "Assign Reprsentative", "You have been assigned as reprsentative for your department.");
                }
            }
            return;
        }

        public static List<CollectionPointVM> GetCollPtList()
        {
            List<CollectionPointVM> collist = new List<CollectionPointVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                collist = entities.CollectionPoints.Select(a => new CollectionPointVM()
                {
                    ColPtId = a.ColPtId,
                    Location = a.Location
                }).ToList<CollectionPointVM>();

                return collist;
            }
        }
        public static CollectionPointVM GetCollPt(string deptCode)
        {
            CollectionPointVM collectionPoint = new CollectionPointVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var collPt = entities.Departments.Where(d => d.DeptCode.Equals(deptCode)).Select(d => d.ColPtId).First();
                collectionPoint = entities.CollectionPoints.Where(c => c.ColPtId == collPt).Select(c => new CollectionPointVM()
                {
                    ColPtId = c.ColPtId,
                    Location = c.Location
                }).First<CollectionPointVM>();
            }
            return collectionPoint;
        }

        //set CollPt by DepartmentCode , collPt
        public static void setCollPt(string deptCode, int collPt)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Department department = entities.Departments.Where(d => d.DeptCode.Equals(deptCode)).First<Department>();
                {
                    department.ColPtId = collPt;
                    entities.SaveChanges();
                }
            }
            return;
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

        //getDept by empId
        public static DepartmentVM GetDept(int empId)
        {
            DepartmentVM department = new DepartmentVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string deptCode = EmployeeBL.GetDeptCode(empId);
                department = entities.Departments.Where(d => d.DeptCode.Equals(deptCode))
                .Select(d => new DepartmentVM()
                {
                    DeptCode = d.DeptCode,
                    DeptName = d.DeptName,
                    DeptCtcNo = d.DeptCtcNo,
                    DeptFaxNo = d.DeptFaxNo,
                    ColPtId = d.ColPtId,
                    DeptHeadId = d.DeptHeadId,
                    DeptRepId = d.DeptRepId,
                    DelegateApproverId = d.DelegateApproverId,
                    EmpId = empId

                }).First<DepartmentVM>();
            }
            return department;
        }

        public static List<DepartmentVM> GetAllDept()
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<DepartmentVM> dList = new List<DepartmentVM>();
                foreach (Department d in entities.Departments.ToList())
                {
                    DepartmentVM dVM = new DepartmentVM();
                    dVM.DeptCode = d.DeptCode;
                    dVM.DeptName = d.DeptName;
                    dVM.DeptCtcNo = d.DeptCtcNo;
                    dVM.DeptFaxNo = d.DeptFaxNo;
                    dVM.ColPtId = d.ColPtId;
                    dVM.DeptHeadId = d.DeptHeadId;
                    dVM.DeptRepId = d.DeptRepId;
                    dVM.DelegateApproverId = d.DelegateApproverId;
                    dVM.DelegateFromDate = d.DelegateFromDate;
                    dVM.DelegateToDate = d.DelegateToDate;
                    dList.Add(dVM);
                }
                return dList;
            }
        }
    }
}

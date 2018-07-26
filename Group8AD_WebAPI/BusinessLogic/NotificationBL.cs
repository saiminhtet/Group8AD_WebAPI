using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;

namespace Group8AD_WebAPI.BusinessLogic
{
    public static class NotificationBL
    {
        //add new reqNoti
        public static void AddNewReqNotification(int empId, RequestVM currReq)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    Notification noti = new Notification();
                    noti.FromEmp = currReq.EmpId;
                    noti.ToEmp = (int)currReq.ApproverId;
                    noti.NotificationDateTime = System.DateTime.Now;
                    noti.RouteUri = "";
                    noti.Type = "Request Submitted";
                    noti.Content = "Request Submitted";
                    noti.IsRead = true;
                    entities.Notifications.Add(noti);
                    entities.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //get Notifiction list 
        public static List<NotificationVM> GetNotifications(int empId)
        {
            List<NotificationVM> notilists = new List<NotificationVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                notilists = entities.Notifications.Where(n => n.ToEmp == empId).Select(n => new NotificationVM()
                {
                    NotificationId = n.NotificationId,
                    NotificationDateTime = n.NotificationDateTime,
                    FromEmp = n.FromEmp,
                    ToEmp = n.ToEmp,
                    RouteUri = n.RouteUri,
                    Type = n.Type,
                    Content = n.Content,
                    IsRead = n.IsRead,
                    EmpId = empId
                }).ToList<NotificationVM>();
            }
            return notilists;
        }

        //AddLowStkNotification with empId and item
        public static void AddLowStkNotification(int empId, Item i)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    Notification noti = new Notification();
                    noti.FromEmp = empId;
                    noti.ToEmp = 104;
                    noti.NotificationDateTime = System.DateTime.Now;
                    noti.RouteUri = "";
                    noti.Type = "Low Stock";
                    noti.Content = "In a recent stationery request disbursement, there are some items with balance below reorder level.";
                    noti.IsRead = true;
                    entities.Notifications.Add(noti);
                    entities.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
              
            }
        }

        //AddFulfillNotification with empId and repId
        public static void AddFulfillNotification(int empId, int repId)
        {            
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var emploeeses=entities.Employees.Where(n => n.EmpId == empId).FirstOrDefault();
                if (emploeeses != null)
                {                  
                        Notification noti = new Notification();
                        noti.FromEmp = empId;
                        noti.ToEmp = repId;
                        noti.NotificationDateTime = System.DateTime.Now;
                        noti.RouteUri = "";
                        noti.Type = "Request Submitted";
                        noti.Content = "Request Submitted";
                        noti.IsRead = true;                        
                        entities.Notifications.Add(noti);
                        entities.SaveChanges();
                    
                    //}
                }
            }
        }

        //AddAcptNotification with repId
        public static void AddAcptNotification(int repId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    Notification noti = new Notification();
                    noti.FromEmp = 104;
                    noti.ToEmp = repId;
                    noti.NotificationDateTime = System.DateTime.Now;
                    noti.RouteUri = "";
                    noti.Type = "Request Submitted";
                    noti.Content = "Request Submitted";
                    noti.IsRead = true;
                    entities.Notifications.Add(noti);
                    entities.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                //Notification acptNoti = entities.Notifications.Where(n => n.Employee.Department.DeptRepId == repId).FirstOrDefault();
                //if (acptNoti != null)
                //{
                //    acptNoti.Employee.Department.DeptRepId = repId;
                //    entities.Notifications.Add(acptNoti);
                //    entities.SaveChanges();
                //}
            }
        }

        //Notification NotifyManager with notfication
        public static NotificationVM NotifyManager(NotificationVM nn)
        {
            NotificationVM notiManager = new NotificationVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                notiManager = entities.Notifications.Where(n => n.Employee.Role == "Store Manager").Select(n => new NotificationVM()
                {
                    NotificationId = n.NotificationId,
                    NotificationDateTime = n.NotificationDateTime,
                    FromEmp = n.FromEmp,
                    ToEmp = n.ToEmp,
                    RouteUri = n.RouteUri,
                    Type = n.Type,
                    Content = n.Content,
                    IsRead = n.IsRead
                }).First<NotificationVM>();
            }
            return notiManager;
        }
        //Notification NotifySupervisor with notfication
        public static NotificationVM NotifySupervisor(NotificationVM nn)
        {
            NotificationVM notiSupervisor = new NotificationVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                notiSupervisor = entities.Notifications.Where(n => n.Employee.Role == "Store Supervisor").Select(n => new NotificationVM()
                {
                    NotificationId = n.NotificationId,
                    NotificationDateTime = n.NotificationDateTime,
                    FromEmp = n.FromEmp,
                    ToEmp = n.ToEmp,
                    RouteUri = n.RouteUri,
                    Type = n.Type,
                    Content = n.Content,
                    IsRead = n.IsRead
                }).First<NotificationVM>();
            }
            return notiSupervisor;
        }
        //AddAcptNotification with repId
        public static void AdjApprNotification(int fromEmpId , int toEmpId, NotificationVM n)
        {
            NotificationVM adjApprNoti = new NotificationVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    Notification noti = new Notification();
                    noti.FromEmp = fromEmpId;
                    noti.ToEmp = toEmpId;
                    noti.NotificationDateTime = System.DateTime.Now;
                    noti.RouteUri = "";
                    noti.Type = "Adjustment Submitted";
                    noti.Content = "Missing Stock";
                    noti.IsRead = true;
                    entities.Notifications.Add(noti);
                    entities.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                //adjApprNoti = entities.Notifications.Select(r => new NotificationVM()
                //{
                //    FromEmp = fromEmpId,
                //    ToEmp = toEmpId,
                //    NotificationId = n.NotificationId,
                //    NotificationDateTime = n.NotificationDateTime,
                //    RouteUri = n.RouteUri,
                //    Type = n.Type,
                //    Content = n.Content,
                //    IsRead = n.IsRead

                //}).First<NotificationVM>();
            }
        }
    }
}
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
        public static void AddNewReqNotification(int empId, Request currReq)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var newReqNoti = entities.Notifications.Where(n => n.Employee.EmpId == empId).FirstOrDefault();
                if (newReqNoti != null)
                {
                    newReqNoti.Employee.EmpId = empId;
                    Request request = currReq;
                    entities.Notifications.Add(newReqNoti);
                    entities.SaveChanges();
                }
            }
        }

        //get Notifiction list 
        public static List<NotificationVM> GetNotifications(int empId)
        {
            List<NotificationVM> notilists = new List<NotificationVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                notilists = entities.Notifications.Where(n => n.Employee.EmpId == empId).Select(n => new NotificationVM()
                {
                    NotificationId = n.NotificationId,
                    NotificationDateTime = n.NotificationDateTime,
                    FromEmp = n.FromEmp,
                    ToEmp = n.ToEmp,
                    RouteUri = n.RouteUri,
                    Type = n.Type,
                    Content = n.Content,
                    IsRead = n.IsRead
                }).ToList<NotificationVM>();
            }
            return notilists;
        }

        //AddLowStkNotification with empId and item
        public static void AddLowStkNotification(int empId, Item i)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var lowStkNoti = entities.Notifications.Where(n => n.Employee.EmpId == empId).FirstOrDefault();
                if (lowStkNoti != null)
                {
                    lowStkNoti.Employee.EmpId = empId;
                    Item item = i;
                    entities.Notifications.Add(lowStkNoti);
                    entities.SaveChanges();
                }
            }
        }

        //AddFulfillNotification with empId and repId
        public static void AddFulfillNotification(int empId, int repId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var fulFillNoti = entities.Notifications.Where(n => n.Employee.EmpId == empId && n.Employee.Department.DeptRepId == repId).FirstOrDefault();
                if (fulFillNoti != null)
                {
                    fulFillNoti.Employee.EmpId = empId;
                    fulFillNoti.Employee.Department.DeptRepId = repId;
                    entities.Notifications.Add(fulFillNoti);
                    entities.SaveChanges();
                }
            }
        }

        //AddAcptNotification with repId
        public static void AddAcptNotification(int repId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Notification acptNoti = entities.Notifications.Where(n => n.Employee.Department.DeptRepId == repId).FirstOrDefault();
                if (acptNoti != null)
                {
                    acptNoti.Employee.Department.DeptRepId = repId;
                    entities.Notifications.Add(acptNoti);
                    entities.SaveChanges();
                }
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
                adjApprNoti = entities.Notifications.Select(r => new NotificationVM()
                {
                    FromEmp = fromEmpId,
                    ToEmp = toEmpId,
                    NotificationId = n.NotificationId,
                    NotificationDateTime = n.NotificationDateTime,
                    RouteUri = n.RouteUri,
                    Type = n.Type,
                    Content = n.Content,
                    IsRead = n.IsRead

                }).First<NotificationVM>();
            }
        }
    }
}
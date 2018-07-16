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
        //add Newreqnotification with empId and currReq
        public static void AddNewReqNotification(int empId, int currReq)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var reqNoti = entities.Notifications.Where(n => n.Employee.EmpId == empId && n.NotificationId == currReq).FirstOrDefault();
                if (reqNoti != null)
                {
                    reqNoti.NotificationId = currReq;
                    reqNoti.Employee.EmpId = empId;
                    entities.Notifications.Add(reqNoti);
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
                notilists = entities.Notifications.Select(n => new NotificationVM()
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
                var fulFillNoti = entities.Notifications.Where(n => n.Employee.EmpId == empId).FirstOrDefault();
                if (fulFillNoti != null)
                {
                    fulFillNoti.Employee.EmpId = empId;
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
                var acptNoti = entities.Notifications.Where(n => n.Employee.EmpId == repId).FirstOrDefault();
                if (acptNoti != null)
                {
                    acptNoti.Employee.EmpId = repId;
                    entities.Notifications.Add(acptNoti);
                    entities.SaveChanges();
                }
            }
        }

        //Notification NotifyManager with notfication
        public static NotificationVM NotifyManager(Notification n)
        {
            NotificationVM notiManager = new NotificationVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                notiManager = entities.Notifications.Where(nn => nn.NotificationId == n.NotificationId).Select(nn => new NotificationVM()
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
        public static NotificationVM NotifySupervisor(Notification n)
        {
            NotificationVM notiSupervisor = new NotificationVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                notiSupervisor = entities.Notifications.Where(nn => nn.NotificationId == n.NotificationId).Select(nn => new NotificationVM()
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
        public static void AdjApprNotification(int fromEmpId , int toEmpId, Notification n)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var adjApprNoti = entities.Notifications.Where(nn => nn.FromEmp == fromEmpId && nn.ToEmp == toEmpId && nn.NotificationId == n.NotificationId).FirstOrDefault();
                if (adjApprNoti != null)
                {
                    adjApprNoti.FromEmp = fromEmpId;
                    adjApprNoti.ToEmp = toEmpId;
                    adjApprNoti.NotificationId = n.NotificationId;
                    entities.Notifications.Add(adjApprNoti);
                    entities.SaveChanges();
                }
            }
        }
    }
}
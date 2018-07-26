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
        // add new notification
        public static void AddNewNotification(int fromEmpId, int toEmpId, string type, string content)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Notification notif = new Notification();
                notif.NotificationDateTime = DateTime.Now;
                notif.FromEmp = fromEmpId;
                notif.ToEmp = toEmpId;
                notif.Type = type;
                notif.Content = content;
                notif.IsRead = false;
                entities.Notifications.Add(notif);
                entities.SaveChanges();
            }
        }

        // toggle read notification
        public static bool ToggleReadNotification(NotificationVM n)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    Notification n_orig = entities.Notifications.ToList().Find(x => x.NotificationId == n.NotificationId);
                    n_orig.IsRead = !n_orig.IsRead;
                    entities.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // mark all notification as read
        public static bool MarkAllAsRead(List<NotificationVM> nList)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    foreach (NotificationVM nvm in nList)
                    {
                        Notification n_orig = entities.Notifications.ToList().Find(x => x.NotificationId == nvm.NotificationId);
                        n_orig.IsRead = true;
                    }
                    entities.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // mark all notification as read
        public static bool MarkAllAsRead(int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    List<Notification> nList = entities.Notifications.Where(x => x.ToEmp == empId).ToList();
                    nList.ForEach(x => x.IsRead = true);
                    entities.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // mark one notification as read
        public static bool MarkOneAsRead(NotificationVM n)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    Notification n_orig = entities.Notifications.ToList().Find(x => x.NotificationId == n.NotificationId);
                    n_orig.IsRead = true;
                    entities.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

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

                //var lowStkNoti = entities.Notifications.Where(n => n.Employee.EmpId == empId).FirstOrDefault();
                //if (lowStkNoti != null)
                //{
                //    lowStkNoti.Employee.EmpId = empId;
                //    Item item = i;
                //    entities.Notifications.Add(lowStkNoti);
                //    entities.SaveChanges();
                //}
            }
        }

        //AddFulfillNotification with empId and repId
        public static void AddFulfillNotification(int empId, int repId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var emploeeses = entities.Employees.Where(n => n.EmpId == empId).FirstOrDefault();
                if (emploeeses != null)
                {
                    //var request = entities.Notifications.Where(n => n.Employee.Department.DeptRepId == repId).FirstOrDefault();
                    //if (request != null)
                    //{
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
        public static void AdjApprNotification(int fromEmpId, int toEmpId, NotificationVM n)
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
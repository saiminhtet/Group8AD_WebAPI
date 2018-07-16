using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8AD_WebAPI.Models;

namespace Group8AD_WebAPI.BusinessLogic
{
    public static class NotificationBL
    {
        //add Newreqnotification with empId and currReq
        public static void AddNewReqNotification(int empId, int currReq)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                
            }
        }

        //get Notifiction list 
        public static List<Notification> GetNotifications(int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Notification> notifications = entities.Notification.Where(n => n.FromEmp == empId).ToList<Notification>();
                return notifications;
            }
        }

        //AddLowStkNotification with empId and item
        public static void AddLowStkNotification(int empId, Item i)
        {
           
        }

        //AddFulfillNotification with empId and repId
        public static void AddFulfillNotification(int empId, int repId)
        {
            
        }

        //AddAcptNotification with repId
        public static void AddAcptNotification(int repId)
        {
            
        }

        //Notification NotifyManager with notfication
        public static Notification NotifyManager(Notification n)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                return n;
            }
        }
        //Notification NotifySupervisor with notfication
        public static Notification NotifySupervisor(Notification n)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                return n;
            }
        }
        //AddAcptNotification with repId
        public static void AdjApprNotification(int fromEmpId , int toEmpId, Notification n)
        {
            
        }

    }
}
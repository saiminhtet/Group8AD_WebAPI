using Group8AD_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.BusinessLogic
{
    public static class ItemBL
    {

        //get itemlist by category and descending 
        public static List<Item> GetItems(string cat, string desc)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Item> itemlist = null;

                if (cat == null || desc == null)
                {
                    if (cat == null)
                    {
                        itemlist = entities.Items.Where(i => i.Desc == desc).ToList<Item>();
                        return itemlist;
                    }
                    if (desc == null)
                    {
                        itemlist = entities.Items.Where(i => i.Cat == cat).ToList<Item>();
                        return itemlist;
                    }
                }
                itemlist = entities.Items.Where(i => i.Cat == cat && i.Desc == desc).ToList<Item>();
                return itemlist;
            }
        }



    }
}
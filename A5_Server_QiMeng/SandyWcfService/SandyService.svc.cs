using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AdoLib_QiMeng;

namespace SandyWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SandyService : SandyServiceInterface
    {
        public Sandy sandy;

        public SandyService()
        {
            sandy = new Sandy("OrdersDB", "ism6236", "ism6236bo");
        }

        public List<string> readTable(string s) 
        {
            return sandy.readTable(s);
        }


        public List<string> listPurchases(string cid) 
        {
            return sandy.listPurchases(cid);
        }


        public List<string> runSelect(string sql) 
        {
            return sandy.runSelect(sql);
        }


        public void placeOrder(string cid, int[] pids, int[] quantity) 
        {
            sandy.placeOrder(cid, pids, quantity);
        }
    
    }
}

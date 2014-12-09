using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SandyWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface SandyServiceInterface
    {
        [OperationContract]
        List<string> readTable(string s);

        [OperationContract]
        List<string> listPurchases(string cid);

        [OperationContract]
        List<string> runSelect(string sql);

        [OperationContract]
        void placeOrder(string cid, int[] pids, int[] quantity);
    

    }


    
}

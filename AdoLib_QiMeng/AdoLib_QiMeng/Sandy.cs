using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace AdoLib_QiMeng
{
    public class Sandy
    {
        public SqlConnection cnn;
        public Sandy(String dbn, String uid, String pass)
        {
            cnn = new SqlConnection();
            cnn.ConnectionString = String.Format("Data Source=(local);Initial Catalog={0};Persist Security Info=True;User ID={1};Password={2};", dbn, uid, pass);
            cnn.Open();
        }
        
        ~Sandy()
        {
            try
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();

            }
            catch (Exception e) {  Console.WriteLine(e.Message); }
        }

       
        
        public List<String> runSelect( String query)
        {
            List<String> result = new List<String>();
            try
            {
                SqlCommand command = new SqlCommand(query, cnn);
                SqlDataReader reader = command.ExecuteReader();
                String header = "";
                for (int i = 0; i < reader.FieldCount; i++) 
                {
                    header += String.Format("{0,-20}", reader.GetName(i));
                }
                result.Add(header);
                    while (reader.Read())
                    {
                        String content = "";
                        for(int i = 0; i<reader.FieldCount; i++)
                        {
                            content += String.Format("{0,-20}", reader[i].ToString());
                        }
                        result.Add(content);
                    }
                    reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return result;
        }
        
        
        
        public void transanctSQL(string[] sql) // this method is used to excute "update, delete or insert SQL statement"  The term is "non-query"
        {
            SqlCommand command = new SqlCommand();
            SqlTransaction tran = cnn.BeginTransaction();
            command.Connection = cnn;
            try
            {
                command.Transaction = tran;
                for (int i = 0; i < sql.Length; i++)
                {
                    command.CommandText = sql[i];
                    command.ExecuteNonQuery();
                }
                tran.Commit();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                tran.Rollback();
                throw e;  
            }
            catch (Exception e)
            {
                tran.Rollback();
                throw e;
            }
        }
        
        
        
        public List<String> readTable(String tableName) 
        {
            String query = "SELECT * FROM " + tableName;
            return runSelect(query);   
        }
        
        
        
        public List<String> listPurchases(String cid) 
        {
            String query = "SELECT Orders.OrderDate,Product.Pid, Product.Price, Customer.Name, LineItem.Quantity from Customer, Product, Orders, LineItem " +
            "where Customer.Cid ="+cid+"  and Customer.Cid = Orders.Cid\n" +"and Orders.Oid = LineItem.Oid\n" +"and LineItem.Pid = Product.Pid;";
            return runSelect(query);
        }
        
        
        
        public void placeOrder(String customerId, int[] productId, int[] quantities) 
        {

            try
            {
                //create sql arrays at first, there should be productId.length + 1 statments in the end
                String[] sql = new String[productId.Length + 1];

                // get the order# that will be set to the new order
                List<String> orders = runSelect("select * from orders");
                int Oid = orders.Count;

                // create order
                int Cid = Convert.ToInt32(customerId);
                DateTime date = DateTime.Now;
                SqlDateTime Orderdate= new SqlDateTime(date);

                // create insert order sql statement, this is the first statment in the sql statement array
                sql[0] = String.Format("Insert Into Orders(Oid, OrderDate, Cid) Values ({0}, '{1}', {2});", Oid, Orderdate, Cid);

                // create insert lineitem sql statement array
                for (int i = 0; i < productId.Length; i++)
                {
                    sql[i + 1] = String.Format("insert into LineItem(Oid, Pid, Quantity) Values ({0}, {1}, {2})", Oid, productId[i], quantities[i]);
                }

                // start transaction
                try
                {
                    transanctSQL(sql);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}

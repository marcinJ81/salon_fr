### SalonFR
Very simple application for barber. User interface was create in C# Winforms, database in SQLite.
### [Post in my blog](http://ismartdev.pl/salonfr/projekt-salon-fryzjerski-wstep-i-podsumowanie/ "SalonFR")
[![](https://github.com/marcinJ81/salon_fr/blob/master/Image/salonfr.png)](https://github.com/marcinJ81/salon_fr/blob/master/Image/salonfr.png "Example Screen")
### Some Code
When I didn't have user interface. I wrote unit test for verify how work insert data to Database. In this example I verified adding client.
```csharp
[Test]
 public void ShouldAddNewClient_ReturnNewID()
{
     SqlLiteDB.SqlLiteDBCreateTable();
     int clientID = selectClient.GetNextClientId(SGetIdFromSpecificTable.queryGetLatestClientID());
     Client client = new Client()
     {
          client_id =clientID,
         client_name = "Julian",
         client_sname = "Krol",
         client_phone = "123456789",
         client_description = "test kolejny"
     };
        int lastIndex =  addClient.InsertObjectToDB(client);
  Assert.AreEqual(lastIndex,clientID);
}
```
Verifyng update client data
First interface declaration
````csharp
public interface IUpdateObject<T>
{
    bool UpdateObject(T dataobjectForChange, int id);
    bool VerifyUpdateData(T newData, T modifiedData);
}
````
Next interface definiotion in UpdateClient class 
```csharp
 public class UpdateClient : IUpdateObject<Client>
{
    private ISelectClient selectClient;

    public UpdateClient(ISelectClient selectClient)
    {
        this.selectClient = selectClient;
   }

    public bool UpdateObject(Client dataobjectForChange, int id)
    {
        var updateClient = SUpdateScripts.SqlLiteDBUpdateClient(dataobjectForChange, id);
        string result = DBConnectAndExecute.ExecuteQuery(updateClient);
        if (result != string.Empty)
        {
            return false;
        }

        return VerifyUpdateData(dataobjectForChange, selectClient.GetClients(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery())
                .Where(x => x.client_id == id).First());
    }

    public bool VerifyUpdateData(Client newData, Client modiefiedData)
    {
        List<bool> listError = new List<bool>();
        listError.Add(newData.client_name == modiefiedData.client_name ? true : false);
        listError.Add(newData.client_sname == modiefiedData.client_sname ? true : false);
        listError.Add(newData.client_description == modiefiedData.client_description ? true : false);
        listError.Add(newData.client_phone == modiefiedData.client_phone ? true : false);
        return !listError.Any(x => !x);
    }
}
```
Project in .Net MVC
[![](https://github.com/marcinJ81/salon_fr/blob/master/Image/mvc1.png)](https://github.com/marcinJ81/salon_fr/blob/master/Image/mvc1.png "Example Screen")



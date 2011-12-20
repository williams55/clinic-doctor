<%@ WebService Language="C#" Class="DoctorServices" %>

//using System.Web.Script.Services;
using System;
using System.Web.Script.Services;
using System.Web.Services;
using ClinicDoctor.Data;
using ClinicDoctor.Entities;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]

public class DoctorServices : WebService
{
    public class Doctor
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string ShortName { get; set; }
        public string UserName { get; set; }


    }
    public class room
    {
        public Int64 Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
    }

    [WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Doctor GetFromStaff(string UserName)
    {
        Staff obj = DataRepository.StaffProvider.GetByUserName(UserName);
        Doctor objDoctor = new Doctor();
        if (obj != null)
            objDoctor = new Doctor { Firstname = obj.FirstName, LastName = obj.LastName, ShortName = obj.ShortName, UserName = obj.UserName };
        return objDoctor;
    }
    [WebMethod]
    public room GetFromRoom(string Id)
    {

        Room obj = DataRepository.RoomProvider.GetById(long.Parse("0" + Id));
        room objroom = new room();
        if (obj != null)
            objroom = new room { Id = obj.Id, Title = obj.Title, Note = obj.Note };
        return objroom;

    }








}

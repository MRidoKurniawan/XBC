using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repository
{
    public class OfficeRepo
    {
        public static List<OfficeViewModel> OfficeAll(string search)
        {
            List<OfficeViewModel> result = new List<OfficeViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from o in db.t_office
                          where (o.name.Contains(search) && o.is_delete == false)
                          select new OfficeViewModel
                          {
                              id = o.id,
                              name = o.name,
                              phone = o.phone,
                              email = o.email,
                              address = o.address,
                              notes = o.notes,
                              isDelete = o.is_delete,
                              contact = o.phone + " / " + o.email
                          }).ToList();
            }
            return result == null ? new List<OfficeViewModel>() : result;
        }
        public static OfficeViewModel ById(long id)
        {
            OfficeViewModel result = new OfficeViewModel();
            using (var db = new XBC_Context())
            {
                result = (from o in db.t_office
                          where o.id == id
                          select new OfficeViewModel
                          {
                              id = o.id,
                              name = o.name,
                              phone = o.phone,
                              email = o.email,
                              address = o.address,
                              notes = o.notes,
                              contact = o.phone + " / " + o.email,
                              isDelete = o.is_delete
                          }).FirstOrDefault();
                if (result == null)
                {
                    result = new OfficeViewModel();
                }
            }
            return result;
        }
        public static List<RoomViewModel> ByOfficeId(long OfId)
        {
            List<RoomViewModel> result = new List<RoomViewModel>();
            using (var db = new XBC_Context())
            {
                var listRoom = db.t_room.Where(o => o.office_id == OfId && o.is_delete==false).ToList();
                if (listRoom != null)
                {
                    foreach (var r in listRoom)
                    {
                        result.Add(new RoomViewModel
                        {
                            id = r.id,
                            officeId = r.office_id,
                            code = r.code,
                            name = r.name,
                            capacity = r.capacity,
                            any_projector = r.any_projector,
                            notes = r.notes,
                            isDelete = r.is_delete
                        });
                    }
                }
                else
                {
                    result = new List<RoomViewModel>();
                }
            }
            return result;
        }
        public static ResponseResult Update(OfficeViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    if (entity.id == 0)//create
                    {
                        t_office ofc = new t_office();
                        ofc.name = entity.name;
                        ofc.phone = entity.phone;
                        ofc.email = entity.email;
                        ofc.address = entity.address;
                        ofc.notes = entity.notes;
                        ofc.is_delete = entity.isDelete;

                        ofc.created_by = 1;
                        ofc.created_on = DateTime.Now;
                        ofc.is_delete = false;
                        db.t_office.Add(ofc);
                        db.SaveChanges();
                        entity.id = ofc.id;

                        foreach (var item in entity.details)
                        {
                            t_room room = new t_room();
                            room.office_id = entity.id;
                            room.code = item.code;
                            room.name = item.name;
                            room.capacity = item.capacity;
                            room.any_projector = item.any_projector;
                            room.notes = entity.notes;

                            room.created_by = 1;
                            room.created_on = DateTime.Now;

                            db.t_room.Add(room);
                        }
                        db.SaveChanges();

                        object data = new
                        {
                            ofc.id,
                            ofc.name,
                            ofc.phone,
                            ofc.email,
                            ofc.address,
                            ofc.notes
                        };
                        var json = new JavaScriptSerializer().Serialize(data);

                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);
                        db.SaveChanges();
                        entity.id = ofc.id;
                        result.Entity = entity;
                    }
                    else //edit
                    {
                        t_office ofc = db.t_office.Where(o => o.id == entity.id).FirstOrDefault();
                        if (ofc != null)
                        {
                            var Serial = new JavaScriptSerializer();
                            object dataBefore = new
                            {
                                ofc.name,
                                ofc.phone,
                                ofc.email,
                                ofc.address,
                                ofc.notes
                            };
                            ofc.name = entity.name;
                            ofc.phone = entity.phone;
                            ofc.email = entity.email;
                            ofc.address = entity.address;
                            ofc.notes = entity.notes;
                            ofc.is_delete = entity.isDelete;
                            ofc.modified_by = 1;
                            ofc.modified_on = DateTime.Now;
                            db.SaveChanges();
                            object dataAfter = new
                            {
                                ofc.name,
                                ofc.phone,
                                ofc.email,
                                ofc.address,
                                ofc.notes
                            };
                            t_audit_log log = new t_audit_log();
                            log.type = "Modified";
                            log.json_before = Serial.Serialize(dataBefore);
                            log.json_after = Serial.Serialize(dataAfter);

                            log.created_by = 1;
                            log.created_on = DateTime.Now;

                            db.t_audit_log.Add(log);
                            db.SaveChanges();
                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = "Office Not Found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
       public static ResponseResult Delete(OfficeViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_office ofc = db.t_office.Where(o => o.id == entity.id).FirstOrDefault();
                    if (ofc != null)
                    {
                        ofc.is_delete = true;
                        ofc.deleted_by = 1;
                        ofc.deleted_on = DateTime.Now;
                        db.SaveChanges();

                        object data = new
                        {
                            ofc.id,
                            ofc.name,
                            ofc.phone,
                            ofc.address
                        };
                        var json = new JavaScriptSerializer().Serialize(data);

                        t_audit_log log = new t_audit_log();
                        log.type = "Modified";
                        log.json_insert = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);
                        db.SaveChanges();
                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Office Not Fount";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

    }
}
